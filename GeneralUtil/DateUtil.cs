using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dager
{
    public static class DateUtil
    {
        public static List<DateTime> GetYearMonthsRange(string startYearMonth, string endYearMonth, string format = "yyyy-MM")
        {

            GeneralUtil.CheckNullArgs(startYearMonth, endYearMonth);
            GeneralUtil.CheckEmptyStrings(startYearMonth, endYearMonth);

            List<DateTime> Months = new List<DateTime>();
            DateTime startDT = DateTime.ParseExact(startYearMonth, format, CultureInfo.InvariantCulture);
            DateTime endDT = DateTime.ParseExact(endYearMonth, format, CultureInfo.InvariantCulture);
            while (startDT <= endDT)
            {
                Months.Add(startDT);
                startDT = startDT.AddMonths(1);
            }
            return Months;
        }
        
    }
}
