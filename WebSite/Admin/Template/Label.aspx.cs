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
    using System.Xml;

    public partial class LabelUI : AdminPage
    {
        private string m_LabelLibPath;
        private string m_LabelName;

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            if ((this.RbtDataType.SelectedIndex <= 2) || this.TestDbSource())
            {
                string iname = this.TxtLabelName.Text.Trim();
                string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                string xmlfilepath = string.Empty;
                if (!string.IsNullOrEmpty(this.m_LabelName))
                {
                    xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + this.ViewState["oldlabelname"].ToString() + ".config";
                }
                else
                {
                    xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + iname + ".config";
                }
                if (this.SaveToXmlFile(xmlfilepath))
                {
                    if (string.IsNullOrEmpty(this.m_LabelName))
                    {
                        if (!LabelManage.Exists(iname))
                        {
                            BasePage.ResponseRedirect("LabelProperty.aspx?action=add&name=" + base.Server.UrlEncode(iname));
                        }
                        else
                        {
                            this.NReq.Text = "该标签已存在！";
                            this.NReq.IsValid = false;
                        }
                    }
                    else if (string.Compare(this.ViewState["oldlabelname"].ToString(), iname, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        if (File.Exists(base.Server.MapPath(this.m_LabelLibPath) + @"\" + iname + ".config"))
                        {
                            this.NReq.Text = "您不能改成已存在的标签名！";
                            this.NReq.IsValid = false;
                        }
                        else
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.ViewState["oldlabelname"].ToString() + ".config");
                            File.Copy(xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + iname + ".config");
                            if (File.Exists(base.Server.MapPath(path) + @"\" + iname + ".config"))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(path) + @"\" + iname + ".config");
                            }
                            File.Move(xmlfilepath, HttpContext.Current.Server.MapPath(path) + @"\" + iname + ".config");
                            BasePage.ResponseRedirect("LabelProperty.aspx?action=modify&name=" + base.Server.UrlEncode(iname));
                        }
                    }
                    else
                    {
                        BasePage.ResponseRedirect("LabelProperty.aspx?action=modify&name=" + base.Server.UrlEncode(iname));
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("保存失败！", "LabelManage.aspx");
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if ((this.RbtDataType.SelectedIndex <= 2) || this.TestDbSource())
            {
                string strB = this.TxtLabelName.Text.Trim();
                string strA = this.ViewState["oldlabelname"].ToString();
                if ((string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) != 0) && File.Exists(base.Server.MapPath(this.m_LabelLibPath) + @"\" + strB + ".config"))
                {
                    this.NReq.Text = "您不能改成已存在的标签名！";
                    this.NReq.IsValid = false;
                }
                else
                {
                    string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                    string xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + strA + ".config";
                    if (this.SaveToXmlFile(xmlfilepath))
                    {
                        if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + strA + ".config");
                            File.Copy(xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + strB + ".config");
                            if (File.Exists(base.Server.MapPath(path) + @"\" + strB + ".config"))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(path) + @"\" + strB + ".config");
                            }
                            File.Move(xmlfilepath, HttpContext.Current.Server.MapPath(path) + @"\" + strB + ".config");
                        }
                        else
                        {
                            File.Copy(xmlfilepath, HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + strB + ".config", true);
                        }
                        BasePage.ResponseRedirect("LabelManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("保存失败！", "LabelManage.aspx");
                    }
                }
            }
        }

        protected void BtnTestDataSource_Click(object sender, EventArgs e)
        {
            this.TestDbSource();
        }

        private static string GetDbPath(string dbPath)
        {
            if (!string.IsNullOrEmpty(dbPath))
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    return current.Server.MapPath("~/" + dbPath);
                }
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);
            }
            return null;
        }

        private void InitDropLabelType()
        {
            this.DropLabelType.DataSource = LabelManage.GetLabelTypeList();
            this.DropLabelType.DataTextField = "Name";
            this.DropLabelType.DataValueField = "Name";
            this.DropLabelType.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部分类";
            item.Value = "全部分类";
            this.DropLabelType.Items.Insert(0, item);
        }

        protected void JumpUrl(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LinkJump.SelectedValue))
            {
                BasePage.ResponseRedirect(this.LinkJump.SelectedValue);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_LabelName = BasePage.RequestString("name").Trim();
            this.m_LabelLibPath = "~/" + SiteConfig.SiteOption.LabelDir;
            this.InitDropLabelType();
            this.DropLabelType.Attributes.Add("onChange", "addclass('" + this.DropLabelType.ClientID + "','" + this.TxtLabelType.ClientID + "')");
            if (!this.Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.m_LabelName))
                {
                    this.TxtLabelName.Text = this.m_LabelName;
                    if (!string.IsNullOrEmpty(this.TxtLabelName.Text))
                    {
                        string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                        string xmlpath = HttpContext.Current.Server.MapPath(path) + @"\" + this.TxtLabelName.Text + ".config";
                        this.SetInit(xmlpath);
                    }
                    else
                    {
                        this.PanelOutSide.Visible = false;
                    }
                }
                else
                {
                    this.TxtLabelName.Text = this.m_LabelName;
                    this.ViewState["oldlabelname"] = this.m_LabelName;
                    string str = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                    string str2 = HttpContext.Current.Server.MapPath(str) + @"\" + this.m_LabelName + ".config";
                    string str3 = HttpContext.Current.Server.MapPath(this.m_LabelLibPath) + @"\" + this.m_LabelName + ".config";
                    if (File.Exists(str3))
                    {
                        if (File.Exists(str2))
                        {
                            File.Delete(str2);
                        }
                        File.Copy(str3, str2, true);
                    }
                    this.SetInit(str2);
                    string str4 = XmlManage.ReadFileNode(str2, "root/OutType");
                    this.BtnSave.Visible = true;
                    this.LinkJump.Visible = true;
                    ListItem item = new ListItem();
                    item.Text = "跳转到";
                    this.LinkJump.Items.Add(item);
                    this.LinkJump.AutoPostBack = true;
                    ListItem item2 = new ListItem();
                    item2.Text = "设置标签参数";
                    item2.Value = "LabelProperty.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                    this.LinkJump.Items.Add(item2);
                    ListItem item3 = new ListItem();
                    string selectedValue = this.RbtDataType.SelectedValue;
                    if (selectedValue != null)
                    {
                        if (!(selectedValue == "static"))
                        {
                            if ((selectedValue == "sql_sysstoredquery") || (selectedValue == "xml_read"))
                            {
                                item3.Text = "标签内容编辑";
                                item3.Value = "LabelTemplate.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                                this.LinkJump.Items.Add(item3);
                                goto Label_0412;
                            }
                        }
                        else
                        {
                            item3.Text = "标签内容编辑";
                            switch (str4)
                            {
                                case "txt":
                                case "xml":
                                    item3.Value = "LabelTemplateStatic.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                                    break;

                                default:
                                    item3.Value = "LabelTemplate.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                                    break;
                            }
                            this.LinkJump.Items.Add(item3);
                            goto Label_0412;
                        }
                    }
                    item3.Text = "标签查询设置";
                    item3.Value = "LabelSqlBuild.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                    this.LinkJump.Items.Add(item3);
                    ListItem item4 = new ListItem();
                    item4.Text = "标签内容编辑";
                    item4.Value = "LabelTemplate.aspx?action=modify&name=" + base.Server.UrlEncode(this.m_LabelName);
                    this.LinkJump.Items.Add(item4);
                }
            }
        Label_0412:
            this.SmpNavigator.AdditionalNode = this.m_LabelName;
        }

        protected void RbtDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TxtTestStat.Text = string.Empty;
            if (string.Compare(this.RbtDataType.SelectedValue, "static", StringComparison.Ordinal) == 0)
            {
                this.RBLOutType.Items[0].Selected = true;
                this.RBLOutType.Items[1].Selected = false;
            }
            else
            {
                this.RBLOutType.Items[0].Selected = false;
                this.RBLOutType.Items[1].Selected = true;
            }
            switch (this.RbtDataType.SelectedValue)
            {
                case "static":
                    this.PanelOutSide.Visible = false;
                    return;

                case "sql_sysquery":
                    this.PanelOutSide.Visible = false;
                    return;

                case "sql_sysstoredquery":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = false;
                    this.TxtDataSource.Text = "存储过程名称";
                    return;

                case "sql_outquery":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "Server=;Database=;uid=;pwd=";
                    return;

                case "odbc_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "DSN=;Uid=;Pwd=;";
                    return;

                case "mdb_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "temp/test.mdb";
                    return;

                case "xsl_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "temp/test.xls";
                    return;

                case "ole_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "Data Source=;Provider=Microsoft.JET.OLEDB.4.0;";
                    return;

                case "orc_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "Data Source=;User Id=;Password=;Integrated Security=no;";
                    return;

                case "xml_read":
                    this.PanelOutSide.Visible = true;
                    this.BtnTestDataSource.Visible = true;
                    this.TxtDataSource.Text = "temp/test.xml";
                    return;
            }
        }

        protected bool SaveToXmlFile(string xmlfilepath)
        {
            string path = HttpContext.Current.Server.MapPath(this.m_LabelLibPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(xmlfilepath))
            {
                XmlDocument document = new XmlDocument();
                XmlNode newChild = document.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("", "root", "");
                document.AppendChild(element);
                document.Save(xmlfilepath);
            }
            string text = this.TxtLabelType.Text;
            if (text == "全部分类")
            {
                text = string.Empty;
            }
            if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelType", text))
            {
                return false;
            }
            if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelIntro", this.TxtLabelIntro.Text))
            {
                return false;
            }
            if (!XmlManage.SaveFileNode(xmlfilepath, "root", "OutType", this.RBLOutType.SelectedValue))
            {
                return false;
            }
            if ((string.Compare(this.RbtDataType.SelectedValue, "sql_sysstoredquery", StringComparison.Ordinal) == 0) && !XmlManage.SaveFileNode(xmlfilepath, "root", "LabelSqlString", this.TxtDataSource.Text))
            {
                return false;
            }
            if (this.RbtDataType.SelectedIndex > 2)
            {
                string nodevalue = this.TxtDataSource.Text;
                string selectedValue = this.RbtDataType.SelectedValue;
                if (string.Compare(this.RbtDataType.SelectedValue, "mdb_read", StringComparison.Ordinal) == 0)
                {
                    nodevalue = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + GetDbPath(this.TxtDataSource.Text);
                    selectedValue = "ole_read";
                    if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelDbPath", this.TxtDataSource.Text))
                    {
                        return false;
                    }
                }
                if (string.Compare(this.RbtDataType.SelectedValue, "xsl_read", StringComparison.Ordinal) == 0)
                {
                    nodevalue = "Provider=Microsoft.JET.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + GetDbPath(this.TxtDataSource.Text);
                    selectedValue = "ole_read";
                    if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelDbPath", this.TxtDataSource.Text))
                    {
                        return false;
                    }
                }
                if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelDataSource", nodevalue))
                {
                    return false;
                }
                if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelDataType", selectedValue))
                {
                    return false;
                }
            }
            else if (!XmlManage.SaveFileNode(xmlfilepath, "root", "LabelDataType", this.RbtDataType.SelectedValue))
            {
                return false;
            }
            return true;
        }

        protected void SetInit(string xmlpath)
        {
            this.TxtLabelType.Text = XmlManage.ReadFileNode(xmlpath, "root/LabelType");
            this.TxtLabelIntro.Text = XmlManage.ReadFileNode(xmlpath, "root/LabelIntro");
            string strB = XmlManage.ReadFileNode(xmlpath, "root/LabelDataType");
            string str2 = XmlManage.ReadFileNode(xmlpath, "root/OutType");
            for (int i = 0; i < this.RbtDataType.Items.Count; i++)
            {
                if ((string.Compare(this.RbtDataType.Items[i].Value, strB, StringComparison.OrdinalIgnoreCase) == 0) && !this.RbtDataType.Items[i].Selected)
                {
                    this.RbtDataType.Items[i].Selected = true;
                    break;
                }
            }
            for (int j = 0; j < this.RBLOutType.Items.Count; j++)
            {
                if ((string.Compare(this.RBLOutType.Items[j].Value, str2, StringComparison.OrdinalIgnoreCase) == 0) && !this.RBLOutType.Items[j].Selected)
                {
                    this.RBLOutType.Items[j].Selected = true;
                    break;
                }
            }
            if ((string.Compare(strB, "static", StringComparison.Ordinal) == 0) || (string.Compare(strB, "sql_sysquery", StringComparison.Ordinal) == 0))
            {
                this.PanelOutSide.Visible = false;
            }
            else if (this.PanelOutSide.Visible)
            {
                if (string.Compare(strB, "ole_read", StringComparison.Ordinal) == 0)
                {
                    string str3 = XmlManage.ReadFileNode(xmlpath, "root/LabelDbPath");
                    if (string.IsNullOrEmpty(str3))
                    {
                        this.TxtDataSource.Text = XmlManage.ReadFileNode(xmlpath, "root/LabelDataSource");
                    }
                    else
                    {
                        this.TxtDataSource.Text = str3;
                    }
                }
                else if (string.Compare(strB, "sql_sysstoredquery", StringComparison.Ordinal) == 0)
                {
                    this.TxtDataSource.Text = XmlManage.ReadFileNode(xmlpath, "root/LabelSqlString");
                    this.BtnTestDataSource.Visible = false;
                }
                else
                {
                    this.TxtDataSource.Text = XmlManage.ReadFileNode(xmlpath, "root/LabelDataSource");
                }
            }
        }

        private bool TestDbSource()
        {
            if (string.IsNullOrEmpty(this.TxtDataSource.Text))
            {
                this.TxtTestStat.Text = "请输入正确的代码！";
                return false;
            }
            string text = this.TxtDataSource.Text;
            string selectedValue = this.RbtDataType.SelectedValue;
            if (string.Compare(selectedValue, "mdb_read", StringComparison.Ordinal) == 0)
            {
                text = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + GetDbPath(this.TxtDataSource.Text);
                selectedValue = "ole_read";
            }
            if (string.Compare(selectedValue, "xsl_read", StringComparison.Ordinal) == 0)
            {
                text = "Provider=Microsoft.JET.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + GetDbPath(this.TxtDataSource.Text);
                selectedValue = "ole_read";
            }
            if (LabelManage.TestOutSideDatabase(selectedValue, text))
            {
                this.TxtTestStat.Text = "连接成功！";
                return true;
            }
            this.TxtTestStat.Text = "连接失败！";
            return false;
        }
    }
}

