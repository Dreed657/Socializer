﻿namespace Socializer.Services
{
    using System;
    using System.Threading.Tasks;

    public class TimeService
    {
        public static string TimeAgo(DateTime dt)
        {
            if (dt > DateTime.Now)
            {
                return "about sometime from now";
            }

            var span = DateTime.Now - dt;

            if (span.Days > 365)
            {
                var years = span.Days / 365;

                if (span.Days % 365 != 0)
                {
                    years += 1;
                }

                return $"about {years} {(years == 1 ? "year" : "years")} ago";
            }

            if (span.Days > 30)
            {
                var months = span.Days / 30;

                if (span.Days % 31 != 0)
                {
                    months += 1;
                }

                return $"about {months} {(months == 1 ? "month" : "months")} ago";
            }

            if (span.Days > 0)
            {
                return $"about {span.Days} {(span.Days == 1 ? "day" : "days")} ago";
            }

            if (span.Hours > 0)
            {
                return $"about {span.Hours} {(span.Hours == 1 ? "hour" : "hours")} ago";
            }

            if (span.Minutes > 0)
            {
                return $"about {span.Minutes} {(span.Minutes == 1 ? "minute" : "minutes")} ago";
            }

            if (span.Seconds > 5)
            {
                return $"about {span.Seconds} seconds ago";
            }

            if (span.Seconds <= 5)
            {
                return "just now";
            }

            return string.Empty;
        }
    }
}
