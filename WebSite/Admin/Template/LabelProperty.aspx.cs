namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class LabelProperty : AdminPage
    {
        private string action;
        private string m_LabelLibPath;
        private string m_LabelName;
        private string xmlfilepath;

        protected void BtnAddProperty_Click(object sender, EventArgs e)
        {
            if (LabelManage.AttributeExists(this.xmlfilepath, this.TxtAttributeName.Text) && LabelManage.AddAttribute(this.xmlfilepath, this.TxtAttributeName.Text, this.TxtDefaultValue.Text, this.TxtIntro.Text))
            {
                this.TxtAttributeName.Text = string.Empty;
                this.TxtDefaultValue.Text = string.Empty;
                this.TxtIntro.Text = string.Empty;
                this.GdvPropertys.DataBind();
            }
        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            string str = XmlManage.ReadFileNode(this.xmlfilepath, "root/OutType");
            string str2 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataType");
            if (str2 != null)
            {
                if (!(str2 == "static"))
                {
                    if (str2 == "sql_sysstoredquery")
                    {
                        BasePage.ResponseRedirect("LabelTemplate.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
                        return;
                    }
                    if (str2 == "xml_read")
                    {
                        BasePage.ResponseRedirect("LabelTemplate.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
                        return;
                    }
                }
                else
                {
                    switch (str)
                    {
                        case "txt":
                        case "xml":
                            BasePage.ResponseRedirect("LabelTemplateStatic.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
                            return;

                        default:
                            BasePage.ResponseRedirect("LabelTemplateStatic.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
                            return;
                    }
                }
            }
            BasePage.ResponseRedirect("LabelSqlBuild.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
        }

        protected void BtnPrv_Click(object sender, EventArgs e)
        {
            if (string.Compare(this.action, "modify", StringComparison.OrdinalIgnoreCase) == 0)
            {
                BasePage.ResponseRedirect("Label.aspx?name=" + base.Server.UrlEncode(this.m_LabelName));
            }
            else
            {
                BasePage.ResponseRedirect("Label.aspx?name=" + base.Server.UrlEncode(this.m_LabelName));
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.m_LabelName + ".config", true);
                BasePage.ResponseRedirect("LabelManage.aspx");
            }
            catch (IOException)
            {
                AdminPage.WriteErrMsg("没有标签目录或临时目录或标签文件的访问权限！", "LabelManage.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                AdminPage.WriteErrMsg("没有标签目录或临时目录或标签文件的访问权限！", "LabelManage.aspx");
            }
        }

        protected void GdvProperty_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (e.OldValues["AttributeName"] != null)
            {
                this.OdsPropertys.UpdateParameters["oldAttributeName"].DefaultValue = e.OldValues["AttributeName"].ToString();
            }
            else
            {
                this.OdsPropertys.UpdateParameters.Remove(this.OdsPropertys.UpdateParameters["oldAttributeName"]);
            }
        }

        protected void GdvPropertys_RowCommand(object sender, CommandEventArgs e)
        {
            string str;
            bool flag = false;
            if (((str = e.CommandName) != null) && (str == "Delete"))
            {
                flag = LabelManage.DeleteAttribute(this.xmlfilepath, e.CommandArgument.ToString());
                this.OdsPropertys.DeleteParameters[0].DefaultValue = this.xmlfilepath;
            }
            else
            {
                flag = false;
            }
            if (flag)
            {
                this.GdvPropertys.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.action = BasePage.RequestString("action");
            this.m_LabelName = BasePage.RequestString("name");
            this.m_LabelLibPath = "~/" + SiteConfig.SiteOption.LabelDir;
            if (string.IsNullOrEmpty(this.m_LabelName))
            {
                BasePage.ResponseRedirect("Label.aspx");
            }
            else
            {
                string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                this.xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + this.m_LabelName + ".config";
                if (string.Compare(this.action, "modify", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.BtnSave.Visible = true;
                }
                this.OdsPropertys.SelectParameters[0].DefaultValue = this.xmlfilepath;
                this.OdsPropertys.UpdateParameters[0].DefaultValue = this.xmlfilepath;
            }
            this.SmpNavigator.AdditionalNode = this.m_LabelName;
        }
    }
}

