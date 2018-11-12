namespace EasyOne.SqlServerDal.Crm
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public sealed class Contacter : IContacter
    {
        private int m_TotalOfContacter;

        public bool Add(ContacterInfo contacterInfo)
        {
            Parameters parms = new Parameters();
            if (contacterInfo.ContacterId <= 0)
            {
                contacterInfo.ContacterId = DBHelper.GetMaxId("PE_Contacter", "ContacterID") + 1;
            }
            parms.AddInParameter("@ContacterID", DbType.Int32, contacterInfo.ContacterId);
            parms.AddInParameter("@ClientID", DbType.Int32, contacterInfo.ClientId);
            parms.AddInParameter("@ParentID", DbType.Int32, contacterInfo.ParentId);
            parms.AddInParameter("@CreateTime", DbType.DateTime, DateTime.Now);
            parms.AddInParameter("@Owner", DbType.String, contacterInfo.Owner);
            GetContacterParameters(contacterInfo, parms);
            return DBHelper.ExecuteProc("PR_Crm_Contacter_Add", parms);
        }

        public bool CheckExistsHomepage(string homepage)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE Homepage=@Homepage", new Parameters("@Homepage", DbType.String, DBHelper.FilterBadChar(homepage)));
        }

        public bool CheckExistsMsn(string msn)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE MSN=@MSN", new Parameters("@MSN", DbType.String, DBHelper.FilterBadChar(msn)));
        }

        public bool CheckExistsPhone(string phone)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE Mobile=@Phone OR OfficePhone=@Phone OR HomePhone=@Phone", new Parameters("@Phone", DbType.String, phone));
        }

        public bool CheckExistsQQ(string qq)
        {
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE QQ=@QQ", new Parameters("@QQ", DbType.String, DBHelper.FilterBadChar(qq)));
        }

        private static ContacterInfo ContacterFromrdr(NullableDataReader rdr, int flag)
        {
            ContacterInfo info = new ContacterInfo();
            info.ContacterId = rdr.GetInt32("ContacterID");
            info.ClientId = rdr.GetInt32("ClientID");
            info.UserName = rdr.GetString("UserName");
            info.ParentId = rdr.GetInt32("ParentID");
            info.UserType = (ContacterType) rdr.GetInt32("UserType");
            info.TrueName = rdr.GetString("TrueName");
            info.Sex = (UserSexType) rdr.GetInt32("Sex");
            info.Title = rdr.GetString("Title");
            info.Company = rdr.GetString("Company");
            info.Department = rdr.GetString("Department");
            info.Position = rdr.GetString("Position");
            info.Operation = rdr.GetString("Operation");
            info.CompanyAddress = rdr.GetString("CompanyAddress");
            info.Email = rdr.GetString("Email");
            info.Homepage = rdr.GetString("Homepage");
            info.QQ = rdr.GetString("QQ");
            info.Icq = rdr.GetString("ICQ");
            info.Msn = rdr.GetString("MSN");
            info.Yahoo = rdr.GetString("Yahoo");
            info.UC = rdr.GetString("UC");
            info.Aim = rdr.GetString("Aim");
            info.OfficePhone = rdr.GetString("OfficePhone");
            info.HomePhone = rdr.GetString("HomePhone");
            info.Phs = rdr.GetString("PHS");
            info.Fax = rdr.GetString("Fax");
            info.Mobile = rdr.GetString("Mobile");
            info.Country = rdr.GetString("Country");
            info.Province = rdr.GetString("Province");
            info.City = rdr.GetString("City");
            info.Address = rdr.GetString("Address");
            info.ZipCode = rdr.GetString("ZipCode");
            info.NativePlace = rdr.GetString("NativePlace");
            info.Nation = rdr.GetString("Nation");
            info.Birthday = rdr.GetNullableDateTime("Birthday");
            info.IdCard = rdr.GetString("IDCard");
            info.Marriage = (UserMarriageType) rdr.GetInt32("Marriage");
            info.Family = rdr.GetString("Family");
            info.Income = rdr.GetInt32("Income");
            info.Education = rdr.GetInt32("Education");
            info.GraduateFrom = rdr.GetString("GraduateFrom");
            info.InterestsOfLife = rdr.GetString("InterestsOfLife");
            info.InterestsOfCulture = rdr.GetString("InterestsOfCulture");
            info.InterestsOfAmusement = rdr.GetString("InterestsOfAmusement");
            info.InterestsOfSport = rdr.GetString("InterestsOfSport");
            info.InterestsOfOther = rdr.GetString("InterestsOfOther");
            info.CreateTime = rdr.GetDateTime("CreateTime");
            info.UpdateTime = rdr.GetDateTime("UpdateTime");
            info.Owner = rdr.GetString("Owner");
            if (flag == 1)
            {
                info.ShortedForm = rdr.GetString("ShortedForm");
            }
            return info;
        }

        public bool Delete(string contacterId)
        {
            return DBHelper.ExecuteSql("DELETE FROM PE_Contacter WHERE ContacterID IN (" + DBHelper.ToValidId(contacterId) + ")");
        }

        public bool DeleteByUserName(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExecuteSql("DELETE FROM PE_Contacter WHERE UserName = @UserName", cmdParams);
        }

        public bool Exists(int contacterId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ContacterID", DbType.Int32, contacterId);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE ContacterID = @ContacterID", cmdParams);
        }

        public bool Exists(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_Contacter WHERE UserName = @UserName", cmdParams);
        }

        public IList<ContacterInfo> GetAllMobileContacters()
        {
            string strSql = "SELECT * FROM PE_Contacter WHERE (Mobile <>'') OR (PHS <>'')";
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public ContacterInfo GetContacterByClientId(int clientId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Contacter WHERE ClientID = @ClientID", cmdParams))
            {
                if (reader.Read())
                {
                    return ContacterFromrdr(reader, 0);
                }
                return new ContacterInfo(true);
            }
        }

        public ContacterInfo GetContacterById(int contacterId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ContacterID", DbType.Int32, contacterId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Contacter WHERE ContacterID = @ContacterID", cmdParams))
            {
                if (reader.Read())
                {
                    return ContacterFromrdr(reader, 0);
                }
                return new ContacterInfo(true);
            }
        }

        public ContacterInfo GetContacterByUserName(string userName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@UserName", DbType.String, userName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_Contacter WHERE userName = @UserName", cmdParams))
            {
                if (reader.Read())
                {
                    return ContacterFromrdr(reader, 0);
                }
                return new ContacterInfo(true);
            }
        }

        public Dictionary<int, string> GetContacterListByClientId(int clientId)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT ContacterId, TrueName FROM PE_Contacter WHERE ClientID = @ClientID", cmdParams))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32("ContacterId"), reader.GetString("TrueName"));
                }
            }
            return dictionary;
        }

        private static void GetContacterParameters(ContacterInfo contacterInfo, Parameters parms)
        {
            parms.AddInParameter("@UserType", DbType.Int32, contacterInfo.UserType);
            parms.AddInParameter("@UserName", DbType.String, contacterInfo.UserName);
            parms.AddInParameter("@TrueName", DbType.String, contacterInfo.TrueName);
            parms.AddInParameter("@Country", DbType.String, contacterInfo.Country);
            parms.AddInParameter("@Province", DbType.String, contacterInfo.Province);
            parms.AddInParameter("@City", DbType.String, contacterInfo.City);
            parms.AddInParameter("@ZipCode", DbType.String, contacterInfo.ZipCode);
            parms.AddInParameter("@Address", DbType.String, contacterInfo.Address);
            parms.AddInParameter("@Mobile", DbType.String, contacterInfo.Mobile);
            parms.AddInParameter("@OfficePhone", DbType.String, contacterInfo.OfficePhone);
            parms.AddInParameter("@HomePhone", DbType.String, contacterInfo.HomePhone);
            parms.AddInParameter("@PHS", DbType.String, contacterInfo.Phs);
            parms.AddInParameter("@Fax", DbType.String, contacterInfo.Fax);
            parms.AddInParameter("@Homepage", DbType.String, contacterInfo.Homepage);
            parms.AddInParameter("@Email", DbType.String, contacterInfo.Email);
            parms.AddInParameter("@QQ", DbType.String, contacterInfo.QQ);
            parms.AddInParameter("@MSN", DbType.String, contacterInfo.Msn);
            parms.AddInParameter("@ICQ", DbType.String, contacterInfo.Icq);
            parms.AddInParameter("@Yahoo", DbType.String, contacterInfo.Yahoo);
            parms.AddInParameter("@UC", DbType.String, contacterInfo.UC);
            parms.AddInParameter("@Aim", DbType.String, contacterInfo.Aim);
            parms.AddInParameter("@Company", DbType.String, contacterInfo.Company);
            parms.AddInParameter("@Operation", DbType.String, contacterInfo.Operation);
            parms.AddInParameter("@CompanyAddress", DbType.String, contacterInfo.CompanyAddress);
            parms.AddInParameter("@Department", DbType.String, contacterInfo.Department);
            parms.AddInParameter("@Position", DbType.String, contacterInfo.Position);
            parms.AddInParameter("@Title", DbType.String, contacterInfo.Title);
            parms.AddInParameter("@BirthDay", DbType.DateTime, contacterInfo.Birthday);
            parms.AddInParameter("@IDCard", DbType.String, contacterInfo.IdCard);
            parms.AddInParameter("@NativePlace", DbType.String, contacterInfo.NativePlace);
            parms.AddInParameter("@Nation", DbType.String, contacterInfo.Nation);
            parms.AddInParameter("@Sex", DbType.Int32, contacterInfo.Sex);
            parms.AddInParameter("@Marriage", DbType.Int32, contacterInfo.Marriage);
            parms.AddInParameter("@Education", DbType.Int32, contacterInfo.Education);
            parms.AddInParameter("@GraduateFrom", DbType.String, contacterInfo.GraduateFrom);
            parms.AddInParameter("@InterestsOfLife", DbType.String, contacterInfo.InterestsOfLife);
            parms.AddInParameter("@InterestsOfCulture", DbType.String, contacterInfo.InterestsOfCulture);
            parms.AddInParameter("@InterestsOfAmusement", DbType.String, contacterInfo.InterestsOfAmusement);
            parms.AddInParameter("@InterestsOfSport", DbType.String, contacterInfo.InterestsOfSport);
            parms.AddInParameter("@InterestsOfOther", DbType.String, contacterInfo.InterestsOfOther);
            parms.AddInParameter("@Family", DbType.String, contacterInfo.Family);
            parms.AddInParameter("@Income", DbType.Int32, contacterInfo.Income);
            parms.AddInParameter("@UpdateTime", DbType.DateTime, DateTime.Now);
        }

        public IList<ContacterInfo> GetList(int startRowIndexId, int maxNumberRows, int searchType, string keyword)
        {
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            string str = "PE_Contacter C INNER JOIN PE_Client Cl ON C.ClientID = Cl.ClientID";
            List<ContacterInfo> list = new List<ContacterInfo>();
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxNumberRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, "ContacterID");
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "C.*, Cl.ShortedForm");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, str);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            StringBuilder builder = new StringBuilder("UserType > 0 ");
            switch (searchType)
            {
                case 1:
                    builder.Append("AND ContacterID = " + Convert.ToInt32(keyword));
                    break;

                case 2:
                    builder.Append("AND TrueName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 3:
                    builder.Append("AND UserType = 1");
                    break;

                case 4:
                    builder.Append("AND UserType = 2");
                    break;

                case 5:
                    builder.Append("AND TrueName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 6:
                    builder.Append("AND C.ClientID = " + Convert.ToInt32(keyword));
                    break;

                case 7:
                    builder.Append("AND C.Owner = '" + DBHelper.FilterBadChar(PEContext.Current.Admin.AdminName) + "'");
                    break;
            }
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, builder.ToString());
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 1));
                }
            }
            this.m_TotalOfContacter = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_Contacter", "ContacterID");
        }

        public IList<ContacterInfo> GetMobileContacterByGroupId(string groupId)
        {
            string strSql = "SELECT C.* \r\n                            FROM PE_Contacter C INNER JOIN PE_Users U ON C.UserName = U.UserName \r\n                            WHERE U.GroupId IN(" + DBHelper.ToValidId(groupId) + ") AND (C.Mobile <>'' OR C.PHS <> '')";
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public IList<ContacterInfo> GetMobileContacterByRegion(string country, string province, string city)
        {
            string strSql = "SELECT *\r\n                              FROM PE_Contacter \r\n                              WHERE Country = @Country AND Province = @Province AND City = @City AND (Mobile <>'' OR PHS <>'')";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Country", DbType.String, country);
            cmdParams.AddInParameter("@Province", DbType.String, province);
            cmdParams.AddInParameter("@City", DbType.String, city);
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public IList<ContacterInfo> GetMobileContacterByTrueName(string trueName)
        {
            string strSql = "SELECT * \r\n                              FROM PE_Contacter \r\n                              WHERE TrueName IN ('" + trueName + "') AND (Mobile <>'' OR PHS <>'')";
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public IList<ContacterInfo> GetMobileContacterByUserId(int startId, int endId)
        {
            string strSql = "SELECT C.* \r\n                              FROM PE_Contacter C INNER JOIN PE_Users U ON C.UserName = U.UserName \r\n                              WHERE U.UserId >= @StartId AND U.UserId <= @EndId AND (C.Mobile <>'' OR C.PHS <>'')";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@StartId", DbType.Int32, startId);
            cmdParams.AddInParameter("@EndId", DbType.Int32, endId);
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public IList<ContacterInfo> GetMobileContacterByUserName(string userName)
        {
            string strSql = "SELECT C.* \r\n                              FROM PE_Contacter C INNER JOIN PE_Users U ON C.UserName = U.UserName \r\n                              WHERE U.UserName IN('" + userName + "') AND (C.Mobile <>'' OR C.PHS <>'')";
            List<ContacterInfo> list = new List<ContacterInfo>();
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql))
            {
                while (reader.Read())
                {
                    list.Add(ContacterFromrdr(reader, 0));
                }
            }
            return list;
        }

        public int GetTotalOfContacter()
        {
            return this.m_TotalOfContacter;
        }

        public bool Update(ContacterInfo contacterInfo)
        {
            Parameters parms = new Parameters();
            parms.AddInParameter("@ContacterID", DbType.Int32, contacterInfo.ContacterId);
            parms.AddInParameter("@ClientID", DbType.Int32, contacterInfo.ClientId);
            parms.AddInParameter("@ParentID", DbType.Int32, contacterInfo.ParentId);
            GetContacterParameters(contacterInfo, parms);
            return DBHelper.ExecuteProc("PR_Crm_Contacter_Update", parms);
        }

        public bool UpdateClientForSameCompany(int clientId, int companyId)
        {
            string strSql = "UPDATE PE_Contacter SET ClientID = @ClientID WHERE ContacterID IN (SELECT ContacterID FROM PE_Users WHERE CompanyID = @CompanyID)";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ClientID", DbType.Int32, clientId);
            cmdParams.AddInParameter("@CompanyID", DbType.Int32, companyId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }
    }
}

