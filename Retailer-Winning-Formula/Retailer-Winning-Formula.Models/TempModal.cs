using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class TempModal
    {
        public List<CalculationResultModel> calculationResult { get; set; }
        public List<SummaryAnalysisModal> SummaryAnalysisList { get; set; }
        public BaseCalculationModal BaseCalculations { get; set; }
    }
}
