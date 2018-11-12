namespace EasyOne.WebSite.Admin.Template
{
    using ICSharpCode.SharpZipLib.Zip;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class LabelManageUI : AdminPage
    {

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Label.aspx");
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (this.GdvLabelList.SelectList.Length <= 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的标签！</li>");
            }
            else if (LabelManage.Delete(this.GdvLabelList.SelectList.ToString()))
            {
                this.GdvLabelList.DataBind();
            }
            else
            {
                AdminPage.WriteErrMsg("删除失败！");
            }
        }

        protected void BtnDW_Click(object sender, EventArgs e)
        {
            string str2 = HttpContext.Current.Server.MapPath("~/" + SiteConfig.SiteOption.LabelDir);
            if (!FileSystemObject.IsExist(str2 + @"\Dreamweaver", FsoMethod.Folder))
            {
                FileSystemObject.Create(str2 + @"\Dreamweaver", FsoMethod.Folder);
                if (!FileSystemObject.IsExist(str2 + @"\Dreamweaver\_folderinfo.txt", FsoMethod.File))
                {
                    FileSystemObject.Create(str2 + @"\Dreamweaver\_folderinfo.txt", FsoMethod.File);
                    FileSystemObject.WriteFile(str2 + @"\Dreamweaver\_folderinfo.txt", "PE2007标签");
                }
            }
            foreach (LabelManageInfo info in LabelManage.GetLabelList(string.Empty))
            {
                StringBuilder builder = new StringBuilder();
                IList<LabelAttributeInfo> attributeList = LabelManage.GetAttributeList(str2 + @"\" + info.Name + ".config");
                if (!FileSystemObject.IsExist(str2 + @"\Dreamweaver\" + info.Name + ".csn", FsoMethod.File))
                {
                    FileSystemObject.Create(str2 + @"\Dreamweaver\" + info.Name + ".csn", FsoMethod.File);
                }
                builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
                builder.Append("<snippet name = \"" + info.Name + "\" description = \"" + info.Intro + "\" preview=\"code\" type=\"block\">\n");
                builder.Append("<insertText location=\"beforeSelection\">\n");
                builder.Append("<![CDATA[{PE.Label ");
                builder.Append("id=\"" + info.Name + "\"");
                foreach (LabelAttributeInfo info2 in attributeList)
                {
                    builder.Append(" " + info2.AttributeName + "=\"" + info2.DefaultValue + "\"");
                }
                builder.Append(" /}]]>\n");
                builder.Append("</insertText>\n");
                builder.Append("<insertText location=\"afterSelection\"><![CDATA[]]>\n");
                builder.Append("</insertText>\n");
                builder.Append("</snippet>\n");
                FileSystemObject.WriteFile(str2 + @"\Dreamweaver\" + info.Name + ".csn", builder.ToString());
            }
            PackFiles(str2 + @"\Dreamweaver.zip", str2 + @"\Dreamweaver");
            if (!ResponseFile(this.Page.Request, this.Page.Response, "PE2007_DwPlus.zip", str2 + @"\Dreamweaver.zip", 0xfa000L))
            {
                base.Response.Write("下载文件出错！");
            }
        }

        protected void GdvLabelList_RowCommand(object sender, CommandEventArgs e)
        {
            bool flag = false;
            switch (e.CommandName)
            {
                case "Deleted":
                    if (LabelManage.Delete(e.CommandArgument.ToString()))
                    {
                        AdminPage.WriteSuccessMsg("删除成功", "LabelManage.aspx");
                        return;
                    }
                    AdminPage.WriteErrMsg("删除失败", "LabelManage.aspx");
                    return;

                case "Copy":
                    if (LabelManage.Copy(e.CommandArgument.ToString()))
                    {
                        AdminPage.WriteSuccessMsg("复制成功", "LabelManage.aspx");
                        return;
                    }
                    AdminPage.WriteErrMsg("复制失败", "LabelManage.aspx");
                    return;
            }
            flag = false;
        }

        public static void PackFiles(string fileName, string directory)
        {
            try
            {
                FastZip zip = new FastZip();
                zip.CreateEmptyDirectories = true;
                zip.CreateZip(fileName, directory, true, "");
                zip = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public static bool ResponseFile(HttpRequest _request, HttpResponse _response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream input = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(input);
                try
                {
                    _response.AddHeader("Accept-Ranges", "bytes");
                    _response.Buffer = false;
                    long length = input.Length;
                    long num2 = 0L;
                    int count = 0x2800;
                    int millisecondsTimeout = ((int) Math.Floor((decimal) ((1000M * count) / _speed))) + 1;
                    if (_request.Headers["Range"] != null)
                    {
                        _response.StatusCode = 0xce;
                        num2 = Convert.ToInt64(_request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    _response.AddHeader("Content-Length", (length - num2).ToString());
                    if (num2 != 0L)
                    {
                        _response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num2, length - 1L, length));
                    }
                    _response.AddHeader("Connection", "Keep-Alive");
                    _response.ContentType = "application/octet-stream";
                    _response.Charset = "UTF-8";
                    _response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                    _response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
                    reader.BaseStream.Seek(num2, SeekOrigin.Begin);
                    int num5 = ((int) Math.Floor((decimal) ((length - num2) / count))) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        if (_response.IsClientConnected)
                        {
                            _response.BinaryWrite(reader.ReadBytes(count));
                            Thread.Sleep(millisecondsTimeout);
                        }
                        else
                        {
                            i = num5;
                        }
                    }
                    _response.End();
                }
                catch (HttpException)
                {
                    return false;
                }
                finally
                {
                    reader.Close();
                    input.Close();
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}

