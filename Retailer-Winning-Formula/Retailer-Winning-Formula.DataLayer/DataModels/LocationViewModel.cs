using Retailer_Winning_Formula.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeZone = Retailer_Winning_Formula.DataLayer.Entities.TimeZone;

namespace Retailer_Winning_Formula.DataLayer.DataModels
{
    public class LocationViewModel
    {
        public int RowNo { get; set; }
        public List<TimeZone> TimeZones { get; set; }
        public List<AnnualSalesVolume> AnnualSalesVolumes { get; set; }
    }
}
