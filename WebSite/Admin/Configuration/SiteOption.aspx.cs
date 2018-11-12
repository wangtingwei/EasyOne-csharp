namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SiteOption : AdminPage
    {
        private const string DefaultSiteManageCode = "8888";

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                SiteConfigInfo config = SiteConfig.ConfigReadFromFile();
                SiteConfigInfo info2 = SiteConfig.ConfigInfo();
                bool flag = false;
                config.SiteOption.EnableSiteManageCode = DataConverter.CBoolean(this.RadlEnableSiteManageCode.SelectedValue);
                if (config.SiteOption.EnableSiteManageCode && string.IsNullOrEmpty(this.TxtSiteManageCode.Text.Trim()))
                {
                    AdminPage.WriteErrMsg("请指定后台管理认证码！");
                }
                config.SiteOption.SiteManageCode = this.TxtSiteManageCode.Text.Trim();
                config.SiteOption.TicketTime = DataConverter.CLng(this.TxtTicketTime.Text.Trim());
                config.SiteOption.EnableSoftKey = DataConverter.CBoolean(this.RadlEnableSoftKey.SelectedValue);
                config.SiteOption.ManageDir = this.TxtManageDir.Text.Trim();
                config.SiteOption.EnableUploadFiles = DataConverter.CBoolean(this.RadlEnableUploadFiles.SelectedValue);
                config.SiteOption.UploadFilePathRule = this.TxtUploadFilePathRule.Value;
                config.SiteOption.UploadFileMaxSize = DataConverter.CLng(this.TxtUploadFileMaxSize.Text);
                config.SiteOption.TemplateDir = this.TxtTemplateDir.Text.Trim();
                config.SiteOption.IncludeFilePath = this.TxtIncludeFilePath.Text.Trim();
                config.SiteOption.IsAbsoluatePath = DataConverter.CBoolean(this.RadlUrlType.SelectedValue);
                config.SiteOption.IsAutoSignIn = DataConverter.CBoolean(this.RadlIsAutoSignin.SelectedValue);
                config.SiteOption.AutoSignInTime = DataConverter.CLng(this.TxtAutoSigninTime.Text);
                config.SiteOption.RefreshQueueSize = DataConverter.CLng(this.TxtRefreshQueueSize.Text);
                config.SiteOption.CollectionSleep = DataConverter.CLng(this.TxtCollectionSleep.Text);
                if (info2.SiteOption.EnablePointMoneyExp != DataConverter.CBoolean(this.RadlEnablePointMoneyExp.SelectedValue))
                {
                    config.SiteOption.EnablePointMoneyExp = DataConverter.CBoolean(this.RadlEnablePointMoneyExp.SelectedValue);
                    flag = true;
                }
                System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/");
                ConnectionStringsSection section = configuration.Sections["connectionStrings"] as ConnectionStringsSection;
                bool flag2 = false;
                if (section != null)
                {
                    if (this.RadlConnProtecte.SelectedValue == "true")
                    {
                        if (!section.SectionInformation.IsProtected)
                        {
                            section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                            flag2 = true;
                        }
                    }
                    else if (section.SectionInformation.IsProtected)
                    {
                        section.SectionInformation.UnprotectSection();
                        flag2 = true;
                    }
                }
                try
                {
                    string str = "";
                    if (this.CheckFolder(config.SiteOption.AdvertisementDir, this.TxtADDir.Text.Trim()))
                    {
                        str = "因为修改了广告目录，请去手动修改Config/QueryStirng.config中的url=\"~/IAA/ADCount.aspx\"中的IAA为新的广告目录";
                    }
                    this.CheckFolder(config.SiteOption.UploadDir, this.TxtUploadDir.Text.Trim());
                    if (string.Compare(config.SiteOption.CreateHtmlPath, "/", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        config.SiteOption.CreateHtmlPath = string.Empty;
                    }
                    else
                    {
                        this.CheckFolder(config.SiteOption.CreateHtmlPath, this.TxtCreateHtmlPath.Text.Trim());
                    }
                    config.SiteOption.CreateHtmlPath = this.TxtCreateHtmlPath.Text.Trim();
                    config.SiteOption.AdvertisementDir = this.TxtADDir.Text.Trim();
                    config.SiteOption.UploadDir = this.TxtUploadDir.Text.Trim();
                    new SiteConfig().Update(config);
                    SiteCache.Remove("EasyOneSiteConfig");
                    if (string.Compare(this.ViewState["TemplateDir"].ToString(), config.SiteOption.TemplateDir, StringComparison.Ordinal) != 0)
                    {
                        SiteCache.RemoveByPattern(@"CK_Label_\S*");
                    }
                    if (flag2)
                    {
                        configuration.Save();
                    }
                    if (config.SiteOption.SiteManageCode == "8888")
                    {
                        str = str + "后台管理认证码使用的是系统默认值，为了网站安全，请及时修改！";
                    }
                    if (config.SiteOption.ManageDir == "Admin")
                    {
                        str = str + "后台管理目录名使用的是系统默认值，为了网站安全，请及时修改！";
                    }
                    if ((string.Compare(this.HdnManageDir.Value.Trim(), this.TxtManageDir.Text.Trim(), StringComparison.Ordinal) == 0) && !flag)
                    {
                        AdminPage.WriteSuccessMsg("<font color='red'>" + str + "</font><br>网站信息配置保存成功！", "SiteOption.aspx");
                    }
                    else
                    {
                        this.Session["IndexRightUrl"] = "Configuration/SiteOption.aspx";
                        this.Session["IndexLeftUrl"] = "Configuration/SiteConfigGuide.aspx";
                        base.Response.Write("<script>alert('" + str + @"\n网站信息配置保存成功！'); top.location = '../../" + this.TxtManageDir.Text.Trim() + "/index.aspx';</script>");
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("<li>文件未找到</li>", "SiteOption.aspx");
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "SiteOption.aspx");
                }
                catch (ConfigurationErrorsException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "SiteOption.aspx");
                }
            }
        }

        private bool CheckFolder(string oldName, string newName)
        {
            if (string.Compare(oldName, newName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return false;
            }
            string str = base.Server.MapPath("~/");
            if (string.IsNullOrEmpty(oldName))
            {
                Directory.CreateDirectory(str + newName);
            }
            else if (Directory.Exists(str + oldName))
            {
                if (!Directory.Exists(str + newName))
                {
                    Directory.Move(str + oldName, str + newName);
                }
            }
            else
            {
                FileInfo[] files = new DirectoryInfo(str + oldName).GetFiles();
                if (!string.IsNullOrEmpty(newName))
                {
                    newName = VirtualPathUtility.AppendTrailingSlash(newName);
                }
                foreach (FileInfo info2 in files)
                {
                    info2.CopyTo(str + newName + info2.Name);
                }
                Directory.Delete(str + oldName);
            }
            return true;
        }

        private void ModifySiteOption()
        {
            EasyOne.Components.SiteOption siteOption = SiteConfig.ConfigInfo().SiteOption;
            this.RadlEnableSiteManageCode.SelectedValue = this.SelectValue(siteOption.EnableSiteManageCode);
            this.RadlEnablePointMoneyExp.SelectedValue = this.SelectValue(siteOption.EnablePointMoneyExp);
            this.TxtSiteManageCode.Text = siteOption.SiteManageCode;
            if (siteOption.SiteManageCode == "8888")
            {
                this.LblNotes.Visible = true;
            }
            this.TxtTicketTime.Text = siteOption.TicketTime.ToString();
            this.RadlEnableSoftKey.SelectedValue = this.SelectValue(siteOption.EnableSoftKey);
            ConnectionStringsSection section = WebConfigurationManager.OpenWebConfiguration("~/").Sections["connectionStrings"] as ConnectionStringsSection;
            if (section != null)
            {
                this.RadlConnProtecte.SelectedValue = section.SectionInformation.IsProtected.ToString().ToLower();
            }
            this.HdnManageDir.Value = siteOption.ManageDir;
            this.TxtManageDir.Text = siteOption.ManageDir;
            this.TxtADDir.Text = siteOption.AdvertisementDir;
            this.TxtCreateHtmlPath.Text = siteOption.CreateHtmlPath;
            this.TxtUploadDir.Text = siteOption.UploadDir;
            this.TxtUploadFilePathRule.Value = siteOption.UploadFilePathRule;
            this.TxtUploadFileMaxSize.Text = siteOption.UploadFileMaxSize.ToString();
            this.ViewState["TemplateDir"] = siteOption.TemplateDir;
            this.TxtTemplateDir.Text = siteOption.TemplateDir;
            this.TxtIncludeFilePath.Text = siteOption.IncludeFilePath;
            this.RadlUrlType.SelectedValue = this.SelectValue(siteOption.IsAbsoluatePath);
            this.RadlEnableUploadFiles.SelectedValue = this.SelectValue(siteOption.EnableUploadFiles);
            this.RadlIsAutoSignin.SelectedValue = this.SelectValue(siteOption.IsAutoSignIn);
            this.TxtAutoSigninTime.Text = siteOption.AutoSignInTime.ToString();
            this.TxtRefreshQueueSize.Text = siteOption.RefreshQueueSize.ToString();
            this.TxtCollectionSleep.Text = siteOption.CollectionSleep.ToString();
            this.RadlUrlType.Items[0].Text = DataSecurity.HtmlEncode("相对路径（形如：<a href='/News/200509/1358.html'>标题</a>）当一个网站有多个域名时，一般采用此方式当一个网站有多个镜像网站时，必须采用此方式");
            this.RadlUrlType.Items[1].Text = DataSecurity.HtmlEncode("绝对路径（形如：<a href='http://www.EasyOne.net/News/200509/1358.html'>标题</a>）当要把频道做为子站点来访问时，必须使用此方式要使用此方式，必须把网站URL设置正确。");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteConfig.SiteInfo.ProductEdition.CompareTo("eShop") == 0)
            {
                this.EnablePointMoneyExp.Style.Add("display", "none");
            }
            if (!this.Page.IsPostBack)
            {
                this.ModifySiteOption();
            }
        }

        private string SelectValue(bool selected)
        {
            if (selected)
            {
                return "true";
            }
            return "false";
        }
    }
}

