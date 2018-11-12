namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Security;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class FileManage : AdminPage
    {

        protected string m_ConfigUploadDir;
        protected string m_CurrentDir;
        protected DataTable m_CurrentDirectoryInfo;
        protected int m_ItemIndex;
        protected string m_ParentDir;
        protected string m_UrlReferrer;
        private static Dictionary<string, string> s_ExtensionImgDictionary;

        protected void BindData()
        {
            DataView defaultView = this.m_CurrentDirectoryInfo.DefaultView;
            if (!string.IsNullOrEmpty(BasePage.RequestString("SortField")) && !string.IsNullOrEmpty(BasePage.RequestString("Sort")))
            {
                defaultView.Sort = BasePage.RequestString("SortField") + " " + BasePage.RequestString("Sort");
            }
            this.Pager.RecordCount = defaultView.Count;
            if (this.Pager.PageSize > defaultView.Count)
            {
                this.RptFiles.DataSource = defaultView;
                this.RptFiles.DataBind();
            }
            else
            {
                int num = (this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize;
                int num2 = num + this.Pager.PageSize;
                List<DataRowView> list = new List<DataRowView>();
                for (int i = num; i < num2; i++)
                {
                    if (i >= defaultView.Count)
                    {
                        break;
                    }
                    list.Add(defaultView[i]);
                }
                this.RptFiles.DataSource = list;
                this.RptFiles.DataBind();
            }
        }

        protected void BtnDelAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this.m_CurrentDirectoryInfo.Rows)
                {
                    string file = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + row["name"].ToString();
                    file = base.Request.PhysicalApplicationPath + file.Replace("/", @"\");
                    if (row["type"].ToString() == "2")
                    {
                        FileSystemObject.Delete(file, FsoMethod.File);
                    }
                    if (row["type"].ToString() == "1")
                    {
                        FileSystemObject.Delete(file, FsoMethod.Folder);
                    }
                }
            }
            catch (SecurityException exception)
            {
                AdminPage.WriteErrMsg(exception.Message);
            }
            catch (UnauthorizedAccessException exception2)
            {
                AdminPage.WriteErrMsg(exception2.Message);
            }
            AdminPage.WriteSuccessMsg("<li>批量删除成功。</li>", this.m_UrlReferrer);
        }

        protected void BtnDelCurrentFiles_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in this.m_CurrentDirectoryInfo.Rows)
                {
                    string file = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + row["name"].ToString();
                    file = base.Request.PhysicalApplicationPath + file.Replace("/", @"\");
                    if (row["type"].ToString() == "2")
                    {
                        FileSystemObject.Delete(file, FsoMethod.File);
                    }
                }
            }
            catch (SecurityException exception)
            {
                AdminPage.WriteErrMsg(exception.Message);
            }
            catch (UnauthorizedAccessException exception2)
            {
                AdminPage.WriteErrMsg(exception2.Message);
            }
            AdminPage.WriteSuccessMsg("<li>批量删除成功。</li>", this.m_UrlReferrer);
        }

        protected void BtnDelSelected_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> pair in this.GetRptFilesSelectRows())
            {
                string file = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + pair.Key;
                file = base.Request.PhysicalApplicationPath + file.Replace("/", @"\");
                try
                {
                    if (pair.Value == "DelFiles")
                    {
                        FileSystemObject.Delete(file, FsoMethod.File);
                    }
                    if (pair.Value == "DelDir")
                    {
                        FileSystemObject.Delete(file, FsoMethod.Folder);
                    }
                    continue;
                }
                catch (SecurityException exception)
                {
                    AdminPage.WriteErrMsg(exception.Message);
                    continue;
                }
                catch (UnauthorizedAccessException exception2)
                {
                    AdminPage.WriteErrMsg(exception2.Message);
                    continue;
                }
            }
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            AdminPage.WriteSuccessMsg("<li>批量删除成功。</li>", this.m_UrlReferrer);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void BtnThumb_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.RptFiles.Items)
            {
                CheckBox box;
                HiddenField field = (HiddenField) item.FindControl("HdnName");
                HiddenField field2 = (HiddenField) item.FindControl("HdnType");
                HiddenField field3 = (HiddenField) item.FindControl("HdnExtension");
                if (BasePage.RequestString("ShowType") != "0")
                {
                    box = (CheckBox) item.FindControl("ChkListFiles");
                }
                else
                {
                    box = (CheckBox) item.FindControl("ChkFiles");
                }
                if (box.Checked)
                {
                    if (field2.Value == "1")
                    {
                        this.CreateThumbByFolder(field.Value);
                    }
                    if (field2.Value == "2")
                    {
                        this.CreateThumb("", field.Value, field3.Value);
                    }
                }
            }
            AdminPage.WriteSuccessMsg("<li>批量生成缩略图成功。</li>", this.m_UrlReferrer);
        }

        protected void BtnWaterMark_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.RptFiles.Items)
            {
                CheckBox box;
                HiddenField field = (HiddenField) item.FindControl("HdnName");
                HiddenField field2 = (HiddenField) item.FindControl("HdnType");
                HiddenField field3 = (HiddenField) item.FindControl("HdnExtension");
                if (BasePage.RequestString("ShowType") != "0")
                {
                    box = (CheckBox) item.FindControl("ChkListFiles");
                }
                else
                {
                    box = (CheckBox) item.FindControl("ChkFiles");
                }
                if (box.Checked)
                {
                    if (field2.Value == "1")
                    {
                        this.WaterMarkByFolder(field.Value);
                    }
                    if (field2.Value == "2")
                    {
                        this.WaterMarkByFile("", field.Value, field3.Value);
                    }
                }
            }
            AdminPage.WriteSuccessMsg("<li>批量添加水印成功。</li>", this.m_UrlReferrer);
        }

        protected void CreateThumb(string filePath, string fileName, string extension)
        {
            string str = this.m_CurrentDir + "/" + filePath + "/";
            if (this.IsPhoto(extension))
            {
                Thumbs.GetThumbsPath(str + fileName, str + Path.GetFileNameWithoutExtension(fileName) + "_S." + extension);
            }
        }

        protected void CreateThumbByFolder(string folderPath)
        {
            string path = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + folderPath;
            path = (base.Request.PhysicalApplicationPath + path.Replace("/", @"\")).Replace(@"\\", @"\");
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                foreach (DataRow row in FileSystemObject.GetDirectoryInfos(path, FsoMethod.All).Rows)
                {
                    if (row["type"].ToString() == "2")
                    {
                        this.CreateThumb(folderPath, row["name"].ToString(), row["content_type"].ToString());
                    }
                    if (row["type"].ToString() == "1")
                    {
                        this.CreateThumbByFolder(folderPath + "/" + row["name"].ToString());
                    }
                }
            }
        }

        protected bool GetDeleteEnabled(string forderName)
        {
            string str;
            bool flag = true;
            if (((str = forderName) != null) && (((str == "AuthorPic") || (str == "CopyFromPic")) || ((str == "TrademarkPic") || (str == "ProducerPic"))))
            {
                flag = false;
            }
            if (forderName == SiteConfig.SiteOption.AdvertisementDir)
            {
                flag = false;
            }
            return flag;
        }

        protected string GetFileContent(string fileName, string extension)
        {
            fileName = fileName.Replace(@"\", "/");
            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.ToLower();
            }
            string str = base.BasePath + this.m_ConfigUploadDir + this.m_CurrentDir + "/" + fileName;
            StringBuilder builder = new StringBuilder();
            switch (extension)
            {
                case "jpeg":
                case "jpe":
                case "bmp":
                case "png":
                case "jpg":
                case "gif":
                    builder.Append(@"<img src=\'" + str + @"\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@" border=\'0\' />");
                    break;

                case "wmv":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "ra":
                case "ram":
                case "swf":
                    builder.Append(@"<object classid=\'clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\' codebase=\'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append(@"><param name=\'movie\' value=\'" + str + @"\'>");
                    builder.Append(@"<param name=\'wmode\' value=\'transparent\'>");
                    builder.Append(@"<param name=\'quality\' value=\'autohigh\'>");
                    builder.Append(@"<embed src=\'" + str + @"\' quality=\'autohigh\'");
                    builder.Append(@" pluginspage=\'http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\' type=\'application/x-shockwave-flash\'");
                    builder.Append(@" wmode=\'transparent\'");
                    builder.Append(@" width=\'200\'");
                    builder.Append(@" height=\'120\'");
                    builder.Append("></embed></object>");
                    break;

                default:
                    builder.Append("&nbsp;此文件非图片或动画，无预览&nbsp;");
                    break;
            }
            return builder.ToString();
        }

        protected string GetFilePath(string fileName)
        {
            fileName = fileName.Replace(@"\", "/");
            return (base.BasePath + this.m_ConfigUploadDir + this.m_CurrentDir + "/" + fileName);
        }

        private Dictionary<string, string> GetRptFilesSelectRows()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (RepeaterItem item in this.RptFiles.Items)
            {
                CheckBox box;
                LinkButton button;
                if (BasePage.RequestString("ShowType") != "0")
                {
                    box = (CheckBox) item.FindControl("ChkListFiles");
                    button = (LinkButton) item.FindControl("LbtnDelList");
                }
                else
                {
                    box = (CheckBox) item.FindControl("ChkFiles");
                    button = (LinkButton) item.FindControl("LbtnDel");
                }
                if (box.Checked)
                {
                    dictionary.Add(button.CommandArgument, button.CommandName);
                }
            }
            return dictionary;
        }

        protected string GetShowExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.ToLower();
            }
            if (s_ExtensionImgDictionary.ContainsKey(extension))
            {
                return s_ExtensionImgDictionary[extension];
            }
            return "other";
        }

        protected string GetSize(string size)
        {
            if (!string.IsNullOrEmpty(size))
            {
                return FileSystemObject.ConvertSizeToShow(Convert.ToInt64(size));
            }
            return string.Empty;
        }

        protected string GetSortShow(string field)
        {
            string str2;
            string str = "";
            if (!(BasePage.RequestStringToLower("SortField") == field) || ((str2 = BasePage.RequestStringToLower("Sort")) == null))
            {
                return str;
            }
            if (!(str2 == "desc"))
            {
                if (str2 != "asc")
                {
                    return str;
                }
            }
            else
            {
                return "↑";
            }
            return "↓";
        }

        protected string GetSorturl(string field)
        {
            string str = "fileManage.aspx?Dir=" + base.Server.UrlEncode(this.m_CurrentDir) + "&ShowType=" + BasePage.RequestString("ShowType") + "&SortField=" + field;
            if (BasePage.RequestStringToLower("SortField") == field)
            {
                switch (BasePage.RequestStringToLower("Sort"))
                {
                    case "desc":
                        return (str + "&Sort=ASC");

                    case "asc":
                        return (str + "&Sort=DESC");
                }
            }
            return (str + "&Sort=DESC");
        }

        protected void InitTheExtensionImgDictionary()
        {
            if (s_ExtensionImgDictionary == null)
            {
                s_ExtensionImgDictionary = new Dictionary<string, string>();
                s_ExtensionImgDictionary.Add("jpeg", "img");
                s_ExtensionImgDictionary.Add("jpe", "img");
                s_ExtensionImgDictionary.Add("bmp", "img");
                s_ExtensionImgDictionary.Add("png", "img");
                s_ExtensionImgDictionary.Add("swf", "Ftype_flash");
                s_ExtensionImgDictionary.Add("dll", "img");
                s_ExtensionImgDictionary.Add("vbp", "sys");
                s_ExtensionImgDictionary.Add("wmv", "Ftype_media");
                s_ExtensionImgDictionary.Add("avi", "Ftype_media");
                s_ExtensionImgDictionary.Add("asf", "Ftype_media");
                s_ExtensionImgDictionary.Add("mpg", "Ftype_media");
                s_ExtensionImgDictionary.Add("rm", "Ftype_rm");
                s_ExtensionImgDictionary.Add("ra", "Ftype_rm");
                s_ExtensionImgDictionary.Add("ram", "Ftype_rm");
                s_ExtensionImgDictionary.Add("rar", "zip");
                s_ExtensionImgDictionary.Add("zip", "zip");
                s_ExtensionImgDictionary.Add("xml", "xml");
                s_ExtensionImgDictionary.Add("txt", "txt");
                s_ExtensionImgDictionary.Add("exe", "exe");
                s_ExtensionImgDictionary.Add("doc", "doc");
                s_ExtensionImgDictionary.Add("html", "html");
                s_ExtensionImgDictionary.Add("htm", "htm");
                s_ExtensionImgDictionary.Add("jpg", "jpg");
                s_ExtensionImgDictionary.Add("gif", "gif");
                s_ExtensionImgDictionary.Add("xls", "xls");
                s_ExtensionImgDictionary.Add("asp", "asp");
            }
        }

        private bool IsPhoto(string extension)
        {
            string str;
            bool flag = false;
            if (((str = extension) == null) || (((!(str == "jpeg") && !(str == "jpe")) && (!(str == "bmp") && !(str == "png"))) && (!(str == "jpg") && !(str == "gif"))))
            {
                return flag;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitTheExtensionImgDictionary();
            this.m_ConfigUploadDir = SiteConfig.SiteOption.UploadDir;
            this.m_CurrentDir = BasePage.RequestString("Dir").Replace("..", string.Empty);
            this.m_UrlReferrer = "FileManage.aspx?Dir=" + base.Server.UrlEncode(this.m_CurrentDir);
            if (!string.IsNullOrEmpty(this.m_ConfigUploadDir))
            {
                string path = "";
                if (!string.IsNullOrEmpty(this.m_CurrentDir) && (this.m_CurrentDir.LastIndexOf("/", StringComparison.Ordinal) > 0))
                {
                    this.m_ParentDir = base.Server.UrlEncode(this.m_CurrentDir.Remove(this.m_CurrentDir.LastIndexOf("/", StringComparison.Ordinal), this.m_CurrentDir.Length - this.m_CurrentDir.LastIndexOf("/", StringComparison.Ordinal)));
                }
                path = this.m_ConfigUploadDir + this.m_CurrentDir + "/";
                this.LblCurrentDir.Text = path;
                path = base.Request.PhysicalApplicationPath + path;
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    if (string.IsNullOrEmpty(this.TxtSearchKeyword.Text))
                    {
                        this.m_CurrentDirectoryInfo = FileSystemObject.GetDirectoryInfos(path, FsoMethod.All);
                    }
                    else
                    {
                        string text = this.TxtSearchKeyword.Text;
                        if (!text.Contains("*"))
                        {
                            text = "*" + text + "*.*";
                        }
                        this.m_CurrentDirectoryInfo = FileSystemObject.SearchFiles(path, text);
                    }
                }
            }
            else
            {
                this.PanContent.Visible = false;
                this.LitMessage.Text = "请在网站配置中配置上传文件目录";
                this.LitMessage.Visible = true;
            }
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
            this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void RptFiles_ItemCommand(object source, CommandEventArgs e)
        {
            string file = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + ((string) e.CommandArgument);
            file = base.Request.PhysicalApplicationPath + file.Replace("/", @"\");
            if (e.CommandName == "DelFiles")
            {
                try
                {
                    FileSystemObject.Delete(file, FsoMethod.File);
                    AdminPage.WriteSuccessMsg("删除成功！", this.m_UrlReferrer);
                }
                catch (SecurityException exception)
                {
                    AdminPage.WriteErrMsg(exception.Message);
                }
                catch (UnauthorizedAccessException exception2)
                {
                    AdminPage.WriteErrMsg(exception2.Message);
                }
            }
            if (e.CommandName == "DelDir")
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                try
                {
                    FileSystemObject.Delete(file, FsoMethod.Folder);
                }
                catch (SecurityException exception3)
                {
                    AdminPage.WriteErrMsg(exception3.Message);
                }
                catch (UnauthorizedAccessException exception4)
                {
                    AdminPage.WriteErrMsg(exception4.Message);
                }
                AdminPage.WriteSuccessMsg("<li>删除目录成功。</li>", this.m_UrlReferrer);
            }
        }

        protected void WaterMarkByFile(string filePath, string fileName, string extension)
        {
            string str = this.m_CurrentDir + "/" + filePath + "/";
            if (this.IsPhoto(extension))
            {
                WaterMark.AddWaterMark(str + fileName);
            }
        }

        protected void WaterMarkByFolder(string folderPath)
        {
            string path = this.m_ConfigUploadDir + this.m_CurrentDir + "/" + folderPath;
            path = (base.Request.PhysicalApplicationPath + path.Replace("/", @"\")).Replace(@"\\", @"\");
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                foreach (DataRow row in FileSystemObject.GetDirectoryInfos(path, FsoMethod.All).Rows)
                {
                    if (row["type"].ToString() == "2")
                    {
                        this.WaterMarkByFile(folderPath, row["name"].ToString(), row["content_type"].ToString());
                    }
                    if (row["type"].ToString() == "1")
                    {
                        this.WaterMarkByFolder(folderPath + "/" + row["name"].ToString());
                    }
                }
            }
        }
    }
}

