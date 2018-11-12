namespace EasyOne.WebSite.User
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Controls.Editor;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Message : DynamicPage
    {
        protected const int MaxContentLength = 0x3e8;

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.TxtInceptUser.Text = string.Empty;
            this.TxtTitle.Text = string.Empty;
            this.EditorContent.Value = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            this.ExecuteMessageSave();
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (BasePage.RequestString("Action") == "Edit")
            {
                if (EasyOne.Accessories.Message.Delete(MessageDelType.Id, this.HdnMessageID.Value))
                {
                    this.ExecuteSendMessage();
                }
                else
                {
                    DynamicPage.WriteErrMsg("发送短消息失败！");
                }
            }
            else
            {
                this.ExecuteSendMessage();
            }
        }

        private void ExecuteMessageSave()
        {
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.Title = StringHelper.RemoveXss(this.TxtTitle.Text.Trim());
            messageInfo.Content = StringHelper.RemoveXss(this.EditorContent.Value);
            messageInfo.Sender = PEContext.Current.User.UserName;
            messageInfo.SendTime = DateTime.Now;
            messageInfo.Incept = this.TxtInceptUser.Text;
            messageInfo.IsSend = 0;
            messageInfo.IsRead = 0;
            bool flag = false;
            if (BasePage.RequestString("Action") == "Edit")
            {
                messageInfo.MessageId = DataConverter.CLng(this.HdnMessageID.Value);
                flag = EasyOne.Accessories.Message.Update(messageInfo);
            }
            else
            {
                flag = EasyOne.Accessories.Message.Add(messageInfo);
            }
            if (flag)
            {
                DynamicPage.WriteSuccessMsg("<li>恭喜您，保存短信息成功。</li><br /><li>短消息保存在您的草稿箱中。</li>", "MessageManager.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg("保存失败！");
            }
        }

        private void ExecuteSendMessage()
        {
            StringBuilder builder = new StringBuilder();
            MessageInfo messageInfo = new MessageInfo();
            messageInfo.Title = StringHelper.RemoveXss(this.TxtTitle.Text.Trim());
            messageInfo.Content = StringHelper.RemoveXss(this.EditorContent.Value);
            messageInfo.Sender = PEContext.Current.User.UserName;
            messageInfo.SendTime = DateTime.Now;
            messageInfo.IsSend = 1;
            messageInfo.IsRead = 0;
            string[] strArray = this.TxtInceptUser.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length >= DataConverter.CLng(this.LblMaxSendNum.Text))
            {
                DynamicPage.WriteErrMsg("最多只能发送给 " + this.LblMaxSendNum.Text + " 个用户，您的名单 " + this.LblMaxSendNum.Text + " 位以后的请重新发送！");
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            StringBuilder builder4 = new StringBuilder();
            StringBuilder builder5 = new StringBuilder();
            foreach (string str in strArray)
            {
                if (Users.Exists(str))
                {
                    if (UserFriend.CheckBlackFriend(str, PEContext.Current.User.UserName))
                    {
                        StringHelper.AppendString(builder5, str);
                    }
                    else
                    {
                        messageInfo.Incept = str;
                        if (EasyOne.Accessories.Message.Add(messageInfo))
                        {
                            StringHelper.AppendString(sb, str);
                        }
                        else
                        {
                            StringHelper.AppendString(builder4, str);
                        }
                    }
                }
                else
                {
                    StringHelper.AppendString(builder3, str);
                }
            }
            if (builder3.Length != 0)
            {
                builder.Append("<li>没有向：" + builder3.ToString() + " 发送短消息，因为不存在该会员！</li><br/>");
            }
            if (builder4.Length != 0)
            {
                builder.Append("<li>没有向会员：" + builder4.ToString() + " 发送短消息，可能会员名有误！</li><br/>");
            }
            if (builder5.Length != 0)
            {
                builder.Append("<li>没有向会员：" + builder5.ToString() + "发送短消息，因为被列入黑名单，或者被对方列入黑名单！</li><br/>");
            }
            if (builder.Length == 0)
            {
                if (sb.Length != 0)
                {
                    DynamicPage.WriteSuccessMsg("<li>向会员：" + sb.ToString() + " 发送短消息成功！</li>", "MessageManager.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("未找到任何会员！");
                }
            }
            else if (sb.Length != 0)
            {
                DynamicPage.WriteSuccessMsg("<li>向会员：" + sb.ToString() + " 发送短消息成功！</li><br/>但也有发送短消息失败的会员，如：<br/>" + builder.ToString(), "MessageManager.aspx");
            }
            else
            {
                DynamicPage.WriteErrMsg(builder.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = PEContext.Current.User.UserName;
            if (!base.IsPostBack)
            {
                UserPurviewInfo userPurview = PEContext.Current.User.UserInfo.UserPurview;
                if (userPurview.MaxSendToUsers <= 0)
                {
                    DynamicPage.WriteErrMsg("对不起，你没有发送短消息的权限！");
                }
                this.LblMaxSendNum.Text = userPurview.MaxSendToUsers.ToString();
                string str2 = BasePage.RequestString("Action");
                int messageId = BasePage.RequestInt32("MessageID");
                if (string.IsNullOrEmpty(BasePage.RequestString("inceptUser")))
                {
                    this.TxtInceptUser.Text = BasePage.RequestString("UserName");
                }
                else
                {
                    this.TxtInceptUser.Text = BasePage.RequestString("inceptUser");
                }
                if (!string.IsNullOrEmpty(str2) && (messageId == 0))
                {
                    DynamicPage.WriteErrMsg("指定的短消息ID错误！");
                }
                else
                {
                    string str3 = str2;
                    if (str3 != null)
                    {
                        if (!(str3 == "Edit"))
                        {
                            if (str3 == "Reply")
                            {
                                this.ShowModifyInfo(EasyOne.Accessories.Message.GetMessageOfReply(messageId, userName), str2);
                            }
                            else if (str3 == "Forward")
                            {
                                this.ShowModifyInfo(EasyOne.Accessories.Message.GetMessageOfForward(messageId, userName), str2);
                            }
                            else if (str3 == "Complain")
                            {
                                this.ShowComplain();
                            }
                            else if (str3 == "Commend")
                            {
                                this.ShowCommend();
                            }
                        }
                        else
                        {
                            this.ShowModifyInfo(EasyOne.Accessories.Message.GetMessageOfEdit(messageId, userName), str2);
                        }
                    }
                }
                this.DropFriends.DataSource = UserFriend.GetFriendNameList(PEContext.Current.User.UserName);
                this.DropFriends.DataBind();
                this.DropFriends.Items.Insert(0, new ListItem("请选择..."));
                this.DropFriends.Attributes.Add("onchange", "SelectFromFriend()");
            }
        }

        private void ShowCommend()
        {
            int generalId = BasePage.RequestInt32("MessageID");
            if (generalId > 0)
            {
                Product product = new Product(generalId);
                ProductInfo productInfoData = product.ProductInfoData;
                if (!productInfoData.IsNull)
                {
                    this.TxtTitle.Text = "推荐商品： " + productInfoData.ProductName + "，值得购买！ ";
                    this.EditorContent.Value = "商品名称：" + productInfoData.ProductName + "<br />";
                    this.EditorContent.Value = this.EditorContent.Value + "商品编号：" + productInfoData.ProductNum + "<br />";
                    string str = (base.Request.UrlReferrer == null) ? "" : base.Request.UrlReferrer.ToString();
                    if (string.IsNullOrEmpty(str))
                    {
                        this.EditorContent.Value = this.EditorContent.Value + "商品页面：";
                    }
                    else
                    {
                        string str2 = this.EditorContent.Value;
                        this.EditorContent.Value = str2 + "商品页面：<a href='" + str + "' target='_blank'>" + str + "</a>";
                    }
                }
            }
        }

        private void ShowComplain()
        {
            int generalId = BasePage.RequestInt32("MessageID");
            if (generalId > 0)
            {
                ProductInfo productInfo = new Product(generalId).GetProductInfo();
                if (!productInfo.IsNull)
                {
                    this.TxtTitle.Text = "举报商品 [" + productInfo.ProductName + "] 报价过高";
                    this.EditorContent.Value = "商品名称：" + productInfo.ProductName + "<br />";
                    this.EditorContent.Value = this.EditorContent.Value + "商品编号：" + productInfo.ProductNum + "<br />";
                    string str = this.EditorContent.Value;
                    this.EditorContent.Value = str + "商品页面：<a href='" + base.Request.UrlReferrer.ToString() + "' target='_blank'>" + base.Request.UrlReferrer.ToString() + "</a>";
                    IList<AdministratorInfo> list = Administrators.AdminList(0, 0, 0, 0);
                    this.TxtInceptUser.Text = list[list.Count - 1].AdminName;
                }
            }
        }

        private void ShowModifyInfo(MessageInfo info, string action)
        {
            if (info == null)
            {
                DynamicPage.WriteErrMsg("参数错误！");
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
                                base.Title = "转发短消息";
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
                        base.Title = "编辑短消息";
                        this.TxtInceptUser.Text = info.Incept;
                        this.TxtTitle.Text = info.Title;
                        this.HdnMessageID.Value = info.MessageId.ToString();
                        this.EditorContent.Value = info.Content;
                        return;
                    }
                    base.Title = "回复短消息";
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

