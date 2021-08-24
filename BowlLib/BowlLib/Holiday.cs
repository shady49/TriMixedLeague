using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlerLib
{
    public class Holiday
    {
        public static DateTime getFirstTuesdayOfMonthAndYear(int Month, int Year)
        {
            DateTime dt;
            DateTime.TryParse(String.Format("{0}/{1}/1", Year, Month), out dt);
            for (int i = 0; i < 7; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Tuesday)
                {
                    return dt;
                }
                else
                {
                    dt = dt.AddDays(1);
                }
            }
            // If get to here, punt
            return DateTime.Now;
        }
        public static bool IsHoliday(DateTime date)
        {
            return
                Holiday.IsNewYearsDay(date)
             || Holiday.IsNewYearsEve(date)
             || Holiday.IsThanksgivingDay(date)
             || Holiday.IsDayAfterThanksgiving(date)
             || Holiday.IsChristmasEve(date)
             || Holiday.IsChristmasDay(date)
             || Holiday.IsFourthOfJuly(date)
             || Holiday.IsLaborDay(date)
             || Holiday.IsMemorialDay(date);
        }
        public static bool IsNewYearsDay(DateTime date)
        {
            return date.DayOfYear == AdjustForWeekendHoliday(new DateTime(date.Year, 1, 1)).DayOfYear;
        }
        public static bool IsNewYearsEve(DateTime date)
        {
            return date.DayOfYear == AdjustForWeekendHoliday(new DateTime(date.Year, 12, 31)).DayOfYear;
        }
        public static bool IsChristmasEve(DateTime date)
        {
            return date.DayOfYear == AdjustForWeekendHoliday(new DateTime(date.Year, 12, 24)).DayOfYear;
        }
        public static bool IsChristmasDay(DateTime date)
        {
            return date.DayOfYear == AdjustForWeekendHoliday(new DateTime(date.Year, 12, 25)).DayOfYear;
        }
        public static bool IsFourthOfJuly(DateTime date)
        {
            return date.DayOfYear == AdjustForWeekendHoliday(new DateTime(date.Year, 7, 4)).DayOfYear;
        }
        public static bool IsLaborDay(DateTime date)
        { // First Monday in September
            DateTime laborDay = new DateTime(date.Year, 9, 1);
            DayOfWeek dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            return date.DayOfYear == laborDay.DayOfYear;
        }
        public static bool IsMemorialDay(DateTime date)
        { //Last Monday in May
            DateTime memorialDay = new DateTime(date.Year, 5, 31);
            DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            return date.DayOfYear == memorialDay.DayOfYear;
        }
        public static bool IsThanksgivingDay(DateTime date)
        {//4th Thursday in November
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(date.Year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(date.Year, 11, thanksgiving);
            return date.DayOfYear == thanksgivingDay.DayOfYear;
        }
        public static bool IsDayAfterThanksgiving(DateTime date)
        {//Day after Thanksgiving
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(date.Year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(date.Year, 11, thanksgiving + 1);
            return date.DayOfYear == thanksgivingDay.DayOfYear;
        }

        private static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }
    }
}
