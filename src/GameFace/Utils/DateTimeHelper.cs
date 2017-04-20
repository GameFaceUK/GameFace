using System;

namespace GameFace.Utils
{
    public static class MyDateTimeExtension
    {
        public static int QuarterNumber(this DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        public static DateTime FirstDayOfQuarter(this DateTime date)
        {
            return new DateTime(date.Year, ((date.Month - 1) / 3) * 3 + 1, 1);
        }

        public static DateTime LastDayOfQuarter(this DateTime date)
        {
            var first = date.FirstDayOfQuarter();

            return first.AddMonths(3).AddDays(-1);
        }

        public static bool CheckDateInSprint( this DateTime record, DateTime first, DateTime last)
        {

            return (first < record && record < last || first == record.Date || last == record);
        }

    }
}
