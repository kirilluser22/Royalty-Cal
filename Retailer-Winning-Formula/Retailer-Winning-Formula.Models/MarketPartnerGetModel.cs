using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Models
{
    public class MarketPartnerGetModel
    {
        public long Id { get; set; }
        public string StoreName { get; set; }
        public string PartnerId { get; set; }
        public string BusinessLegalName { get; set; }
        public short? CurrencyId { get; set; }
        public string Currency { get; set; }
        public int TimeZoneId { get; set; }
        public string TimeZone { get; set; }
        public int[] ProductAndServiceIds { get; set; }
        public string ProductAndServices { get; set; }
        public int? LanguageId { get; set; }
        public string Language { get; set; }
        public int[] BuyingGroupIds { get; set; }
        public string BuyingGroups { get; set; }
    }
}
