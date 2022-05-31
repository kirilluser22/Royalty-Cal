namespace Retailer_Winning_Formula.Models
{
    public class BaseCalculationModal
    {
        #region EnteredFactors
        public decimal AnnuaSalesVolume { get; set; }
        public decimal PercentageOfRevenuePlanSales { get; set; }
        public decimal SalesAssociateCommission { get; set; }
        public decimal SlpPotentialEnrolments { get; set; }
        public decimal PlanRevenue { get; set; }
        public decimal CommissionPaid { get; set; }
        #endregion

        #region Modifiable Factors
        public decimal AvgMonthlyRetailValue { get; set; }
        public decimal AvgTicketValue { get; set; }
        public decimal AnnualNumberOfTransactions { get; set; }
        public decimal MonthlyPlanRevenue { get; set; }
        #endregion

        #region Default Factors
        public decimal SmartOnePremiumCost { get; set; }
        public decimal SmartLivingAvgMonthlyPlanRevenue { get; set; }
        public decimal MarketPartnerSlpPercnt { get; set; }
        public decimal EnrolmentRetentionRate { get; set; }
        public decimal EnrolmentIncentiveGiftCard { get; set; }
        public decimal MonthlyPlanCost { get; set; }

        #endregion

        #region Calculated Factors
        public decimal TransactionAttachmentRate { get; set; }
        public int NoOfMonths { get; set; }
        public decimal PlanSoldPerMonth { get; set; }
        public decimal PlanSoldPerYear { get; set; }

        #endregion
    }
}
