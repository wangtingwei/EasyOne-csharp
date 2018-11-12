namespace EasyOne.Logging
{
    using System;
    using System.Collections.Generic;

    public interface ILogManager
    {
        void Add(LogInfo logInfo);
        bool Delete(DateTime time);
        bool Delete(string id);
        IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows);
        IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, LogCategory category);
        IList<LogInfo> GetList(int startRowIndexId, int maxNumberRows, string searchType, string keyword);
        LogInfo GetLogById(int id);
        int GetTotalOfLog();
        bool Update(LogInfo logInfo);
    }
}

