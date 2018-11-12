namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SmsMessageToContacter : AdminPage
    {
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            IList<ContacterInfo> allMobileContacters = new List<ContacterInfo>();
            if (this.RadIncept1.Checked)
            {
                allMobileContacters = Contacter.GetAllMobileContacters();
            }
            if (this.RadIncept2.Checked)
            {
                allMobileContacters = Contacter.GetMobileContacterByRegion(this.Region.Country, this.Region.Province, this.Region.City);
            }
            if (this.RadIncept3.Checked)
            {
                allMobileContacters = Contacter.GetMobileContacterByUserName(this.TxtContact.Text.Trim());
            }
            StringBuilder sb = new StringBuilder();
            foreach (ContacterInfo info in allMobileContacters)
            {
                string mobile = info.Mobile;
                if (string.IsNullOrEmpty(mobile))
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

        protected void BtnSubmitServer_Click(object sender, EventArgs e)
        {
            try
            {
                AdminPage.WriteSuccessMsg(SmsMessage.SendMessage(this.TxtSendNum.Text, this.TxtContent.Text, this.SendTiming.Value, this.SendTime.Value, PEContext.Current.Admin.AdminName), "SmsMessageToContacter.aspx");
            }
            catch (CustomException exception)
            {
                AdminPage.WriteErrMsg(exception.Message);
            }
        }

        private void InitControl()
        {
            if (!base.IsPostBack)
            {
                this.TxtMessage.Text = "{$2}，您好！这是一条测试短信。----动易网络（请勿回复此短信）";
                this.Dpk.Text = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd HH:mm:ss");
                this.TxtMessage.Attributes.Add("onpropertychange", "checkLength()");
                this.TxtMessage.Attributes.Add("oninput", "checkLength()");
                this.Panel2.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }
    }
}

