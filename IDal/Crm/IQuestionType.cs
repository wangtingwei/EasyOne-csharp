namespace EasyOne.IDal.Crm
{
    using System;
    using System.Collections.Generic;

    public interface IQuestionType
    {
        bool Add(string typeName);
        bool Delete(int typeId);
        bool Exists(string typeName);
        int GetMaxId();
        IDictionary<int, string> GetTypeList();
        string GetTypeName(int typeId);
        bool Update(int typeId, string typeName);
    }
}

