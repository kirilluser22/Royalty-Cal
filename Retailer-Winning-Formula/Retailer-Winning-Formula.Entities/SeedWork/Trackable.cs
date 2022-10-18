using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Entities.SeedWork
{
    public abstract class Trackable
    {
        static Trackable()
        {
            Triggers<Trackable>.Inserting += entry => entry.Entity.CreatedAt = entry.Entity.UpdatedAt = Time.EasternTime();
            Triggers<Trackable>.Updating += entry => entry.Entity.UpdatedAt = Time.EasternTime();
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
