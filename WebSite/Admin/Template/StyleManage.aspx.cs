namespace EasyOne.WebSite.Admin.Template
{
    using AjaxControlToolkit;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class StyleManage : AdminPage
    {
        protected string currentDirectory;
        protected string m_UrlReferrer;
        protected string parentDir;
        protected StringBuilder replaceData = new StringBuilder();
        protected StringBuilder replaceResult = new StringBuilder();
        protected StringBuilder searchResult = new StringBuilder();
        protected string skinStyleDir;

        protected void BindData()
        {
            DataView defaultView = FileSystemObject.GetDirectoryInfos(this.currentDirectory, FsoMethod.All).DefaultView;
            this.Pager.RecordCount = defaultView.Count;
            int num = (this.Pager.CurrentPageIndex - 1) * this.Pager.PageSize;
            int num2 = num + this.Pager.PageSize;
            List<DataRowView> list = new List<DataRowView>();
            for (int i = num; i < num2; i++)
            {
                if (i < defaultView.Count)
                {
                    list.Add(defaultView[i]);
                }
            }
            this.EgvFiles.DataSource = list;
            this.EgvFiles.DataBind();
        }

        protected void BtnMove_Click(object sender, EventArgs e)
        {
            if (this.EgvFiles.SelectList.Length == 0)
            {
                AdminPage.WriteErrMsg("未选中文件或文件夹", this.m_UrlReferrer);
            }
            else
            {
                string[] strArray = this.EgvFiles.SelectList.ToString().Split(new char[] { ',' });
                string path = base.Request.PhysicalApplicationPath + this.skinStyleDir;
                if (string.IsNullOrEmpty(this.DrpMove.SelectedValue))
                {
                    AdminPage.WriteErrMsg("<li>请选择目标文件夹</li>", this.m_UrlReferrer);
                }
                try
                {
                    DirectoryInfo info = new DirectoryInfo(path);
                    if (info.Exists)
                    {
                        foreach (string str2 in strArray)
                        {
                            if (!string.IsNullOrEmpty(str2))
                            {
                                string str3 = info.FullName + this.DrpMove.SelectedValue + str2;
                                DirectoryInfo info2 = new DirectoryInfo(this.currentDirectory + str2 + "/");
                                DirectoryInfo info3 = new DirectoryInfo(info.FullName + this.DrpMove.SelectedValue);
                                DirectoryInfo info4 = new DirectoryInfo(str3);
                                if (info2.Exists)
                                {
                                    if (info3.FullName == info2.FullName)
                                    {
                                        AdminPage.WriteErrMsg("<li>不能移动到自己目录下</li>", this.m_UrlReferrer);
                                    }
                                    if (info2.FullName == info4.FullName)
                                    {
                                        AdminPage.WriteErrMsg("<li>不能移动到相同目录下</li>", this.m_UrlReferrer);
                                    }
                                    if (info4.Exists)
                                    {
                                        AdminPage.WriteErrMsg("<li>目标目录已经存在</li>", this.m_UrlReferrer);
                                    }
                                    FileSystemObject.Move(info2.FullName, str3, FsoMethod.Folder);
                                }
                                FileInfo info5 = new FileInfo(this.currentDirectory + str2);
                                FileInfo info6 = new FileInfo(str3);
                                if (info5.Exists)
                                {
                                    if (info5.FullName == info6.FullName)
                                    {
                                        AdminPage.WriteErrMsg("<li>目标路径与源路径相同</li>", this.m_UrlReferrer);
                                    }
                                    if (info6.Exists)
                                    {
                                        AdminPage.WriteErrMsg("<li>目标文件已经存在</li>", this.m_UrlReferrer);
                                    }
                                    FileSystemObject.Move(info5.FullName, str3, FsoMethod.File);
                                }
                            }
                        }
                        base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                        AdminPage.WriteSuccessMsg("移动成功", this.m_UrlReferrer);
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("<li>文件未找到</li>", this.m_UrlReferrer);
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>移动文件夹或文件时失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.m_UrlReferrer);
                }
            }
        }

        protected void EBtnBatchDel_Click(object sender, EventArgs e)
        {
            if (this.EgvFiles.SelectList.Length == 0)
            {
                AdminPage.WriteErrMsg("未选中文件或文件夹", this.m_UrlReferrer);
            }
            else
            {
                string[] strArray = this.EgvFiles.SelectList.ToString().Split(new char[] { ',' });
                try
                {
                    foreach (string str in strArray)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            if (string.IsNullOrEmpty(Path.GetExtension(str)))
                            {
                                FileSystemObject.Delete(this.currentDirectory + str, FsoMethod.Folder);
                            }
                            else
                            {
                                FileSystemObject.Delete(this.currentDirectory + str, FsoMethod.File);
                            }
                        }
                    }
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("删除成功", this.m_UrlReferrer);
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("<li>文件未找到</li>", this.m_UrlReferrer);
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>删除文件夹或文件时失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.m_UrlReferrer);
                }
            }
        }

        protected void EBtnCopyDir_Click(object sender, EventArgs e)
        {
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            FileSystemObject.CopyDirectory(this.currentDirectory + this.HdnCopyDir.Value, this.currentDirectory + this.TxtCopyDir.Text);
            AdminPage.WriteSuccessMsg("<li>复制目录成功。</li>", this.m_UrlReferrer);
        }

        protected void EBtnCopyFile_Click(object sender, EventArgs e)
        {
            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
            FileSystemObject.CopyFile(this.currentDirectory + this.HdnCopyFile.Value, this.currentDirectory + this.TxtCopyFile.Text);
            AdminPage.WriteSuccessMsg("<li>复制文件成功。</li>", this.m_UrlReferrer);
        }

        protected void EBtnCreateTemplate_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("StyleSheets.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"]));
        }

        protected void EBtnModifyName_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string str = this.currentDirectory + @"\";
                string oldFile = str + this.HdnName.Value;
                try
                {
                    if (this.HdnType.Value == "1")
                    {
                        string file = str + this.TxtDirName.Text;
                        if (oldFile == file)
                        {
                            AdminPage.WriteErrMsg("重命名目录名不能和原来目录名一样！", this.m_UrlReferrer);
                        }
                        if (!FileSystemObject.IsExist(file, FsoMethod.Folder))
                        {
                            FileSystemObject.Move(oldFile, file, FsoMethod.Folder);
                            base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                            AdminPage.WriteSuccessMsg("重命名目录成功", this.m_UrlReferrer);
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("<li>重命目标目录名已经存在。</li>", this.m_UrlReferrer);
                        }
                    }
                    else
                    {
                        string str4 = str + this.TxtFileName.Text;
                        if (oldFile == str4)
                        {
                            AdminPage.WriteErrMsg("重命名文件名不能和原来文件名一样！", this.m_UrlReferrer);
                        }
                        if (!FileSystemObject.IsExist(str4, FsoMethod.File))
                        {
                            FileSystemObject.Move(oldFile, str4, FsoMethod.File);
                            AdminPage.WriteSuccessMsg("重命名文件成功", this.m_UrlReferrer);
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("<li>重命目标文件名已经存在。</li>", this.m_UrlReferrer);
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("文件未找到", this.m_UrlReferrer);
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>重命名文件夹或文件失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.m_UrlReferrer);
                }
            }
        }

        protected void EBtnNewDir_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                try
                {
                    DirectoryInfo info = new DirectoryInfo(this.currentDirectory + @"\" + this.TxtNewDir.Text);
                    if (info.Exists)
                    {
                        AdminPage.WriteErrMsg("新建的目录已经存在", this.m_UrlReferrer);
                    }
                    FileSystemObject.Create(this.currentDirectory + @"\" + this.TxtNewDir.Text, FsoMethod.Folder);
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("新建目录成功", this.m_UrlReferrer);
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("文件未找到", this.m_UrlReferrer);
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>新建目录失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.m_UrlReferrer);
                }
            }
        }

        protected void EgvFiles_RowCommand(object sender, CommandEventArgs e)
        {
            try
            {
                string str;
                string commandName = e.CommandName;
                if (commandName != null)
                {
                    if (!(commandName == "DelFiles"))
                    {
                        if (commandName == "DelDir")
                        {
                            goto Label_0082;
                        }
                        if (commandName == "CopyDir")
                        {
                            goto Label_00C3;
                        }
                        if (commandName == "CopyFiles")
                        {
                            goto Label_01A7;
                        }
                    }
                    else
                    {
                        FileSystemObject.Delete(this.currentDirectory + e.CommandArgument.ToString(), FsoMethod.File);
                        AdminPage.WriteSuccessMsg("<li>删除文件成功。</li>", this.m_UrlReferrer);
                    }
                }
                return;
            Label_0082:
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                FileSystemObject.Delete(this.currentDirectory + e.CommandArgument.ToString(), FsoMethod.Folder);
                AdminPage.WriteSuccessMsg("<li>删除目录成功。</li>", this.m_UrlReferrer);
                return;
            Label_00C3:
                if (!FileSystemObject.IsExist(this.currentDirectory + "复件" + e.CommandArgument.ToString(), FsoMethod.Folder))
                {
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    FileSystemObject.CopyDirectory(this.currentDirectory + e.CommandArgument.ToString(), this.currentDirectory + "复件" + e.CommandArgument.ToString());
                    AdminPage.WriteSuccessMsg("<li>复制目录成功。</li>", this.m_UrlReferrer);
                }
                else
                {
                    this.HdnCopyDir.Value = e.CommandArgument.ToString();
                    this.LblCopyDir.Text = "复件" + e.CommandArgument.ToString();
                    this.TxtCopyDir.Text = "复件" + e.CommandArgument.ToString();
                    this.MpeCopyDir.Show();
                }
                return;
            Label_01A7:
                str = e.CommandArgument.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strArray = str.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    string newValue = "复件" + strArray[strArray.Length - 1];
                    string str3 = str.Replace(strArray[strArray.Length - 1], newValue);
                    if (!FileSystemObject.IsExist(this.currentDirectory + str3, FsoMethod.File))
                    {
                        FileSystemObject.CopyFile(this.currentDirectory + e.CommandArgument.ToString(), this.currentDirectory + str3);
                        AdminPage.WriteSuccessMsg("<li>复制文件成功。</li>", this.m_UrlReferrer);
                    }
                    else
                    {
                        this.HdnCopyFile.Value = e.CommandArgument.ToString();
                        this.LblCopyFile.Text = str3;
                        this.TxtCopyFile.Text = str3;
                        this.MpeCopyFile.Show();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                AdminPage.WriteErrMsg("<li>文件未找到！</li>", this.m_UrlReferrer);
            }
            catch (UnauthorizedAccessException)
            {
                AdminPage.WriteErrMsg("<li>访问文件夹或文件时失败！检查您的服务器是否给风格文件夹写入权限。</li>", this.m_UrlReferrer);
            }
        }

        protected void EgvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int num = DataConverter.CLng(DataBinder.Eval(e.Row.DataItem, "type"));
                string str = "";
                if (num == 1)
                {
                    str = "StyleManage.aspx?Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"] + "/" + DataBinder.Eval(e.Row.DataItem, "name").ToString());
                    e.Row.Attributes.Add("ondblclick", "RowDblclick('" + str + "');");
                }
                else if (this.IsEdit(DataBinder.Eval(e.Row.DataItem, "type").ToString(), DataBinder.Eval(e.Row.DataItem, "content_type").ToString()))
                {
                    str = "StyleSheets.aspx?Action=Modify&Dir=" + base.Server.UrlEncode(base.Request.QueryString["Dir"] + "/" + DataBinder.Eval(e.Row.DataItem, "name").ToString());
                    e.Row.Attributes.Add("ondblclick", "RowDblclick('" + str + "');");
                }
            }
        }

        protected string GetSize(string size)
        {
            return FileSystemObject.ConvertSizeToShow((long) DataConverter.CLng(size));
        }

        protected void InitPage()
        {
            this.ValeTxtNewDir.ValidationExpression = "^[^\\/ :*?\"<>|.]+";
            this.ValeTxtFileName.ValidationExpression = "^[^\\/ :*?\"<>|.]+\\.css$";
            this.ValeTxtDirName.ValidationExpression = "^[^\\/ :*?\"<>|.]+";
            this.ValeTxtCopyDir.ValidationExpression = "^[^\\/ :*?\"<>|.]+";
            this.ValeTxtCopyFile.ValidationExpression = "^[^\\/ :*?\"<>|.]+\\.css$";
            string skinStyleDir = this.skinStyleDir;
            if (!string.IsNullOrEmpty(skinStyleDir))
            {
                if (!this.Page.IsPostBack)
                {
                    DirectoryInfo info = new DirectoryInfo(base.Request.PhysicalApplicationPath + skinStyleDir);
                    if (info.Exists)
                    {
                        foreach (DirectoryInfo info2 in info.GetDirectories("*", SearchOption.AllDirectories))
                        {
                            if (!info2.FullName.Contains("标签库") && !info2.FullName.Contains("分页标签库"))
                            {
                                string text = (info2.FullName.Remove(0, info.FullName.Length) + "/").Replace(@"\", "/");
                                this.DrpMove.Items.Add(new ListItem(text, text));
                            }
                        }
                    }
                }
                string str4 = BasePage.RequestString("Dir");
                if (!string.IsNullOrEmpty(str4))
                {
                    this.LblCurrentDir.Text = str4;
                    if (str4.LastIndexOf("/", StringComparison.Ordinal) > 0)
                    {
                        this.parentDir = str4.Remove(str4.LastIndexOf("/", StringComparison.Ordinal), str4.Length - str4.LastIndexOf("/", StringComparison.Ordinal));
                    }
                }
                else
                {
                    this.LblCurrentDir.Text = "/" + str4;
                }
                this.currentDirectory = base.Request.PhysicalApplicationPath + skinStyleDir + str4 + "/";
                this.currentDirectory = this.currentDirectory.Replace("/", @"\");
                this.currentDirectory = this.currentDirectory.Replace(@"\\", @"\");
                if (!this.Page.IsPostBack)
                {
                    DirectoryInfo info3 = new DirectoryInfo(this.currentDirectory);
                    if (info3.Exists)
                    {
                        this.BindData();
                    }
                }
            }
            else
            {
                this.PanButton.Visible = false;
                this.LitMessageText.Text = "请到网站配置中配置风格方案文件夹！";
                this.LitMessageText.Visible = true;
            }
            if (!string.IsNullOrEmpty(BasePage.RequestString("Dir")))
            {
                this.LitParentDirLink.Text = "<a href=\"StyleManage.aspx?Dir=" + this.parentDir + "\">返回上一级</a>";
                this.LitParentDirButton.Text = " <input id=\"Button1\" type=\"button\" class=\"inputbutton\" value=\"返回上一级\" onclick=\"javascript:location.href='StyleManage.aspx?Dir=" + this.parentDir + "';\" />";
            }
            else
            {
                this.LitParentDirLink.Text = "<a disabled='disabled'>返回上一级</a>";
                this.LitParentDirButton.Text = " <input id=\"Button1\" type=\"button\" class=\"inputbutton\" value=\"返回上一级\" disabled='disabled' />";
            }
        }

        protected bool IsEdit(string type, string contentType)
        {
            return (!(type == "1") && !(contentType != "css"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.skinStyleDir = "/Skin";
            this.m_UrlReferrer = "StyleManage.aspx?Dir=" + base.Server.UrlEncode(BasePage.RequestString("Dir"));
            this.SmpNavigator.CurrentNode = "<a href=\"StyleManage.aspx\">风格管理</a>";
            string str = BasePage.RequestString("Dir").Replace("..", string.Empty);
            if (!string.IsNullOrEmpty(str))
            {
                StringBuilder builder = new StringBuilder();
                string[] strArray = str.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    StringBuilder builder2 = new StringBuilder();
                    for (int j = 0; j <= i; j++)
                    {
                        builder2.Append("/" + strArray[j]);
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append(" >> ");
                    }
                    builder.Append("<a href=\"StyleManage.aspx?Dir=" + base.Server.UrlEncode(builder2.ToString()) + "\">" + strArray[i] + "</a>");
                }
                this.SmpNavigator.AdditionalNode = builder.ToString();
            }
            this.InitPage();
        }

        protected void Pager_PageChanged(object src, PageChangedEventArgs e)
        {
            this.Pager.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }
    }
}

