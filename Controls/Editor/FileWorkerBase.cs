namespace EasyOne.Controls.Editor
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public abstract class FileWorkerBase : Page
    {
        private const string DEFAULT_USER_FILES_PATH = "/UserFiles/";
        private string sUserFilesDirectory;
        private string sUserFilesPath;

        protected FileWorkerBase()
        {
        }

        protected string UserFilesDirectory
        {
            get
            {
                if (this.sUserFilesDirectory == null)
                {
                    this.sUserFilesDirectory = base.Server.MapPath(this.UserFilesPath);
                    if (!FileSystemObject.IsExist(this.sUserFilesDirectory, FsoMethod.Folder))
                    {
                        FileSystemObject.Create(this.sUserFilesDirectory, FsoMethod.Folder);
                    }
                }
                return this.sUserFilesDirectory;
            }
        }

        protected string UserFilesPath
        {
            get
            {
                if (this.sUserFilesPath == null)
                {
                    this.sUserFilesPath = (string) base.Application["EasyOne:UserFilesPath"];
                    if ((this.sUserFilesPath == null) || (this.sUserFilesPath.Length == 0))
                    {
                        this.sUserFilesPath = (string) this.Session["EasyOne:UserFilesPath"];
                        if ((this.sUserFilesPath == null) || (this.sUserFilesPath.Length == 0))
                        {
                            this.sUserFilesPath = ConfigurationManager.AppSettings["EasyOne:UserFilesPath"];
                            if ((this.sUserFilesPath == null) || (this.sUserFilesPath.Length == 0))
                            {
                                this.sUserFilesPath = "/UserFiles/";
                            }
                            if ((this.sUserFilesPath == null) || (this.sUserFilesPath.Length == 0))
                            {
                                this.sUserFilesPath = base.Request.QueryString["ServerPath"];
                            }
                        }
                    }
                    if (!this.sUserFilesPath.EndsWith("/", StringComparison.Ordinal))
                    {
                        this.sUserFilesPath = this.sUserFilesPath + "/";
                    }
                }
                HttpPostedFile file = base.Request.Files["NewFile"];
                this.sUserFilesPath = this.sUserFilesPath.Replace("{$FileType}", Path.GetExtension(file.FileName).ToLower().Replace(".", ""));
                this.sUserFilesPath = this.sUserFilesPath.Replace("{$Year}", DateTime.Now.Year.ToString());
                this.sUserFilesPath = this.sUserFilesPath.Replace("{$Month}", DateTime.Now.Month.ToString());
                this.sUserFilesPath = this.sUserFilesPath.Replace("{$Day}", DateTime.Now.Day.ToString());
                this.sUserFilesPath = this.sUserFilesPath.Replace("//", "/");
                if (base.Request.ApplicationPath.EndsWith("/", StringComparison.Ordinal))
                {
                    return ("/" + SiteConfig.SiteOption.UploadDir + "/" + this.sUserFilesPath).Replace("//", "/");
                }
                return (base.Request.ApplicationPath + "/" + SiteConfig.SiteOption.UploadDir + this.sUserFilesPath);
            }
        }
    }
}

