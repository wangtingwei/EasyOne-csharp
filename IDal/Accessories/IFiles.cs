namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IFiles
    {
        bool Add(FileInfo fileInfo);
        int Count();
        bool Delete(int id);
        IList<FileInfo> GetList();
        IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows);
        IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows, string path);
        int GetMaxId();
        FileInfo GetModel(int id);
        FileInfo GetModel(string path);
    }
}

