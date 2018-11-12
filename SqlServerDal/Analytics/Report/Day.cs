namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Day : AbstractTimeReport
    {
        public override int[] GetAllList()
        {
            return AbstractTimeReport.GetArray(0x18, "SELECT * From PE_StatDay WHERE TDay = 'Total'", null);
        }

        public override int[] GetList(string value)
        {
            return AbstractTimeReport.GetArray(0x18, "SELECT * From PE_StatDay WHERE TDay = @day", new Parameters("@day", DbType.String, value));
        }
    }
}

