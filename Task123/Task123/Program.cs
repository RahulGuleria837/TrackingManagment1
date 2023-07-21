using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task123
{

    class Program
    {
        static void Main()
        {
           
            int year = 2023; 
            int month = 6;  
            

            DateTime fDayofMonth = new DateTime(year, month, 1);
            DateTime lDayOfMonth =fDayofMonth.AddMonths(1).AddDays(-1);

            DateTime fSunday = GetNextWeekday(fDayofMonth, DayOfWeek.Sunday);
            DateTime laSaturday = GetPreviousWeekday(lDayOfMonth, DayOfWeek.Saturday);

            TimeSpan output = laSaturday - fSunday;

            Console.WriteLine (output.TotalDays);
            Console.ReadLine();
        }

        // Helper function to get the next occurrence of a specific weekday
        static DateTime GetNextWeekday(DateTime date, DayOfWeek dayOfWeek)
        {
            while (date.DayOfWeek != dayOfWeek)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        // Helper function to get the previous occurrence of a specific weekday
        static DateTime GetPreviousWeekday(DateTime date, DayOfWeek dayOfWeek)
        {
            while (date.DayOfWeek != dayOfWeek)
            {
                date = date.AddDays(-1);
            }
            return date;
        }
    }
}
