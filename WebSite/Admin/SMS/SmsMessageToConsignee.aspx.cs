namespace EasyOne.WebSite.Admin.Sms
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Shop;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class SmsMessageToConsignee : AdminPage
    {
        protected Button BtnSubmit;
        protected Button BtnSubmitServer;
        protected DatePicker Dpk;
        protected Label LabMessageCount;
        protected Label LabRecieverCount;
        protected HiddenField MD5String;
        protected HtmlGenericControl order;
        protected Panel Panel1;
        protected Panel Panel2;
        protected RadioButton RadIncept1;
        protected RadioButton RadIncept2;
        protected RadioButton RadIncept3;
        protected RadioButtonList RadlDeliverStatus;
        protected RadioButtonList RadlOrderStatus;
        protected RadioButtonList RadlOrderType;
        protected RadioButtonList RadlPayStatus;
        protected RadioButton RadSendType;
        protected RadioButton RadTimeSen;
        protected EasyOne.Controls.RequiredFieldValidator RequiredFieldValidator1;
        protected HiddenField Reserve;
        protected HiddenField SendTime;
        protected HiddenField SendTiming;
        protected ExtendedSiteMapPath SmpNavigator;
        protected TextBox TxtAddress;
        protected TextBox TxtAgentName;
        protected TextBox TxtBeginDate;
        protected TextBox TxtBeginId;
        protected TextBox TxtClientName;
        protected TextBox TxtContacterName;
        protected TextBox TxtContent;
        protected TextBox TxtEndDate;
        protected TextBox TxtEndId;
        protected TextBox TxtMaxMoney;
        protected TextBox TxtMessage;
        protected TextBox TxtMessageNumber;
        protected TextBox TxtMinMoney;
        protected TextBox TxtMobile;
        protected TextBox TxtOrderId;
        protected TextBox TxtOrderNum;
        protected TextBox TxtPhone;
        protected TextBox TxtProductName;
        protected TextBox TxtRemark;
        protected TextBox TxtSendNum;
        protected TextBox TxtUserName;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            IList<OrderInfo> list = new List<OrderInfo>();
            if (this.RadIncept1.Checked)
            {
                list = Order.GetList(0, 0, "", "", "", "");
            }
            if (this.RadIncept2.Checked)
            {
                string keywords = this.GetKeywords();
                list = Order.GetList(0, 0, "20", "0", keywords, null);
            }
            if (this.RadIncept3.Checked)
            {
                foreach (string str2 in this.TxtOrderId.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    OrderInfo orderById = Order.GetOrderById(DataConverter.CLng(str2));
                    list.Add(orderById);
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (OrderInfo info2 in list)
            {
                string mobile = info2.Mobile;
                if (string.IsNullOrEmpty(mobile))
                {
                    mobile = info2.Phone;
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    string append = mobile + "," + info2.ContacterName + "," + info2.OrderNum;
                    StringHelper.AppendString(sb, append, "\r\n");
                }
            }
            this.TxtSendNum.Text = sb.ToString();
            this.TxtContent.Text = this.TxtMessage.Text;
            this.LabRecieverCount.Text = list.Count.ToString();
            this.LabMessageCount.Text = (list.Count * ((this.TxtContent.Text.Length / 70) + 1)).ToString();
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
                AdminPage.WriteSuccessMsg(SmsMessage.SendMessage(this.TxtSendNum.Text, this.TxtContent.Text, this.SendTiming.Value, this.SendTime.Value, PEContext.Current.Admin.AdminName), "SmsMessageToUser.aspx");
            }
            catch (CustomException exception)
            {
                AdminPage.WriteErrMsg(exception.Message);
            }
        }

        private string GetKeywords()
        {
            string str = this.TxtBeginId.Text.Trim().Replace("|", "");
            string str2 = this.TxtEndId.Text.Trim().Replace("|", "");
            string str3 = this.TxtBeginDate.Text.Trim().Replace("|", "");
            string str4 = this.TxtEndDate.Text.Trim().Replace("|", "");
            string str5 = this.TxtMinMoney.Text.Trim().Replace("|", "");
            string str6 = this.TxtMaxMoney.Text.Trim().Replace("|", "");
            string selectedValue = this.RadlOrderStatus.SelectedValue;
            string str8 = this.RadlPayStatus.SelectedValue;
            string str9 = this.RadlDeliverStatus.SelectedValue;
            string str10 = this.TxtOrderNum.Text.Trim().Replace("|", "");
            string str11 = this.TxtClientName.Text.Trim().Replace("|", "");
            string str12 = this.TxtUserName.Text.Trim().Replace("|", "");
            string str13 = this.TxtAgentName.Text.Trim().Replace("|", "");
            string str14 = this.TxtContacterName.Text.Trim().Replace("|", "");
            string str15 = this.TxtAddress.Text.Trim().Replace("|", "");
            string str16 = this.TxtPhone.Text.Trim().Replace("|", "");
            string str17 = this.TxtMobile.Text.Trim().Replace("|", "");
            string str18 = this.TxtRemark.Text.Trim().Replace("|", "");
            string str19 = this.TxtProductName.Text.Trim().Replace("|", "");
            string str20 = this.RadlOrderType.SelectedValue;
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}", new object[] { 
                str, str2, str3, str4, str5, str6, selectedValue, str8, str9, str10, str11, str12, str13, str14, str15, str16, 
                str17, str18, str19, str20
             });
        }

        private void InitControl()
        {
            if (!base.IsPostBack)
            {
                this.TxtMessage.Text = "{$2}，您好！您的订单已经\x00d7\x00d7。----动易网络（请勿回复此短信）";
                this.Dpk.Text = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd HH:mm:ss");
                this.TxtMessage.Attributes.Add("onpropertychange", "checkLength()");
                this.TxtMessage.Attributes.Add("oninput", "checkLength()");
                this.RadIncept2.Attributes.Add("onclick", "ShowOrder(true)");
                this.RadIncept1.Attributes.Add("onclick", "ShowOrder(false)");
                this.RadIncept3.Attributes.Add("onclick", "ShowOrder(false)");
                this.Panel2.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }
    }
}

