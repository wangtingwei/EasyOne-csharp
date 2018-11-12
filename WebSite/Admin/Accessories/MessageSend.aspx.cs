namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Controls.Editor;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class MessageSend : AdminPage
    {

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.TxtInceptUser.Text = string.Empty;
            this.TxtSender.Text = string.Empty;
            this.TxtTitle.Text = string.Empty;
            this.TxtUserName.Text = string.Empty;
            this.EditorContent.Value = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.Title = StringHelper.RemoveXss(this.TxtTitle.Text.Trim());
            messageInfo.Content = StringHelper.RemoveXss(this.EditorContent.Value);
            messageInfo.Sender = PEContext.Current.Admin.AdminName;
            messageInfo.SendTime = DateTime.Now;
            messageInfo.Incept = this.GetUsersName().Split(new char[] { '|' })[0];
            messageInfo.IsSend = 0;
            messageInfo.IsRead = 0;
            bool flag = false;
            if (BasePage.RequestString("Action") == "Edit")
            {
                messageInfo.MessageId = DataConverter.CLng(this.HdnMessageID.Value);
                flag = Message.Update(messageInfo);
            }
            else
            {
                flag = Message.Add(messageInfo);
            }
            if (flag)
            {
                if (string.IsNullOrEmpty(this.GetUsersName().Split(new char[] { '|' })[1]))
                {
                    AdminPage.WriteSuccessMsg("<li>保存短信息成功。</li>", "MessageManage.aspx");
                }
                else
                {
                    AdminPage.WriteSuccessMsg("<li>保存短信息成功。</li><br/>但也有保存短消息失败的会员，如：<br/><li>没有向：" + this.GetUsersName().Split(new char[] { '|' })[1] + " 保存短消息，因为不存在该会员！</li><br/>", "MessageManage.aspx");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("保存短信息失败！");
            }
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.Title = StringHelper.RemoveXss(this.TxtTitle.Text.Trim());
            messageInfo.Content = StringHelper.RemoveXss(this.EditorContent.Value);
            messageInfo.Sender = this.TxtSender.Text.Trim();
            messageInfo.SendTime = DateTime.Now;
            messageInfo.IsSend = 1;
            messageInfo.Incept = this.GetUsersName().Split(new char[] { '|' })[0].Trim();
            if (!string.IsNullOrEmpty(messageInfo.Incept))
            {
                if (BasePage.RequestString("Action") == "Edit")
                {
                    if (Message.Delete(MessageDelType.Id, this.HdnMessageID.Value))
                    {
                        if (Message.Add(messageInfo))
                        {
                            if (string.IsNullOrEmpty(this.GetUsersName().Split(new char[] { '|' })[1]))
                            {
                                AdminPage.WriteSuccessMsg("发送短消息成功。", "MessageManage.aspx");
                            }
                            else
                            {
                                AdminPage.WriteSuccessMsg("<li>发送短消息成功。</li><br/>但也有发送短消息失败的会员，如：<br/><li>没有向：" + this.GetUsersName().Split(new char[] { '|' })[1] + " 发送短消息，因为不存在该会员！</li><br/>", "MessageManage.aspx");
                            }
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("发送短消息失败！");
                        }
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("发送短消息失败！");
                    }
                }
                else if (Message.Add(messageInfo))
                {
                    if (string.IsNullOrEmpty(this.GetUsersName().Split(new char[] { '|' })[1]))
                    {
                        AdminPage.WriteSuccessMsg("发送短消息成功。", "MessageManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteSuccessMsg("<li>发送短消息成功。</li><br/>但也有发送短消息失败的会员，如：<br/><li>没有向：" + this.GetUsersName().Split(new char[] { '|' })[1] + " 发送短消息，因为不存在该会员！</li><br/>", "MessageManage.aspx");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("发送短消息失败！");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("发送短消息失败！因为收件人不存在。");
            }
        }

        private void ChklUserGroupListBind()
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            if (userGroupList.Count > 0)
            {
                this.ChklUserGroupList.Items.Clear();
                this.ChklUserGroupList.DataSource = userGroupList;
                this.ChklUserGroupList.DataTextField = "GroupName";
                this.ChklUserGroupList.DataValueField = "GroupId";
                this.ChklUserGroupList.DataBind();
            }
        }

        private string GetUsersName()
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (this.TblAddMessage.Visible)
            {
                if (this.RadIncept1.Checked)
                {
                    foreach (string str3 in Message.GetUserNameList(""))
                    {
                        str = str + str3 + ",";
                    }
                }
                if (this.RadIncept2.Checked)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < this.ChklUserGroupList.Items.Count; i++)
                    {
                        if (this.ChklUserGroupList.Items[i].Selected)
                        {
                            builder.Append(this.ChklUserGroupList.Items[i].Value + ",");
                        }
                    }
                    foreach (string str4 in Message.GetUserNameList(builder.ToString().TrimEnd(new char[] { ',' })))
                    {
                        str = str + str4 + ",";
                    }
                }
                if (this.RadIncept3.Checked)
                {
                    string[] strArray = this.TxtUserName.Text.Split(new char[] { ',' });
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (Users.Exists(strArray[j]))
                        {
                            str = str + strArray[j] + ",";
                        }
                        else
                        {
                            str2 = str2 + strArray[j] + ",";
                        }
                    }
                }
            }
            else
            {
                string[] strArray2 = this.TxtInceptUser.Text.Split(new char[] { ',' });
                for (int k = 0; k < strArray2.Length; k++)
                {
                    if (Users.Exists(strArray2[k]))
                    {
                        str = str + strArray2[k] + ",";
                    }
                    else
                    {
                        str2 = str2 + strArray2[k] + ",";
                    }
                }
            }
            return (str.TrimEnd(new char[] { ',' }) + "|" + str2.TrimEnd(new char[] { ',' }));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string adminName = PEContext.Current.Admin.AdminName;
                string str2 = BasePage.RequestString("Action");
                int messageId = BasePage.RequestInt32("MessageID");
                this.TxtSender.Text = adminName;
                if (string.IsNullOrEmpty(str2))
                {
                    if (!string.IsNullOrEmpty(BasePage.RequestString("UserName")))
                    {
                        this.RadIncept3.Checked = true;
                        this.TxtUserName.Text = BasePage.RequestString("UserName");
                    }
                    this.ChklUserGroupListBind();
                }
                else
                {
                    this.TblEditMessage.Visible = true;
                    this.TblAddMessage.Visible = false;
                    MessageInfo messageById = Message.GetMessageById(messageId);
                    if ((string.Compare(adminName, messageById.Sender, StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(adminName, messageById.Incept, StringComparison.OrdinalIgnoreCase) != 0))
                    {
                        BasePage.ResponseRedirect("MessageRead.aspx?MessageID=" + messageId.ToString());
                    }
                    else
                    {
                        string str3 = str2;
                        if (str3 != null)
                        {
                            if (!(str3 == "Edit"))
                            {
                                if (!(str3 == "Reply"))
                                {
                                    if (str3 == "Forward")
                                    {
                                        this.ShowModifyInfo(messageById, str2);
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                if (string.Compare(adminName, messageById.Sender, StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    this.ShowModifyInfo(messageById, str2);
                                }
                                return;
                            }
                            this.ShowModifyInfo(messageById, str2);
                        }
                    }
                }
            }
        }

        private void ShowModifyInfo(MessageInfo info, string action)
        {
            if (info == null)
            {
                AdminPage.WriteErrMsg("参数错误！");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                string sender = info.Sender;
                string str2 = info.SendTime.ToLongTimeString();
                string str3 = action;
                if (str3 != null)
                {
                    if (!(str3 == "Edit"))
                    {
                        if (!(str3 == "Reply"))
                        {
                            if (str3 == "Forward")
                            {
                                this.TxtTitle.Text = "Fw: " + info.Title;
                                builder.Append("============== 下面是转发信息 ==============<br />");
                                builder.Append("原发件人：" + sender + "<br />");
                                builder.Append("原发件内容：<br />");
                                builder.Append(info.Content);
                                builder.Append("<br />================================================<br />");
                                this.EditorContent.Value = builder.ToString();
                            }
                            return;
                        }
                    }
                    else
                    {
                        this.TxtInceptUser.Text = info.Incept;
                        this.TxtTitle.Text = info.Title;
                        this.HdnMessageID.Value = info.MessageId.ToString();
                        this.EditorContent.Value = info.Content;
                        return;
                    }
                    this.TxtInceptUser.Text = info.Sender;
                    this.TxtTitle.Text = "Re: " + info.Title;
                    builder.Append("======在 ");
                    builder.Append(str2);
                    builder.Append(" 您来信中写道：======<br />");
                    builder.Append(info.Content);
                    builder.Append("<br />================================================<br />");
                    this.EditorContent.Value = builder.ToString();
                }
            }
        }
    }
}

