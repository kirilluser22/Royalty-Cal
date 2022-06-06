using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Retailer_Winning_Formula.DataLayer.Repositories;
using Retailer_Winning_Formula.Infrastructure;
using Retailer_Winning_Formula.Infrastructure.Extensions;
using Retailer_Winning_Formula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retailer_Winning_Formula.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserReportsRepository _userReportsService;
        private readonly ISettingsRepository _settingsService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger,
             IHttpContextAccessor httpContextAccessor,
             IUserReportsRepository userReportsService,
             ISettingsRepository settingsService,
             IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userReportsService = userReportsService;
            _settingsService = settingsService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(Constants.DefaultValues.AvgTicketValue))
                && string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(Constants.DefaultValues.AvgMonthlyRetailValue)))
            {
                var settings = await _settingsService.GetDefaultSettings();
                _httpContextAccessor.HttpContext.Session.SetString(Constants.DefaultValues.AvgTicketValue, settings.FirstOrDefault(s => s.Key == Constants.Settings.AvgTicketValue).Value.ToString());
                _httpContextAccessor.HttpContext.Session.SetString(Constants.DefaultValues.AvgMonthlyRetailValue, settings.FirstOrDefault(s => s.Key == Constants.Settings.AvgMonthlyRetailValue).Value.ToString());
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateSummary([FromForm] FactorsRequestModel factorsRequest)
        {

            var settings = await _settingsService.GetDefaultSettings();

            BaseCalculationModal baseCalculations = new BaseCalculationModal
            {
                PercentageOfRevenuePlanSales = factorsRequest.PercentageOfRevenuePlanSales / 100,
                SalesAssociateCommission = factorsRequest.SalesAssociateCommission / 100,
                SlpPotentialEnrolments = factorsRequest.SlpPotentialEnrolments / 100,
                NoOfMonths = 12,
                AvgMonthlyRetailValue = Convert.ToDecimal(_httpContextAccessor.HttpContext.Session.GetString(Constants.DefaultValues.AvgMonthlyRetailValue)),
                AvgTicketValue = Convert.ToDecimal(_httpContextAccessor.HttpContext.Session.GetString(Constants.DefaultValues.AvgTicketValue)),
                AnnuaSalesVolume = factorsRequest.AnnualSalesVolume * 1000000,
                SmartOnePremiumCost = settings.FirstOrDefault(s => s.Key == Constants.Settings.SmartOnePremiumCost).Value,
                SmartLivingAvgMonthlyPlanRevenue = settings.FirstOrDefault(s => s.Key == Constants.Settings.SmartLivingAvgMonthlyPlanRevenue).Value,
                MarketPartnerSlpPercnt = settings.FirstOrDefault(s => s.Key == Constants.Settings.MarketPartnerSlpPercnt).Value,
                EnrolmentRetentionRate = settings.FirstOrDefault(s => s.Key == Constants.Settings.EnrolmentRetentionRate).Value,
                EnrolmentIncentiveGiftCard = settings.FirstOrDefault(s => s.Key == Constants.Settings.EnrolmentIncentiveGiftCard).Value,
            };
            baseCalculations = CalculationExtension.ProcessBaseCalculations(baseCalculations);


            var result = CalculationExtension.CreateDetailedAnalysisWithSummary(baseCalculations);
            TempModal tempResult = new TempModal
            {
                calculationResult = result.Item1,
                SummaryAnalysisList = result.Item2,
                BaseCalculations = baseCalculations
            };
            _httpContextAccessor.HttpContext.Session.SetString(Constants.TempDataCalculations, JsonConvert.SerializeObject(tempResult));
            _httpContextAccessor.HttpContext.Session.SetString(Constants.TempDataSummary, JsonConvert.SerializeObject(result.Item2));
            return PartialView("_Summary", result.Item2);
        }


        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public IActionResult TemplatePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SummaryPage(string isSuccess)
        {
            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(Constants.TempDataSummary)))
                return RedirectToAction(nameof(Index));

            List<SummaryAnalysisModal> summaryAnalysis =
                JsonConvert.DeserializeObject<List<SummaryAnalysisModal>>(_httpContextAccessor.HttpContext.Session.GetString(Constants.TempDataSummary));

            ViewBag.isSuccess = isSuccess;
            return View(summaryAnalysis);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendReport([FromForm] UserInfoModel modal)
        {
            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString(Constants.TempDataCalculations)))
                return RedirectToAction(nameof(Index));

            TempModal tempData = JsonConvert.DeserializeObject<TempModal>(_httpContextAccessor.HttpContext.Session.GetString(Constants.TempDataCalculations));
            _userReportsService.SendAndSaveUserReport(tempData, modal, $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}");

            return Json($"<p>Report successfully created.</p><p>Emailed to: {modal.Email}</p>");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSessionDefaultValues([FromForm] DefaultValuesUpdateModal modal)
        {
            if (ModelState.IsValid)
            {
                _httpContextAccessor.HttpContext.Session.SetString(Constants.DefaultValues.AvgTicketValue, modal.AvgTicketValue.ToString());
                _httpContextAccessor.HttpContext.Session.SetString(Constants.DefaultValues.AvgMonthlyRetailValue, modal.AvgMonthlyRetailValue.ToString());
            }
            return PartialView("_OtherFactors", modal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errMsg = JsonConvert.DeserializeObject<ErrorViewModel>(_httpContextAccessor.HttpContext.Session.GetString(Constants.ErrorMessages.ErrorMsg));
            return View(errMsg);
        }
    }
}
