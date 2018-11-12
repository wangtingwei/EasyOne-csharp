namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Xml;

    public partial class LabelXmlShow : AdminPage
    {
        private string Dbtype;
        private string labelname;
        private string xmlfilepath;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.labelname = BasePage.RequestString("name");
            if (!string.IsNullOrEmpty(this.labelname))
            {
                string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                this.xmlfilepath = HttpContext.Current.Server.MapPath(path) + @"\" + this.labelname + ".config";
                this.Dbtype = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataType");
                string presstr = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelDataSource");
                string str3 = XmlManage.ReadFileNode(this.xmlfilepath, "root/LabelSqlString");
                string istr = string.Empty;
                XmlDocument document = new XmlDocument();
                document.Load(this.xmlfilepath);
                XmlNodeList attriblist = document.SelectNodes("root/attributes");
                presstr = this.ParaProc(attriblist, presstr);
                str3 = this.ParaProc(attriblist, str3).Replace("@pagesize", "10").Replace("@startrow", "0");
                if ((string.Compare(this.Dbtype, "xml_read", StringComparison.OrdinalIgnoreCase) != 0) && string.IsNullOrEmpty(str3))
                {
                    base.Response.Write("查询语句为空！");
                }
                else
                {
                    istr = LabelManage.GetDBQuery(this.Dbtype, presstr, str3, attriblist);
                    this.XmlProc(this.Dbtype, presstr, istr);
                }
            }
            else
            {
                base.Response.Write("标签名称为空！");
            }
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

        protected void XmlProc(string dbtype, string isource, string istr)
        {
            XmlDocument document = new XmlDocument();
            if ((string.Compare(dbtype, "static", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(dbtype, "xml_read", StringComparison.Ordinal) != 0))
            {
                if (string.IsNullOrEmpty(istr))
                {
                    base.Response.Write("查询结果为空，请检查数据库中是否有数据，或查询条件是否正确！");
                }
                else
                {
                    string str2;
                    if (((str2 = istr) != null) && (str2 == "queryerr"))
                    {
                        base.Response.Write("查询错，请检查您的查询语句是否符合规则！");
                    }
                    else
                    {
                        try
                        {
                            document.LoadXml(istr);
                        }
                        catch (XmlException exception)
                        {
                            base.Response.Write(exception.Message);
                            return;
                        }
                        base.Response.Clear();
                        base.Response.Buffer = true;
                        base.Response.Charset = "utf-8";
                        base.Response.AddHeader("Content-Disposition", "attachment;filename=labelout.xml");
                        base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                        base.Response.ContentType = "Application/Octet-Stream";
                        base.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        base.Response.Write(document.OuterXml);
                    }
                }
            }
            else if (string.Compare(dbtype, "xml_read", StringComparison.Ordinal) == 0)
            {
                string str;
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str = current.Server.MapPath("~/" + isource);
                }
                else
                {
                    str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, isource);
                }
                try
                {
                    document.Load(str);
                }
                catch (XmlException exception2)
                {
                    base.Response.Write(exception2.Message);
                    return;
                }
                base.Response.Clear();
                base.Response.Buffer = true;
                base.Response.Charset = "utf-8";
                base.Response.AddHeader("Content-Disposition", "attachment;filename=labelout.xml");
                base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                base.Response.ContentType = "Application/Octet-Stream";
                base.Response.Write(document.OuterXml);
            }
        }
    }
}

