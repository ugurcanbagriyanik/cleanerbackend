using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers
{
    public static class DatetimeHelper
    {
        public static DateTime? ToDateTimeUtc(this string datestring)
        {
            if (String.IsNullOrEmpty(datestring))
            {
                return null;
            }

            return datestring.ToDateTime()!.Value.ToUniversalTime();
        }        
        public static DateTimeOffset? ToDateTimeOffsetUtc(this string datestring)
        {
            if (String.IsNullOrEmpty(datestring))
            {
                return null;
            }

            return datestring.ToDateTimeOffset()!.Value.ToUniversalTime();
        }

        public static DateTime? ToDateTime(this string datestring)
        {
            if (String.IsNullOrEmpty(datestring))
            {
                return null;
            }

            return DateTime.Parse(datestring, new CultureInfo("EN-us"));
        }

        public static DateTimeOffset? ToDateTimeOffset(this string datestring)
        {
            if (String.IsNullOrEmpty(datestring))
            {
                return null;
            }

            return DateTimeOffset.Parse(datestring, new CultureInfo("EN-us"));
        }
    }
}
