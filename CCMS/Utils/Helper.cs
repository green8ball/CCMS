using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Utils
{
    public class Helper
    {
        public static IEnumerable<int> EachYear(int from, int thru)
        {
            for (var year = from; year <= thru; year = year += 1)
                yield return year;
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

    }
}
