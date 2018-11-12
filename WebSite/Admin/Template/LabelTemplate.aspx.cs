namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Xml.XPath;

    public partial class LabelTemplate : AdminPage
    {
        private string action;
        private string Dbtype;
        private string m_LabelLibPath;
        public string m_LabelName;
        private string xmlfilepath;
        private XmlNode rootnode;


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("LabelManage.aspx");
        }

        protected void BtnFinal_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    new XmlDocument().LoadXml(this.TxtTemplate.Text);
                }
                catch (XmlException)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, base.GetType(), "click", "alert('标签模板中存在错误，请检查后再保存！')", true);
                    return;
                }
                if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelTemplate", this.TxtTemplate.Text))
                {
                    File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.m_LabelName + ".config", true);
                    BasePage.ResponseRedirect("LabelManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("没有标签目录或临时目录或标签文件的访问权限！", "LabelManage.aspx");
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
            if ((string.Compare(this.Dbtype, "xml_read", StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(this.Dbtype, "sql_sysstoredquery", StringComparison.OrdinalIgnoreCase) == 0))
            {
                BasePage.ResponseRedirect("LabelProperty.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
            }
            else
            {
                BasePage.ResponseRedirect("LabelSqlBuild.aspx?action=" + this.action + "&name=" + base.Server.UrlEncode(this.m_LabelName));
            }
        }

        protected void BtnShowDetal_Click(object sender, EventArgs e)
        {
            this.BtnShowSchema.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.BtnShowDetal.BackColor = Color.Red;
            this.ShowXml.Text = this.ShowXml.Text = this.GetXmlScheam(0).ToString();
        }

        protected void BtnShowSchema_Click(object sender, EventArgs e)
        {
            this.BtnShowSchema.BackColor = Color.Red;
            this.BtnShowDetal.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.ShowXml.Text = this.ShowXml.Text = this.GetXmlScheam(2).ToString();
        }

        protected void BtnShowTemplate_Click(object sender, EventArgs e)
        {
            if (XmlManage.SaveFileNode(this.xmlfilepath, "root", "LabelTemplate", this.TxtTemplate.Text))
            {
                File.Copy(this.xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.m_LabelName + ".config", true);
            }
            SiteCache.Remove("pe_labdefing_" + this.m_LabelName);
            if (!string.IsNullOrEmpty(this.TxtTemplateTest.Text))
            {
                TemplateInfo templateInfo = new TemplateInfo();
                NameValueCollection values = new NameValueCollection();
                values.Add("id", "1");
                templateInfo.QueryList = values;
                templateInfo.TemplateContent = this.TxtTemplateTest.Text;
                templateInfo.CurrentPage = DataConverter.CLng(this.TxtTempPage.Text);
                templateInfo.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                templateInfo.PageName = this.TxtTempPageName.Text;
                templateInfo = TemplateTransform.GetHtml(templateInfo);
                this.LabShow.Text = templateInfo.TemplateContent;
                if (templateInfo.TotalPub > 0)
                {
                    this.LabShow.Text = this.LabShow.Text + "<br/>本页取得的总数据输出量为：" + templateInfo.TotalPub.ToString();
                }
                if (templateInfo.PageNum > 0)
                {
                    this.LabShow.Text = this.LabShow.Text + "<br/>本页分页数为：" + templateInfo.PageNum.ToString();
                }
            }
        }

        protected void BtnXml_Click(object sender, EventArgs e)
        {
            base.Response.Write("<script>windows.open('LabelXmlShow.aspx?name=" + base.Server.UrlEncode(this.m_LabelName) + "','_blank')</script>");
        }

        protected StringBuilder GetXmlScheam(int type)
        {
            StringBuilder builder = new StringBuilder();
            foreach (XmlScheme scheme in XmlManage.GetXmlTree(this.rootnode, 0, 0, string.Empty, type, 0))
            {
                int num = 0;
                builder.Append("<div id=\"fixdiv\">");
                while (num < scheme.Level)
                {
                    if (num > 0)
                    {
                        builder.Append("&nbsp;&nbsp;|");
                    }
                    else
                    {
                        builder.Append("|");
                    }
                    num++;
                }
                if (scheme.Level > 0)
                {
                    string str;
                    builder.Append("-");
                    if (scheme.Repnum > 1)
                    {
                        str = "0";
                    }
                    else if (string.Compare(scheme.Type, "attributes", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        str = "2";
                    }
                    else
                    {
                        str = "1";
                    }
                    builder.Append("<span path=\"" + scheme.Path + "\" tn=\"" + scheme.Name + "\" onclick=\"cit()\" outype=\"" + str + "\"");
                }
                else
                {
                    builder.Append("<span");
                }
                string str2 = scheme.Type;
                if (str2 == null)
                {
                    goto Label_017D;
                }
                if (!(str2 == "havechile"))
                {
                    if (str2 == "attributes")
                    {
                        goto Label_0158;
                    }
                    goto Label_017D;
                }
                builder.Append(" class=\"havechilediv\">");
                builder.Append(scheme.Name);
                goto Label_0196;
            Label_0158:
                builder.Append(" class=\"attribdiv\">");
                builder.Append("@" + scheme.Name);
                goto Label_0196;
            Label_017D:
                builder.Append(" class=\"nodediv\">");
                builder.Append(scheme.Name);
            Label_0196:
                builder.Append("</span>");
                if (scheme.Repnum > 1)
                {
                    builder.Append("[repet:" + scheme.Repnum.ToString() + "]");
                }
                builder.Append("</div>");
            }
            return builder;
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
                this.Dbtype = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataType");
                string presstr = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                string str3 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlString");
                string istr = string.Empty;
                XmlDocument document = new XmlDocument();
                document.Load(this.xmlfilepath);
                XmlNodeList attriblist = document.SelectNodes("root/attributes");
                presstr = this.ParaProc(attriblist, presstr);
                str3 = this.ParaProc(attriblist, str3).Replace("@pagesize", "10").Replace("@startrow", "0");
                if (((string.Compare(this.Dbtype, "static", StringComparison.Ordinal) == 0) && (string.Compare(this.Dbtype, "xml_read", StringComparison.Ordinal) == 0)) && string.IsNullOrEmpty(str3))
                {
                    this.ShowXml.Text = "查询语句为空！";
                    return;
                }
                if (((string.Compare(this.Dbtype, "static", StringComparison.Ordinal) != 0) && (string.Compare(this.Dbtype, "sql_sysquery", StringComparison.Ordinal) != 0)) && ((string.Compare(this.Dbtype, "sql_sysstoredquery", StringComparison.Ordinal) != 0) && string.IsNullOrEmpty(presstr)))
                {
                    this.ShowXml.Text = "数据源地址为空！";
                    return;
                }
                istr = LabelManage.GetDBQuery(this.Dbtype, presstr, str3, attriblist);
                XmlDocument document2 = (XmlDocument) this.XmlProc(this.Dbtype, presstr, istr);
                if (!string.IsNullOrEmpty(document2.OuterXml))
                {
                    this.rootnode = document2.DocumentElement;
                    this.ShowXml.Text = this.GetXmlScheam(2).ToString();
                }
                else
                {
                    document2.LoadXml("<root></root>");
                    this.rootnode = document2.DocumentElement;
                    this.ShowXml.Text = this.GetXmlScheam(2).ToString();
                }
                if (LabelManage.GetAttributeList(this.xmlfilepath).Count == 0)
                {
                    this.attlist.Text = "您尚未添加参数!<a href=\"LabelProperty.aspx?action=" + this.action + "&name=" + this.m_LabelName + "\">添加参数</a>";
                }
                else
                {
                    foreach (LabelAttributeInfo info in LabelManage.GetAttributeList(this.xmlfilepath))
                    {
                        string text = this.attlist.Text;
                        this.attlist.Text = text + "<div onclick=\"cit()\" outype=\"3\" class=\"spanfixdiv\" alt=\"" + info.Intro + "&#10默认值：" + info.DefaultValue + "\">" + info.AttributeName + "</div>";
                    }
                }
                this.TxtTemplate.Attributes.Add("onmouseup", "dragend(this)");
                this.TxtTemplate.Attributes.Add("onClick", "savePos(this)");
                this.TxtTemplate.Attributes.Add("onmousemove", "DragPos(this)");
                if (!base.IsPostBack)
                {
                    string filename = HttpContext.Current.Server.MapPath("~/") + @"\Temp\DefaultLabelTemplate.xsl";
                    if (string.Compare(this.action, "modify", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        this.TxtTemplate.Text = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelTemplate").Replace("><", ">\n<");
                    }
                    else
                    {
                        XmlDocument document3 = new XmlDocument();
                        document3.Load(filename);
                        this.TxtTemplate.Text = document3.OuterXml.Replace("><", ">\n<");
                    }
                    this.TxtTemplateTest.Text = "{PE.Label id=\"" + this.m_LabelName + "\" /}";
                }
            }
            this.SmpNavigator.AdditionalNode = this.m_LabelName;
        }

        protected string ParaProc(XmlNodeList attriblist, string presstr)
        {
            foreach (XmlNode node in attriblist)
            {
                string[] strArray = node.SelectSingleNode("default").InnerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                if (strArray.Length > 1)
                {
                    presstr = presstr.Replace("@" + node.SelectSingleNode("name").InnerText, strArray[0]);
                }
                else
                {
                    presstr = presstr.Replace("@" + node.SelectSingleNode("name").InnerText, node.SelectSingleNode("default").InnerText);
                }
            }
            return presstr;
        }

        protected IXPathNavigable XmlProc(string itype, string isource, string istr)
        {
            XmlDocument document = new XmlDocument();
            if (string.Compare(itype, "xml_read", StringComparison.Ordinal) == 0)
            {
                string str;
                HttpContext current = HttpContext.Current;
                if (isource.ToLower().IndexOf("http") < 0)
                {
                    if (current != null)
                    {
                        str = current.Server.MapPath("~/" + isource);
                    }
                    else
                    {
                        str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, isource);
                    }
                }
                else
                {
                    str = isource;
                }
                try
                {
                    document.Load(str);
                }
                catch (XmlException exception)
                {
                    this.ShowXml.Text = exception.ToString();
                }
                return document;
            }
            if (string.Compare(itype, "static", StringComparison.Ordinal) != 0)
            {
                if (string.IsNullOrEmpty(istr))
                {
                    this.ShowXml.Text = "查询结果为空，请检查数据库中是否有数据，或查询条件是否正确！";
                    return document;
                }
                if (string.Compare(istr, "queryerr", StringComparison.Ordinal) == 0)
                {
                    this.ShowXml.Text = "查询错，请检查您的查询语句是否符合规则！";
                    return document;
                }
                try
                {
                    document.LoadXml(istr);
                }
                catch (XmlException exception2)
                {
                    this.ShowXml.Text = exception2.ToString();
                }
            }
            return document;
        }
    }
}

