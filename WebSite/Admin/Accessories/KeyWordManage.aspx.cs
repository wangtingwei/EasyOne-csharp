namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class KeyWordManage : AdminPage
    {


        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            if (Keywords.Delete(this.EgvKeyWord.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除记录成功！</li>", "KeyWordManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!base.IsPostBack && (BasePage.RequestString("Action") == "Delete")) && Keywords.Delete(BasePage.RequestString("KeywordID")))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的记录成功！</li>", "KeyWordManage.aspx");
            }
        }
    }
}

