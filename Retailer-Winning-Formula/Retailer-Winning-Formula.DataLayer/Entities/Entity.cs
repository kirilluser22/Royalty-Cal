﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Retailer_Winning_Formula.DataLayer.Entities
{
    public partial class Entity
    {
        public Entity()
        {
            EntityAddress = new HashSet<EntityAddress>();
            EntityEmailAddress = new HashSet<EntityEmailAddress>();
            EntityPhoneNumber = new HashSet<EntityPhoneNumber>();
            Hbfs = new HashSet<Hbfs>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? CreationTime { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }
        public string UserName { get; set; }
        public Guid WebLinkGuid { get; set; }
        public short? LanguageId { get; set; }

        public virtual PreferredAddress PreferredAddress { get; set; }
        public virtual PreferredEmailAddress PreferredEmailAddress { get; set; }
        public virtual PreferredPhoneNumber PreferredPhoneNumber { get; set; }
        public virtual ICollection<EntityAddress> EntityAddress { get; set; }
        public virtual ICollection<EntityEmailAddress> EntityEmailAddress { get; set; }
        public virtual ICollection<EntityPhoneNumber> EntityPhoneNumber { get; set; }
        public virtual ICollection<Hbfs> Hbfs { get; set; }
    }
}