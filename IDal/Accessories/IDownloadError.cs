namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IDownloadError
    {
        bool Add(DownloadErrorInfo downloadErrorInfo);
        bool Clear();
        bool Delete(string errorId);
        bool Exists(int infoId, string errorInformation);
        IList<DownloadErrorInfo> GetDownloadErrorList(int startRowIndexId, int maxiNumRows, string searchType, string keyword);
        int GetTotalOfDownloadError();
        bool UpdateErrorTimes(int infoId, string errorInformation);
    }
}

