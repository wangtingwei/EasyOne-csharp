namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class LabelUpload : AdminPage
    {

        protected void EBtnUpload_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.FileUpload1.HasFile)
            {
                string fileName = this.FileUpload1.FileName;
                if (Path.GetExtension(fileName) == ".config")
                {
                    if (!LabelManage.Exists(Path.GetFileNameWithoutExtension(fileName)))
                    {
                        string path = WebConfigurationManager.AppSettings["EasyOne:LabelXsltPath"];
                        path = HttpContext.Current.Server.MapPath(path) + @"\" + fileName;
                        this.FileUpload1.SaveAs(path);
                        bool flag = false;
                        XmlDocument document = new XmlDocument();
                        try
                        {
                            document.Load(path);
                            if (((document.SelectSingleNode("root") != null) && (document.SelectSingleNode("root/LabelType") != null)) && (document.SelectSingleNode("root/LabelTemplate") != null))
                            {
                                flag = true;
                            }
                        }
                        catch (XmlException)
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            string str3 = "~/" + SiteConfig.SiteOption.LabelDir;
                            string destFileName = HttpContext.Current.Server.MapPath(str3) + @"\" + fileName;
                            try
                            {
                                File.Copy(path, destFileName, false);
                            }
                            catch (IOException)
                            {
                                File.Delete(path);
                                AdminPage.WriteErrMsg("上传错误！请检查标签文件格式是否正确");
                            }
                            AdminPage.WriteSuccessMsg("上传成功", "javascript:opener.location.reload();window.close();");
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("文件不是规范的标签文件");
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("标签已存在，请改名后上传");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("请上传.config文件");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

