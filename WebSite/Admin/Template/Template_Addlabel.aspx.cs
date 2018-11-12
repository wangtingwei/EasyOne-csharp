namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class Template_Addlabel : AdminPage
    {
        public string outstr;

        protected StringBuilder CtrlProc(string aname, string avalue, string defaultValue, string aintro, string[] sArray)
        {
            StringBuilder builder = new StringBuilder();
            if (sArray.Length > 1)
            {
                builder.Append("<tr><td align=\"right\"><span class=\"listspan\" sid=\"" + aname + "\" defaultvalue=\"" + defaultValue + "\" stype=\"1\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
                builder.Append("<td align=\"left\"><select name=\"" + aname + "\">");
                for (int i = 0; i < sArray.Length; i++)
                {
                    if (i == DataConverter.CLng(avalue))
                    {
                        builder.Append(string.Concat(new object[] { "<option value='", i, "' selected>", sArray[i], "</option>" }));
                    }
                    else
                    {
                        builder.Append(string.Concat(new object[] { "<option value='", i, "'>", sArray[i], "</option>" }));
                    }
                }
                builder.Append("</select></td></tr>");
                return builder;
            }
            if (string.Compare(aname.ToLower(), "nodelist", StringComparison.Ordinal) == 0)
            {
                builder.Append("<tr><td align=\"right\"><span class=\"listspan\" sid=\"" + aname + "\"  defaultvalue=\"" + defaultValue + "\" stype=\"3\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
                builder.Append("<td  align=\"left\"><select name=\"" + aname + "\" style=\"height:100px;width:200px;\" multiple>");
                foreach (NodeInfo info in Nodes.GetNodeNameForContainerItems())
                {
                    if (info.NodeType != NodeType.Container)
                    {
                        continue;
                    }
                    bool flag = true;
                    if (!string.IsNullOrEmpty(avalue))
                    {
                        char ch = Convert.ToChar(",");
                        string[] strArray = avalue.Split(new char[] { ch });
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (DataConverter.CLng(strArray[j], -1) == info.NodeId)
                            {
                                flag = false;
                                builder.Append("<option value='" + info.NodeId.ToString() + "' selected>" + info.NodeName + "</option>");
                            }
                        }
                    }
                    if (flag)
                    {
                        builder.Append("<option value='" + info.NodeId.ToString() + "'>" + info.NodeName + "</option>");
                    }
                }
                builder.Append("</select></td></tr>");
                return builder;
            }
            if (string.Compare(aname.ToLower(), "speciallist", StringComparison.Ordinal) == 0)
            {
                builder.Append("<tr><td align=\"right\"><span class=\"listspan\" sid=\"" + aname + "\" defaultvalue=\"" + defaultValue + "\" stype=\"3\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
                builder.Append("<td  align=\"left\"><select name=\"" + aname + "\" style=\"height:100px;width:200px;\" multiple>");
                foreach (SpecialInfo info2 in Special.GetSpecialList())
                {
                    bool flag2 = true;
                    if (!string.IsNullOrEmpty(avalue))
                    {
                        char ch2 = Convert.ToChar(",");
                        string[] strArray2 = avalue.Split(new char[] { ch2 });
                        for (int k = 0; k < strArray2.Length; k++)
                        {
                            if (DataConverter.CLng(strArray2[k], -1) == info2.SpecialId)
                            {
                                flag2 = false;
                                builder.Append("<option value='" + info2.SpecialId.ToString() + "' selected>" + info2.SpecialName + "</option>");
                            }
                        }
                    }
                    if (flag2)
                    {
                        builder.Append("<option value='" + info2.SpecialId.ToString() + "'>" + info2.SpecialName + "</option>");
                    }
                }
                builder.Append("</select></td></tr>");
                return builder;
            }
            switch (avalue)
            {
                case "false":
                    builder.Append("<tr><td align=\"right\"><span sid=\"" + aname + "\" defaultvalue=\"" + defaultValue + "\" stype=\"2\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
                    builder.Append("<td align=\"left\"><input type=\"checkbox\" id=\"" + aname + "\" /></td></tr>");
                    return builder;

                case "true":
                    builder.Append("<tr><td align=\"right\"><span sid=\"" + aname + "\" defaultvalue=\"" + defaultValue + "\" stype=\"2\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
                    builder.Append("<td align=\"left\"><input type=\"checkbox\" id=\"" + aname + "\" checked /></td></tr>");
                    return builder;
            }
            builder.Append("<tr><td align=\"right\"><span sid=\"" + aname + "\" defaultvalue=\"" + defaultValue + "\" stype=\"0\" title=\"" + aname + "\">" + aintro + "</span>：</td>");
            builder.Append("<td align=\"left\"><input type=\"text\" id=\"" + aname + "\" value=\"" + avalue + "\"/></td></tr>");
            return builder;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestString("a") == "m")
            {
                this.ShowLabelModify();
            }
            else
            {
                this.ShowLabelAdd();
            }
        }

        protected void ShowLabelAdd()
        {
            string str = BasePage.RequestString("n");
            if (!string.IsNullOrEmpty(str))
            {
                this.LabelName.Text = str;
                LabelManageInfo labelByName = LabelManage.GetLabelByName(str);
                if (!labelByName.IsNull)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<table width='100%'>");
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(labelByName.Define.ToString());
                    foreach (XmlNode node in document.SelectNodes("root/attributes"))
                    {
                        string innerText = node.SelectSingleNode("default").InnerText;
                        string[] sArray = innerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                        builder.Append(this.CtrlProc(node.SelectSingleNode("name").InnerText, innerText, innerText, node.SelectSingleNode("intro").InnerText, sArray));
                    }
                    string input = (document.SelectSingleNode("root/UsePage") == null) ? "" : document.SelectSingleNode("root/UsePage").InnerText.Trim();
                    if (DataConverter.CBoolean(input))
                    {
                        builder.Append("<tr><td align=\"right\">是否分页：</td><td align=\"left\"><input id=\"page\" type=\"checkbox\"/></td></tr>");
                        builder.Append("<tr><td align=\"right\">分页显示数：</td><td align=\"left\"><input type=\"text\" id=\"pagesize\" value=\"10\"/></td></tr>");
                        builder.Append("<tr><td align=\"right\">是否主分页：</td><td align=\"left\"><input id=\"urlpage\" type=\"checkbox\" value=\"true\"/></td></tr>");
                    }
                    builder.Append("<tr><td align=\"right\">缓存时间：</td><td align=\"left\"><input type=\"text\" id=\"cachetime\" value=\"0\" /></td></tr>");
                    builder.Append("<tr><td align=\"right\">不解析内部标签：</td><td align=\"left\"><input id=\"nolabelproc\" type=\"checkbox\"/></td></tr>");
                    builder.Append("<tr><td align=\"right\">容器类型：</td><td align=\"left\"><input type=\"text\" id=\"spantype\" /></td></tr>");
                    builder.Append("<tr><td align=\"right\">容器风格：</td><td align=\"left\"><input type=\"text\" id=\"cssname\" /></td></tr>");
                    builder.Append("</table>");
                    this.labelbody.Text = builder.ToString();
                    input = (document.SelectSingleNode("root/LabelIntro") == null) ? "" : document.SelectSingleNode("root/LabelIntro").InnerText.Trim();
                    if (!string.IsNullOrEmpty(input))
                    {
                        this.labelintro.Text = input;
                    }
                }
            }
        }

        protected void ShowLabelModify()
        {
            string labelName = string.Empty;
            string str2 = BasePage.RequestString("n");
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = str2.Replace("{", "<").Replace("}", ">");
                XmlDocument document = new XmlDocument();
                document.LoadXml(str2);
                XmlElement documentElement = document.DocumentElement;
                if (documentElement.HasAttribute("id"))
                {
                    labelName = documentElement.GetAttribute("id");
                    this.LabelName.Text = labelName;
                    LabelManageInfo labelByName = LabelManage.GetLabelByName(labelName);
                    if (!labelByName.IsNull)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<table width='100%'>");
                        XmlDocument document2 = new XmlDocument();
                        document2.LoadXml(labelByName.Define.ToString());
                        foreach (XmlNode node in document2.SelectNodes("root/attributes"))
                        {
                            string innerText = node.SelectSingleNode("name").InnerText;
                            string avalue = string.Empty;
                            if (documentElement.HasAttribute(innerText))
                            {
                                avalue = documentElement.GetAttribute(innerText);
                            }
                            else
                            {
                                avalue = node.SelectSingleNode("default").InnerText;
                            }
                            string[] sArray = node.SelectSingleNode("default").InnerText.Split(new string[] { "|||" }, StringSplitOptions.None);
                            builder.Append(this.CtrlProc(node.SelectSingleNode("name").InnerText, avalue, node.SelectSingleNode("default").InnerText, node.SelectSingleNode("intro").InnerText, sArray));
                        }
                        string input = (document2.SelectSingleNode("root/UsePage") == null) ? "" : document2.SelectSingleNode("root/UsePage").InnerText.Trim();
                        string attribute = string.Empty;
                        if (DataConverter.CBoolean(input))
                        {
                            if (documentElement.HasAttribute("page") && DataConverter.CBoolean(documentElement.GetAttribute("page")))
                            {
                                attribute = " checked";
                            }
                            builder.Append("<tr><td align=\"right\">是否分页：</td><td align=\"left\"><input id=\"page\" type=\"checkbox\"" + attribute + "/></td></tr>");
                            attribute = "10";
                            if (documentElement.HasAttribute("pagesize") && !string.IsNullOrEmpty(documentElement.GetAttribute("pagesize")))
                            {
                                attribute = documentElement.GetAttribute("pagesize");
                            }
                            builder.Append("<tr><td align=\"right\">分页显示数：</td><td align=\"left\"><input type=\"text\" id=\"pagesize\" value=\"" + attribute + "\"/></td></tr>");
                            attribute = string.Empty;
                            if (documentElement.HasAttribute("urlpage") && DataConverter.CBoolean(documentElement.GetAttribute("urlpage")))
                            {
                                attribute = " checked";
                            }
                            builder.Append("<tr><td align=\"right\">是否主分页：</td><td align=\"left\"><input id=\"urlpage\" type=\"checkbox\"" + attribute + "/></td></tr>");
                        }
                        builder.Append("<tr><td align=\"right\">缓存时间：</td><td align=\"left\"><input type=\"text\" id=\"cachetime\"");
                        if (documentElement.HasAttribute("cachetime"))
                        {
                            builder.Append("value=\"" + documentElement.GetAttribute("cachetime") + "\"");
                        }
                        builder.Append("/></td></tr>");
                        if (documentElement.HasAttribute("noprocinlabel") && DataConverter.CBoolean(documentElement.GetAttribute("noprocinlabel")))
                        {
                            attribute = " checked";
                        }
                        builder.Append("<tr><td align=\"right\">不解析内部标签：</td><td align=\"left\"><input id=\"nolabelproc\" type=\"checkbox\"" + attribute + "/></td></tr>");
                        builder.Append("<tr><td align=\"right\">容器类型：</td><td align=\"left\"><input type=\"text\" id=\"spantype\"");
                        if (documentElement.HasAttribute("span"))
                        {
                            builder.Append("value=\"" + documentElement.GetAttribute("span") + "\"");
                        }
                        builder.Append("/></td></tr>");
                        builder.Append("<tr><td align=\"right\">容器风格：</td><td align=\"left\"><input type=\"text\" id=\"cssname\"");
                        if (documentElement.HasAttribute("class"))
                        {
                            builder.Append("value=\"" + documentElement.GetAttribute("class") + "\"");
                        }
                        builder.Append("/></td></tr>");
                        builder.Append("</table>");
                        this.labelbody.Text = builder.ToString();
                        input = (document2.SelectSingleNode("root/LabelIntro") == null) ? "" : document2.SelectSingleNode("root/LabelIntro").InnerText.Trim();
                        if (!string.IsNullOrEmpty(input))
                        {
                            this.labelintro.Text = input;
                        }
                    }
                }
            }
        }
    }
}

