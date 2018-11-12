namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using EasyOne.DalFactory;

    public class Cards
    {
        private static readonly ICards dal = DataAccess.CreateCards();
        private string m_Message;
        private static readonly string[] ValidUnitType = new string[] { "点", "天", "月", "年", "元" };

        public int BatchAddCards(CardInfo info, string cardText, string split, string agentName)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            string[] strArray = cardText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] field = strArray[i].Trim().Split(split.Trim().ToCharArray(), StringSplitOptions.None);
                info.CardNum = DataSecurity.GetArrayValue(0, field);
                string arrayValue = DataSecurity.GetArrayValue(1, field);
                if (!string.IsNullOrEmpty(info.CardNum))
                {
                    if (string.IsNullOrEmpty(arrayValue))
                    {
                        builder.Append("<li>卡号为：").Append(info.CardNum).Append(" 的充值卡没有提供密码，不能添加！").Append("</li>");
                    }
                    else
                    {
                        info.Password = StringHelper.Base64StringEncode(arrayValue);
                        info.AgentName = DataSecurity.FilterBadChar(agentName);
                        if (CardAdd(info))
                        {
                            builder.Append("<li>卡号为：" + info.CardNum + " 的充值卡成功添加到数据库中！</li>");
                            num++;
                        }
                        else
                        {
                            builder.Append("<li>卡号为：" + info.CardNum + " 的充值卡已经存在！</li>");
                        }
                    }
                }
            }
            this.m_Message = builder.ToString();
            return num;
        }

        public static bool CardAdd(CardInfo info)
        {
            return dal.CardAdd(info);
        }

        public static bool DelCard(int cardId)
        {
            return dal.DelCard(cardId);
        }

        public static CardInfo GetCardById(int cardId)
        {
            return dal.GetCardById(cardId);
        }

        public static CardInfo GetCardByNumAndPassword(string cardNum, string password)
        {
            if (!string.IsNullOrEmpty(cardNum) && !string.IsNullOrEmpty(password))
            {
                return dal.GetCardByNumAndPassword(cardNum, password);
            }
            return new CardInfo(true);
        }

        public static CardInfo GetCardByOrderItemId(int productId, string tableName, int orderItemId)
        {
            return dal.GetCardByOrderItemId(productId, tableName, orderItemId);
        }

        public static IList<CardInfo> GetCardList(string tableName, int productId, int orderItemId)
        {
            return dal.GetCardList(tableName, productId, orderItemId);
        }

        public static IList<CardInfo> GetCardList(int startRowIndexId, int maxiNumRows, string cardType, string cardStatus, string field, string keyword, string agentName)
        {
            if (field == "2")
            {
                keyword = DataConverter.CLng(keyword).ToString(CultureInfo.CurrentCulture);
            }
            return dal.GetCardList(startRowIndexId, maxiNumRows, string.IsNullOrEmpty(cardType) ? -1 : DataConverter.CLng(cardType), DataConverter.CLng(cardStatus), DataConverter.CLng(field), DataSecurity.FilterBadChar(keyword), DataSecurity.FilterBadChar(agentName));
        }

        public static string GetCardStatus(CardInfo info)
        {
            if (!string.IsNullOrEmpty(info.UserName))
            {
                return "<span style='color:gray'>已使用</span>";
            }
            if (info.OrderItemId > 0)
            {
                return "已售出";
            }
            if (info.EndDate < DateTime.Today)
            {
                return "<span style='color:red'>已失效</span>";
            }
            if (info.ProductId > 0)
            {
                return "<span style='color:green'>未售出</span>";
            }
            return "<span style='color:green'>未使用</span>";
        }

        public static string GetCardType(int cardtype)
        {
            if (cardtype != 0)
            {
                return "<span style='color:#00f'>其他公司卡</span>";
            }
            return "本站充值卡";
        }

        public static string GetProductName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                return productName;
            }
            return "<span style='color:#00f'>不通过商店销售</span>";
        }

        public static int GetTotalofCards(string cardType, string cardStatus, string field, string keyword, string agentName)
        {
            if (field == "2")
            {
                keyword = DataConverter.CLng(keyword).ToString(CultureInfo.CurrentCulture);
            }
            return dal.GetTotalofCards(DataConverter.CLng(cardType), DataConverter.CLng(cardStatus), DataConverter.CLng(field), DataSecurity.FilterBadChar(keyword), DataSecurity.FilterBadChar(agentName));
        }

        public static IList<CardInfo> GetUnsoldCard(string table, int productId, int amount)
        {
            return dal.GetUnsoldCard(table, productId, amount);
        }

        public static string GetValidUnitType(int type)
        {
            if (type < ValidUnitType.Length)
            {
                return ValidUnitType[type];
            }
            return "";
        }

        public static bool Update(CardInfo info)
        {
            return dal.Update(info);
        }

        public string Message
        {
            get
            {
                return this.m_Message;
            }
        }
    }
}

