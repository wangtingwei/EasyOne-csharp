namespace EasyOne.SqlServerDal.Crm
{
    using EasyOne.IDal.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class QuestionType : IQuestionType
    {
        public bool Add(string typeName)
        {
            string strSql = "INSERT INTO PE_QuestionType (TypeID, TypeName) VALUES (@TypeID, @TypeName)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TypeID", DbType.Int32, this.GetMaxId() + 1);
            cmdParams.AddInParameter("@TypeName", DbType.String, typeName);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int typeId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_QuestionType WHERE TypeID = @TypeID", new Parameters("@TypeID", DbType.Int32, typeId));
        }

        public bool Exists(string typeName)
        {
            return DBHelper.ExistsSql("SELECT * FROM PE_QuestionType WHERE TypeName = @TypeName", new Parameters("@TypeName", DbType.String, typeName));
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_QuestionType", "TypeID");
        }

        public IDictionary<int, string> GetTypeList()
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_QuestionType ORDER BY TypeID DESC"))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32("TypeID"), reader.GetString("TypeName"));
                }
            }
            return dictionary;
        }

        public string GetTypeName(int typeId)
        {
            return DBHelper.ExecuteScalarSql("SELECT TypeName FROM PE_QuestionType WHERE TypeID = @ID", new Parameters("@ID", DbType.Int32, typeId)).ToString();
        }

        public bool Update(int typeId, string typeName)
        {
            string strSql = "UPDATE PE_QuestionType SET TypeName = @TypeName WHERE TypeID = @TypeID";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TypeID", DbType.Int32, typeId);
            cmdParams.AddInParameter("@TypeName", DbType.String, typeName);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}

