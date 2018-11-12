namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;
    using System.Xml.XPath;

    public sealed class XmlManage
    {
        private string filePath;
        private XmlDocument xmlDoc = new XmlDocument();

        private XmlManage()
        {
        }

        public static bool CheckXmlDataSource(string datasource)
        {
            if (!string.IsNullOrEmpty(datasource))
            {
                string str2;
                XmlDocument document = new XmlDocument();
                string pattern = @"^http:\/\/(.*?)";
                if (Regex.IsMatch(datasource.ToLower(), pattern))
                {
                    bool flag = false;
                    Uri requestUri = new Uri(datasource);
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                        if ("OK" == response.StatusCode.ToString())
                        {
                            flag = true;
                        }
                    }
                    catch (WebException)
                    {
                        return false;
                    }
                    if (flag)
                    {
                        try
                        {
                            document.Load(datasource);
                            return true;
                        }
                        catch (XmlException)
                        {
                            return false;
                        }
                    }
                    return false;
                }
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str2 = current.Server.MapPath("~/" + datasource);
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, datasource);
                }
                if (System.IO.File.Exists(str2))
                {
                    try
                    {
                        document.Load(str2);
                        return true;
                    }
                    catch (XmlException)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool ExistsNode(string nodeName)
        {
            if (this.xmlDoc.SelectSingleNode(nodeName) == null)
            {
                return false;
            }
            return true;
        }

        public DataTable GetAllNodeAndTheFirstAttribute(string nodeName, string attribute)
        {
            return this.GetNodeValue(nodeName, attribute);
        }

        public DataTable GetAllNodeValue(string nodeName)
        {
            return this.GetNodeValue(nodeName, null);
        }

        public Dictionary<string, string> GetAttributesValue(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
            {
                return null;
            }
            System.Xml.XmlNode node = this.xmlDoc.SelectSingleNode(nodeName);
            if (node.Attributes == null)
            {
                return null;
            }
            XmlAttributeCollection attributes = node.Attributes;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (XmlAttribute attribute in attributes)
            {
                dictionary.Add(attribute.Name, attribute.Value);
            }
            return dictionary;
        }

        public string GetAttributesValue(string nodeName, string arrtibuteName)
        {
            if (!string.IsNullOrEmpty(nodeName) && !string.IsNullOrEmpty(arrtibuteName))
            {
                return this.xmlDoc.SelectSingleNode(nodeName).Attributes.GetNamedItem(arrtibuteName).Value.ToLower().Trim();
            }
            return string.Empty;
        }

        public static string GetAttributesValue(IXPathNavigable node, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                XmlElement element = (XmlElement) node;
                if (element.HasAttribute(name))
                {
                    return element.Attributes.GetNamedItem(name).Value;
                }
            }
            return string.Empty;
        }

        public string GetNodeValue(string path)
        {
            return this.GetSingleNodeValue(path);
        }

        private DataTable GetNodeValue(string nodeName, string attribute)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("NodeName", typeof(string));
            DataColumn column2 = new DataColumn("NodeValue", typeof(string));
            if (!string.IsNullOrEmpty(attribute))
            {
                DataColumn column3 = new DataColumn("Attribute", typeof(string));
                table.Columns.Add(column3);
            }
            table.Columns.Add(column);
            table.Columns.Add(column2);
            if (string.IsNullOrEmpty(nodeName))
            {
                foreach (System.Xml.XmlNode node2 in this.xmlDoc.DocumentElement.ChildNodes)
                {
                    DataRow row = table.NewRow();
                    row["NodeName"] = node2.Name.ToLower();
                    row["NodeValue"] = node2.InnerText;
                    if (!string.IsNullOrEmpty(attribute))
                    {
                        row["Attribute"] = node2.Attributes[attribute].Value;
                    }
                    table.Rows.Add(row);
                }
            }
            else
            {
                System.Xml.XmlNode node4 = this.xmlDoc.DocumentElement.SelectSingleNode(nodeName);
                if ((node4 != null) && node4.HasChildNodes)
                {
                    foreach (System.Xml.XmlNode node5 in node4)
                    {
                        DataRow row2 = table.NewRow();
                        row2["NodeName"] = node5.Name;
                        row2["NodeValue"] = node5.InnerText;
                        if (!string.IsNullOrEmpty(attribute))
                        {
                            row2["Attribute"] = node5.Attributes[attribute].Value;
                        }
                        table.Rows.Add(row2);
                    }
                }
            }
            table.AcceptChanges();
            return table;
        }

        public string GetSingleNodeValue(string nodeName)
        {
            if (!string.IsNullOrEmpty(nodeName) && (this.xmlDoc.SelectSingleNode(nodeName) != null))
            {
                return this.xmlDoc.SelectSingleNode(nodeName).InnerText;
            }
            return string.Empty;
        }

        public static IList<XmlScheme> GetXmlTree(IXPathNavigable inputNode, int loopdeep, int outdeep, string xpath, int outype, int stat)
        {
            IList<XmlScheme> list = new List<XmlScheme>();
            if ((outdeep == 0) || (outdeep > 100))
            {
                outdeep = 100;
            }
            if (loopdeep < outdeep)
            {
                System.Xml.XmlNode node = (System.Xml.XmlNode) inputNode;
                int num = loopdeep;
                num++;
                XmlScheme item = new XmlScheme();
                item.Level = loopdeep;
                item.Station = stat;
                item.Path = xpath;
                item.Name = node.Name;
                item.Text = node.InnerText;
                if (!node.HasChildNodes)
                {
                    item.Type = "onlyone";
                    list.Add(item);
                    return list;
                }
                XmlNodeList childNodes = node.ChildNodes;
                IList<System.Xml.XmlNode> list3 = new List<System.Xml.XmlNode>();
                bool flag = false;
                foreach (System.Xml.XmlNode node2 in childNodes)
                {
                    if (string.Compare(node2.GetType().Name, "XmlElement", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        continue;
                    }
                    flag = true;
                    bool flag2 = false;
                    if (outype > 0)
                    {
                        foreach (System.Xml.XmlNode node3 in list3)
                        {
                            if (string.Compare(node3.Name, node2.Name, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                XmlElement element = (XmlElement) node3;
                                element.SetAttribute("pe_tempshownum", (Convert.ToInt32(element.GetAttribute("pe_tempshownum")) + 1).ToString());
                                flag2 = true;
                            }
                        }
                    }
                    if (!flag2)
                    {
                        ((XmlElement) node2).SetAttribute("pe_tempshownum", "1");
                        list3.Add(node2);
                    }
                }
                if (flag)
                {
                    item.Type = "havechile";
                }
                else
                {
                    item.Type = "nochile";
                }
                XmlElement element3 = (XmlElement) node;
                if (element3.HasAttribute("pe_tempshownum"))
                {
                    item.Repnum = Convert.ToInt32(element3.GetAttribute("pe_tempshownum").ToString());
                }
                list.Add(item);
                foreach (System.Xml.XmlNode node4 in list3)
                {
                    int num3 = 0;
                    if (list3.IndexOf(node4) == 0)
                    {
                        num3 = 1;
                    }
                    else if (list3.IndexOf(node4) == (list3.Count - 1))
                    {
                        num3 = 2;
                    }
                    if (string.Compare(node4.Name, "#text", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        string str = xpath + "/" + node.Name;
                        foreach (XmlScheme scheme2 in GetXmlTree(node4, num, outdeep, str, outype, num3))
                        {
                            list.Add(scheme2);
                        }
                        continue;
                    }
                }
            }
            return list;
        }

        public static XmlManage Instance(string fileName, XmlType type)
        {
            XmlManage manage = new XmlManage();
            switch (type)
            {
                case XmlType.None:
                    manage.Load(fileName);
                    return manage;

                case XmlType.File:
                    manage.Load(fileName);
                    return manage;

                case XmlType.Content:
                    manage.LoadXml(fileName);
                    return manage;
            }
            return manage;
        }

        private void Load(string fileName)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                this.filePath = current.Server.MapPath("~/" + fileName);
            }
            else
            {
                this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }
            this.xmlDoc.Load(this.filePath);
        }

        private void LoadXml(string xmlContent)
        {
            this.xmlDoc.LoadXml(xmlContent);
        }

        public static string ReadFileNode(string filepath, string nodename)
        {
            if ((!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(nodename)) && System.IO.File.Exists(filepath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(filepath);
                }
                catch (XmlException)
                {
                    return string.Empty;
                }
                try
                {
                    return document.SelectSingleNode(nodename).InnerText;
                }
                catch (NullReferenceException)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public bool Remove(string nodeName)
        {
            try
            {
                System.Xml.XmlNode oldChild = this.xmlDoc.SelectSingleNode(nodeName);
                oldChild.ParentNode.RemoveChild(oldChild);
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }

        public void Save(string fileName)
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                this.filePath = current.Server.MapPath("~/" + fileName);
            }
            else
            {
                this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }
            this.xmlDoc.Save(this.filePath);
        }

        public static bool SaveFileNode(string filepath, string nodepath, string nodename, string nodevalue)
        {
            if (((!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(nodepath)) && !string.IsNullOrEmpty(nodename)) && System.IO.File.Exists(filepath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(filepath);
                    foreach (System.Xml.XmlNode node in document.SelectNodes(nodepath))
                    {
                        XmlNodeList list2 = node.SelectNodes(nodename);
                        if (list2.Count > 0)
                        {
                            foreach (System.Xml.XmlNode node2 in list2)
                            {
                                XmlElement element = (XmlElement) node2;
                                element.RemoveAll();
                                if (!string.IsNullOrEmpty(nodevalue))
                                {
                                    if (((nodevalue.IndexOf("<", StringComparison.Ordinal) > 0) || (nodevalue.IndexOf(">", StringComparison.Ordinal) > 0)) || (nodevalue.IndexOf("'", StringComparison.Ordinal) > 0))
                                    {
                                        XmlCDataSection section = document.CreateCDataSection(nodevalue);
                                        element.AppendChild(section);
                                        continue;
                                    }
                                    element.InnerText = nodevalue;
                                }
                            }
                            continue;
                        }
                        XmlElement newChild = document.CreateElement("", nodename, "");
                        if (((nodevalue.IndexOf("<", StringComparison.Ordinal) > 0) || (nodevalue.IndexOf(">", StringComparison.Ordinal) > 0)) || (nodevalue.IndexOf("'", StringComparison.Ordinal) > 0))
                        {
                            XmlCDataSection section2 = document.CreateCDataSection(nodevalue);
                            newChild.AppendChild(section2);
                        }
                        else
                        {
                            newChild.InnerText = nodevalue;
                        }
                        node.AppendChild(newChild);
                    }
                    document.Save(filepath);
                    return true;
                }
                catch (XmlException)
                {
                    return false;
                }
            }
            return false;
        }

        public string SelectNode(string nodeName)
        {
            System.Xml.XmlNode node = this.xmlDoc.SelectSingleNode(nodeName);
            if (node == null)
            {
                return string.Empty;
            }
            return node.OuterXml;
        }

        public bool SetAttributesValue(string nodeName, string key, string val)
        {
            System.Xml.XmlNode node = this.xmlDoc.SelectSingleNode(nodeName);
            if (node == null)
            {
                return false;
            }
            ((XmlElement) node).SetAttribute(key, val);
            return true;
        }

        public bool SetNodeValue(string nodeName, string val)
        {
            System.Xml.XmlNode node = this.xmlDoc.SelectSingleNode(nodeName);
            if (node == null)
            {
                return false;
            }
            node.InnerText = val;
            return true;
        }

        public IXPathNavigable XmlNode(string path)
        {
            return this.xmlDoc.SelectSingleNode(path);
        }

        public string Xml
        {
            get
            {
                StringWriter writer = new StringWriter();
                this.xmlDoc.Save(writer);
                string str = writer.ToString();
                writer.Dispose();
                return str;
            }
        }

        public IXPathNavigable XmlDoc
        {
            get
            {
                return this.xmlDoc;
            }
        }
    }
}

