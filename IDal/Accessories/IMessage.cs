namespace EasyOne.IDal.Accessories
{
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;

    public interface IMessage
    {
        bool Add(MessageInfo messageInfo);
        bool Clear(MessageManageType manageType, string userName, string messageIdList);
        int Count();
        bool Delete(MessageDelType deleteType, string deleteValue);
        MessageInfo GetMessageById(int messageId);
        IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, MessageSearchField searchType, string keyword);
        IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, int read);
        IList<MessageInfo> GetMessageList(int startRowIndexId, int maxNumberRows, string userName, MessageManageType manageType, MessageSearchField searchField, string keyword);
        MessageInfo GetMessageOfEdit(int messageId, string userName);
        MessageInfo GetMessageOfForward(int messageId, string userName);
        MessageInfo GetMessageOfReply(int messageId, string userName);
        int GetUnreadMessageFirstId(string userName);
        IList<string> GetUserNameList(string groupId);
        int UnreadMessageCount(string userName);
        bool Update(MessageInfo messageInfo);
        bool UpdateState(int messageId);
    }
}

