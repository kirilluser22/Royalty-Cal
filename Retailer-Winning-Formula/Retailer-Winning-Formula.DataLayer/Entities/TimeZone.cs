﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Retailer_Winning_Formula.DataLayer.Entities
{
    public partial class TimeZone
    {
        public short Id { get; set; }
        public string Abbreviation { get; set; }
        public string Tzname { get; set; }
        public string Offset { get; set; }
        public bool? IsCurrentlyDst { get; set; }
    }
}