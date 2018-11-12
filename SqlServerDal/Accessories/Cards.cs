namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Cards : ICards
    {
        private static Parameters AddCommonParameters(CardInfo info)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@CardType", DbType.Int32, info.CardType);
            parameters.AddInParameter("@ProductID", DbType.Int32, info.ProductId);
            parameters.AddInParameter("@TableName", DbType.String, info.TableName);
            parameters.AddInParameter("@CardNum", DbType.String, info.CardNum);
            parameters.AddInParameter("@Password", DbType.String, info.Password);
            parameters.AddInParameter("@AgentName", DbType.String, info.AgentName);
            parameters.AddInParameter("@Money", DbType.Decimal, info.Money);
            parameters.AddInParameter("@ValidNum", DbType.Int32, info.ValidNum);
            parameters.AddInParameter("@ValidUnit", DbType.Int32, info.ValidUnit);
            parameters.AddInParameter("@EndDate", DbType.DateTime, info.EndDate);
            parameters.AddInParameter("@UserName", DbType.String, info.UserName);
            parameters.AddInParameter("@CreateTime", DbType.DateTime, info.CreateTime);
            parameters.AddInParameter("@OrderItemID", DbType.Int32, info.OrderItemId);
            return parameters;
        }

        public bool CardAdd(CardInfo info)
        {
            Parameters cmdParams = AddCommonParameters(info);
            cmdParams.AddInParameter("@ProductName", DbType.String, info.ProductName);
            return DBHelper.ExecuteProc("PR_UserManage_Cards_Add", cmdParams);
        }

        private static CardInfo CardFromDataReader(NullableDataReader rdr)
        {
            CardInfo info = new CardInfo();
            info.AgentName = rdr.GetString("AgentName");
            info.CardId = rdr.GetInt32("CardId");
            info.CardNum = rdr.GetString("CardNum");
            info.CardType = rdr.GetInt32("CardType");
            info.CreateTime = rdr.GetDateTime("CreateTime");
            info.EndDate = rdr.GetDateTime("EndDate");
            info.Money = rdr.GetDecimal("Money");
            info.OrderItemId = rdr.GetInt32("OrderItemId");
            info.Password = rdr.GetString("Password");
            info.ProductId = rdr.GetInt32("ProductId");
            info.TableName = rdr.GetString("TableName");
            info.ProductName = rdr.GetString("ProductName");
            info.UserName = rdr.GetString("UserName");
            info.UseTime = rdr.GetNullableDateTime("UseTime");
            info.ValidNum = rdr.GetInt32("ValidNum");
            info.ValidUnit = rdr.GetInt32("ValidUnit");
            return info;
        }

        public bool DelCard(int cardId)
        {
            string strSql = "DELETE FROM PE_Cards WHERE CardID = @CardId AND UserName = '' AND OrderItemID = 0";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CardId", DbType.Int32, cardId);
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public CardInfo GetCardById(int cardId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CardId", DbType.Int32, cardId);
            string strSql = "SELECT TOP 1 * FROM PE_Cards WHERE CardId = @CardId";
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return CardFromDataReader(reader);
                }
                return new CardInfo(true);
            }
        }

        public CardInfo GetCardByNumAndPassword(string cardNum, string password)
        {
            string strSql = "SELECT TOP 1 * From PE_Cards WHERE UserName = '' AND CardNum = @CardNum AND Password = @Password";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@CardNum", DbType.String, cardNum);
            cmdParams.AddInParameter("@Password", DbType.String, password);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return CardFromDataReader(reader);
                }
                return new CardInfo(true);
            }
        }

        public CardInfo GetCardByOrderItemId(int productId, string tableName, int orderItemId)
        {
            string strSql = "SELECT TOP 1 * FROM PE_Cards WHERE ProductId = @ProductId AND TableName = @TableName AND OrderItemId = @OrderItemId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductId", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            cmdParams.AddInParameter("@OrderItemId", DbType.Int32, orderItemId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return CardFromDataReader(reader);
                }
                return new CardInfo(true);
            }
        }

        public IList<CardInfo> GetCardList(string tableName, int productId, int orderItemId)
        {
            string strSql = "SELECT * FROM PE_Cards WHERE OrderItemID = @OrderItemID AND ProductID = @ProductID AND TableName = @TableName";
            IList<CardInfo> list = new List<CardInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@OrderItemID", DbType.Int32, orderItemId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(CardFromDataReader(reader));
                }
            }
            return list;
        }

        public IList<CardInfo> GetCardList(int startRowIndexId, int maxiNumRows, int cardType, int cardStatus, int field, string keyword, string agentName)
        {
            IList<CardInfo> list = new List<CardInfo>();
            CommonListParameters cmdParams = new CommonListParameters(startRowIndexId, maxiNumRows);
            cmdParams.TableName = "PE_Cards";
            cmdParams.StrColumn = "*";
            cmdParams.SortColumn = "CardID";
            cmdParams.Sorts = Sorts.Desc;
            cmdParams.Filter = GetField(cardType, cardStatus, field, keyword, agentName);
            cmdParams.CreateParameter();
            using (NullableDataReader reader = DBHelper.ExecuteReaderProc("PR_Common_GetList", cmdParams))
            {
                while (reader.Read())
                {
                    CardInfo item = CardFromDataReader(reader);
                    list.Add(item);
                }
            }
            return list;
        }

        private static string GetField(int cardType, int cardStatus, int field, string keyword, string agentName)
        {
            StringBuilder builder = new StringBuilder("1 = 1 AND ProductID NOT IN (SELECT DISTINCT ItemID FROM PE_CommonModel WHERE Status=-3) ");
            switch (cardType)
            {
                case 0:
                    builder.Append(" AND CardType = 0 ");
                    break;

                case 1:
                    builder.Append(" AND CardType = 1 ");
                    break;
            }
            switch (cardStatus)
            {
                case 1:
                    builder.Append(" AND UserName = '' AND DATEDIFF(day, GETDATE(), Enddate) >= 0 ");
                    break;

                case 2:
                    builder.Append(" AND UserName <> '' ");
                    break;

                case 3:
                    builder.Append(" AND UserName = '' AND DATEDIFF(dd, GETDATE(), Enddate) < 0 ");
                    break;
            }
            switch (field)
            {
                case 1:
                    builder.Append(" AND CardNum LIKE '%" + DBHelper.FilterBadChar(keyword) + "%'");
                    break;

                case 2:
                    builder.Append(" AND Money = " + DBHelper.ToNumber(keyword) + " ");
                    break;

                case 3:
                    builder.Append(" AND AgentName = '" + DBHelper.FilterBadChar(keyword) + "'");
                    break;

                case 4:
                    builder.Append(" AND UserName = '" + DBHelper.FilterBadChar(keyword) + "'");
                    break;
            }
            if (!string.IsNullOrEmpty(agentName))
            {
                builder.Append(" AND AgentName = '" + DBHelper.FilterBadChar(agentName) + "'");
            }
            return builder.ToString();
        }

        public int GetTotalofCards(int cardType, int cardStatus, int field, string keyword, string agentName)
        {
            object obj2 = DBHelper.ExecuteScalarSql("SELECT COUNT(*) FROM PE_Cards WHERE " + GetField(cardType, cardStatus, field, keyword, agentName));
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public IList<CardInfo> GetUnsoldCard(string tableName, int productId, int amount)
        {
            string strSql = "SELECT TOP " + amount.ToString() + " * FROM PE_Cards WHERE OrderItemID = 0 AND ProductID = @ProductID AND TableName = @TableName";
            IList<CardInfo> list = new List<CardInfo>();
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@ProductID", DbType.Int32, productId);
            cmdParams.AddInParameter("@TableName", DbType.String, tableName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(CardFromDataReader(reader));
                }
            }
            return list;
        }

        public bool Update(CardInfo info)
        {
            Parameters cmdParams = AddCommonParameters(info);
            cmdParams.AddInParameter("@CardID", DbType.Int32, info.CardId);
            cmdParams.AddInParameter("@UseTime", DbType.DateTime, info.UseTime);
            return DBHelper.ExecuteProc("PR_UserManage_Cards_Update", cmdParams);
        }
    }
}

