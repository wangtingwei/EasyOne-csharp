namespace EasyOne.SqlServerDal.Analytics.Report
{
    using System;

    public class Week : AbstractTimeReport
    {
        public override int[] GetAllList()
        {
            return AbstractTimeReport.GetArray(7, "SELECT * From PE_StatWeek WHERE Tweek = 'Total'", null);
        }

        public override int[] GetList(string value)
        {
            return AbstractTimeReport.GetArray(7, "SELECT * From PE_StatWeek WHERE Tweek = 'Current'", null);
        }
    }
}

