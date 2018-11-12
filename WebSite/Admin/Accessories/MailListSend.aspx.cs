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
    using System.Net.Mail;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class MailListSend : AdminPage
    {

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            IList<string[]> userData = this.GetUserData();
            if (userData != null)
            {
                if (userData.Count <= 0)
                {
                    AdminPage.WriteErrMsg("没有找到会员的可发送的Email地址！");
                }
                MailInfo mailInfo = this.GetMailInfo();
                foreach (string[] strArray in userData)
                {
                    if (mailInfo.MailToAddressList != null)
                    {
                        mailInfo.MailToAddressList.Clear();
                    }
                    if (DataValidator.IsEmail(strArray[1]))
                    {
                        IList<MailAddress> list2 = new List<MailAddress>();
                        list2.Add(new MailAddress(strArray[1], strArray[0]));
                        mailInfo.MailToAddressList = list2;
                        if (SendMail.Send(mailInfo) == MailState.Ok)
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                        continue;
                    }
                    num2++;
                }
                AdminPage.WriteSuccessMsg("成功发送邮件：" + num.ToString() + "封，未发送邮件：" + num2.ToString() + "封（邮件地址错误）。");
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
            this.TxtSenderName.Text = SiteConfig.SiteInfo.Webmaster;
            this.TxtSenderEmail.Text = SiteConfig.SiteInfo.WebmasterEmail;
        }

        private MailInfo GetMailInfo()
        {
            MailInfo info = new MailInfo();
            info.Subject = this.TxtSubject.Text;
            info.MailBody = this.EditorContent.Value;
            string selectedValue = this.RadlPriority.SelectedValue;
            if (selectedValue != null)
            {
                if (!(selectedValue == "0"))
                {
                    if (selectedValue == "1")
                    {
                        info.Priority = MailPriority.Low;
                    }
                    else if (selectedValue == "2")
                    {
                        info.Priority = MailPriority.High;
                    }
                }
                else
                {
                    info.Priority = MailPriority.Normal;
                }
            }
            if (!string.IsNullOrEmpty(this.TxtSenderName.Text))
            {
                info.FromName = this.TxtSenderName.Text;
            }
            if (!string.IsNullOrEmpty(this.TxtSenderEmail.Text))
            {
                info.ReplyTo = new MailAddress(this.TxtSenderEmail.Text);
            }
            return info;
        }

        private IList<string[]> GetUserData()
        {
            int num;
            string text = string.Empty;
            if (this.RadUserType0.Checked)
            {
                num = 0;
            }
            else if (this.RadUserType1.Checked)
            {
                num = 1;
                StringBuilder builder = new StringBuilder("");
                for (int i = 0; i < this.ChklUserGroupList.Items.Count; i++)
                {
                    if (this.ChklUserGroupList.Items[i].Selected)
                    {
                        builder.Append(this.ChklUserGroupList.Items[i].Value + ",");
                    }
                }
                text = builder.ToString().TrimEnd(new char[] { ',' });
                if (string.IsNullOrEmpty(text))
                {
                    AdminPage.WriteErrMsg("请指定会员组！");
                    this.ChklUserGroupList.Focus();
                    return null;
                }
            }
            else if (this.RadUserType2.Checked)
            {
                num = 2;
                text = this.TxtUserName.Text;
                if (string.IsNullOrEmpty(text))
                {
                    AdminPage.WriteErrMsg("请输入用户名！");
                    this.TxtUserName.Focus();
                    return null;
                }
            }
            else
            {
                num = 3;
                if (string.IsNullOrEmpty(this.TxtEmails.Text))
                {
                    AdminPage.WriteErrMsg("请输入会员Email！");
                    this.TxtEmails.Focus();
                    return null;
                }
                IList<string[]> list = new List<string[]>();
                foreach (string str2 in this.TxtEmails.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    list.Add(new string[] { "", str2 });
                }
                return list;
            }
            return Users.GetUserNameAndEmailList(num, text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ChklUserGroupListBind();
            }
        }
    }
}

