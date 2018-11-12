namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class CompanyInfo : UserControl
    {
        private int m_CompanyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.m_CompanyId > 0))
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            EasyOne.Model.Crm.CompanyInfo compayById = Company.GetCompayById(this.m_CompanyId);
            this.LblCompanyName.Text = compayById.CompanyName;
            this.LblCountry.Text = compayById.Country;
            this.LblProvince.Text = compayById.Province;
            this.LblCity.Text = compayById.City;
            this.LblAddress.Text = compayById.Address;
            this.LblZipCode.Text = compayById.ZipCode;
            this.LblAnnualSales.Text = compayById.AnnualSales;
            this.LblBankAccount.Text = compayById.BankAccount;
            this.LblBankOfDeposit.Text = compayById.BankOfDeposit;
            this.LblBusinessScope.Text = compayById.BusinessScope;
            this.LblCompanyPic.Text = compayById.CompanyPic;
            this.LblCompanyIntro.Text = compayById.CompanyIntro;
            this.LblCompanySize.Text = Choiceset.GetDataText("PE_Company", "CompanySize", compayById.CompanySize);
            this.LblFax.Text = compayById.Fax;
            this.LblHomepage.Text = compayById.Homepage;
            this.LblPhone.Text = compayById.Phone;
            this.LblRegisteredCapital.Text = compayById.RegisteredCapital;
            this.LblTaxNum.Text = compayById.TaxNum;
            this.LblStatusInField.Text = Choiceset.GetDataText("PE_Company", "StatusInField", compayById.StatusInField);
            this.LblManagementForms.Text = Choiceset.GetDataText("PE_Company", "ManagementForms", compayById.ManagementForms);
        }

        public int CompanyId
        {
            get
            {
                return this.m_CompanyId;
            }
            set
            {
                this.m_CompanyId = value;
            }
        }
    }
}

