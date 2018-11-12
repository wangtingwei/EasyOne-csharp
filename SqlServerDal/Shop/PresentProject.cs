namespace EasyOne.SqlServerDal.Shop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Shop;
    using EasyOne.Model.Shop;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public sealed class PresentProject : IPresentProject
    {
        private int m_TotalOfProject;

        public bool Add(PresentProjectInfo presentProjectInfo)
        {
            Parameters cmdParams = GetParameters(presentProjectInfo);
            return DBHelper.ExecuteProc("PR_Shop_PresentProject_Add", cmdParams);
        }

        public bool Delete(int projectId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProjectId", DbType.Int32, projectId);
            return DBHelper.ExecuteProc("PR_Shop_PresentProject_Delete", cmdParams);
        }

        public bool ExistsPresent(int presentId)
        {
            return DBHelper.ExistsSql(string.Format("SELECT TOP 1 ProjectID FROM dbo.PE_PresentProject WHERE PresentID LIKE '{0}' OR PresentID LIKE  '{0},%'  OR PresentID LIKE '%,{0}' OR PresentID LIKE '%,{0},%'", presentId.ToString()));
        }

        public IList<PresentProjectInfo> GetList(int startRowIndexId, int maxNumberRows)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String);
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String);
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String);
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String);
            database.SetParameterValue(storedProcCommand, "@StartRows", startRowIndexId);
            database.SetParameterValue(storedProcCommand, "@PageSize", maxNumberRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "ProjectId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "*");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_PresentProject");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            List<PresentProjectInfo> list = new List<PresentProjectInfo>();
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(PresentProjectFromrdr(reader));
                }
            }
            this.m_TotalOfProject = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        private static Parameters GetParameters(PresentProjectInfo presentProjectInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@ProjectName", DbType.String, presentProjectInfo.ProjectName);
            parameters.AddInParameter("@BeginDate", DbType.DateTime, presentProjectInfo.BeginDate);
            parameters.AddInParameter("@EndDate", DbType.DateTime, presentProjectInfo.EndDate);
            parameters.AddInParameter("@MinMoney", DbType.Decimal, presentProjectInfo.MinMoney);
            parameters.AddInParameter("@MaxMoney", DbType.Decimal, presentProjectInfo.MaxMoney);
            parameters.AddInParameter("@PresentContent", DbType.String, presentProjectInfo.PresentContent);
            parameters.AddInParameter("@Price", DbType.Decimal, presentProjectInfo.Price);
            parameters.AddInParameter("@PresentID", DbType.String, presentProjectInfo.PresentId);
            parameters.AddInParameter("@Cash", DbType.Decimal, presentProjectInfo.Cash);
            parameters.AddInParameter("@PresentExp", DbType.Int32, presentProjectInfo.PresentExp);
            parameters.AddInParameter("@PresentPoint", DbType.Int32, presentProjectInfo.PresentPoint);
            parameters.AddInParameter("@Disabled", DbType.Boolean, presentProjectInfo.Disabled);
            return parameters;
        }

        public PresentProjectInfo GetPresentProjectById(int projectId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@id", DbType.Int32, projectId);
            using (NullableDataReader reader = new NullableDataReader(DBHelper.ExecuteReaderProc("PR_Shop_PresentProject_List", cmdParams)))
            {
                if (reader.Read())
                {
                    return PresentProjectFromrdr(reader);
                }
                return new PresentProjectInfo(true);
            }
        }

        public PresentProjectInfo GetPresentProjectByTotalMoney(decimal totalMoney)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(string.Concat(new object[] { "SELECT TOP 1 * FROM PE_PresentProject WHERE MinMoney<=", totalMoney, " AND MaxMoney>", totalMoney, " AND BeginDate<='", DateTime.Now, "' AND EndDate>='", DateTime.Now, "' AND Disabled = 0" })))
            {
                if (reader.Read())
                {
                    return PresentProjectFromrdr(reader);
                }
                return new PresentProjectInfo(true);
            }
        }

        public int GetTotalOfPresentProject()
        {
            return this.m_TotalOfProject;
        }

        public bool Locked(int projectId, bool isLocked)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProjectId", DbType.Int32, projectId);
            cmdParams.AddInParameter("@Disabled", DbType.String, isLocked);
            return DBHelper.ExecuteProc("PR_Shop_PresentProject_Locked", cmdParams);
        }

        private static PresentProjectInfo PresentProjectFromrdr(NullableDataReader rdr)
        {
            PresentProjectInfo info = new PresentProjectInfo();
            info.ProjectId = rdr.GetInt32("ProjectID");
            info.ProjectName = rdr.GetString("ProjectName");
            info.BeginDate = rdr.GetDateTime("BeginDate");
            info.EndDate = rdr.GetDateTime("EndDate");
            info.MinMoney = rdr.GetDecimal("MinMoney");
            info.MaxMoney = rdr.GetDecimal("MaxMoney");
            info.PresentContent = rdr.GetString("PresentContent");
            info.Price = rdr.GetDecimal("Price");
            info.PresentId = rdr.GetString("PresentID");
            info.Cash = rdr.GetDecimal("Cash");
            info.PresentExp = rdr.GetInt32("PresentExp");
            info.PresentPoint = rdr.GetInt32("PresentPoint");
            info.Disabled = rdr.GetBoolean("Disabled");
            return info;
        }

        public bool Update(PresentProjectInfo presentProjectInfo)
        {
            Parameters cmdParams = GetParameters(presentProjectInfo);
            cmdParams.AddInParameter("@ProjectID", DbType.Int32, presentProjectInfo.ProjectId);
            return DBHelper.ExecuteProc("PR_Shop_PresentProject_Update", cmdParams);
        }
    }
}

