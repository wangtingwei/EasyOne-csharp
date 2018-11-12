namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowTemplates : AdminPage
    {
        protected string FilePath;
        protected string ParentDir;


        protected void BindData(string dir)
        {
            this.RepFiles.DataSource = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + dir, FsoMethod.All);
            this.RepFiles.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string templateDir = SiteConfig.SiteOption.TemplateDir;
                string str2 = BasePage.RequestString("FilesDir").Replace("..", string.Empty);
                string str3 = "";
                if (!string.IsNullOrEmpty(str2))
                {
                    str3 = str2.Replace("/", @"\");
                }
                string str4 = "";
                if (!string.IsNullOrEmpty(templateDir))
                {
                    str4 = templateDir.Replace("/", @"\");
                }
                this.HdnFileText.Value = str2;
                DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + str4 + str3);
                if (info.Exists)
                {
                    this.BindData(str4 + str3);
                }
            }
        }

        protected void RepFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView) e.Item.DataItem;
                if ((dataItem["name"].ToString() == "标签库") || (dataItem["name"].ToString() == "分页标签库"))
                {
                    e.Item.Visible = false;
                }
            }
        }
    }
}

