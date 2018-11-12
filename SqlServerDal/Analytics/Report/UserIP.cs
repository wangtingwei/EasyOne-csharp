namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;

    public class UserIP : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatIp", "TIp", "TIpNum");
        }

        public override string MaxValue
        {
            get
            {
                return string.Empty;
            }
        }

        public override int Sum
        {
            get
            {
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TIpNum) FROM PE_StatIp"));
            }
        }
    }
}

