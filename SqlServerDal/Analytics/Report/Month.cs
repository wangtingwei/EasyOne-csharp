namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Month : AbstractTimeReport
    {
        public override int[] GetAllList()
        {
            return AbstractTimeReport.GetArray(0x1f, "SELECT TOP 1 * From PE_StatMonth WHERE TMonth = 'Total'", null);
        }

        public override int[] GetList(string value)
        {
            return AbstractTimeReport.GetArray(0x1f, "SELECT TOP 1 * From PE_StatMonth WHERE TMonth = @Month", new Parameters("@Month", DbType.String, value));
        }
    }
}

