namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class RegionManage : AdminPage
    {

        protected void GdvRegion_RowCommand(object sender, CommandEventArgs e)
        {
            string regionId = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                if (Region.Delete(regionId))
                {
                    BasePage.ResponseRedirect("RegionManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("删除失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

