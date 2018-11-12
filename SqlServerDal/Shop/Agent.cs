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
    using System.Text;

    public class Agent : IAgent
    {
        private int m_Total;

        public IList<string> GetAgentNameList(int startRowIndexId, int maxiNumRows, string keyword)
        {
            IList<string> list = new List<string>();
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
            database.SetParameterValue(storedProcCommand, "@PageSize", maxiNumRows);
            database.SetParameterValue(storedProcCommand, "@SortColumn", "UserId");
            database.SetParameterValue(storedProcCommand, "@StrColumn", "UserName");
            database.SetParameterValue(storedProcCommand, "@Sorts", "DESC");
            database.SetParameterValue(storedProcCommand, "@TableName", "PE_Users U INNER JOIN PE_UserGroups G ON U.GroupID = G.GroupID ");
            StringBuilder builder = new StringBuilder();
            builder.Append(" G.GroupType = 1");
            if (!string.IsNullOrEmpty(keyword))
            {
                builder.Append(" AND U.UserName LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
            }
            database.SetParameterValue(storedProcCommand, "@Filter", builder.ToString());
            database.AddOutParameter(storedProcCommand, "@Total", DbType.Int32, 10);
            using (NullableDataReader reader = new NullableDataReader(database.ExecuteReader(storedProcCommand)))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString("UserName"));
                }
            }
            this.m_Total = (int) database.GetParameterValue(storedProcCommand, "@Total");
            return list;
        }

        public int GetTotal()
        {
            return this.m_Total;
        }

        public bool Payment(AgentInfo agentInfo)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@Money", DbType.Decimal, agentInfo.Money);
            cmdParams.AddInParameter("@AgentName", DbType.String, agentInfo.AgentName);
            flag = DBHelper.ExecuteSql("UPDATE PE_Users SET Balance = Balance - @Money WHERE UserName = @AgentName", cmdParams);
            if (agentInfo.Margin > 0M)
            {
                if (flag)
                {
                    Parameters parameters2 = new Parameters();
                    parameters2.AddInParameter("@Margin", DbType.Decimal, agentInfo.Margin);
                    parameters2.AddInParameter("@AgentName", DbType.String, agentInfo.AgentName);
                    flag2 = DBHelper.ExecuteSql("UPDATE PE_Users SET Balance = Balance + @Margin WHERE UserName = @AgentName", parameters2);
                }
            }
            else
            {
                flag2 = true;
            }
            if (flag && flag2)
            {
                flag3 = DBHelper.ExecuteSql("UPDATE PE_Orders SET MoneyReceipt = MoneyTotal, Status = 1 WHERE OrderID = @OrderID", new Parameters("@OrderID", DbType.Int32, agentInfo.OrderId));
            }
            return flag3;
        }
    }
}

