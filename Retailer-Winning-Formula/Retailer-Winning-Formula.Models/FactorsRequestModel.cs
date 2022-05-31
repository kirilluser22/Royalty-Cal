using System;

namespace Retailer_Winning_Formula.Models
{
    public class FactorsRequestModel
    {
        public decimal AnnualSalesVolume { get; set; }
        public decimal PercentageOfRevenuePlanSales { get; set; }
        public decimal SalesAssociateCommission { get; set; }
        public decimal SlpPotentialEnrolments { get; set; }
        public decimal AvgMonthlyRetailValue { get; set; }
        public decimal AvgTicketValue { get; set; }
    }
}
