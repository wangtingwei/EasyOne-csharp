namespace EasyOne.IDal.Templates
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Templates;
    using System;
    using System.Collections.Generic;

    public interface IIncludeFile
    {
        bool Add(IncludeFileInfo includeFileInfo);
        bool Delete(int id);
        bool ExistsFileName(string fileName);
        bool ExistsName(string name);
        IncludeFileInfo GetIncludeFileInfoById(int id);
        IList<IncludeFileInfo> GetIncludeFileInfoList();
        IList<IncludeFileInfo> GetIncludeFileInfoList(int startRowIndexId, int maxNumberRows);
        IList<IncludeFileInfo> GetIncludeFileListByAssociateType(AssociateType associateType);
        int GetTotalOfIncludeFileInfo();
        bool Update(IncludeFileInfo includeFileInfo);
    }
}

