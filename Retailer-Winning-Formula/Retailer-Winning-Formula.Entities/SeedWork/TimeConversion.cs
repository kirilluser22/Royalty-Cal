using System;
using System.Collections.Generic;
using System.Text;

namespace Retailer_Winning_Formula.Entities.SeedWork
{
    public static class Time
    {
        public static DateTime EasternTime()
        {
            DateTime localDateTime = DateTime.Now;
            var dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(localDateTime,
                        TimeZoneInfo.Local.Id, "Eastern Standard Time");
            return dateTime;
        }
        public static DateTime EasternDate()
        {
            DateTime localDate = DateTime.Now.Date;
            var date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(localDate,
                        TimeZoneInfo.Local.Id, "Eastern Standard Time");
            return date;
        }
    }
}
