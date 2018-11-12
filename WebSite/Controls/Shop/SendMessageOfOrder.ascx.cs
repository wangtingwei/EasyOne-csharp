namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Common;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class SendMessageOfOrder : BaseUserControl
    {
        protected string m_Description;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string[] strArray = ((base.Request.Cookies[base.Request.FilePath + this.ClientID] == null) ? "true|false|false|false|false" : base.Request.Cookies[base.Request.FilePath + this.ClientID].Value).Split(new char[] { '|' });
                if (strArray.Length == 5)
                {
                    this.ChkSendMessageToUser.Checked = DataConverter.CBoolean(strArray[0]);
                    this.ChkSendEmailToUser.Checked = DataConverter.CBoolean(strArray[1]);
                    this.ChkSendEmailToContacter.Checked = DataConverter.CBoolean(strArray[2]);
                    this.ChkSendSMSToUser.Checked = DataConverter.CBoolean(strArray[3]);
                    this.ChkSendSMSToContacter.Checked = DataConverter.CBoolean(strArray[4]);
                }
            }
            else
            {
                string str2 = string.Concat(new object[] { this.ChkSendMessageToUser.Checked, "|", this.ChkSendEmailToUser.Checked, "|", this.ChkSendEmailToContacter.Checked, "|", this.ChkSendSMSToUser.Checked, "|", this.ChkSendSMSToContacter.Checked });
                HttpCookie cookie = new HttpCookie(base.Request.FilePath + this.ClientID, str2);
                cookie.Expires = DateTime.MaxValue;
                base.Response.Cookies.Add(cookie);
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
                this.ChkSendMessageToUser.Text = "同时使用站内短信通知会员" + this.m_Description;
                this.ChkSendEmailToUser.Text = "同时使用Email通知会员" + this.m_Description;
                this.ChkSendSMSToUser.Text = "同时发送手机短信通知会员" + this.m_Description;
                this.ChkSendEmailToContacter.Text = "同时使用Email通知收货人" + this.m_Description;
                this.ChkSendSMSToContacter.Text = "同时发送手机短信通知收货人" + this.m_Description;
            }
        }

        public bool NeedSendMessage
        {
            get
            {
                bool flag = false;
                if ((!this.ChkSendMessageToUser.Checked && !this.ChkSendEmailToUser.Checked) && ((!this.ChkSendEmailToContacter.Checked && !this.ChkSendSMSToContacter.Checked) && !this.ChkSendSMSToUser.Checked))
                {
                    return flag;
                }
                return true;
            }
        }

        public bool SendEmailToContacter
        {
            get
            {
                return this.ChkSendEmailToContacter.Checked;
            }
            set
            {
                this.ChkSendEmailToContacter.Checked = value;
            }
        }

        public bool SendEmailToContacterVisible
        {
            get
            {
                return this.ChkSendEmailToContacter.Visible;
            }
            set
            {
                this.ChkSendEmailToContacter.Visible = value;
            }
        }

        public bool SendEmailToUser
        {
            get
            {
                return this.ChkSendEmailToUser.Checked;
            }
            set
            {
                this.ChkSendEmailToUser.Checked = value;
            }
        }

        public bool SendMessageToUser
        {
            get
            {
                return this.ChkSendMessageToUser.Checked;
            }
            set
            {
                this.ChkSendMessageToUser.Checked = value;
            }
        }

        public bool SendSmsToContacter
        {
            get
            {
                return this.ChkSendSMSToContacter.Checked;
            }
            set
            {
                this.ChkSendSMSToContacter.Checked = value;
            }
        }

        public bool SendSmsToContacterVisible
        {
            get
            {
                return this.ChkSendSMSToContacter.Visible;
            }
            set
            {
                this.ChkSendSMSToContacter.Visible = value;
            }
        }

        public bool SendSmsToUser
        {
            get
            {
                return this.ChkSendSMSToUser.Checked;
            }
            set
            {
                this.ChkSendSMSToUser.Checked = value;
            }
        }
    }
}

