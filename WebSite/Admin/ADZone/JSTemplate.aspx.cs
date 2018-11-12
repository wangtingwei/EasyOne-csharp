namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class JSTemplate : AdminPage
    {

        private string BuildTableBody()
        {
            StringBuilder builder = new StringBuilder();
            int num = 6;
            ADZoneJS ejs = new ADZoneJS();
            string[] fileSize = ejs.GetFileSize();
            for (int i = 1; i < (num + 1); i++)
            {
                builder.Append("<tr class=\"tdbg\" onmouseout=\"this.className='tdbg'\" onmouseover=\"this.className='tdbgmouseover'\">");
                builder.Append("<td align=\"center\">" + i + "</td>");
                builder.Append(" <td align=\"center\">");
                builder.Append(BasePage.EnumToHtml<ADZoneType>((ADZoneType) i));
                builder.Append("</td>");
                builder.Append("<td align=\"center\">");
                builder.Append(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir));
                builder.Append("ADTemplate/");
                builder.Append(ejs.GetTemplateName((ADZoneType) i));
                builder.Append("</td>");
                builder.Append("<td align=\"center\">");
                builder.Append(fileSize[i]);
                builder.Append("</td>");
                builder.Append(" <td align=\"center\">");
                builder.Append("<a href=\"ModifyJSTemplate.aspx?Action=ModifyTemplate&ZoneType=" + i + "\">修改模板内容</a>");
                builder.Append("</td>");
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        private string BuildTableHeard()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table class=\"border\" border=\"0\" cellspacing=\"1\" width=\"100%\" cellpadding=\"0\">");
            builder.Append("<tr class=\"title\">");
            builder.Append("<td align=\"center\">");
            builder.Append("<strong>类型ID</strong></td>");
            builder.Append("<td align=\"center\">");
            builder.Append("<strong>模板类型名称</strong></td>");
            builder.Append("<td align=\"center\">");
            builder.Append("<strong>模板文件所在路径</strong></td>");
            builder.Append("<td align=\"center\">");
            builder.Append("<strong>模板文件大小</strong></td>");
            builder.Append("<td align=\"center\">");
            builder.Append("<strong>操作</strong></td>");
            builder.Append("</tr>");
            return builder.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowTemplate.InnerHtml = this.BuildTableHeard() + this.BuildTableBody();
        }
    }
}

