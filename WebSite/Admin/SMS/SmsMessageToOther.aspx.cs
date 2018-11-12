namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class SmsMessageToOther : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string[] strArray = this.TxtSendNum.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                this.TxtReciever.Text = this.TxtSendNum.Text;
                this.TxtContent.Text = this.TxtMessage.Text;
                this.LabRecieverCount.Text = strArray.Length.ToString();
                this.LabMessageCount.Text = (strArray.Length * ((this.TxtContent.Text.Length / 70) + 1)).ToString();
                if (this.RadSendType.Checked)
                {
                    this.SendTiming.Value = "0";
                }
                else
                {
                    this.SendTiming.Value = "1";
                }
                this.SendTime.Value = this.Dpk.Text;
                if (this.RadSendType.Checked)
                {
                    this.SendTiming.Value = "0";
                }
                else
                {
                    this.SendTiming.Value = "1";
                }
                this.Panel2.Visible = true;
                this.Panel1.Visible = false;
            }
        }

        protected void BtnSubmitServer_Click(object sender, EventArgs e)
        {
            try
            {
                AdminPage.WriteSuccessMsg(SmsMessage.SendMessage(this.TxtSendNum.Text, this.TxtContent.Text, this.SendTiming.Value, this.SendTime.Value, PEContext.Current.Admin.AdminName), "SmsMessageToOther.aspx");
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
                this.TxtMessage.Attributes.Add("onpropertychange", "checkLength()");
                this.TxtMessage.Attributes.Add("oninput", "checkLength()");
                this.TxtMessageNumber.Text = this.TxtMessage.Text.Trim().Length.ToString();
                this.Dpk.Text = DateTime.Now.AddDays(1.0).ToString();
                this.TxtSendNum.Text = "13800000000,张三,2380\r\n13900000000 李四 3278";
                this.Panel2.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }
    }
}

