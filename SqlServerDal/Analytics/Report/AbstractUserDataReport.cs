namespace EasyOne.SqlServerDal.Analytics.Report
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.IDal.Analytics;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public abstract class AbstractUserDataReport : IUserDataReport
    {
        private int m_ItemTotal;

        protected AbstractUserDataReport()
        {
        }

        protected Dictionary<string, int> GetDictionary(int startRowIndexId, int maxiNumRows, string tableName, string key, string value)
        {
            Database database = DatabaseFactory.CreateDatabase();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            DbCommand storedProcCommand = database.GetStoredProcCommand("PR_Common_GetList");
            database.AddInParameter(storedProcCommand, "@StartRows", DbType.Int32, startRowIndexId);
            database.AddInParameter(storedProcCommand, "@PageSize", DbType.Int32, maxiNumRows);
            database.AddInParameter(storedProcCommand, "@SortColumn", DbType.String, value);
            database.AddInParameter(storedProcCommand, "@StrColumn", DbType.String, "*");
            database.AddInParameter(storedProcCommand, "@Sorts", DbType.String, "DESC");
            database.AddInParameter(storedProcCommand, "@Filter", DbType.String, "");
            database.AddInParameter(storedProcCommand, "@TableName", DbType.String, tableName);
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, maxiNumRows);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetString(key), reader.GetInt32(value));
                }
            }
            this.m_ItemTotal = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return dictionary;
        }

        public abstract Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows);

        public int Count
        {
            get
            {
                return this.m_ItemTotal;
            }
        }

        public abstract string MaxValue { get; }

        public abstract int Sum { get; }
    }
}

