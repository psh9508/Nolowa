using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToElapsedTime(this DateTime src)
        {
            var timeSpan = DateTime.Now - src;

            if (timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds != 0)
            {
                return timeSpan.Seconds + "초";
            }
            else if (timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes != 0)
            {
                return timeSpan.Minutes + "분";
            }
            else if (timeSpan.Days == 0 && timeSpan.Hours != 0)
            {
                return timeSpan.Hours + "시";
            }
            else if (timeSpan.Days != 0)
            {
                if (timeSpan.Days >= 999)
                    return "999일";

                return timeSpan.Days + "일";
            }
            else
            {
                return "1초";
            }
        }
    }
}
