namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class MultiplePhotoUpload : AdminPage
    {

        private string m_FieldName;
        private string m_FileExtArr;
        private int m_ModelId;
        private ModelInfo m_ModelInfo;
        private int m_NodeId;
        private NodeInfo m_NodeInfo;
        private int m_PhotoSize;
        private string m_ShowPath;
        private bool m_Watermark;

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.LblMessage.Text = "权限错误：你当前的网站没有开启上传功能，请检查你的网站配置。";
            }
            else
            {
                string str = BasePage.RequestString("ReturnJSFunction");
                int num = DataConverter.CLng(base.Request.Form["ThumbIndex"]);
                if (string.IsNullOrEmpty(str))
                {
                    str = "DealwithUpload";
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                int num2 = 0;
                StringBuilder builder2 = new StringBuilder();
                if (!PEContext.Current.Admin.Identity.IsAuthenticated)
                {
                    int uploadSize = PEContext.Current.User.UserInfo.UserPurview.UploadSize;
                    if (this.m_PhotoSize > uploadSize)
                    {
                        this.m_PhotoSize = uploadSize;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    num2++;
                    System.Web.UI.WebControls.FileUpload upload = (System.Web.UI.WebControls.FileUpload) this.FindControl("FileUpload" + i.ToString());
                    if (upload.HasFile)
                    {
                        string str2 = Path.GetExtension(upload.FileName).ToLower();
                        if (!this.CheckFilePostfix(str2.Replace(".", "")))
                        {
                            builder2.Append("文件" + upload.FileName + "上传文件类型不对！必须上传" + this.m_FileExtArr + @"的后缀名！\n");
                        }
                        else if (((int) upload.FileContent.Length) > (this.m_PhotoSize * 0x400))
                        {
                            builder2.Append("文件" + upload.FileName + "请上传小于" + this.m_PhotoSize.ToString() + @"KB的文件！\n");
                        }
                        else
                        {
                            string str3 = DataSecurity.MakeFileRndName() + i.ToString();
                            string filename = FileSystemObject.CreateFileFolder((VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + this.FileSavePath(upload.FileName)).Replace("//", "/"), HttpContext.Current) + str3 + str2;
                            upload.SaveAs(filename);
                            Thumbs.GetThumbsPath(this.m_ShowPath + str3 + str2, this.m_ShowPath + str3 + "_S" + str2);
                            if (this.m_Watermark)
                            {
                                WaterMark.AddWaterMark(this.m_ShowPath + str3 + str2);
                            }
                            EasyOne.Model.Accessories.FileInfo fileInfo = new EasyOne.Model.Accessories.FileInfo();
                            fileInfo.Name = upload.FileName;
                            fileInfo.Path = this.m_ShowPath + str3 + str2;
                            fileInfo.Size = (int) upload.FileContent.Length;
                            fileInfo.Quote = 1;
                            Files.Add(fileInfo);
                            if (i == num)
                            {
                                builder.Append("parent." + str + "ChangeThumbField(\"" + fileInfo.Path + "\",\"" + this.m_ShowPath + str3 + "_S" + str2 + "\");");
                            }
                            else
                            {
                                builder.Append("parent." + str + "DealwithUpload(\"" + fileInfo.Path + "\",\"" + fileInfo.Size.ToString() + "\",\"" + fileInfo.Id.ToString() + "\",\"" + this.m_ShowPath + str3 + "_S" + str2 + "\");");
                            }
                            builder2.Append("文件" + upload.FileName + @"上传成功！\n");
                        }
                    }
                }
                if (builder2.Length > 0)
                {
                    builder.Append("parent." + str + "ErrMessage(\"" + builder2.ToString() + "\");");
                }
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
            }
        }

        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }

        private string FileSavePath(string fileName)
        {
            string str = "";
            if ((this.m_NodeInfo != null) && SiteConfig.SiteOption.EnableUploadFiles)
            {
                str = Nodes.UploadPathParse(this.m_NodeInfo, fileName);
                this.m_ShowPath = str;
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["EasyOne:NodeId"] != null)
            {
                this.m_NodeId = (int) this.Session["EasyOne:NodeId"];
            }
            else
            {
                this.m_NodeId = DataConverter.CLng(base.Request["NodeId"]);
            }
            this.m_ModelId = DataConverter.CLng(base.Request["ModelId"]);
            this.m_FieldName = BasePage.RequestString("FieldName");
            if (this.m_NodeId > 0)
            {
                this.m_NodeInfo = Nodes.GetCacheNodeById(this.m_NodeId);
            }
            if (this.m_ModelId > 0)
            {
                this.m_ModelInfo = ModelManager.GetModelInfoById(this.m_ModelId);
            }
            if ((this.m_ModelInfo != null) && !string.IsNullOrEmpty(this.m_FieldName))
            {
                Collection<string> settings = Field.GetFieldInfoByFieldName(this.m_ModelId, this.m_FieldName).Settings;
                this.m_PhotoSize = DataConverter.CLng(settings[0]);
                this.m_FileExtArr = settings[1];
                if (settings.Count >= 3)
                {
                    this.m_Watermark = DataConverter.CBoolean(settings[2]);
                }
            }
            if (string.IsNullOrEmpty(this.m_FileExtArr))
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
        }
    }
}

