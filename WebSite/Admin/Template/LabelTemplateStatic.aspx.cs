namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class LabelTemplateStatic : AdminPage
    {
        private string action;
        private string labelname;
        private string m_LabelLibPath;
        private string xmlfilepath;

        protected void BtnFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelTemplate", this.TxtTemplate.Text))
                {
                    File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.labelname + ".config", true);
                    BasePage.ResponseRedirect("LabelManage.aspx");
                }
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

        protected void BtnPrv_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("LabelProperty.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.labelname));
        }

        protected void BtnShowTemplate_Click(object sender, EventArgs e)
        {
            if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelTemplate", this.TxtTemplate.Text))
            {
                File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.labelname + ".config", true);
            }
            SiteCache.Remove("pe_labdefing_" + this.labelname);
            if (!string.IsNullOrEmpty(this.TxtTemplateTest.Text))
            {
                TemplateInfo templateInfo = new TemplateInfo();
                NameValueCollection values = new NameValueCollection();
                values.Add("id", "1");
                templateInfo.QueryList = values;
                templateInfo.TemplateContent = this.TxtTemplateTest.Text;
                templateInfo.CurrentPage = 1;
                templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                templateInfo.PageName = string.Empty;
                templateInfo = TemplateTransform.GetHtml(templateInfo);
                this.LabShow.Text = templateInfo.TemplateContent;
            }
            else
            {
                this.LabShow.Text = "请输入测试标签";
            }
        }

        protected void BuildLabelList(string typename)
        {
            this.LblLabelList.Text = string.Empty;
            foreach (LabelManageInfo info in LabelManage.GetLabelList(typename))
            {
                string text = this.LblLabelList.Text;
                this.LblLabelList.Text = text + "<div onclick=\"cit()\" outype=\"1\" class=\"spanfixdiv\" alt=\"" + info.Intro + "\">" + info.Name + "</div>";
            }
        }

        protected void DropLabelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BuildLabelList(this.DropLabelList.SelectedValue);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.action = BasePage.RequestString("action");
            this.labelname = BasePage.RequestString("name");
            this.m_LabelLibPath = "~/" + SiteConfig.SiteOption.LabelDir;
            if (string.IsNullOrEmpty(this.labelname))
            {
                BasePage.ResponseRedirect("Label.aspx");
            }
            else
            {
                string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                this.xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + this.labelname + ".config";
                if (!base.IsPostBack)
                {
                    if (LabelManage.GetAttributeList(this.xmlfilepath).Count == 0)
                    {
                        this.attlist.Text = "您尚未建立属性!<a href=\"LabelProperty.aspx?action=" + this.action + "&name=" + this.labelname + "\">建立属性</a>";
                    }
                    else
                    {
                        foreach (LabelAttributeInfo info in LabelManage.GetAttributeList(this.xmlfilepath))
                        {
                            string text = this.attlist.Text;
                            this.attlist.Text = text + "<div onclick=\"cit()\" outype=\"2\" class=\"spanfixdiv\" alt=\"" + info.Intro + "&#10默认值：" + info.DefaultValue + "\">" + info.AttributeName + "</div>";
                        }
                    }
                    if (!string.IsNullOrEmpty(XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelTemplate")))
                    {
                        this.TxtTemplate.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelTemplate");
                    }
                    ListItem item = new ListItem();
                    item.Text = "全部分类";
                    this.DropLabelList.DataSource = LabelManage.GetLabelTypeList();
                    this.DropLabelList.DataTextField = "Name";
                    this.DropLabelList.DataValueField = "Name";
                    this.DropLabelList.DataBind();
                    this.DropLabelList.Items.Insert(0, item);
                    this.BuildLabelList(string.Empty);
                    this.TxtTemplateTest.Text = "{PE.Label id=\"" + this.labelname + "\" /}";
                }
                this.TxtTemplate.Attributes.Add("onmouseup", "dragend(this)");
                this.TxtTemplate.Attributes.Add("onClick", "savePos(this)");
                this.TxtTemplate.Attributes.Add("onmousemove", "DragPos(this)");
            }
            this.SmpNavigator.AdditionalNode = this.labelname;
        }
    }
}

