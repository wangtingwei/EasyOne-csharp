namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class DownServerManage : AdminPage
    {


        private void DeleteDownServer()
        {
            if (DownServer.Delete(BasePage.RequestInt32("ServerID")))
            {
                AdminPage.WriteSuccessMsg("删除下载服务器成功！", "DownServerManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("删除下载服务器失败！");
            }
        }

        protected void EBtnShowType_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvDownServer.SelectList;
            int showType = DataConverter.CLng(this.DropShowType.SelectedItem.Value);
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要设置显示方式行！</li>");
            }
            else if (DownServer.SetShowType(selectList.ToString(), showType))
            {
                AdminPage.WriteSuccessMsg("设置显示方式成功！", "DownServerManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("设置显示方式失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (BasePage.RequestString("Action") == "Delete"))
            {
                this.DeleteDownServer();
            }
        }
    }
}

