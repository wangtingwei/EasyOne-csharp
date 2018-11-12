namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IChoiceset
    {
        ChoicesetInfo GetChoicesetInfoByFieldAndTableName(string tableName, string fieldName);
        IList<ChoicesetInfo> GetChoicesetList();
        bool SetFieldValue(string fieldValue, string tableName, string fieldName);
    }
}

