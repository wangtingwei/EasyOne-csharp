namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class DownloadErrorManage : AdminPage
    {

        protected void EBtnClear_Click(object sender, EventArgs e)
        {
            if (DownloadError.Clear())
            {
                BasePage.ResponseRedirect("DownloadErrorManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("清空下载地址错误信息失败！");
            }
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvDownloadError.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("对不起，您还没选择要删除的下载报错信息！");
            }
            else if (DownloadError.Delete(selectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("删除下载报错信息成功！", "DownloadErrorManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("删除下载地址错误信息失败！");
            }
        }

        protected void EgvDownloadError_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Deleted")
            {
                DownloadError.Delete(e.CommandArgument.ToString());
            }
        }

        protected string GetSoftName(int infoId)
        {
            return ContentManage.GetContentDataById(infoId).Rows[0]["Title"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

