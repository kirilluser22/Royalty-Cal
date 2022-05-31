using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class CalculationResultModel
    {
        public CalculationResultModel()
        {
            RetainedEnrolments = new List<RetainedEnrolmentsResultModel>();
        }
        public decimal SmartOneProtectionPlan { set; get; }
        public decimal SmartOnePlanRevenue { set; get; }
        public decimal LessPlanCost { set; get; }
        public decimal LessSalesAssociateCommission { set; get; }
        public decimal NetSmarterOneRevenue { set; get; }
        
        public List<RetainedEnrolmentsResultModel> RetainedEnrolments { set; get; }
    }

    public class RetainedEnrolmentsResultModel
    {
        public RetainedEnrolmentsResultModel()
        {
            //NonRenewdEnrolments = new List<NonRenewdEnrolmentsResultModel>();
        }
        public decimal RetainedEnrolments { set; get; }
        public decimal NewSmarterLivingEnrolments { set; get; }
        public decimal TotalSmarterLivingEnrolments { set; get; }
        public decimal NonRenewdEnrolmentsYear1Value { set; get; }
        public decimal NonRenewdEnrolmentsYear2Value { set; get; }
        public decimal NonRenewdEnrolmentsYear3Value { set; get; }
        public decimal NonRenewdEnrolmentsYear4Value { set; get; }

        public decimal GiftCardEnrolmentIncentive { set; get; }
        public decimal RecuringSlpRevenue { set; get; }
        public decimal YearSlpRecuringRevenue { set; get; }
        public decimal TotalYrRevenue { set; get; }
    }

    public class NonRenewdEnrolmentsResultModel
    {
        public int NonRenewdEnrolmentYear { set; get; }
        public decimal NonRenewdEnrolmentYearValue { set; get; }
    }
}
