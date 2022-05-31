using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class DefaultValuesUpdateModal
    {
        [Required(ErrorMessage = "AvgTicketValue is Required")]
        [Range(1, 99999999)]
        public decimal AvgTicketValue { get; set; }

        [Required(ErrorMessage = "AvgMonthlyRetailValue is Required")]
        [Range(1, 99999999)]
        public decimal AvgMonthlyRetailValue { get; set; }
    }
}
