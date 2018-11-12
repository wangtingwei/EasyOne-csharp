namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserSystem : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatSystem", "TSystem", "TSysNum");
        }

        public override string MaxValue
        {
            get
            {
                string str = string.Empty;
                using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatSystem ORDER BY TSysNum DESC", null))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TSystem") + " (" + reader.GetInt32("TSysNum").ToString() + ")";
                    }
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TSysNum) FROM PE_StatSystem"));
            }
        }
    }
}

