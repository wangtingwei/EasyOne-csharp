namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserScreen : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatScreen", "TScreen", "TScrNum");
        }

        public override string MaxValue
        {
            get
            {
                string str = string.Empty;
                using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatScreen ORDER BY TScrNum DESC", null))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TScreen") + " (" + reader.GetInt32("TScrNum").ToString() + ")";
                    }
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TScrNum) FROM PE_StatScreen"));
            }
        }
    }
}

