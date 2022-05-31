using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class SmartOnBoardingRequestModel
    {
        public ContactInformation ContactInfo { get; set; }
        public BusinessInformation BusinessInfo { get; set; }
        public List<LocationInformation> Locations { get; set; }
    }
    public class LocationInformation
    {
        public long? Id { get; set; }
        public long? SubBusinessInfoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int? AnnualSalesVolumeId { get; set; }
        public string ProvinceCode { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public long PhoneNo { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public short TimeZoneId { get; set; }
    }
    public class BusinessInformation
    {
        public long? Id { get; set; }
        public string StoreName { get; set; }
        public string PartnerId { get; set; }
        public string BusinessLegalName { get; set; }
        public short? CurrencyId { get; set; }
        public int TimeZoneId { get; set; }
        public int[] ProductAndServiceIds { get; set; }
        public int? LanguageId { get; set; }
        public int[] BuyingGroupIds { get; set; }
    }

    public class ContactInformation
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public short? LanguageId { get; set; }
    }
}
