namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Contents;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;

    public partial class ShowCheckTitleMessage : AdminPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            int nodeId = BasePage.RequestInt32("NodeId");
            string str = BasePage.RequestString("Title");
            if (!string.IsNullOrEmpty(str))
            {
                if (ContentManage.ExistSameTitle(nodeId, str))
                {
                    this.LblCheckTitleMessage.Text = "<li>该标题已经存在！</li>";
                }
                else
                {
                    this.LblCheckTitleMessage.Text = "<li>该标题还没有被使用！</li>";
                }
            }
            else
            {
                this.LblCheckTitleMessage.Text = "<li>被检测的标题不可以为空！</li>";
            }
        }
    }
}

