namespace EasyOne.Controls.Editor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using System.Text;
    using System.Web;
    using System.Xml;

    public class FileBrowserConnector : FileWorkerBase
    {
        private XmlNode CreateBaseXml(XmlDocument xml, string command, string resourceType, string currentFolder)
        {
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlNode node = XmlUtil.AppendElement(xml, "Connector");
            XmlUtil.SetAttribute(node, "command", command);
            XmlUtil.SetAttribute(node, "resourceType", resourceType);
            XmlNode node2 = XmlUtil.AppendElement(node, "CurrentFolder");
            XmlUtil.SetAttribute(node2, "path", currentFolder);
            XmlUtil.SetAttribute(node2, "url", this.GetUrlFromPath(resourceType, currentFolder));
            return node;
        }

        private void CreateFolder(XmlNode connectorNode, string resourceType, string currentFolder)
        {
            string attributeValue = "0";
            string str2 = base.Request.QueryString["NewFolderName"];
            if ((str2 == null) || (str2.Length == 0))
            {
                attributeValue = "102";
            }
            else
            {
                string str3 = this.ServerMapFolder(resourceType, currentFolder);
                try
                {
                    PEUtil.CreateDirectory(Path.Combine(str3, str2));
                }
                catch (ArgumentException)
                {
                    attributeValue = "102";
                }
                catch (PathTooLongException)
                {
                    attributeValue = "102";
                }
                catch (IOException)
                {
                    attributeValue = "101";
                }
                catch (SecurityException)
                {
                    attributeValue = "103";
                }
                catch (Exception)
                {
                    attributeValue = "110";
                }
            }
            XmlUtil.SetAttribute(XmlUtil.AppendElement(connectorNode, "Error"), "number", attributeValue);
        }

        private void FileUpload(string resourceType, string currentFolder)
        {
            HttpPostedFile file = base.Request.Files["NewFile"];
            string str = "0";
            string fileName = "";
            if (file == null)
            {
                str = "202";
            }
            else
            {
                string str4;
                string str3 = this.ServerMapFolder(resourceType, currentFolder);
                fileName = Path.GetFileName(file.FileName);
                int num = 0;
                while (true)
                {
                    str4 = Path.Combine(str3, fileName);
                    if (!File.Exists(str4))
                    {
                        break;
                    }
                    num++;
                    fileName = string.Concat(new object[] { Path.GetFileNameWithoutExtension(file.FileName), "(", num, ")", Path.GetExtension(file.FileName) });
                    str = "201";
                }
                file.SaveAs(str4);
            }
            base.Response.Clear();
            base.Response.Write("<script type=\"text/javascript\">");
            base.Response.Write("window.parent.frames['frmUpload'].OnUploadCompleted(" + str + ",'" + fileName.Replace("'", @"\'") + "') ;");
            base.Response.Write("</script>");
            base.Response.End();
        }

        private void GetFiles(XmlNode connectorNode, string resourceType, string currentFolder)
        {
            string path = this.ServerMapFolder(resourceType, currentFolder);
            XmlNode node = XmlUtil.AppendElement(connectorNode, "Files");
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                decimal num2 = Math.Round((decimal) (files[i].Length / 1024M));
                if ((num2 < 1M) && (files[i].Length != 0L))
                {
                    num2 = 1M;
                }
                XmlNode node2 = XmlUtil.AppendElement(node, "File");
                XmlUtil.SetAttribute(node2, "name", files[i].Name);
                XmlUtil.SetAttribute(node2, "size", num2.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void GetFolders(XmlNode connectorNode, string resourceType, string currentFolder)
        {
            string path = this.ServerMapFolder(resourceType, currentFolder);
            XmlNode node = XmlUtil.AppendElement(connectorNode, "Folders");
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                XmlUtil.SetAttribute(XmlUtil.AppendElement(node, "Folder"), "name", directories[i].Name);
            }
        }

        private string GetUrlFromPath(string resourceType, string folderPath)
        {
            if ((resourceType != null) && (resourceType.Length != 0))
            {
                return (base.UserFilesPath + resourceType + folderPath);
            }
            return (base.UserFilesPath.TrimEnd(new char[] { '/' }) + folderPath);
        }

        protected override void OnLoad(EventArgs e)
        {
            string command = base.Request.QueryString["Command"];
            if (command != null)
            {
                string resourceType = base.Request.QueryString["Type"];
                if (resourceType != null)
                {
                    string currentFolder = base.Request.QueryString["CurrentFolder"];
                    if (currentFolder != null)
                    {
                        if (!currentFolder.EndsWith("/", StringComparison.Ordinal))
                        {
                            currentFolder = currentFolder + "/";
                        }
                        if (!currentFolder.StartsWith("/", StringComparison.Ordinal))
                        {
                            currentFolder = "/" + currentFolder;
                        }
                        if (command == "FileUpload")
                        {
                            this.FileUpload(resourceType, currentFolder);
                        }
                        else
                        {
                            base.Response.ClearHeaders();
                            base.Response.Clear();
                            base.Response.CacheControl = "no-cache";
                            base.Response.ContentEncoding = Encoding.UTF8;
                            base.Response.ContentType = "text/xml";
                            XmlDocument xml = new XmlDocument();
                            XmlNode connectorNode = this.CreateBaseXml(xml, command, resourceType, currentFolder);
                            string str4 = command;
                            if (str4 != null)
                            {
                                if (!(str4 == "GetFolders"))
                                {
                                    if (str4 == "GetFoldersAndFiles")
                                    {
                                        this.GetFolders(connectorNode, resourceType, currentFolder);
                                        this.GetFiles(connectorNode, resourceType, currentFolder);
                                    }
                                    else if (str4 == "CreateFolder")
                                    {
                                        this.CreateFolder(connectorNode, resourceType, currentFolder);
                                    }
                                }
                                else
                                {
                                    this.GetFolders(connectorNode, resourceType, currentFolder);
                                }
                            }
                            base.Response.Write(xml.OuterXml);
                            base.Response.End();
                        }
                    }
                }
            }
        }

        private string ServerMapFolder(string resourceType, string folderPath)
        {
            string path = Path.Combine(base.UserFilesDirectory, resourceType);
            PEUtil.CreateDirectory(path);
            return Path.Combine(path, folderPath.TrimStart(new char[] { '/' }));
        }
    }
}

