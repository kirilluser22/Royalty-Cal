﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Retailer_Winning_Formula.DataLayer.Entities
{
    public partial class PreferredEmailAddress
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public long EmailAddressId { get; set; }

        public virtual EmailAddress EmailAddress { get; set; }
        public virtual Entity Entity { get; set; }
    }
}