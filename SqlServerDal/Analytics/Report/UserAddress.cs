namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserAddress : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatAddress", "TAddress", "TAddNum");
        }

        public override string MaxValue
        {
            get
            {
                string str = string.Empty;
                using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatAddress ORDER BY TAddNum DESC", null))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TAddress") + " (" + reader.GetInt32("TAddNum").ToString() + ")";
                    }
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TAddNum) FROM PE_StatAddress"));
            }
        }
    }
}

