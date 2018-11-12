namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class CompanyControl : BaseUserControl
    {
        private string m_CompanyName;

        private void ModifyInitialize()
        {
            EasyOne.Model.Crm.CompanyInfo compayById =  Company.GetCompayById(this.CompanyId);
            this.TxtCompanyName.Text = compayById.CompanyName;
            this.Region1.Country = compayById.Country;
            this.Region1.Province = compayById.Province;
            this.Region1.City = compayById.City;
            this.TxtAddress.Text = compayById.Address;
            this.TxtZipCode.Text = compayById.ZipCode;
            this.TxtAnnualSales.Text = compayById.AnnualSales;
            this.TxtBankAccount.Text = compayById.BankAccount;
            this.TxtBankOfDeposit.Text = compayById.BankOfDeposit;
            this.TxtBusinessScope.Text = compayById.BusinessScope;
            this.TxtCompanyPic.Text = compayById.CompanyPic;
            this.TxtCompanyIntro.Text = compayById.CompanyIntro;
            this.DropCompanySize.SelectedValue = compayById.CompanySize.ToString();
            this.TxtFax.Text = compayById.Fax;
            this.TxtHomepage.Text = compayById.Homepage;
            this.DropManagementForms.SelectedValue = compayById.ManagementForms.ToString();
            this.TxtPhone.Text = compayById.Phone;
            this.TxtRegisteredCapital.Text = compayById.RegisteredCapital;
            this.DropStatusInField.SelectedValue = compayById.StatusInField.ToString();
            this.TxtTaxNum.Text = compayById.TaxNum;
            this.ViewState["CompanyInfo"] = compayById;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Choiceset.DropDownListDataBind("PE_Company", "StatusInField", this.DropStatusInField);
                Choiceset.DropDownListDataBind("PE_Company", "CompanySize", this.DropCompanySize);
                Choiceset.DropDownListDataBind("PE_Company", "ManagementForms", this.DropManagementForms);
                if (this.Action == "modify")
                {
                    this.ModifyInitialize();
                }
                else
                {
                    this.TxtCompanyName.Text = this.CompanyName;
                }
            }
        }

        public string Action
        {
            get
            {
                return this.HdnCompanyAction.Value;
            }
            set
            {
                this.HdnCompanyAction.Value = value.ToLower();
            }
        }

        public int CompanyClientId
        {
            get
            {
                return DataConverter.CLng(this.ViewState["CompanyClientId"]);
            }
            set
            {
                this.ViewState["CompanyClientId"] = value;
            }
        }

        public int CompanyId
        {
            get
            {
                return DataConverter.CLng(this.ViewState["CompanyId"]);
            }
            set
            {
                this.ViewState["CompanyId"] = value;
            }
        }

        public EasyOne.Model.Crm.CompanyInfo CompanyInfo
        {
            get
            {
                EasyOne.Model.Crm.CompanyInfo compayById = new EasyOne.Model.Crm.CompanyInfo();
                if (this.Action == "modify")
                {
                    compayById = this.ViewState["CompanyInfo"] as EasyOne.Model.Crm.CompanyInfo;
                    if (compayById == null)
                    {
                        compayById = Company.GetCompayById(this.CompanyId);
                    }
                }
                else
                {
                    compayById.ClientId = this.CompanyClientId;
                }
                compayById.CompanyName = this.TxtCompanyName.Text;
                compayById.Country = this.Region1.Country;
                compayById.Province = this.Region1.Province;
                compayById.City = this.Region1.City;
                compayById.Address = this.TxtAddress.Text;
                compayById.ZipCode = this.TxtZipCode.Text;
                compayById.AnnualSales = this.TxtAnnualSales.Text;
                compayById.BankAccount = this.TxtBankAccount.Text;
                compayById.BankOfDeposit = this.TxtBankOfDeposit.Text;
                compayById.BusinessScope = this.TxtBusinessScope.Text;
                compayById.CompanyPic = this.TxtCompanyPic.Text;
                compayById.CompanyIntro = this.TxtCompanyIntro.Text;
                compayById.CompanySize = DataConverter.CLng(this.DropCompanySize.SelectedValue);
                compayById.Fax = this.TxtFax.Text;
                compayById.Homepage = this.TxtHomepage.Text;
                compayById.ManagementForms = DataConverter.CLng(this.DropManagementForms.SelectedValue);
                compayById.Phone = this.TxtPhone.Text;
                compayById.RegisteredCapital = this.TxtRegisteredCapital.Text;
                compayById.StatusInField = DataConverter.CLng(this.DropStatusInField.SelectedValue);
                compayById.TaxNum = this.TxtTaxNum.Text;
                return compayById;
            }
        }

        public string CompanyName
        {
            get
            {
                return this.m_CompanyName;
            }
            set
            {
                this.m_CompanyName = value;
            }
        }
    }
}

