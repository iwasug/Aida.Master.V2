using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public static class DateTimeHelper
    {
        public static string DateToString(this DateTime? date)
        {
            if (date != null)
            {
                return date.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToFormattedDate(string str, string format = "")
        {
            DateTime dt;

            format = string.IsNullOrEmpty(format) ? "yyyy-MM-dd" : format;

            try
            {
                dt = DateTime.ParseExact(str, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    throw new Exception(string.Format("date format sebagai berikut {0} salah", format));
                }
                else
                {
                    throw new Exception("default date format adalah yyyy-MM-dd");
                }
            }

            return dt;
        }

        public static DateTime ToUtcID(this DateTime dateTime)
        {
            DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            return dt;
        }
    }
}
