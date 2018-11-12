namespace EasyOne.Crm
{
    using EasyOne.IDal.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public sealed class QuestionType
    {
        private static readonly IQuestionType dal = DataAccess.CreateQuestionType();

        private QuestionType()
        {
        }

        public static bool Add(string typeName)
        {
            return dal.Add(typeName);
        }

        public static bool Delete(int typeId)
        {
            return dal.Delete(typeId);
        }

        public static bool Exists(string typeName)
        {
            return dal.Exists(typeName);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static IDictionary<int, string> GetTypeList()
        {
            return dal.GetTypeList();
        }

        public static string GetTypeName(int typeId)
        {
            return dal.GetTypeName(typeId);
        }

        public static bool Update(int typeId, string typeName)
        {
            return dal.Update(typeId, typeName);
        }
    }
}

