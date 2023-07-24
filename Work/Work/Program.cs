using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work
{
    class Program
    {
        static void Main()
        {
             int year = 2024;
             int month = 2;


            DateTime fDayofMonth = new DateTime(year, month, 1);
            DateTime lDayOfMonth = fDayofMonth.AddMonths(1).AddDays(-1);

            DateTime fSunday = GetFirstSunday(fDayofMonth, DayOfWeek.Sunday);
            DateTime laSaturday = GetLastSaturday(lDayOfMonth, DayOfWeek.Saturday);

            TimeSpan output = laSaturday - fSunday;

            Console.WriteLine(output.TotalDays);
            Console.ReadLine();

        }

        //To get the first sunday of month
        static DateTime GetFirstSunday(DateTime date, DayOfWeek dayOfWeek)
        {
            while (date.DayOfWeek != dayOfWeek)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        //To get the last saturday of month
        static DateTime GetLastSaturday(DateTime date, DayOfWeek dayOfWeek)
        {

            while (date.DayOfWeek != dayOfWeek) 
            {
                date=date.AddDays(-1);
            }
            return date;
        }
    }
}
