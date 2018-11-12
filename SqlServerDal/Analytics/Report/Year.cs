namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Year : AbstractTimeReport
    {
        public override int[] GetAllList()
        {
            return AbstractTimeReport.GetArray(12, "SELECT TOP 1 * From PE_StatYear WHERE TYear = 'Total'", null);
        }

        public override int[] GetList(string value)
        {
            return AbstractTimeReport.GetArray(12, "SELECT TOP 1 * From PE_StatYear WHERE TYear = @Year", new Parameters("@Year", DbType.String, value));
        }
    }
}

