using System;

namespace Retailer_Winning_Formula.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public int? StatusCode { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
