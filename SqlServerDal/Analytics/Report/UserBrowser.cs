namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserBrowser : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatBrowser", "TBrowser", "TBrwNum");
        }

        public override string MaxValue
        {
            get
            {
                NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatBrowser ORDER BY TBrwNum DESC", null);
                string str = string.Empty;
                if (reader.Read())
                {
                    str = reader.GetString("TBrowser") + " (" + reader.GetInt32("TBrwNum").ToString() + ")";
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TBrwNum) FROM PE_StatBrowser"));
            }
        }
    }
}

