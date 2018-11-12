namespace EasyOne.WebSite.Controls.Crm
{
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class LiaisonInformation : UserControl, ICallbackEventHandler
    {
        private string result;

        public string GetCallbackResult()
        {
            return this.result;
        }

        public void GetContacter(ContacterInfo contacterInfo)
        {
            contacterInfo.Country = this.Region1.Country;
            contacterInfo.Province = this.Region1.Province;
            contacterInfo.City = this.Region1.City;
            contacterInfo.Address = this.TxtAddress.Text;
            contacterInfo.ZipCode = this.TxtZipCode.Text;
            contacterInfo.OfficePhone = this.TxtOfficePhone.Text;
            contacterInfo.HomePhone = this.TxtHomePhone.Text;
            contacterInfo.Mobile = this.TxtMobile.Text;
            contacterInfo.Fax = this.TxtFax.Text;
            contacterInfo.Phs = this.TxtPHS.Text;
            contacterInfo.Homepage = this.TxtHomepage.Text;
            contacterInfo.Email = this.TxtEmail.Text;
            contacterInfo.QQ = this.TxtQQ.Text;
            contacterInfo.Msn = this.TxtMSN.Text;
            contacterInfo.Icq = this.TxtICQ.Text;
            contacterInfo.Yahoo = this.TxtYahoo.Text;
            contacterInfo.UC = this.TxtUC.Text;
            contacterInfo.Aim = this.TxtAim.Text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "CallbackEventReference", "context");
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "CallbackToServer", "function CallbackToServer(arg,context){" + str + "}", true);
                this.HdnQQ.Value = this.TxtQQ.Text;
                this.HdnMsn.Value = this.TxtMSN.Text;
                this.HdnHomepage.Value = this.TxtHomepage.Text;
                this.HdnHomephone.Value = this.TxtHomePhone.Text;
                this.HdnOfficePhone.Value = this.TxtOfficePhone.Text;
                this.HdnMobile.Value = this.TxtMobile.Text;
            }
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (!string.IsNullOrEmpty(eventArgument))
            {
                if (eventArgument.StartsWith("$QQ"))
                {
                    bool flag = Contacter.CheckExistsQQ(eventArgument.Substring(3));
                    this.result = "{name:'QQ',value:" + flag.ToString().ToLower() + "}";
                }
                else if (eventArgument.StartsWith("$Msn"))
                {
                    bool flag2 = Contacter.CheckExistsMsn(eventArgument.Substring(4));
                    this.result = "{name:'Msn',value:" + flag2.ToString().ToLower() + "}";
                }
                else if (eventArgument.StartsWith("$OP"))
                {
                    string phone = eventArgument.Substring(3);
                    bool flag3 = Contacter.CheckExistsPhone(phone) || Company.CheckExistsPhone(phone);
                    this.result = "{name:'OP',value:" + flag3.ToString().ToLower() + "}";
                }
                else if (eventArgument.StartsWith("$HP"))
                {
                    string str4 = eventArgument.Substring(3);
                    bool flag4 = Contacter.CheckExistsPhone(str4) || Company.CheckExistsPhone(str4);
                    this.result = "{name:'HP',value:" + flag4.ToString().ToLower() + "}";
                }
                else if (eventArgument.StartsWith("$MP"))
                {
                    string str5 = eventArgument.Substring(3);
                    bool flag5 = Contacter.CheckExistsPhone(str5) || Company.CheckExistsPhone(str5);
                    this.result = "{name:'MP',value:" + flag5.ToString().ToLower() + "}";
                }
                else if (eventArgument.StartsWith("$Page"))
                {
                    string strB = eventArgument.Substring(5);
                    if (string.Compare("http://", strB, true) != 0)
                    {
                        bool flag6 = Contacter.CheckExistsHomepage(strB) || Company.CheckExistsHomepage(strB);
                        this.result = "{name:'Page',value:" + flag6.ToString().ToLower() + "}";
                    }
                    else
                    {
                        this.result = "{name:'Page',value:false}";
                    }
                }
            }
        }

        public void SetContacter(ContacterInfo contacterInfo)
        {
            this.Region1.Action = "Modify";
            this.Region1.Country = contacterInfo.Country;
            this.Region1.Province = contacterInfo.Province;
            this.Region1.City = contacterInfo.City;
            this.TxtAddress.Text = contacterInfo.Address;
            this.TxtZipCode.Text = contacterInfo.ZipCode;
            this.TxtOfficePhone.Text = contacterInfo.OfficePhone;
            this.TxtHomePhone.Text = contacterInfo.HomePhone;
            this.TxtMobile.Text = contacterInfo.Mobile;
            this.TxtFax.Text = contacterInfo.Fax;
            this.TxtPHS.Text = contacterInfo.Phs;
            this.TxtHomepage.Text = contacterInfo.Homepage;
            this.TxtEmail.Text = contacterInfo.Email;
            this.TxtQQ.Text = contacterInfo.QQ;
            this.TxtMSN.Text = contacterInfo.Msn;
            this.TxtICQ.Text = contacterInfo.Icq;
            this.TxtYahoo.Text = contacterInfo.Yahoo;
            this.TxtUC.Text = contacterInfo.UC;
            this.TxtAim.Text = contacterInfo.Aim;
        }

        public bool IsValid
        {
            get
            {
                if ((!string.IsNullOrEmpty(this.TxtQQ.Text) && (this.HdnQQ.Value != this.TxtQQ.Text)) && Contacter.CheckExistsQQ(this.TxtQQ.Text))
                {
                    return false;
                }
                if ((!string.IsNullOrEmpty(this.TxtMSN.Text) && (this.HdnMsn.Value != this.TxtMSN.Text)) && Contacter.CheckExistsMsn(this.TxtMSN.Text))
                {
                    return false;
                }
                if ((!string.IsNullOrEmpty(this.TxtHomepage.Text) && (this.HdnHomepage.Value != this.TxtHomepage.Text)) && (Contacter.CheckExistsHomepage(this.TxtHomepage.Text) || Company.CheckExistsHomepage(this.TxtHomepage.Text)))
                {
                    return false;
                }
                if ((!string.IsNullOrEmpty(this.TxtHomePhone.Text) && (this.HdnHomephone.Value != this.TxtHomePhone.Text)) && (Contacter.CheckExistsPhone(this.TxtHomePhone.Text) || Company.CheckExistsPhone(this.TxtHomePhone.Text)))
                {
                    return false;
                }
                if ((!string.IsNullOrEmpty(this.TxtOfficePhone.Text) && (this.HdnOfficePhone.Value != this.TxtOfficePhone.Text)) && (Contacter.CheckExistsPhone(this.TxtOfficePhone.Text) || Company.CheckExistsPhone(this.TxtOfficePhone.Text)))
                {
                    return false;
                }
                return ((string.IsNullOrEmpty(this.TxtMobile.Text) || !(this.HdnMobile.Value != this.TxtMobile.Text)) || (!Contacter.CheckExistsPhone(this.TxtMobile.Text) && !Company.CheckExistsPhone(this.TxtMobile.Text)));
            }
        }
    }
}

