namespace EasyOne.WebSite.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Crm;
    using EasyOne.Model.Shop;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using EasyOne.WebSite.Controls.Shop;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Payment : DynamicPage
    {
        protected AddressPicker AddressPick;
        protected Button BtnGotoPreview;
        protected Button BtnReturn;
        protected CheckBox ChkNeedInvoice;
        protected DropDownList dropDeliverTime;
        protected DropDownList DropDeliverType;
        protected DropDownList DropPaymentType;
        protected HtmlForm form1;
        protected Label LblDeliverTypeIntro;
        protected Label LblPaymentTypeIntro;
        protected LinkButton LBtnAddress;
        private string m_CartId;
        private bool m_UserLogin;
        protected RadioButtonList RadlOutOfStockProject;
        protected RadioButtonList RadlPayPlatform;
        protected SelectAgent SelectAgent1;
        protected EasyOne.WebSite.Controls.ShoppingCartUI ShoppingCart1;
        protected ScriptManager SmgePayment;
        protected TextBox TxtContacterName;
        protected TextBox TxtEmail;
        protected TextBox TxtInvoiceContent;
        protected TextBox TxtMobile;
        protected TextBox TxtPhone;
        protected TextBox TxtRemark;
        protected TextBox TxtZipCode;
        protected UpdatePanel UpnlDeliverType;
        protected UpdatePanel UpnlPaymentType;
        protected UpdatePanel UpnlZipCode;
        protected EasyOne.Controls.RequiredFieldValidator ValfEmail;
        protected System.Web.UI.WebControls.RequiredFieldValidator ValfPaymentType;
        protected EasyOne.Controls.RequiredFieldValidator ValrContacterName;
        protected EasyOne.Controls.RequiredFieldValidator ValrZipCode;
        protected CustomValidator ValxPhone;
        protected EmailValidator VmailEmail;
        protected MobileValidator VmblMobile;
        protected TelephoneValidator VtelPhone;
        protected ZipCodeValidator VzipZipCode;

        private void AddressListPostBack(string eventArgument)
        {
            string[] strArray = eventArgument.Split(new string[] { "$$$" }, StringSplitOptions.None);
            if (strArray.Length == 9)
            {
                this.AddressPick.SelectRegion(strArray[0], strArray[1], strArray[2], strArray[3]);
                this.TxtZipCode.Text = strArray[4];
                this.AddressPick.Address = strArray[5];
                this.TxtContacterName.Text = strArray[6];
                this.TxtPhone.Text = strArray[7];
                this.TxtMobile.Text = strArray[8];
            }
        }

        private void AddressPick_AddressChanged(object sender, EventArgs e)
        {
            this.TxtZipCode.Text = this.AddressPick.ZipCode;
        }

        protected void BindDeliveryTime()
        {
            string[] strArray = Choiceset.GetChoicesetInfoByFieldAndTableName("PE_Orders", "DeliveryTime").FieldValue.Replace("|1|", ",").Split(new char[] { ',' });
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                ListItem item = new ListItem(strArray[i].ToString().Replace("1$", "").Replace("0$", "").Trim());
                if (!strArray[i].ToString().StartsWith("1$", StringComparison.Ordinal) && !strArray[i].ToString().StartsWith("0$", StringComparison.Ordinal))
                {
                    item.Selected = true;
                }
                this.dropDeliverTime.Items.Add(item);
            }
        }

        protected void BtnGotoPreview_Click(object sender, EventArgs e)
        {
            OrderFlowInfo flowInfo = this.FlowInfo;
            flowInfo.ShoppingCartId = this.m_CartId;
            flowInfo.ZipCode = this.TxtZipCode.Text;
            flowInfo.PaymentType = DataConverter.CLng(this.DropPaymentType.Text);
            flowInfo.DeliverType = DataConverter.CLng(this.DropDeliverType.Text);
            flowInfo.NeedInvoice = this.ChkNeedInvoice.Checked;
            flowInfo.ConsigneeName = this.TxtContacterName.Text;
            flowInfo.Address = this.AddressPick.Address;
            flowInfo.Email = this.TxtEmail.Text;
            flowInfo.Mobile = this.TxtMobile.Text;
            flowInfo.AgentName = this.SelectAgent1.AgentName;
            flowInfo.NeedInvoice = this.ChkNeedInvoice.Checked;
            flowInfo.HomePhone = this.TxtPhone.Text;
            flowInfo.InvoiceContent = this.TxtInvoiceContent.Text;
            flowInfo.Remark = this.TxtRemark.Text;
            flowInfo.Country = this.AddressPick.Country;
            flowInfo.Province = this.AddressPick.Province;
            flowInfo.City = this.AddressPick.City;
            flowInfo.Area = this.AddressPick.Area;
            flowInfo.PresentId = DataConverter.CLng(base.Request.Form["RdbPresentId"]);
            flowInfo.OutOfStockProject = (OutOfStockProject) DataConverter.CLng(this.RadlOutOfStockProject.SelectedValue);
            flowInfo.DeliveryTime = this.dropDeliverTime.SelectedItem.Text;
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ShoppingCart.aspx");
        }

        protected void DropDeliverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeliverTypeInfo deliverTypeById = new DeliverTypeInfo();
            deliverTypeById = DeliverType.GetDeliverTypeById(DataConverter.CLng(this.DropDeliverType.SelectedValue));
            if (!deliverTypeById.IsNull)
            {
                this.LblDeliverTypeIntro.Text = deliverTypeById.Intro;
            }
        }

        protected void DropPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentTypeInfo paymentTypeById = PaymentType.GetPaymentTypeById(DataConverter.CLng(this.DropPaymentType.SelectedValue));
            this.LblPaymentTypeIntro.Text = paymentTypeById.Intro;
            if (paymentTypeById.Category == 1)
            {
                this.RadlPayPlatform.Visible = true;
                IList<PayPlatformInfo> listOfEnabled = PayPlatform.GetListOfEnabled();
                this.RadlPayPlatform.DataSource = listOfEnabled;
                this.RadlPayPlatform.DataBind();
                this.RadlPayPlatform.SelectedIndex = 0;
            }
            else
            {
                this.RadlPayPlatform.Visible = false;
            }
        }

        private void InitializeDeliverType(int deliverTypeId)
        {
            IList<DeliverTypeInfo> enableDeliverTypeList = new List<DeliverTypeInfo>();
            enableDeliverTypeList = DeliverType.GetEnableDeliverTypeList();
            this.DropDeliverType.DataSource = enableDeliverTypeList;
            this.DropDeliverType.DataBind();
            int typeId = 0;
            string intro = "";
            foreach (DeliverTypeInfo info in enableDeliverTypeList)
            {
                if (deliverTypeId > 0)
                {
                    if (info.TypeId != deliverTypeId)
                    {
                        continue;
                    }
                    typeId = info.TypeId;
                    intro = info.Intro;
                    break;
                }
                if (info.IsDefault)
                {
                    typeId = info.TypeId;
                    intro = info.Intro;
                    break;
                }
            }
            this.DropDeliverType.SelectedValue = typeId.ToString();
            this.LblDeliverTypeIntro.Text = intro;
        }

        private void InitializePaymentType(int paymentTypeId, int payPlatformId)
        {
            IList<PaymentTypeInfo> paymentTypeListByEnabled = PaymentType.GetPaymentTypeListByEnabled();
            int typeId = 0;
            int category = 0;
            IList<PaymentTypeInfo> list2 = new List<PaymentTypeInfo>();
            foreach (PaymentTypeInfo info in paymentTypeListByEnabled)
            {
                bool userLogin = true;
                if (info.Category == 2)
                {
                    userLogin = this.m_UserLogin;
                }
                if (userLogin)
                {
                    if (paymentTypeId > 0)
                    {
                        if (info.TypeId == paymentTypeId)
                        {
                            typeId = paymentTypeId;
                            category = info.Category;
                            this.LblPaymentTypeIntro.Text = info.Intro;
                        }
                    }
                    else if (info.IsDefault)
                    {
                        typeId = info.TypeId;
                        category = info.Category;
                        this.LblPaymentTypeIntro.Text = info.Intro;
                    }
                    list2.Add(info);
                }
            }
            this.DropPaymentType.DataSource = list2;
            this.DropPaymentType.DataBind();
            BasePage.SetSelectedIndexByValue(this.DropPaymentType, typeId.ToString());
            if (category == 1)
            {
                this.RadlPayPlatform.Visible = true;
                IList<PayPlatformInfo> listOfEnabled = PayPlatform.GetListOfEnabled();
                this.RadlPayPlatform.DataSource = listOfEnabled;
                this.RadlPayPlatform.DataBind();
                foreach (PayPlatformInfo info2 in listOfEnabled)
                {
                    if (payPlatformId > 0)
                    {
                        if (info2.PayPlatformId != payPlatformId)
                        {
                            continue;
                        }
                        this.RadlPayPlatform.SelectedValue = info2.PayPlatformId.ToString();
                        return;
                    }
                    if (info2.IsDefault)
                    {
                        this.RadlPayPlatform.SelectedValue = info2.PayPlatformId.ToString();
                        return;
                    }
                }
            }
        }

        private void InitializeUserInfo()
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            if (!usersByUserName.IsNull)
            {
                AddressInfo defaultAddressByUserName = Address.GetDefaultAddressByUserName(usersByUserName.UserName);
                ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(usersByUserName.UserName);
                if (!defaultAddressByUserName.IsNull)
                {
                    this.AddressPick.SelectRegion(defaultAddressByUserName.Country, defaultAddressByUserName.Province, defaultAddressByUserName.City, defaultAddressByUserName.Area);
                    this.AddressPick.Address = defaultAddressByUserName.Address;
                    this.TxtZipCode.Text = defaultAddressByUserName.ZipCode;
                    this.TxtPhone.Text = defaultAddressByUserName.HomePhone;
                    this.TxtMobile.Text = defaultAddressByUserName.Mobile;
                    this.TxtContacterName.Text = defaultAddressByUserName.ConsigneeName;
                }
                else if (!contacterByUserName.IsNull)
                {
                    this.AddressPick.SelectRegion(contacterByUserName.Country, contacterByUserName.Province, contacterByUserName.City, "");
                    this.AddressPick.Address = contacterByUserName.Address;
                    this.TxtZipCode.Text = contacterByUserName.ZipCode;
                    this.TxtPhone.Text = contacterByUserName.HomePhone;
                    this.TxtMobile.Text = contacterByUserName.Mobile;
                    this.TxtContacterName.Text = contacterByUserName.TrueName;
                }
                if (!contacterByUserName.IsNull)
                {
                    this.TxtEmail.Text = contacterByUserName.Email;
                }
            }
        }

        private void ModifyInformation()
        {
            Preview previousPage = (Preview) base.PreviousPage;
            OrderFlowInfo flowInfo = previousPage.FlowInfo;
            this.TxtContacterName.Text = flowInfo.ConsigneeName;
            this.AddressPick.SelectRegion(flowInfo.Country, flowInfo.Province, flowInfo.City, flowInfo.Area);
            this.AddressPick.Address = flowInfo.Address;
            this.TxtZipCode.Text = flowInfo.ZipCode;
            this.TxtPhone.Text = flowInfo.HomePhone;
            this.TxtEmail.Text = flowInfo.Email;
            this.TxtMobile.Text = flowInfo.Mobile;
            this.SelectAgent1.AgentName = flowInfo.AgentName;
            this.InitializeDeliverType(flowInfo.DeliverType);
            this.InitializePaymentType(flowInfo.PaymentType, DataConverter.CLng(((HiddenField) previousPage.FindControl("HdnPayPlatformId")).Value));
            this.ChkNeedInvoice.Checked = flowInfo.NeedInvoice;
            if (this.ChkNeedInvoice.Checked)
            {
                this.TxtInvoiceContent.Text = flowInfo.InvoiceContent;
            }
            this.TxtRemark.Text = flowInfo.Remark;
            this.RadlOutOfStockProject.SelectedValue = ((int) flowInfo.OutOfStockProject).ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.AddressPick.AddressChanged += new EventHandler(this.AddressPick_AddressChanged);
            string applicationName = PayOnline.GetApplicationName();
            if (base.Request.Cookies["Cart" + applicationName] != null)
            {
                this.m_CartId = base.Request.Cookies["Cart" + applicationName]["CartID"];
            }
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(PEContext.Current.User.UserName))
                {
                    this.m_UserLogin = true;
                    ShoppingCart.UpdateUserName(this.m_CartId, PEContext.Current.User.UserName);
                }
                if (!SiteConfig.ShopConfig.EnableGuestBuy && !this.m_UserLogin)
                {
                    base.Response.Redirect("../User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode("../Shop/Payment.aspx"), true);
                }
                if ((base.PreviousPage != null) && (base.PreviousPage is Preview))
                {
                    this.ModifyInformation();
                }
                else
                {
                    this.InitializeDeliverType(0);
                    this.InitializePaymentType(0, 0);
                    this.InitializeUserInfo();
                }
                this.LBtnAddress.OnClientClick = string.Format("window.open('{0}Shop/AddressList.aspx','window','width=600,height=450');return false", base.BasePath);
                this.BindDeliveryTime();
            }
            this.ShoppingCart1.CartId = this.m_CartId;
            this.ShoppingCart1.IsPreview = 0;
            string str2 = base.Request.Params.Get("__EVENTTARGET");
            string eventArgument = base.Request.Params.Get("__EVENTARGUMENT");
            if (str2 == "AddressList_PostBack")
            {
                this.AddressListPostBack(eventArgument);
            }
        }

        protected void ValxPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.TxtPhone.Text) && string.IsNullOrEmpty(this.TxtMobile.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        public OrderFlowInfo FlowInfo
        {
            get
            {
                if (this.ViewState["FlowInfo"] == null)
                {
                    this.ViewState["FlowInfo"] = new OrderFlowInfo();
                }
                return (OrderFlowInfo) this.ViewState["FlowInfo"];
            }
        }
    }
}

