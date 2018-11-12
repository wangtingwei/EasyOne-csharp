namespace EasyOne.Logging
{
    using EasyOne.Common;
    using System;
    using System.Collections.Generic;

    public sealed class DBLog : LogEntry
    {
        private readonly ILogManager dal = DataAccess.CreateLogManager();

        public override void Add(LogInfo info)
        {
            if (info != null)
            {
                this.dal.Add(info);
            }
        }

        public override bool Delete(DateTime time)
        {
            return this.dal.Delete(time);
        }

        public override bool Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            return this.Delete(Convert.ToString(id, (IFormatProvider) null));
        }

        public bool Delete(string id)
        {
            return (DataValidator.IsValidId(id) && this.dal.Delete(id));
        }

        public IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, int category, string searchType, string keyword)
        {
            if (category > 0)
            {
                return this.dal.GetList(startRowIndexId, maxNumberRows, (LogCategory) category);
            }
            if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(keyword))
            {
                searchType = DataSecurity.FilterBadChar(searchType);
                keyword = DataSecurity.FilterBadChar(keyword);
                return this.dal.GetList(startRowIndexId, maxNumberRows, searchType, keyword);
            }
            return this.dal.GetList(startRowIndexId, maxNumberRows);
        }

        public LogInfo GetLogById(int id)
        {
            return this.dal.GetLogById(id);
        }

        public int GetTotalOfLog(int startRowIndexId, int maxNumberRows, int category, string searchType, string keyword)
        {
            return this.dal.GetTotalOfLog();
        }

        public bool Update(LogInfo logInfo)
        {
            if (logInfo == null)
            {
                return false;
            }
            return this.dal.Update(logInfo);
        }
    }
}

