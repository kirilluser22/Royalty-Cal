using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Retailer_Winning_Formula.DataLayer.DataContext;
using Retailer_Winning_Formula.DataLayer.DataModels;
using Retailer_Winning_Formula.DataLayer.Entities;
using Retailer_Winning_Formula.Models;
using Retailer_Winning_Formula.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retailer_Winning_Formula.Controllers
{
    public class SmartPartnerOnBoardingController : Controller
    {
        private readonly ZucDbContext _dataContext;
        private readonly ILogger _logger;

        public SmartPartnerOnBoardingController(ZucDbContext dataContext,
            ILogger<SmartPartnerOnBoardingController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.annualSalesVolume = _dataContext.AnnualSalesVolume.ToList();
            ViewBag.buyingGroup = _dataContext.BuyingGroup.ToList();
            ViewBag.productAndServices = _dataContext.ProdNserv.ToList();
            ViewBag.currencies = _dataContext.Currency.ToList();
            ViewBag.timezones = _dataContext.TimeZone.ToList();
            ViewBag.languages = _dataContext.Language.ToList();
            return View();
        }
        public IActionResult InformationReview()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LocationInfoTemplete(int row)
        {
            var locationViewModel = new LocationViewModel
            {
                RowNo = row,
                AnnualSalesVolumes = _dataContext.AnnualSalesVolume.ToList(),
                TimeZones = _dataContext.TimeZone.ToList()
            };
            return PartialView("_LocationInfoTemplete", locationViewModel);
        }

        [HttpPost]
        public async Task<object> SmarterPartnerData([FromForm] SmartOnBoardingRequestModel request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException();
                if (request.ContactInfo == null) throw new ArgumentNullException(nameof(request.ContactInfo));
                if (request.BusinessInfo == null) throw new ArgumentNullException(nameof(request.BusinessInfo));
                if (!request.Locations.Any()) throw new ArgumentNullException(nameof(request.Locations));
                if (request.ContactInfo.Id != null && request.BusinessInfo.Id != null && request.Locations.Any(a => a.Id != null
                && a.SubBusinessInfoId != null))
                {
                    await UpdateSmartOnBoarding(request);
                    return Ok("Updated Successfully");
                }

                var primaryKeys = new SmartOnBoardingPrimaryKeyModel();
                #region contactInfo start

                primaryKeys.ContactInfoId = await AddContactInfo(request.ContactInfo);

                #endregion end

                #region businessInfo start
                var marketPartner = new MarketPartner();
                if (request.BusinessInfo.Id.HasValue)
                    marketPartner = await UpdateMarketPartner(request.BusinessInfo);
                else
                    marketPartner = await AddMarketPartner(request.BusinessInfo);

                primaryKeys.BusinessInfoId = marketPartner.Id;
                #endregion end

                #region locationInfo
                await AddLocations(request.Locations, marketPartner.LegalName, primaryKeys);
                #endregion end

                return Ok(primaryKeys);
            }
            catch (Exception e)
            {
                _logger.LogError("SmarterPartnerDataException", e);
                return e.Message;
            }

        }

        [HttpGet]
        public async Task<object> BusinessInfo(string partnerId)
        {
            if (string.IsNullOrWhiteSpace(partnerId)) return new ArgumentNullException();
            var marketPartner = await _dataContext.MarketPartner.FirstOrDefaultAsync(m => m.RetailerCode == partnerId);
            if (marketPartner == null) return "Record Not Found";
            var businessViewModel = new MarketPartnerGetModel
            {
                Id = marketPartner.Id,
                StoreName = marketPartner.Dbaname,
                BusinessLegalName = marketPartner.LegalName,
                PartnerId = marketPartner.RetailerCode,
                CurrencyId = marketPartner.CurrencyId,
                BuyingGroupIds = _dataContext.MpbuyingGroup.Where(x => x.MarketPartnerId == marketPartner.Id)
                .Select(x => x.BuyingGroupId).ToArray(),
                ProductAndServiceIds = _dataContext.MpprodNserv.Where(x => x.MarketPartnerId == marketPartner.Id)
                .Select(x => x.ProdNservId).ToArray(),
                LanguageId = marketPartner.LanguageId
            };
            var prodNservsNameList = _dataContext.ProdNserv.Where(p => businessViewModel.ProductAndServiceIds.Contains(p.Id)).ToArray();

            return businessViewModel;
        }

        private async Task<MarketPartner> AddMarketPartner(BusinessInformation request)
        {
            var marketPartner = new MarketPartner
            {
                Dbaname = request.StoreName,
                LegalName = request.BusinessLegalName,
                RetailerCode = request.PartnerId,
                CurrencyId = request.CurrencyId
            };
            await _dataContext.MarketPartner.AddAsync(marketPartner);
            await _dataContext.SaveChangesAsync();
            if (request.ProductAndServiceIds != null && request.ProductAndServiceIds.Any())
            {
                var mpProdNServs = new List<MpprodNserv>();
                foreach (var item in request.ProductAndServiceIds)
                {
                    mpProdNServs.Add(new MpprodNserv
                    {
                        MarketPartnerId = marketPartner.Id,
                        ProdNservId = item
                    });
                }
                _dataContext.MpprodNserv.AddRange(mpProdNServs);
            }

            if (request.BuyingGroupIds != null && request.BuyingGroupIds.Any())
            {
                var mpBuyingGroups = new List<MpbuyingGroup>();
                foreach (var item in request.BuyingGroupIds)
                {
                    mpBuyingGroups.Add(new MpbuyingGroup
                    {
                        MarketPartnerId = marketPartner.Id,
                        BuyingGroupId = item
                    });
                }
                _dataContext.MpbuyingGroup.AddRange(mpBuyingGroups);
            }

            _dataContext.SaveChanges();
            return marketPartner;
        }

        private async Task<long> AddSubMarketPartner(LocationInformation request, string legalName)
        {
            var marketPartner = new MarketPartner
            {
                Dbaname = request.Name,
                LegalName = legalName,
                Asvid = request.AnnualSalesVolumeId
            };
            await _dataContext.MarketPartner.AddAsync(marketPartner);
            await _dataContext.SaveChangesAsync();
            _dataContext.SaveChanges();
            return marketPartner.Id;
        }

        private async Task UpdateSmartOnBoarding(SmartOnBoardingRequestModel request)
        {
            await UpdateContactInfo(request.ContactInfo);
            await UpdateMarketPartner(request.BusinessInfo);
            await UpdateLocations(request.Locations, request.BusinessInfo.BusinessLegalName);
        }
        private async Task<object> UpdateContactInfo(ContactInformation request)
        {
            var contactInfo = new Entity
            {
                Id = request.Id.Value,
                CreationTime = Time.EasternDate(),
                LastUpdated = Time.EasternDate(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                LanguageId = request.LanguageId
            };

            _dataContext.Entity.Update(contactInfo);
            await _dataContext.SaveChangesAsync();
            if (request.PhoneNumber != 0)
            {
                var phoneId = _dataContext.PhoneNumber.Where(p => p.PhoneNumber1 == request.PhoneNumber)
                    .Select(a => a.Id).FirstOrDefault();
                if (phoneId == 0)
                {
                    var phoneNumber = new PhoneNumber
                    {
                        PhoneNumber1 = request.PhoneNumber,
                        PhoneNumberTypeId = 1
                    };
                    await _dataContext.PhoneNumber.AddAsync(phoneNumber);
                    await _dataContext.SaveChangesAsync();
                    var entityPhoneNo = new EntityPhoneNumber
                    {
                        PhoneNumberId = phoneNumber.Id,
                        EntityId = contactInfo.Id
                    };
                    await _dataContext.EntityPhoneNumber.AddAsync(entityPhoneNo);
                }
                else
                {
                    var isExists = await _dataContext.EntityPhoneNumber.AnyAsync(a => a.EntityId == contactInfo.Id &&
                    a.PhoneNumberId == phoneId);
                    if (!isExists)
                    {
                        var entityPhoneNo = new EntityPhoneNumber
                        {
                            PhoneNumberId = phoneId,
                            EntityId = contactInfo.Id
                        };
                        await _dataContext.EntityPhoneNumber.AddAsync(entityPhoneNo);
                    }
                }

                await _dataContext.SaveChangesAsync();
            }
            return contactInfo;
        }
        private async Task<MarketPartner> UpdateMarketPartner(BusinessInformation request)
        {
            var marketPartner = new MarketPartner
            {
                Id = request.Id.Value,
                Dbaname = request.StoreName,
                LegalName = request.BusinessLegalName,
                RetailerCode = request.PartnerId,
                CurrencyId = request.CurrencyId
            };
            _dataContext.MarketPartner.Update(marketPartner);
            await _dataContext.SaveChangesAsync();
            if (request.ProductAndServiceIds != null && request.ProductAndServiceIds.Any())
            {
                var existingMpProdNServIds = await _dataContext.MpprodNserv.Where(a => a.MarketPartnerId == marketPartner.Id).ToListAsync();
                var unusedMpProdNServIds = existingMpProdNServIds.Where(x => !request.ProductAndServiceIds.Any(a => a == x.ProdNservId));
                var newMpProdNServIds = request.ProductAndServiceIds.Where(pId => !existingMpProdNServIds.Any(a => a.ProdNservId == pId));
                _dataContext.MpprodNserv.RemoveRange(unusedMpProdNServIds);
                var mpProdNServs = new List<MpprodNserv>();
                foreach (var item in newMpProdNServIds)
                {
                    mpProdNServs.Add(new MpprodNserv
                    {
                        MarketPartnerId = marketPartner.Id,
                        ProdNservId = item
                    });
                }
                if (mpProdNServs.Any())
                    await _dataContext.MpprodNserv.AddRangeAsync(mpProdNServs);
            }

            if (request.BuyingGroupIds != null && request.BuyingGroupIds.Any())
            {
                var existingMpBuyingGrpIds = await _dataContext.MpbuyingGroup.Where(a => a.MarketPartnerId == marketPartner.Id).ToListAsync();
                var unusedMpBuyingGrpIds = existingMpBuyingGrpIds.Where(x => !request.BuyingGroupIds.Any(a => a == x.BuyingGroupId));
                var newMpBuyingGrpIds = request.BuyingGroupIds.Where(bId => !existingMpBuyingGrpIds.Any(a => a.BuyingGroupId == bId));
                _dataContext.MpbuyingGroup.RemoveRange(unusedMpBuyingGrpIds);
                var mpBuyingGroups = new List<MpbuyingGroup>();
                foreach (var item in newMpBuyingGrpIds)
                {
                    mpBuyingGroups.Add(new MpbuyingGroup
                    {
                        MarketPartnerId = marketPartner.Id,
                        BuyingGroupId = item
                    });
                }
                if (mpBuyingGroups.Any())
                    await _dataContext.MpbuyingGroup.AddRangeAsync(mpBuyingGroups);
            }
            _dataContext.SaveChanges();
            return marketPartner;
        }

        private async Task UpdateSubMarketPartner(LocationInformation request, string legalName)
        {
            var marketPartner = new MarketPartner
            {
                Id = request.SubBusinessInfoId.Value,
                Dbaname = request.Name,
                LegalName = legalName,
                Asvid = request.AnnualSalesVolumeId
            };
            _dataContext.MarketPartner.Update(marketPartner);
            await _dataContext.SaveChangesAsync();
        }

        private async Task UpdateLocations(List<LocationInformation> request, string legalName)
        {
            if (request.Any())
            {
                var mpPhoneNosInsertList = new List<MpphoneNumber>();
                var mpPhoneNosUpdateList = new List<MpphoneNumber>();
                var mpPEmailsInsertList = new List<MpemailAddress>();
                var mpPEmailsUpdateList = new List<MpemailAddress>();
                foreach (var item in request)
                {
                    if (!item.Id.HasValue)
                        await AddLocation(item, legalName);
                    else
                    {
                        await UpdateSubMarketPartner(item, legalName);
                        var address = new Address
                        {
                            Id = item.Id.Value,
                            Address1 = item.Address,
                            City = item.City,
                            Province = item.ProvinceCode,
                            PostalCode = item.PostalCode.Replace(" ", string.Empty),
                            AddressTypeId = 6,
                            TimeZoneId = item.TimeZoneId,
                            CountryId = item.Country == "United States" ? 1 : 2
                        };
                        _dataContext.Address.Update(address);
                        await _dataContext.SaveChangesAsync();
                        var phoneId = _dataContext.PhoneNumber.Where(p => p.PhoneNumber1 == item.PhoneNo)
                        .Select(a => a.Id).FirstOrDefault();

                        if (phoneId == 0)
                        {
                            var mpPhoneNumberId = _dataContext.MpphoneNumber.Where(mp => mp.MarketPartnerId == item.SubBusinessInfoId.Value)
                            .Select(x => x.Id).FirstOrDefault();
                            var phoneNo = new PhoneNumber
                            {
                                PhoneNumber1 = item.PhoneNo,
                                PhoneNumberTypeId = 2,
                            };
                            await _dataContext.PhoneNumber.AddAsync(phoneNo);
                            await _dataContext.SaveChangesAsync();

                            mpPhoneNosUpdateList.Add(new MpphoneNumber
                            {
                                Id = mpPhoneNumberId,
                                MarketPartnerId = item.SubBusinessInfoId.Value,
                                PhoneNumberId = phoneNo.Id
                            });
                        }
                        else
                        {
                            var mpPhoneNo = await _dataContext.MpphoneNumber.Where(x => x.MarketPartnerId == item.SubBusinessInfoId.Value)
                                .AsNoTracking().FirstOrDefaultAsync();
                            if (mpPhoneNo != null && mpPhoneNo.PhoneNumberId != phoneId)
                                mpPhoneNosUpdateList.Add(new MpphoneNumber
                                {
                                    Id = mpPhoneNo.Id,
                                    MarketPartnerId = item.SubBusinessInfoId.Value,
                                    PhoneNumberId = phoneId
                                });
                        }

                        var emailId = _dataContext.EmailAddress.Where(p => p.EmailAddress1 == item.Email)
                        .Select(a => a.Id).FirstOrDefault();

                        if (emailId == 0)
                        {
                            var mpEmailId = _dataContext.MpemailAddress.Where(mp => mp.MarketPartnerId == item.SubBusinessInfoId.Value)
                            .Select(x => x.Id).FirstOrDefault();
                            var email = new EmailAddress
                            {
                                EmailAddress1 = item.Email
                            };
                            await _dataContext.EmailAddress.AddAsync(email);
                            await _dataContext.SaveChangesAsync();

                            mpPEmailsUpdateList.Add(new MpemailAddress
                            {
                                Id = mpEmailId,
                                MarketPartnerId = item.SubBusinessInfoId.Value,
                                EmailAddressId = email.Id
                            });
                        }
                        else
                        {
                            var mpEmail = await _dataContext.MpemailAddress.Where(x => x.MarketPartnerId == item.SubBusinessInfoId.Value)
                                .AsNoTracking().FirstOrDefaultAsync();
                            if (mpEmail != null && mpEmail.EmailAddressId != emailId)
                                mpPEmailsUpdateList.Add(new MpemailAddress
                                {
                                    Id = mpEmail.Id,
                                    MarketPartnerId = item.SubBusinessInfoId.Value,
                                    EmailAddressId = emailId
                                });
                        }
                    }
                }
                _dataContext.MpphoneNumber.UpdateRange(mpPhoneNosUpdateList);
                _dataContext.MpemailAddress.UpdateRange(mpPEmailsUpdateList);
            }

            await _dataContext.SaveChangesAsync();
        }


        private async Task<long> AddContactInfo(ContactInformation request)
        {
            var contactInfo = new Entity
            {
                CreationTime = Time.EasternDate(),
                LastUpdated = Time.EasternDate(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                LanguageId = request.LanguageId
            };

            await _dataContext.Entity.AddAsync(contactInfo);
            await _dataContext.SaveChangesAsync();
            if (request.PhoneNumber != 0)
            {
                var result = _dataContext.PhoneNumber.Where(p => p.PhoneNumber1 == request.PhoneNumber)
                    .Select(a => a.Id).FirstOrDefault();
                if (result == 0)
                {
                    var phoneNumber = new PhoneNumber
                    {
                        PhoneNumber1 = request.PhoneNumber,
                        PhoneNumberTypeId = 1
                    };
                    await _dataContext.PhoneNumber.AddAsync(phoneNumber);
                    await _dataContext.SaveChangesAsync();
                    var entityPhoneNo = new EntityPhoneNumber
                    {
                        PhoneNumberId = phoneNumber.Id,
                        EntityId = contactInfo.Id
                    };
                    await _dataContext.EntityPhoneNumber.AddAsync(entityPhoneNo);
                }
                else
                {
                    var entityPhoneNo = new EntityPhoneNumber
                    {
                        PhoneNumberId = result,
                        EntityId = contactInfo.Id
                    };
                    await _dataContext.EntityPhoneNumber.AddAsync(entityPhoneNo);
                }
                await _dataContext.SaveChangesAsync();
            }
            return contactInfo.Id;
        }

        private async Task<SmartOnBoardingPrimaryKeyModel> AddLocations(List<LocationInformation> request, string legalName, SmartOnBoardingPrimaryKeyModel primaryKeys)
        {
            if (request.Any())
            {
                var mpPhoneNos = new List<MpphoneNumber>();
                var mpPEmails = new List<MpemailAddress>();
                var mpPAddress = new List<Mpaddress>();
                var addressIds = new List<long>();
                var subMarketPartnerIds = new List<long>();
                foreach (var item in request)
                {
                    var mpId = await AddSubMarketPartner(item, legalName);
                    var address = new Address
                    {
                        Address1 = item.Address,
                        City = item.City,
                        Province = item.ProvinceCode,
                        PostalCode = item.PostalCode.Replace(" ", string.Empty),
                        AddressTypeId = 6,
                        TimeZoneId = item.TimeZoneId,
                        CountryId = item.Country == "United States" ? 1 : 2
                    };
                    await _dataContext.Address.AddAsync(address);
                    await _dataContext.SaveChangesAsync();
                    var phoneId = _dataContext.PhoneNumber.Where(p => p.PhoneNumber1 == item.PhoneNo)
                    .Select(a => a.Id).FirstOrDefault();
                    if (phoneId == 0)
                    {
                        var phoneNo = new PhoneNumber
                        {
                            PhoneNumber1 = item.PhoneNo,
                            PhoneNumberTypeId = 2,
                        };
                        await _dataContext.PhoneNumber.AddAsync(phoneNo);
                        await _dataContext.SaveChangesAsync();
                        mpPhoneNos.Add(new MpphoneNumber
                        {
                            MarketPartnerId = mpId,
                            PhoneNumberId = phoneNo.Id
                        });
                    }
                    else
                    {
                        mpPhoneNos.Add(new MpphoneNumber
                        {
                            MarketPartnerId = mpId,
                            PhoneNumberId = phoneId
                        });
                    }

                    var emailId = _dataContext.EmailAddress.Where(p => p.EmailAddress1 == item.Email)
                    .Select(a => a.Id).FirstOrDefault();
                    if (emailId == 0)
                    {
                        var email = new EmailAddress
                        {
                            EmailAddress1 = item.Email,
                        };
                        await _dataContext.EmailAddress.AddAsync(email);
                        await _dataContext.SaveChangesAsync();
                        mpPEmails.Add(new MpemailAddress
                        {
                            MarketPartnerId = mpId,
                            EmailAddressId = email.Id,
                        });
                    }
                    else
                    {
                        mpPEmails.Add(new MpemailAddress
                        {
                            MarketPartnerId = mpId,
                            EmailAddressId = emailId,
                        });
                    }

                    mpPAddress.Add(new Mpaddress
                    {
                        MarketPartnerId = mpId,
                        AddressId = address.Id
                    });
                    addressIds.Add(address.Id);
                    subMarketPartnerIds.Add(mpId);
                }
                await _dataContext.MpphoneNumber.AddRangeAsync(mpPhoneNos);
                await _dataContext.MpemailAddress.AddRangeAsync(mpPEmails);
                await _dataContext.Mpaddress.AddRangeAsync(mpPAddress);
                primaryKeys.LocationInfoIds = addressIds;
                primaryKeys.SubMarketPartnerIds = subMarketPartnerIds;
            }
            await _dataContext.SaveChangesAsync();
            return primaryKeys;
        }

        private async Task AddLocation(LocationInformation item, string legalName)
        {
            var mpPhoneNo = new MpphoneNumber();
            var mpPEmail = new MpemailAddress();
            var mpPAddres = new Mpaddress();
            var address = new Address
            {
                Address1 = item.Address,
                City = item.City,
                Province = item.ProvinceCode,
                PostalCode = item.PostalCode.Replace(" ", string.Empty),
                AddressTypeId = 6,
                TimeZoneId = item.TimeZoneId,
                CountryId = item.Country == "United States" ? 1 : 2
            };
            var mpId = await AddSubMarketPartner(item, legalName);
            await _dataContext.Address.AddAsync(address);
            await _dataContext.SaveChangesAsync();
            var phoneId = _dataContext.PhoneNumber.Where(p => p.PhoneNumber1 == item.PhoneNo)
            .Select(a => a.Id).FirstOrDefault();
            if (phoneId == 0)
            {
                var phoneNo = new PhoneNumber
                {
                    PhoneNumber1 = item.PhoneNo,
                    PhoneNumberTypeId = 2,
                };
                await _dataContext.PhoneNumber.AddAsync(phoneNo);
                await _dataContext.SaveChangesAsync();
                mpPhoneNo.MarketPartnerId = mpId;
                mpPhoneNo.PhoneNumberId = phoneNo.Id;
            }
            else
            {
                mpPhoneNo.MarketPartnerId = mpId;
                mpPhoneNo.PhoneNumberId = phoneId;
            }

            var emailId = _dataContext.EmailAddress.Where(p => p.EmailAddress1 == item.Email)
            .Select(a => a.Id).FirstOrDefault();
            if (emailId == 0)
            {
                var email = new EmailAddress
                {
                    EmailAddress1 = item.Email,
                };
                await _dataContext.EmailAddress.AddAsync(email);
                await _dataContext.SaveChangesAsync();
                mpPEmail.MarketPartnerId = mpId;
                mpPEmail.EmailAddressId = email.Id;
            }
            else
            {
                mpPEmail.MarketPartnerId = mpId;
                mpPEmail.EmailAddressId = emailId;
            }

            mpPAddres.MarketPartnerId = mpId;
            mpPAddres.AddressId = address.Id;
            _dataContext.MpphoneNumber.Add(mpPhoneNo);
            _dataContext.MpemailAddress.Add(mpPEmail);
            _dataContext.Mpaddress.Add(mpPAddres);
            await _dataContext.SaveChangesAsync();
        }

    }
}
