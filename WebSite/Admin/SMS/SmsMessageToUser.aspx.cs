namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class SmsMessageToUser : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                IList<ContacterInfo> allMobileContacters = new List<ContacterInfo>();
                if (this.RadIncept1.Checked)
                {
                    allMobileContacters = Contacter.GetAllMobileContacters();
                }
                if (this.RadIncept2.Checked)
                {
                    allMobileContacters = Contacter.GetMobileContacterByGroupId(this.GetUserGroupId(this.ChklUserGroupList));
                }
                if (this.RadIncept3.Checked)
                {
                    allMobileContacters = Contacter.GetMobileContacterByUserName(this.TxtUserName.Text);
                }
                if (this.RadIncept4.Checked)
                {
                    allMobileContacters = Contacter.GetMobileContacterByUserId(DataConverter.CLng(this.TxtStartUser.Text), DataConverter.CLng(this.TxtEndUser.Text));
                }
                StringBuilder sb = new StringBuilder();
                foreach (ContacterInfo info in allMobileContacters)
                {
                    string mobile = info.Mobile;
                    if (string.IsNullOrEmpty(mobile) || (mobile.Length != 11))
                    {
                        mobile = info.Phs;
                    }
                    if (!string.IsNullOrEmpty(mobile))
                    {
                        string append = mobile + "," + info.TrueName + "," + info.UserName;
                        StringHelper.AppendString(sb, append, "\r\n");
                    }
                }
                this.TxtSendNum.Text = sb.ToString();
                this.TxtContent.Text = this.TxtMessage.Text;
                this.LabRecieverCount.Text = allMobileContacters.Count.ToString();
                this.LabMessageCount.Text = (allMobileContacters.Count * ((this.TxtContent.Text.Length / 70) + 1)).ToString();
                if (this.RadSendType.Checked)
                {
                    this.SendTiming.Value = "0";
                }
                else
                {
                    this.SendTiming.Value = "1";
                }
                this.SendTime.Value = this.Dpk.Text;
                this.Panel2.Visible = true;
                this.Panel1.Visible = false;
            }
        }

        protected void BtnSubmitServer_Click(object sender, EventArgs e)
        {
            try
            {
                string successMessage = SmsMessage.SendMessage(this.TxtSendNum.Text, this.TxtContent.Text, this.SendTiming.Value, this.SendTime.Value, PEContext.Current.Admin.AdminName);
                if (!string.IsNullOrEmpty(BasePage.RequestString("CartID")))
                {
                    ShoppingCart.UpdateInformState(BasePage.RequestString("CartID"), 1);
                    AdminPage.WriteSuccessMsg(successMessage, "../Shop/ShoppingCartManage.aspx");
                }
                AdminPage.WriteSuccessMsg(successMessage, "SmsMessageToUser.aspx");
            }
            catch (CustomException exception)
            {
                AdminPage.WriteErrMsg(exception.Message);
            }
        }

        private string GetUserGroupId(ListControl chklUserGroupList)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < chklUserGroupList.Items.Count; i++)
            {
                if (chklUserGroupList.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, chklUserGroupList.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        private void InitControl()
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("CartID");
                if (!string.IsNullOrEmpty(str))
                {
                    this.TxtMessage.Text = ShoppingCart.GetInformMessage(SiteConfig.SmsConfig.CartInformMessage, str);
                }
                else
                {
                    this.TxtMessage.Text = "{$2}，您好！这是一条测试短信。----动易网络（请勿回复此短信）";
                }
                this.TxtMessage.Attributes.Add("onpropertychange", "checkLength()");
                this.TxtMessage.Attributes.Add("oninput", "checkLength()");
                this.TxtMessageNumber.Text = this.TxtMessage.Text.Trim().Length.ToString();
                this.Dpk.Text = DateTime.Now.AddDays(1.0).ToString();
                IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
                this.ChklUserGroupList.Items.Clear();
                this.ChklUserGroupList.DataSource = userGroupList;
                this.ChklUserGroupList.DataTextField = "GroupName";
                this.ChklUserGroupList.DataValueField = "GroupId";
                this.ChklUserGroupList.DataBind();
                this.Panel2.Visible = false;
                if (!string.IsNullOrEmpty(BasePage.RequestString("UserName")))
                {
                    this.RadIncept3.Checked = true;
                    this.TxtUserName.Text = BasePage.RequestString("UserName");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }
    }
}

