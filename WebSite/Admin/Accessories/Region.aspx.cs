namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class Regions : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                RegionInfo info;
                bool flag = false;
                string area = this.TxtArea.Text.Trim();
                string province = this.TxtProvince.Text.Trim();
                string city = this.TxtCity.Text.Trim();
                if (BasePage.RequestString("Action") == "Modify")
                {
                    info = this.ViewState["Info"] as RegionInfo;
                    if ((((info.Area != area) || (info.Province != province)) || (info.City != city)) && Region.AreaExists(area, province, city))
                    {
                        AdminPage.WriteErrMsg("已存在该区域，请重新输入！");
                    }
                }
                else
                {
                    info = new RegionInfo();
                }
                info.Country = this.TxtCountry.Text.Trim();
                info.Province = province;
                info.City = city;
                info.Area = area;
                info.PostCode = this.TxtPostCode.Text.Trim();
                info.AreaCode = this.TxtAreaCode.Text.Trim();
                if (BasePage.RequestString("Action") == "Modify")
                {
                    flag = Region.Update(info);
                }
                else if (Region.AreaExists(area, province, city))
                {
                    AdminPage.WriteErrMsg("已存在该区域，请重新输入！");
                }
                else
                {
                    flag = Region.Add(info);
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("保存数据成功！", "RegionManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("操作失败！", "");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (BasePage.RequestString("Action") == "Modify"))
            {
                RegionInfo regionById = Region.GetRegionById(BasePage.RequestInt32("ID"));
                this.ViewState["Info"] = regionById;
                this.TxtCountry.Text = regionById.Country;
                this.TxtProvince.Text = regionById.Province;
                this.TxtCity.Text = regionById.City;
                this.TxtArea.Text = regionById.Area;
                this.TxtPostCode.Text = regionById.PostCode;
                this.TxtAreaCode.Text = regionById.AreaCode;
            }
        }
    }
}

