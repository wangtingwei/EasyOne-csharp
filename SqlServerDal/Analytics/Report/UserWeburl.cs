namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserWeburl : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatWeburl", "TWeburl", "TWebNum");
        }

        public override string MaxValue
        {
            get
            {
                string str = string.Empty;
                using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatWeburl ORDER BY TWebNum DESC", null))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TWeburl") + " (" + reader.GetInt32("TWebNum").ToString() + ")";
                    }
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TWebNum) FROM PE_StatWeburl"));
            }
        }
    }
}

