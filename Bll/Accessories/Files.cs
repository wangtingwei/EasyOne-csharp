namespace EasyOne.Accessories
{
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class Files
    {
        private static readonly IFiles dal = DataAccess.CreateFiles();

        private Files()
        {
        }

        public static bool Add(FileInfo fileInfo)
        {
            fileInfo.Id = GetMaxId() + 1;
            return dal.Add(fileInfo);
        }

        public static bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public static IList<FileInfo> GetList()
        {
            return dal.GetList();
        }

        public static IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            return dal.GetList(startRowIndexId, maxNumberRows);
        }

        public static IList<FileInfo> GetList(int startRowIndexId, int maxNumberRows, string path)
        {
            return dal.GetList(startRowIndexId, maxNumberRows, path);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static FileInfo GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public static FileInfo GetModel(string path)
        {
            return dal.GetModel(path);
        }
    }
}

