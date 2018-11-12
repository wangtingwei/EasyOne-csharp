namespace EasyOne.SqlServerDal
{
    using System;

    public sealed class DateTimeQueryScript
    {
        public const string LastMonthEnd = "DATEADD(ms,-3, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0))";
        public const string LastYearEnd = "DATEADD(ms,-3, DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0))";
        public const string ThisMonthEnd = "DATEADD(ms,-3, DATEADD(mm, DATEDIFF(m, 0, GETDATE())+1, 0))";
        public const string ThisMonthFirstMonday = "DATEADD(wk, DATEDIFF(wk, 0, DATEADD(dd, 6 - DATEPART(day, GETDATE()), GETDATE())), 0)";
        public const string ThisMonthStart = "DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0)";
        public const string ThisSeasonStart = "DATEADD(qq, DATEDIFF(qq, 0, GETDATE()), 0)";
        public const string ThisWeekStart = "DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 0)";
        public const string ThisYearEnd = "DATEADD(ms,-3, DATEADD(yy, DATEDIFF(yy, 0, GETDATE())+1, 0))";
        public const string ThisYearStart = "DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0)";
        public const string TodayStart = "DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0)";
        public const string TomorrowStart = "DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 1)";
        public const string YesterdayStart = "DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), -1)";

        private DateTimeQueryScript()
        {
        }
    }
}

