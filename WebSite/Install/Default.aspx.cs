namespace EasyOne.WebSite.Install
{
    using EasyOne.ExtendedControls;
    using EasyOne.Templates;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Reflection;

    public partial class Default : Page
    {
        
        private static FileVersionInfo FvInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private const string PreviousProductName = "PEOLD";
        public static readonly string ProductVersion = FvInfo.ProductVersion;
        private const string UpgradeProductName = "PENEW";
        

        public static TableRow BinDllCheck(ref bool error)
        {
            string binDirectory = HttpRuntime.BinDirectory;
            string[] strArray = new string[] { 
                "AjaxControlToolkit.dll", "CodeEngine.Framework.dll", "ICSharpCode.SharpZipLib.dll", "Microsoft.Practices.EnterpriseLibrary.Common.dll", "Microsoft.Practices.EnterpriseLibrary.Data.dll", "Microsoft.Practices.ObjectBuilder.dll", "Microsoft.ReportViewer.Common.dll", "Microsoft.ReportViewer.ProcessingObjectModel.dll", "Microsoft.ReportViewer.WebForms.dll", "EasyOne.Api.dll", "EasyOne.Common.dll", 
                "EasyOne.Components.dll", "EasyOne.Controls.dll","EasyOne.DalFactory.dll", "EasyOne.Enumerations.dll", "EasyOne.ExtendedControls.dll", "EasyOne.IDal.dll", "EasyOne.Logging.dll", "EasyOne.Model.dll", "EasyOne.ModelControls.dll", "EasyOne.SqlServerDal.dll", "EasyOne.StaticHtml.dll", 
                "EasyOne.Bll.dll", "EasyOne.Web.dll", "System.Web.Extensions.Design.dll", "System.Web.Extensions.dll", "UrlRewritingNet.UrlRewriter.dll"
             };
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Attributes["bgcolor"] = "#ffffff";
            cell.Attributes["width"] = "85%";
            TableCell cell2 = new TableCell();
            cell2.Attributes["bgcolor"] = "#ffffff";
            cell2.Attributes["width"] = "15%";
            Image child = new Image();
            try
            {
                FileInfo[] files = new DirectoryInfo(binDirectory).GetFiles("*.dll");
                foreach (string str2 in strArray)
                {
                    int num = 0;
                    foreach (FileInfo info2 in files)
                    {
                        if (string.Compare(str2, info2.Name, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            break;
                        }
                        num++;
                        if (num == files.Length)
                        {
                            error = true;
                        }
                    }
                }
                if (error)
                {
                    cell.Text = "缺少程序集文件,请将所有的dll文件复制到目录 " + binDirectory + " 中。";
                    child.ImageUrl = "images/error.gif";
                    cell2.Controls.Add(child);
                    return row;
                }
                cell.Text = "程序集文件完整性验证通过！";
                child.ImageUrl = "images/ok.gif";
                cell2.Controls.Add(child);
            }
            catch (DllNotFoundException)
            {
                cell.Text = "请将所有的程序集文件复制到目录 " + binDirectory + " 中。";
                child.ImageUrl = "images/error.gif";
                cell2.Controls.Add(child);
                error = true;
            }
            finally
            {
                row.Cells.Add(cell);
                row.Cells.Add(cell2);
            }
            return row;
        }

        protected void BtnCreateDateBase_Click(object sender, EventArgs e)
        {
            this.LblCreateDataBaseBefore.InnerText = "正在创建数据库。";
            this.LblCreateDataProgress.Visible = true;
            string connectString = "server=" + this.TxtDataSource.Text + ";database=" + this.TxtDataBase.Text + ";uid=" + this.TxtUserID.Text + ";pwd=" + this.HdnPassword.Value;
            string fileName = string.Empty;
            if (string.Compare(this.DropSqlVersion.SelectedValue, "2005", StringComparison.Ordinal) == 0)
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\SQLServer2005.sql");
            }
            if (string.Compare(this.DropSqlVersion.SelectedValue, "2000", StringComparison.Ordinal) == 0)
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\SQLServer2000.sql");
            }
            if (CreateDataBase(fileName, connectString))
            {
                this.ImgCreateDataBaseOK.Visible = true;
                this.LblCreateDataBaseBefore.InnerText = "创建数据库成功！";
                this.BtnCreateDateBase.Visible = false;
                this.NextButtonStep4.Enabled = true;
            }
            else
            {
                this.ImgCreateDataBaseFail.Visible = true;
                this.LblCreateDataBaseBefore.InnerText = "创建数据库失败！";
            }
            this.LblCreateDataProgress.Visible = false;
        }

        protected void BtnCreateDateBase_ServerClick(object sender, EventArgs e)
        {
        }

        private static void ChangeAdminPassword(string connectString, string password)
        {
            string str = string.Empty;
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                str = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "").ToLower();
            }
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = str;
            command.CommandText = "update PE_Admin set AdminPassword=@password WHERE AdminName='admin'";
            command.CommandType = CommandType.Text;
            command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException exception)
            {
                transaction.Rollback();
                throw new Exception(exception.Message);
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }

        protected void ChlkAgreeLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.StartNextButton.Enabled = this.ChlkAgreeLicense.Checked;
        }

        protected void ChlkIsCreateDataBase_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChlkIsCreateDataBase.Checked)
            {
                this.NextButtonStep4.Enabled = true;
                this.BtnCreateDateBase.Enabled = false;
            }
            else
            {
                this.NextButtonStep4.Enabled = false;
                this.BtnCreateDateBase.Enabled = true;
            }
        }

        private static bool CreateDataBase(string fileName, string connectString)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                            {
                                break;
                            }
                            builder.AppendLine(str);
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandText = builder.ToString();
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return true;
        }

        public static bool FolderCheck(string filepath)
        {
            try
            {
                using (new FileStream(filepath + @"\a.Txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                }
                File.Delete(filepath + @"\a.Txt");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InstallFileCheck()
        {
            bool error = false;
            this.TblInstallFileCheck.Rows.Add(BinDllCheck(ref error));
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Attributes["bgcolor"] = "#ffffff";
            cell.Attributes["width"] = "85%";
            TableCell cell2 = new TableCell();
            cell2.Attributes["bgcolor"] = "#ffffff";
            cell2.Attributes["width"] = "15%";
            Image child = new Image();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));
            if (info.GetFiles("*.sql").Length > 0)
            {
                cell.Text = "数据库查询脚本文件存在！";
                child.ImageUrl = "images/ok.gif";
                cell2.Controls.Add(child);
            }
            else
            {
                cell.Text = "数据库查询脚本文件没有！";
                child.ImageUrl = "images/error.gif";
                cell2.Controls.Add(child);
                error = true;
            }
            row.Cells.Add(cell);
            row.Cells.Add(cell2);
            this.TblInstallFileCheck.Rows.Add(row);
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Site.config");
            foreach (string str5 in ((ReadSiteConfigElement(fileName, "AdvertisementDir") + "," + ReadSiteConfigElement(fileName, "IncludeFilePath")) + ",Skin,Config,Template,Temp,UploadFiles").Split(new char[] { ',' }))
            {
                TableRow row2 = new TableRow();
                TableCell cell3 = new TableCell();
                cell3.Attributes["bgcolor"] = "#ffffff";
                cell3.Attributes["width"] = "85%";
                TableCell cell4 = new TableCell();
                cell4.Attributes["bgcolor"] = "#ffffff";
                cell4.Attributes["width"] = "15%";
                Image image2 = new Image();
                if (!FolderCheck(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str5)))
                {
                    cell3.Text = "对 " + str5 + " 目录没有写入和删除权限！";
                    image2.ImageUrl = "images/error.gif";
                    cell4.Controls.Add(image2);
                    error = true;
                }
                else
                {
                    cell3.Text = "对 " + str5 + " 目录权限验证通过！";
                    image2.ImageUrl = "images/ok.gif";
                    cell4.Controls.Add(image2);
                }
                row2.Cells.Add(cell3);
                row2.Cells.Add(cell4);
                this.TblInstallFileCheck.Rows.Add(row2);
            }
            if (error)
            {
                this.NextButtonStep2.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str;
                string strA = WebConfigurationManager.AppSettings["Version"];
                if (string.Compare(strA, ProductVersion, StringComparison.Ordinal) >= 0)
                {
                    //base.Response.Redirect("~/");
                }
                if (string.Compare(ReadSiteConfigElement(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Site.config"), "ProductEdition"), "eShop", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    str = "EasyOne SiteFactory eShop 1.2";
                }
                else
                {
                    str = "EasyOne SiteFactory CMS 1.2";
                }
                using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "License.txt"), Encoding.Default))
                {
                    this.TxtLicense.Value = reader.ReadToEnd();
                }
                this.LblProductName1.Text = str;
                this.LblProductName2.Text = str;
                this.LblProductName3.Text = str;
                this.LblProductCopyright.Text = FvInfo.LegalCopyright;
            }
        }

        protected void PreviousButtonStep3_Click(object sender, EventArgs e)
        {
            this.InstallFileCheck();
        }

        public static string ReadSiteConfigElement(string fileName, string element)
        {
            XmlTextReader reader = new XmlTextReader(fileName);
            string str = string.Empty;
            while (reader.Read())
            {
                if (string.Compare(reader.Name, element, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    str = reader.ReadString();
                    break;
                }
            }
            reader.Close();
            return str;
        }

        private void SetConfig(string valueList)
        {
            string str = valueList.Split(new char[] { ';' })[0];
            string str2 = valueList.Split(new char[] { ';' })[1];
            string str3 = valueList.Split(new char[] { ';' })[2];
            string str4 = valueList.Split(new char[] { ';' })[3];
            string str5 = valueList.Split(new char[] { ';' })[4];
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Site.config");
            WriteSiteConfigElement(fileName, "SiteTitle", str);
            WriteSiteConfigElement(fileName, "SiteUrl", str2);
            WriteSiteConfigElement(fileName, "ManageDir", str3);
            WriteSiteConfigElement(fileName, "SiteManageCode", str4);
            WriteSiteConfigElement(fileName, "VirtualPath", str5);
        }

        public static void WriteSiteConfigElement(string fileName, string element, string value)
        {
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (string.Compare(node2.Name, element, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        node2.InnerText = value;
                        break;
                    }
                }
            }
            document.Save(fileName);
        }

        protected void WzdInstall_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                IncludeFile.CreateAllIncludeFile();
            }
            catch
            {
            }
            base.Response.Redirect("~/");
        }

        protected void WzdInstall_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            switch (e.CurrentStepIndex)
            {
                case 0:
                    this.InstallFileCheck();
                    return;

                case 1:
                {
                    string connectionString = WebConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
                    this.TxtDataSource.Text = connectionString.Split(new char[] { ';' })[0].Split(new char[] { '=' })[1];
                    this.TxtDataBase.Text = connectionString.Split(new char[] { ';' })[1].Split(new char[] { '=' })[1];
                    this.TxtUserID.Text = connectionString.Split(new char[] { ';' })[2].Split(new char[] { '=' })[1];
                    return;
                }
                case 2:
                {
                    string str2 = "server=" + this.TxtDataSource.Text + ";database=" + this.TxtDataBase.Text + ";uid=" + this.TxtUserID.Text + ";pwd=" + this.TxtPassword.Text;
                    try
                    {
                        SqlConnection connection = new SqlConnection(str2);
                        connection.Open();
                        connection.Close();
                        this.HdnPassword.Value = this.TxtPassword.Text;
                    }
                    catch (SqlException)
                    {
                        this.LblCheckConnectString.Visible = true;
                        e.Cancel = true;
                    }
                    break;
                }
                case 3:
                {
                    string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Site.config");
                    this.TxtSiteTitle.Text = ReadSiteConfigElement(fileName, "SiteTitle");
                    this.TxtSiteUrl.Text = ReadSiteConfigElement(fileName, "SiteUrl");
                    this.TxtManageDir.Text = ReadSiteConfigElement(fileName, "ManageDir");
                    this.TxtSiteManageCode.Text = ReadSiteConfigElement(fileName, "SiteManageCode");
                    return;
                }
                case 4:
                {
                    string connectString = "server=" + this.TxtDataSource.Text + ";database=" + this.TxtDataBase.Text + ";uid=" + this.TxtUserID.Text + ";pwd=" + this.HdnPassword.Value;
                    string valueList = this.TxtSiteTitle.Text + ";" + this.TxtSiteUrl.Text + ";" + this.TxtManageDir.Text + ";" + this.TxtSiteManageCode.Text + ";" + base.Request.ApplicationPath;
                    try
                    {
                        this.SetConfig(valueList);
                        System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                        AppSettingsSection section = (AppSettingsSection) configuration.GetSection("appSettings");
                        ConnectionStringsSection section2 = (ConnectionStringsSection) configuration.GetSection("connectionStrings");
                        section2.ConnectionStrings["Connection String"].ConnectionString = connectString;
                        section.Settings["Version"].Value = ProductVersion;
                        configuration.Save();
                        try
                        {
                            ChangeAdminPassword(connectString, this.TxtAdminPassword.Text.Trim());
                        }
                        catch (Exception exception)
                        {
                            this.LblErrorMessage.Text = exception.Message;
                            e.Cancel = true;
                        }
                    }
                    catch
                    {
                        this.LblErrorMessage.Text = "配置信息没有写入，可能是因为Config配置文件下的AppSettings.config、ConnectionStrings.config及Site.config三个文件是只读文件！\r\n<br/>或者是它们的ASPNET（XP）或Network Service（Server）用户没有修改权限！";
                        e.Cancel = true;
                    }
                    break;
                }
                default:
                    return;
            }
        }
    }
}

