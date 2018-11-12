namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class Message
    {
        private static readonly IMessage dal = DataAccess.CreateMessage();

        private Message()
        {
        }

        public static bool Add(MessageInfo messageInfo)
        {
            return dal.Add(messageInfo);
        }

        public static bool Clear(MessageManageType manageType, string userName)
        {
            return Clear(manageType, userName, null);
        }

        public static bool Clear(MessageManageType manageType, string userName, string messageIdList)
        {
            if ((messageIdList != null) && !DataValidator.IsValidId(messageIdList))
            {
                return false;
            }
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            return dal.Clear(manageType, DataSecurity.FilterBadChar(userName), messageIdList);
        }

        public static int Count()
        {
            return dal.Count();
        }

        public static int Count(MessageSearchField searchField, string keyword)
        {
            return dal.Count();
        }

        public static int Count(MessageSearchField searchField, string keyword, string userName, MessageManageType manageType)
        {
            return dal.Count();
        }

        public static bool Delete(MessageDelType deleteType, string deleteValue)
        {
            switch (deleteType)
            {
                case MessageDelType.Id:
                    if (DataValidator.IsValidId(deleteValue))
                    {
                        break;
                    }
                    return false;

                case MessageDelType.Sender:
                {
                    string[] strArray = deleteValue.Split(new char[] { ',' });
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        StringHelper.AppendString(sb, "'" + DataSecurity.FilterBadChar(strArray[i]) + "'");
                    }
                    deleteValue = sb.ToString();
                    break;
                }
                case MessageDelType.Date:
                    deleteValue = DataConverter.CLng(deleteValue).ToString(CultureInfo.CurrentCulture);
                    break;
            }
            return dal.Delete(deleteType, deleteValue);
        }

        public static MessageInfo GetMessageById(int messageId)
        {
            return dal.GetMessageById(messageId);
        }

        public static IList<MessageInfo> GetMessageList(MessageSearchField searchField, string keyword, int maxNumberRows, int startRowIndexId)
        {
            return dal.GetMessageList(startRowIndexId, maxNumberRows, searchField, DataSecurity.FilterBadChar(keyword));
        }

        public static IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, int read)
        {
            return dal.GetMessageList(startRowIndexId, maxNumberRows, userName, read);
        }

        public static IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, MessageManageType manageType, MessageSearchField searchField, string keyword)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new List<MessageInfo>();
            }
            return dal.GetMessageList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(userName), manageType, searchField, DataSecurity.FilterBadChar(keyword));
        }

        public static MessageInfo GetMessageOfEdit(int messageId, string userName)
        {
            return dal.GetMessageOfEdit(messageId, userName);
        }

        public static MessageInfo GetMessageOfForward(int messageId, string userName)
        {
            return dal.GetMessageOfForward(messageId, userName);
        }

        public static MessageInfo GetMessageOfReply(int messageId, string userName)
        {
            return dal.GetMessageOfReply(messageId, userName);
        }

        public static int GetUnreadMessageFirstId(string userName)
        {
            return dal.GetUnreadMessageFirstId(userName);
        }

        public static IList<string> GetUserNameList(string groupId)
        {
            return dal.GetUserNameList(groupId);
        }

        public static int UnreadMessageCount(string userName)
        {
            return dal.UnreadMessageCount(userName);
        }

        public static bool Update(MessageInfo messageInfo)
        {
            return dal.Update(messageInfo);
        }

        public static bool UpdateState(int messageId)
        {
            return dal.UpdateState(messageId);
        }
    }
}

