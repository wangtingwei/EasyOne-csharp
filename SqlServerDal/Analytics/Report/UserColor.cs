namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserColor : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatColor", "TColor", "TColNum");
        }

        public override string MaxValue
        {
            get
            {
                string str = string.Empty;
                using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, "SELECT TOP 1 * From PE_StatColor ORDER BY TColNum DESC", null))
                {
                    if (reader.Read())
                    {
                        str = reader.GetString("TColor") + " (" + reader.GetInt32("TColNum").ToString() + ")";
                    }
                }
                return str;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TColNum) FROM PE_StatColor"));
            }
        }
    }
}

