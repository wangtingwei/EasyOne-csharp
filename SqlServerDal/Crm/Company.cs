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
    using System.Text;

    public sealed class Company : ICompany
    {
        private int m_TotalOfCompany;

        public bool Add(CompanyInfo companyInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(companyInfo, parms);
            companyInfo.CompanyId = GetMaxId() + 1;
            parms.AddInParameter("@CompanyID", DbType.Int32, companyInfo.CompanyId);
            return DBHelper.ExecuteProc("PR_Crm_Company_Add", parms);
        }

        public bool CheckExistsHomepage(string homepage)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Company WHERE Homepage=@Homepage", new Parameters("@Homepage", DbType.String, DBHelper.FilterBadChar(homepage)));
        }

        public bool CheckExistsPhone(string phone)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Company WHERE Phone=@Phone", new Parameters("@Phone", DbType.String, phone));
        }

        private static CompanyInfo CompanyFromrdr(NullableDataReader rdr)
        {
            CompanyInfo info = new CompanyInfo();
            info.CompanyId = rdr.GetInt32("CompanyID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.CompanyName = rdr.GetString("CompanyName");
            info.Country = rdr.GetString("Country");
            info.Province = rdr.GetString("Province");
            info.City = rdr.GetString("City");
            info.Address = rdr.GetString("Address");
            info.ZipCode = rdr.GetString("ZipCode");
            info.Phone = rdr.GetString("Phone");
            info.Fax = rdr.GetString("Fax");
            info.Homepage = rdr.GetString("Homepage");
            info.BankOfDeposit = rdr.GetString("BankOfDeposit");
            info.BankAccount = rdr.GetString("BankAccount");
            info.TaxNum = rdr.GetString("TaxNum");
            info.StatusInField = rdr.GetInt32("StatusInField");
            info.CompanySize = rdr.GetInt32("CompanySize");
            info.BusinessScope = rdr.GetString("BusinessScope");
            info.AnnualSales = rdr.GetString("AnnualSales");
            info.ManagementForms = rdr.GetInt32("ManagementForms");
            info.RegisteredCapital = rdr.GetString("RegisteredCapital");
            info.CompanyIntro = rdr.GetString("CompanyIntro");
            info.CompanyPic = rdr.GetString("CompanyPic");
            return info;
        }

        public bool Delete(int companyId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Company WHERE CompanyID = @CompanyID", new Parameters("@CompanyID", DbType.Int32, companyId));
        }

        public bool Exists(int companyId, string companyName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CompanyID", DbType.Int32, companyId);
            cmdParams.AddInParameter("@CompanyName", DbType.String, companyName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Company WHERE CompanyID!=@CompanyID AND CompanyName=@CompanyName", cmdParams);
        }

        public CompanyInfo GetByCompanyName(string companyName)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 5 * FROM PE_Company WHERE CompanyName LIKE '%" + DBHelper.FilterBadChar(companyName) + "%'"))
            {
                if (reader.Read())
                {
                    return CompanyFromrdr(reader);
                }
                return new CompanyInfo(true);
            }
        }

        public CompanyInfo GetCompanyByClientId(int clientId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Crm_Company_GetByClientId", new Parameters("@ClientID", DbType.Int32, clientId)))
            {
                if (reader.Read())
                {
                    return CompanyFromrdr(reader);
                }
                return new CompanyInfo(true);
            }
        }

        public IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string keyword)
        {
            return this.GetCompanyList(startRowIndexId, maxNumberRows, string.Empty, keyword, false);
        }

        public IList<CompanyInfo> GetCompanyList(int startRowIndexId, int maxNumberRows, string field, string keyword, bool allowEmptyName)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            List<CompanyInfo> list = new List<CompanyInfo>();
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "CompanyID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, "PE_Company");
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            StringBuilder builder = new StringBuilder("1 = 1");
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (field)
                {
                    case "CompanyName":
                        builder.Append(" AND CompanyName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;

                    case "Province":
                        builder.Append(" AND Province LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;

                    case "City":
                        builder.Append(" AND City LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;

                    case "Address":
                        builder.Append(" AND Address LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;

                    case "Phone":
                        builder.Append(" AND Phone LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;

                    case "Fax":
                        builder.Append(" AND Fax LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                        goto Label_0230;
                }
                builder.Append(" AND CompanyName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
        Label_0230:
            if (!allowEmptyName)
            {
                builder.Append(" AND CompanyName<>''");
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(CompanyFromrdr(reader));
                }
            }
            this.m_TotalOfCompany = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public CompanyInfo GetCompayById(int compayId)
        {
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Company WHERE companyId = @companyId", new Parameters("@companyId", DbType.Int32, compayId)))
            {
                if (reader.Read())
                {
                    return CompanyFromrdr(reader);
                }
                return new CompanyInfo(true);
            }
        }

        public IList<CompanyInfo> GetList(int startRowIndexId, int maxNumberRows, string keyword)
        {
            return this.GetCompanyList(startRowIndexId, maxNumberRows, string.Empty, keyword, true);
        }

        private static int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Company", "CompanyID");
        }

        private static void GetParameters(CompanyInfo companyInfo, Parameters parms)
        {
            parms.AddInParameter("@ClientID", DbType.Int32, companyInfo.ClientId);
            parms.AddInParameter("@CompanyName", DbType.String, companyInfo.CompanyName);
            parms.AddInParameter("@Country", DbType.String, companyInfo.Country);
            parms.AddInParameter("@Province", DbType.String, companyInfo.Province);
            parms.AddInParameter("@City", DbType.String, companyInfo.City);
            parms.AddInParameter("@Address", DbType.String, companyInfo.Address);
            parms.AddInParameter("@ZipCode", DbType.String, companyInfo.ZipCode);
            parms.AddInParameter("@Phone", DbType.String, companyInfo.Phone);
            parms.AddInParameter("@Fax", DbType.String, companyInfo.Fax);
            parms.AddInParameter("@Homepage", DbType.String, companyInfo.Homepage);
            parms.AddInParameter("@BankOfDeposit", DbType.String, companyInfo.BankOfDeposit);
            parms.AddInParameter("@BankAccount", DbType.String, companyInfo.BankAccount);
            parms.AddInParameter("@TaxNum", DbType.String, companyInfo.TaxNum);
            parms.AddInParameter("@StatusInField", DbType.Int32, companyInfo.StatusInField);
            parms.AddInParameter("@CompanySize", DbType.Int32, companyInfo.CompanySize);
            parms.AddInParameter("@BusinessScope", DbType.String, companyInfo.BusinessScope);
            parms.AddInParameter("@AnnualSales", DbType.String, companyInfo.AnnualSales);
            parms.AddInParameter("@ManagementForms", DbType.Int32, companyInfo.ManagementForms);
            parms.AddInParameter("@RegisteredCapital", DbType.String, companyInfo.RegisteredCapital);
            parms.AddInParameter("@CompanyIntro", DbType.String, companyInfo.CompanyIntro);
            parms.AddInParameter("@CompanyPic", DbType.String, companyInfo.CompanyPic);
        }

        public int GetTotalOfCompany()
        {
            return this.m_TotalOfCompany;
        }

        public bool RemoveUsers(int companyId)
        {
            return DBHelper.ExecuteSql("UPDATE PE_Users SET CompanyID = 0, UserType = 0 WHERE CompanyID = @CompanyID", new Parameters("@CompanyID", DbType.Int32, companyId));
        }

        public bool Update(CompanyInfo companyInfo)
        {
            Parameters parms = new Parameters();
            GetParameters(companyInfo, parms);
            parms.AddInParameter("@CompanyID", DbType.Int32, companyInfo.CompanyId);
            return DBHelper.ExecuteProc("PR_Crm_Company_Update", parms);
        }
    }
}

