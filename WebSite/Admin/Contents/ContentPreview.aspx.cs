namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI.HtmlControls;

    public partial class ContentPreview : AdminPage
    {

        private string ConversionCode(string content)
        {
            string input = this.PathReplaceLable(content);
            StringBuilder builder = new StringBuilder();
            string pattern = @"\<\!\-\-Code\-\-\>([\s\S]*?)\<\!\-\-Code\-\-\>";
            MatchCollection matchs = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
            if (matchs.Count > 0)
            {
                foreach (Match match in matchs)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("[PE:Code]" + match.Value);
                    }
                    else
                    {
                        builder.Append(match.Value);
                    }
                    input = input.Replace(match.Value, "[PE:Code]");
                }
                foreach (string str3 in builder.ToString().Split(new string[] { "[PE:Code]" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    input = input.Replace("[PE:Code]", str3);
                }
            }
            return input;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int generalId = BasePage.RequestInt32("GeneralID");
                string str = BasePage.RequestString("fieldName");
                if (!string.IsNullOrEmpty(str))
                {
                    DataTable contentDataById = ContentManage.GetContentDataById(generalId);
                    if ((contentDataById == null) || (contentDataById.Rows.Count == 0))
                    {
                        AdminPage.WriteErrMsg("<li>指定项目不存在！</li>");
                    }
                    this.LitContent.Text = this.ConversionCode(contentDataById.Rows[0][str].ToString());
                }
            }
        }

        private string PathReplaceLable(string content)
        {
            string input = content;
            string pattern = @"{PE\.SiteConfig\.ApplicationPath\/}";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match.Value, SiteConfig.SiteInfo.VirtualPath);
            }
            pattern = @"{PE\.SiteConfig\.uploaddir\/}";
            foreach (Match match2 in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            {
                input = input.Replace(match2.Value, SiteConfig.SiteOption.UploadDir);
            }
            return input;
        }
    }
}

