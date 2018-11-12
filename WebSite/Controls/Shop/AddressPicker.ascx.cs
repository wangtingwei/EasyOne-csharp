namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class AddressPicker : BaseUserControl
    {

        public event EventHandler AddressChanged;

        private void DataBindArea()
        {
            this.DropArea.DataSource = Region.GetAreaListByCity(this.DropCity.SelectedValue);
            this.DropArea.DataBind();
        }

        private void DataBindCity()
        {
            this.DropCity.DataSource = Region.GetCityListByProvince(this.DropProvince.SelectedValue);
            this.DropCity.DataBind();
        }

        protected void DropArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSelectedValue();
        }

        protected void DropCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataBindArea();
            this.GetSelectedValue();
        }

        protected void DropCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropProvince.DataSource = Region.GetProvinceListByCountry(this.DropCountry.SelectedValue);
            this.DropProvince.DataBind();
            this.DataBindCity();
            this.DataBindArea();
            this.GetSelectedValue();
        }

        protected void DropProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataBindCity();
            this.DataBindArea();
            this.GetSelectedValue();
        }

        private void GetSelectedValue()
        {
            this.HdnZipCode.Value = Region.GetZipCodeByArea(this.DropCountry.SelectedValue, this.DropProvince.SelectedValue, this.DropCity.SelectedValue, this.DropArea.SelectedValue);
            this.AddressChanged(this, EventArgs.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (this.DropCountry.Items.Count == 0))
            {
                this.SelectRegion("请选择所属国家", "", "", "");
            }
        }

        public void Reset()
        {
            this.SelectRegion("请选择所属国家", "", "", "");
            this.HdnZipCode.Value = "";
            this.TxtAddress.Text = "";
        }

        public void SelectRegion(string country, string province, string city, string area)
        {
            string selectedValue = this.DropCountry.SelectedValue;
            string str2 = this.DropProvince.SelectedValue;
            string str3 = this.DropCity.SelectedValue;
            if (this.DropCountry.Items.Count == 0)
            {
                this.DropCountry.DataSource = Region.GetCountryList();
                this.DropCountry.DataBind();
                this.DropCountry.Items.Insert(0, new ListItem("请选择所属国家", "-1"));
            }
            if (selectedValue != country)
            {
                BaseUserControl.SetSelectedIndexByValue(this.DropCountry, country);
                this.DropProvince.DataSource = Region.GetProvinceListByCountry(country);
                this.DropProvince.DataBind();
            }
            if (str2 != province)
            {
                BaseUserControl.SetSelectedIndexByValue(this.DropProvince, province);
                this.DropCity.DataSource = Region.GetCityListByProvince(province);
                this.DropCity.DataBind();
            }
            if (str3 != city)
            {
                BaseUserControl.SetSelectedIndexByValue(this.DropCity, city);
                this.DropArea.DataSource = Region.GetAreaListByCity(this.DropCity.SelectedValue);
                this.DropArea.DataBind();
            }
            BaseUserControl.SetSelectedIndexByValue(this.DropArea, area);
        }

        public string Address
        {
            get
            {
                return this.TxtAddress.Text.Trim();
            }
            set
            {
                this.TxtAddress.Text = value;
            }
        }

        public string Area
        {
            get
            {
                return this.DropArea.SelectedValue;
            }
            set
            {
                this.DropArea.SelectedValue = value;
            }
        }

        public string City
        {
            get
            {
                return this.DropCity.SelectedValue;
            }
            set
            {
                this.DropCity.SelectedValue = value;
            }
        }

        public string Country
        {
            get
            {
                return this.DropCountry.SelectedValue;
            }
            set
            {
                this.DropCountry.SelectedValue = value;
            }
        }

        public string Province
        {
            get
            {
                return this.DropProvince.SelectedValue;
            }
            set
            {
                this.DropProvince.SelectedValue = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return this.HdnZipCode.Value;
            }
            set
            {
                this.HdnZipCode.Value = value;
            }
        }
    }
}

