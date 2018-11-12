namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class MessageBatchDel : AdminPage
    {

        protected void BtnDelDate_Click(object sender, EventArgs e)
        {
            Message.Delete(MessageDelType.Date, this.DropDelDate.Text.ToString());
            AdminPage.WriteSuccessMsg("删除成功！", "MessageManage.aspx");
        }

        protected void BtnDelSender_Click(object sender, EventArgs e)
        {
            Message.Delete(MessageDelType.Sender, this.TxtSender.Text.ToString());
            AdminPage.WriteSuccessMsg("删除成功！", "MessageManage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

