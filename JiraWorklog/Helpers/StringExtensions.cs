using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Helpers
{
    public static class StringExtensions
    {
        public static string ToTimeSinceString(this DateTime value)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);
            switch (ts.TotalSeconds)
            {
                case var s when s < 0:
                    return "future";

                case var s when s < 1:
                    return "now";

                case var s when (s < 1*MINUTE):
                    return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

                case var s when(s < 60 * MINUTE):
                    return ts.Minutes + " minutes ago";

                case var s when (s < 120 * MINUTE):
                    return "an hour ago";

                case var s when (s < 24 * HOUR):
                    return ts.Hours + " hours ago";

                case var s when (s < 48 * HOUR):
                    return "yesterday";

                case var s when (s < 30 * DAY):
                    return ts.Days + " days ago";

                case var s when (s < 12 * MONTH):
                    var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "one month ago" : months + " months ago";

                default:
                    var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
