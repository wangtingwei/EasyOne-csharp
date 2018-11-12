namespace EasyOne.WebSite.User.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FileUpload : BasePage
    {
        protected Button BtnUpload;
        protected HtmlForm form1;
        protected System.Web.UI.WebControls.FileUpload FupFile;
        protected Label LblMessage;
        private string m_FieldName;
        private string m_FileExtArr;
        private int m_ModelId;
        private ModelInfo m_ModelInfo;
        private string m_ModuleName;
        private int m_NodeId;
        private NodeInfo m_NodeInfo;
        private string m_ShowPath;
        protected EasyOne.Controls.RequiredFieldValidator ValFile;

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (!this.FupFile.HasFile)
            {
                this.ReturnManage("上传失败，重新上传。");
                return;
            }
            int uploadFileMaxSize = 0;
            int uploadSize = 0;
            bool flag = false;
            bool flag2 = false;
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.ReturnManage("权限错误：对不起网站没有开启上传权限。");
                return;
            }
            if (!PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                if (!PEContext.Current.User.Identity.IsAuthenticated)
                {
                    UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(-2);
                    if (string.IsNullOrEmpty(userGroupById.GroupSetting))
                    {
                        this.ReturnManage("匿名会员组不存在！");
                        return;
                    }
                    UserPurviewInfo groupSetting = UserGroups.GetGroupSetting(userGroupById.GroupSetting);
                    if (groupSetting.IsNull)
                    {
                        this.ReturnManage("匿名会员组没有进行权限设置！");
                        return;
                    }
                    if (!groupSetting.EnableUpload)
                    {
                        this.ReturnManage("匿名会员组没有开启上传权限！");
                        return;
                    }
                    uploadSize = groupSetting.UploadSize;
                }
                else
                {
                    if (!PEContext.Current.User.UserInfo.UserPurview.EnableUpload)
                    {
                        this.ReturnManage("所属会员组没有开启上传权限！");
                        return;
                    }
                    uploadSize = PEContext.Current.User.UserInfo.UserPurview.UploadSize;
                }
            }
            string str = Path.GetExtension(this.FupFile.FileName).ToLower();
            if (!this.CheckFilePostfix(str.Replace(".", "")))
            {
                this.ReturnManage("上传文件类型不对！必须上传" + this.m_FileExtArr + "的后缀名！");
                return;
            }
            if (string.Compare(this.m_ModuleName, "Node", StringComparison.OrdinalIgnoreCase) == 0)
            {
                FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(this.m_ModelId, this.m_FieldName);
                Collection<string> settings = fieldInfoByFieldName.Settings;
                switch (fieldInfoByFieldName.FieldType)
                {
                    case FieldType.PictureType:
                        uploadFileMaxSize = DataConverter.CLng(settings[1]);
                        flag2 = DataConverter.CBoolean(settings[4]);
                        flag = DataConverter.CBoolean(settings[5]);
                        goto Label_01EA;

                    case FieldType.FileType:
                        uploadFileMaxSize = DataConverter.CLng(settings[0]);
                        goto Label_01EA;
                }
            }
            else
            {
                uploadFileMaxSize = SiteConfig.SiteOption.UploadFileMaxSize;
            }
        Label_01EA:
            if (!PEContext.Current.Admin.Identity.IsAuthenticated && (uploadFileMaxSize > uploadSize))
            {
                uploadFileMaxSize = uploadSize;
            }
            if (((int) this.FupFile.FileContent.Length) > (uploadFileMaxSize * 0x400))
            {
                this.ReturnManage("请上传小于" + uploadFileMaxSize.ToString() + "KB的文件！");
            }
            else
            {
                string str2 = DataSecurity.MakeFileRndName();
                string filename = FileSystemObject.CreateFileFolder((VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + this.FileSavePath()).Replace("//", "/"), HttpContext.Current) + str2 + str;
                this.FupFile.SaveAs(filename);
                string thumbnailPath = "";
                if (flag)
                {
                    thumbnailPath = this.m_ShowPath + str2 + "_S" + str;
                    Thumbs.GetThumbsPath(this.m_ShowPath + str2 + str, thumbnailPath);
                }
                else
                {
                    thumbnailPath = this.m_ShowPath + str2 + str;
                }
                if (flag2)
                {
                    WaterMark.AddWaterMark(this.m_ShowPath + str2 + str);
                }
                EasyOne.Model.Accessories.FileInfo fileInfo = new EasyOne.Model.Accessories.FileInfo();
                fileInfo.Name = this.FupFile.FileName;
                fileInfo.Path = thumbnailPath;
                fileInfo.Size = (int) this.FupFile.FileContent.Length;
                fileInfo.Quote = 1;
                if (string.Compare(this.m_ModuleName, "soft", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Files.Add(fileInfo);
                }
                this.GetScriptByModuleName(fileInfo);
                this.ReturnManage("上传成功！");
            }
        }

        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }

        private string FileSavePath()
        {
            string str = "";
            if (this.m_NodeInfo != null)
            {
                if (SiteConfig.SiteOption.EnableUploadFiles)
                {
                    str = Nodes.UploadPathParse(this.m_NodeInfo, this.FupFile.FileName);
                    this.m_ShowPath = str;
                }
                return str;
            }
            if (string.Compare(this.m_ModuleName, "ADZone", StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "/" + SiteConfig.SiteOption.AdvertisementDir + "/UploadADPic/";
                this.m_ShowPath = str;
            }
            else if (string.Compare(this.m_ModuleName, "Author", StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "/AuthorPic/";
                this.m_ShowPath = str;
            }
            else if (string.Compare(this.m_ModuleName, "Source", StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "/CopyFromPic/";
                this.m_ShowPath = str;
            }
            else if (string.Compare(this.m_ModuleName, "Trademark", StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "/TrademarkPic/";
                this.m_ShowPath = str;
            }
            else if (string.Compare(this.m_ModuleName, "Producer", StringComparison.OrdinalIgnoreCase) == 0)
            {
                str = "/ProducerPic/";
                this.m_ShowPath = str;
            }
            else
            {
                str = "/";
            }
            str = str + DataSecurity.MakeFolderName() + "/";
            this.m_ShowPath = str;
            return str;
        }

        private void GetScriptByModuleName(EasyOne.Model.Accessories.FileInfo fileInfo)
        {
            string str = BasePage.RequestString("ReturnJSFunction");
            string str2 = BasePage.RequestString("CustomReturnJSFunction");
            if (string.IsNullOrEmpty(str))
            {
                str = "DealwithUpload";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            builder.Append("  parent." + str + "(\"" + fileInfo.Path + "\",\"" + fileInfo.Size.ToString() + "\",\"" + fileInfo.Id.ToString() + "\");");
            if (!string.IsNullOrEmpty(str2))
            {
                builder.Append("  parent." + str2 + "(\"" + fileInfo.Path + "\",\"" + fileInfo.Size.ToString() + "\",\"" + fileInfo.Id.ToString() + "\");");
            }
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }

        private void InitFileExtArr()
        {
            if ((this.m_ModelInfo != null) && !string.IsNullOrEmpty(this.m_FieldName))
            {
                FieldInfo fieldInfoByFieldName = Field.GetFieldInfoByFieldName(this.m_ModelId, this.m_FieldName);
                Collection<string> settings = fieldInfoByFieldName.Settings;
                switch (fieldInfoByFieldName.FieldType)
                {
                    case FieldType.PictureType:
                        this.m_FileExtArr = settings[2];
                        break;

                    case FieldType.FileType:
                        this.m_FileExtArr = settings[1];
                        break;

                    case FieldType.MultiplePhotoType:
                        this.m_FileExtArr = settings[1];
                        break;
                }
            }
            if (string.Compare(this.m_ModuleName, "ADZone", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp|fla";
            }
            if (string.Compare(this.m_ModuleName, "Author", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
            if (string.Compare(this.m_ModuleName, "Source", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
            if (string.Compare(this.m_ModuleName, "Trademark", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
            if (string.Compare(this.m_ModuleName, "Producer", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
            if (string.Compare(this.m_ModuleName, "Shop", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.m_FileExtArr = "gif|png|jpeg|jpg|gif|bmp";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ModuleName = BasePage.RequestString("ModuleName");
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
            this.InitFileExtArr();
        }

        private void ReturnManage(string manage)
        {
            if (!string.IsNullOrEmpty(manage))
            {
                string str = BasePage.RequestString("ReturnJSFunction");
                if (string.IsNullOrEmpty(str))
                {
                    str = "DealwithUpload";
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                builder.Append("   parent." + str + "ErrMessage(\"" + manage + "\");");
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), str + "UpdateParent", builder.ToString());
            }
        }
    }
}

