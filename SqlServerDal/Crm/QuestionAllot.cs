namespace EasyOne.SqlServerDal.Crm
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class QuestionAllot : IQuestionAllot
    {
        private int m_Total;

        public bool Add(int typeId, int adminId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TypeID", DbType.Int32, typeId);
            cmdParams.AddInParameter("@AdminID", DbType.Int32, adminId);
            return DBHelper.ExecuteSql("IF NOT EXISTS(SELECT * FROM [PE_QuestionType_Admin] WHERE TypeID = @TypeID AND AdminID = @AdminID) INSERT INTO [PE_QuestionType_Admin]([TypeID], [AdminID]) VALUES (@TypeID, @AdminID)", cmdParams);
        }

        public bool Delete(string adminIdlist)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_QuestionType_Admin WHERE AdminID In (" + DBHelper.ToValidId(adminIdlist) + ")");
        }

        public bool Delete(int typeId, int adminId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@TypeID", DbType.Int32, typeId);
            cmdParams.AddInParameter("@AdminID", DbType.Int32, adminId);
            return DBHelper.ExecuteSql("DELETE FROM PE_QuestionType_Admin WHERE TypeID = @TypeID AND AdminID = @AdminID", cmdParams);
        }

        private static QuestionAllotInfo GetInfoFormReader(NullableDataReader rdr)
        {
            QuestionAllotInfo info = new QuestionAllotInfo();
            info.TypeId = rdr.GetInt32("TypeID");
            info.TypeName = rdr.GetString("TypeName");
            info.AdminId = rdr.GetInt32("AdminID");
            info.AdminName = rdr.GetString("AdminName");
            return info;
        }

        public IList<QuestionAllotInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            string str = "PE_QuestionType_Admin Q INNER JOIN PE_QuestionType T ON Q.TypeID = T.TypeID INNER JOIN PE_Admin A ON Q.AdminID = A.AdminID";
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "Q.AdminID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "Q.TypeID, T.TypeName, Q.AdminID, A.AdminName");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            IList<QuestionAllotInfo> list = new List<QuestionAllotInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(GetInfoFormReader(reader));
                }
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public IList<QuestionAllotInfo> GetListByAdminId(int adminId)
        {
            IList<QuestionAllotInfo> list = new List<QuestionAllotInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT Q.TypeID, T.TypeName, Q.AdminID, A.AdminName FROM PE_QuestionType_Admin Q INNER JOIN PE_QuestionType T ON Q.TypeID = T.TypeID INNER JOIN PE_Admin A ON Q.AdminID = A.AdminID WHERE Q.AdminID = @AdminID ORDER BY Q.TypeID DESC, Q.AdminID DESC", new Parameters("@AdminID", DbType.Int32, adminId)))
            {
                while (reader.Read())
                {
                    list.Add(GetInfoFormReader(reader));
                }
            }
            return list;
        }

        public int GetTotal()
        {
            return this.m_Total;
        }
    }
}

