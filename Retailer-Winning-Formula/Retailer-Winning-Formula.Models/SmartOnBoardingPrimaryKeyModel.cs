using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class SmartOnBoardingPrimaryKeyModel
    {
        public long ContactInfoId { get; set; }
        public long BusinessInfoId { get; set; }
        public List<long> LocationInfoIds { get; set; }
        public List<long> SubMarketPartnerIds { get; set; }
        public long? EntityPhoneId { get; set; }
    }
}
