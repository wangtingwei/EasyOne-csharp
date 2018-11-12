namespace EasyOne.WebSite.Controls
{
    using EasyOne.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class RegionControl : BaseUserControl
    {
        private string m_City;
        private string m_Country;
        private string m_Province;
        private bool m_ShowCountry = true;

        private void DropCityDataBind(string province, string city)
        {
            if (string.IsNullOrEmpty(province))
            {
                this.DropCity.Enabled = false;
                ListItem item = new ListItem("请选择省份", "");
                this.DropCity.Items.Insert(0, item);
                this.DropCity.SelectedIndex = this.DropCity.Items.IndexOf(item);
            }
            else
            {
                this.DropCity.Enabled = true;
                this.DropCity.DataSource = Region.GetCityListByProvince(province);
                this.DropCity.DataBind();
                ListItem item2 = new ListItem("请选择", "");
                this.DropCity.Items.Insert(0, item2);
                BaseUserControl.SetSelectedIndexByValue(this.DropCity, city);
            }
        }

        protected void DropCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropProvinceDataBind(this.DropCountry.SelectedValue, "");
            this.DropCityDataBind(this.DropProvince.SelectedValue, "");
        }

        private void DropCountryDataBind(string country)
        {
            this.DropCountry.DataSource = Region.GetCountryList();
            this.DropCountry.DataBind();
            if (string.IsNullOrEmpty(country))
            {
                ListItem item = new ListItem("请选择", "");
                this.DropCountry.Items.Insert(0, item);
                this.DropCountry.SelectedIndex = this.DropCountry.Items.IndexOf(item);
            }
            else
            {
                BaseUserControl.SetSelectedIndexByValue(this.DropCountry, country);
            }
        }

        protected void DropProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropCityDataBind(this.DropProvince.SelectedValue, "");
        }

        private void DropProvinceDataBind(string country, string province)
        {
            if (string.IsNullOrEmpty(country))
            {
                this.DropProvince.Enabled = false;
                ListItem item = new ListItem("请选择国家", "");
                this.DropProvince.Items.Insert(0, item);
                this.DropProvince.SelectedIndex = this.DropProvince.Items.IndexOf(item);
            }
            else
            {
                this.DropProvince.Enabled = true;
                this.DropProvince.DataSource = Region.GetProvinceListByCountry(country);
                this.DropProvince.DataBind();
                ListItem item2 = new ListItem("请选择", "");
                this.DropProvince.Items.Insert(0, item2);
                BaseUserControl.SetSelectedIndexByValue(this.DropProvince, province);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DropCountry.Attributes.Add("onchange", "LoadingProvince();");
            string script = "<script type=\"text/javascript\">\r\nfunction LoadingProvince()\r\n{\r\n    var dropProvince = document.getElementById(\"" + this.DropProvince.ClientID + "\");\r\n    dropProvince.options[0] = new Option(\"载入中...\", \"\", 0, 0);\r\n}\r\n</script>";
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Loading", script);
            if (!this.Page.IsPostBack)
            {
                if (!this.m_ShowCountry)
                {
                    this.PlhCountry.Visible = false;
                    ListItem item = new ListItem("中华人民共和国", "中华人民共和国");
                    item.Selected = true;
                    this.DropCountry.Items.Add(item);
                    this.m_Country = this.DropCountry.SelectedValue;
                }
                else
                {
                    this.DropCountryDataBind(this.m_Country);
                }
                this.DropProvinceDataBind(this.m_Country, this.m_Province);
                this.DropCityDataBind(this.m_Province, this.m_City);
            }
        }

        public string Action
        {
            get
            {
                string str = (string) this.ViewState["Action"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Action"] = value;
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
                this.m_City = value;
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
                this.m_Country = value;
            }
        }

        public int Direction
        {
            get
            {
                object obj2 = this.ViewState["Direction"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["Direction"] = value;
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
                this.m_Province = value;
            }
        }

        public bool ShowCountry
        {
            get
            {
                return this.m_ShowCountry;
            }
            set
            {
                this.m_ShowCountry = value;
            }
        }
    }
}

