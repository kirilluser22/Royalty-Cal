using Newtonsoft.Json;
using Retailer_Winning_Formula.DataLayer.JsonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Retailer_Winning_Formula.DataLayer.Entities
{
    public partial class UserReports
    {
        [NotMapped]
        public DefaultFactor _DefaultFactors
        {
            get
            {
                return JsonConvert.DeserializeObject<DefaultFactor>(string.IsNullOrEmpty(DefaultFactors) ? "{}" : DefaultFactors);
            }
            set
            {
                DefaultFactors = JsonConvert.SerializeObject(string.IsNullOrEmpty(DefaultFactors) ? "{}" : DefaultFactors);
            }
        }
    }
}
