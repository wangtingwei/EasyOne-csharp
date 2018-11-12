namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.Common;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;

    public class UserKeyword : AbstractUserDataReport
    {
        public override Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows)
        {
            return base.GetDictionary(startRowIndexId, maxiNumRows, "PE_StatKeyword", "TKeyword", "TKeywordNum");
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
                return DataConverter.CLng(DBHelper.ExecuteScalarSql("SELECT SUM(TKeywordNum) FROM PE_StatKeyword"));
            }
        }
    }
}

