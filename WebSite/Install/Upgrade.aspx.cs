namespace EasyOne.WebSite.Install
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Templates;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Reflection;

    public partial class Upgrade : Page
    {
        public string ConnectionString = "";
        public string dataBaseVersion = "0.0.0.0";
        private static FileVersionInfo fvInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private bool m_UpdateCustomTable;
        public static readonly string ProductCopyright = fvInfo.LegalCopyright;
        public string ProductName;
        public static readonly string ProductVersion = fvInfo.ProductVersion;

        private void AddDictionary()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT PE_Dictionary(Title,TableName,FieldName,FieldValue)VALUES('作者类型','PE_Author','Type','大陆作者|1|1$港台作者|1|0$海外作者|1|0$本站特约|1|0$其他作者|1|0$');INSERT PE_Dictionary(Title,TableName,FieldName,FieldValue)VALUES('订单类型','PE_Orders','OrderType','正常订单|1|1$缺货的订单|1|0$有问题订单|1|0');INSERT PE_Dictionary(Title,TableName,FieldName,FieldValue)VALUES('要求送货时间','PE_Orders','DeliveryTime','对送货时间没有特殊要求|1|1$双休日或者周一至周五的晚上送达|1|0$周一至周五的白天送达|1|0$')";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void AddVersion(string version)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                string[] strArray = version.ToString().Split(new char[] { '.' });
                command.CommandText = "INSERT INTO PE_Version (Major,Minor,Build,Revision,CreatedDate)VALUES(@Major,@Minor,@Build,@Revision,@CreatedDate)";
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Major", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Minor", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Build", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Revision", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime));
                command.Parameters["@Major"].Value = strArray[0];
                command.Parameters["@Minor"].Value = strArray[1];
                command.Parameters["@Build"].Value = strArray[2];
                command.Parameters["@Revision"].Value = strArray[3];
                command.Parameters["@CreatedDate"].Value = DateTime.Now;
                command.ExecuteNonQuery();
                base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;更新版本号&nbsp;" + version + "<br/>");
                base.Response.Flush();
                connection.Close();
            }
        }

        protected void BtnUpgrade_Click(object sender, EventArgs e)
        {
            this.ResponseHeader();
            this.UpgradeScripts();
            base.Response.Flush();
            this.UpgradeTablesToDbo();
            base.Response.Flush();
            this.RCToRelease();
            base.Response.Flush();
            this.UpgradeTo1100();
            this.UpgradeTo1101();
            base.Response.Flush();
            this.UpgradeAppSettings();
            this.CreateAllIncludeFile();
            base.Response.Flush();
            this.ResponseEnd();
        }

        private static bool CheckColumn(string tableName, string columnName)
        {
            foreach (string str in GetNoDecodeColumn())
            {
                string[] strArray2 = str.Split(new char[] { '|' });
                if ((tableName == strArray2[0]) && (columnName == strArray2[1]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckCustomTable(string tableName)
        {
            if (tableName.IndexOf("PE_U_", StringComparison.Ordinal) < 0)
            {
                return false;
            }
            if (!this.m_UpdateCustomTable)
            {
                foreach (ModelInfo info in ModelManager.GetModelList(ModelType.None, ModelShowType.None))
                {
                    if (!info.IsNull)
                    {
                        base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;" + info.TableName + "<br/>");
                        base.Response.Flush();
                        StringBuilder sb = new StringBuilder();
                        IList<Model.CommonModel.FieldInfo> fieldListByModelId = ModelManager.GetFieldListByModelId(info.ModelId);
                        List<string> list3 = new List<string>();
                        foreach (Model.CommonModel.FieldInfo info2 in fieldListByModelId)
                        {
                            if ((info2.FieldLevel == 1) && CheckEncodeFieldType(info2.FieldType))
                            {
                                list3.Add(info2.FieldName);
                                StringHelper.AppendString(sb, info2.FieldName + "=[dbo].[Decode](" + info2.FieldName + ")");
                            }
                        }
                        if (sb.Length > 0)
                        {
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                                {
                                    connection.Open();
                                    SqlCommand command = connection.CreateCommand();
                                    command.CommandTimeout = 0;
                                    command.CommandText = "Update " + info.TableName + " Set " + sb.ToString();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }
                                continue;
                            }
                            catch
                            {
                                using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
                                {
                                    connection2.Open();
                                    SqlCommand command2 = connection2.CreateCommand();
                                    foreach (string str in list3)
                                    {
                                        try
                                        {
                                            command2.CommandText = "Update " + info.TableName + " Set " + str + "=[dbo].[Decode](" + str + ")";
                                            command2.CommandTimeout = 0;
                                            command2.ExecuteNonQuery();
                                            continue;
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                    connection2.Close();
                                }
                                continue;
                            }
                        }
                    }
                }
                this.m_UpdateCustomTable = true;
            }
            return true;
        }

        private static bool CheckEncodeFieldType(FieldType fieldType)
        {
            if (((((fieldType != FieldType.TextType) && (fieldType != FieldType.MultipleTextType)) && ((fieldType != FieldType.ListBoxType) && (fieldType != FieldType.LookType))) && (((fieldType != FieldType.LinkType) && (fieldType != FieldType.CountType)) && ((fieldType != FieldType.PictureType) && (fieldType != FieldType.MultiplePhotoType)))) && ((((fieldType != FieldType.ColorType) && (fieldType != FieldType.AuthorType)) && ((fieldType != FieldType.SourceType) && (fieldType != FieldType.KeywordType))) && (((fieldType != FieldType.OperatingType) && (fieldType != FieldType.Producer)) && (((fieldType != FieldType.Trademark) && (fieldType != FieldType.TitleType)) && (fieldType != FieldType.DownServerType)))))
            {
                return false;
            }
            return true;
        }

        private static bool CheckTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName) || !tableName.StartsWith("PE_", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            foreach (string str in GetNoDecodeTable())
            {
                if (tableName == str)
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckVersionUpgrade()
        {
            string productVersion = ProductVersion;
            if (string.Compare(this.dataBaseVersion, productVersion, StringComparison.Ordinal) >= 0)
            {
                base.Response.Redirect("~/");
            }
        }

        private void CreateAllIncludeFile()
        {
            try
            {
                IncludeFile.CreateAllIncludeFile();
            }
            catch
            {
            }
        }

        private void DecodeDataBase()
        {
            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;对数据库数据进行HtmlDecode解码，数据量大的可能要耐心等候一段时间，在此过程中请不要关闭浏览器<br/>");
            base.Response.Flush();
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                connection.Open();
                if (this.DropSqlVersion.SelectedValue == "2000")
                {
                    command.CommandText = "\r\nIF  EXISTS (SELECT * FROM dbo.sysobjects WHERE name='Decode' AND type='FN')\r\nDROP FUNCTION [dbo].[Decode]\r\n";
                }
                else
                {
                    command.CommandText = "\r\nIF  EXISTS (SELECT * FROM sys.objects WHERE name='Decode' AND type_desc = 'SQL_SCALAR_FUNCTION')\r\nDROP FUNCTION [dbo].[Decode]\r\n";
                }
                command.ExecuteNonQuery();
                command.CommandText = "\r\nCREATE FUNCTION [dbo].[Decode]\r\n (@str nvarchar(4000))  \r\nRETURNS nvarchar(4000)\r\nAS  \r\nBEGIN \r\nSELECT @str = REPLACE(@str,N'<br>',char(13)+ char(10))\r\nSELECT @str = REPLACE(@str,N'&gt;',N'>')\r\nSELECT @str = REPLACE(@str,N'&lt;',N'<')\r\nSELECT @str = REPLACE(@str,N'&nbsp;',N' ')\r\nSELECT @str = REPLACE(@str,N'&#39;',char(39))\r\nSELECT @str = REPLACE(@str,N'&quot;',char(34))\r\nRETURN @str\r\nend\r\n";
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command2 = new SqlCommand();
                connection2.Open();
                command2.Connection = connection2;
                if (this.DropSqlVersion.SelectedValue == "2000")
                {
                    command2.CommandText = "SELECT * FROM dbo.sysobjects WHERE type='U'";
                }
                else
                {
                    command2.CommandText = "SELECT * FROM sys.objects WHERE type_desc='USER_TABLE'";
                }
                command2.CommandType = CommandType.Text;
                command2.CommandTimeout = 300;
                List<string> list = new List<string>();
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader[0].ToString();
                        if (!this.CheckCustomTable(tableName) && !CheckTable(tableName))
                        {
                            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;" + tableName + "<br/>");
                            base.Response.Flush();
                            StringBuilder sb = new StringBuilder();
                            using (SqlConnection connection3 = new SqlConnection(this.ConnectionString))
                            {
                                connection3.Open();
                                SqlCommand command3 = connection3.CreateCommand();
                                if (this.DropSqlVersion.SelectedValue == "2000")
                                {
                                    command3.CommandText = "SELECT c.name AS theColumn  FROM dbo.sysobjects t inner join dbo.syscolumns c inner join dbo.systypes ty on ty.xtype=c.xtype  on t.id=c.id where t.name= '" + tableName + "' and (ty.name='ntext' or ty.name='nvarchar' or ty.name='nchar')";
                                }
                                else
                                {
                                    command3.CommandText = "SELECT c.name AS theColumn FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id JOIN sys.types ty ON c.system_type_id = ty.system_type_id where t.name= '" + tableName + "' and (ty.name='ntext' or ty.name='nvarchar' or ty.name='nchar')";
                                }
                                using (SqlDataReader reader2 = command3.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        string columnName = reader2[0].ToString();
                                        if (CheckColumn(tableName, columnName))
                                        {
                                            list.Add(columnName);
                                            StringHelper.AppendString(sb, columnName + "=[dbo].[Decode](" + columnName + ")");
                                        }
                                    }
                                }
                                connection3.Close();
                            }
                            if (sb.Length > 0)
                            {
                                try
                                {
                                    using (SqlConnection connection4 = new SqlConnection(this.ConnectionString))
                                    {
                                        connection4.Open();
                                        SqlCommand command4 = connection4.CreateCommand();
                                        command4.CommandText = "Update " + tableName + " Set " + sb.ToString();
                                        command4.CommandTimeout = 0;
                                        command4.ExecuteNonQuery();
                                        connection4.Close();
                                    }
                                    continue;
                                }
                                catch
                                {
                                    using (SqlConnection connection5 = new SqlConnection(this.ConnectionString))
                                    {
                                        connection5.Open();
                                        SqlCommand command5 = connection5.CreateCommand();
                                        foreach (string str3 in list)
                                        {
                                            try
                                            {
                                                command5.CommandText = "Update " + tableName + " Set " + str3 + "=[dbo].[Decode](" + str3 + ")";
                                                command5.CommandTimeout = 0;
                                                command5.ExecuteNonQuery();
                                                continue;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                        }
                                        connection5.Close();
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                connection2.Close();
            }
            using (SqlConnection connection6 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command6 = connection6.CreateCommand();
                connection6.Open();
                if (this.DropSqlVersion.SelectedValue == "2000")
                {
                    command6.CommandText = "\r\nIF  EXISTS (SELECT * FROM dbo.sysobjects WHERE name='Decode' AND type='FN')\r\nDROP FUNCTION [dbo].[Decode]\r\n";
                }
                else
                {
                    command6.CommandText = "\r\nIF  EXISTS (SELECT * FROM sys.objects WHERE name='Decode' AND type_desc = 'SQL_SCALAR_FUNCTION')\r\nDROP FUNCTION [dbo].[Decode]\r\n";
                }
                command6.ExecuteNonQuery();
                connection6.Close();
            }
            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;对数据进行HtmlDecode解码完成<br/>");
            base.Response.Flush();
        }

        private bool ExecuteScripts(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
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
                    }
                }
            }
            return true;
        }

        private static string GetFunctionPattern()
        {
            return "pe:GetInfoPath|pe:ReadId3|pe:ReadExif|pe:GetUserFace|pe:ShowHeightLineText|pe:GetLinkInfoPic|pe:JSSlidePic|pe:GetSlidePic|pe:ShowVoteImage|pe:SlidePlay|pe:ViewPhoto|pe:RemoveHtml|pe:GetPhotoPathList|pe:FiltText|ProductExplain|Intro|SoftIntro|MY_product|Content|$userimg|ShowDownloadUrl";
        }

        private static string GetLabelByPath(string xmlfilepath)
        {
            if (!File.Exists(xmlfilepath))
            {
                return "";
            }
            try
            {
                FileInfo info = new FileInfo(xmlfilepath);
                using (StreamReader reader = info.OpenText())
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
            return "";
        }

        private DataTable GetLabelList()
        {
            DataTable directoryAllInfos = FileSystemObject.GetDirectoryAllInfos(base.Request.PhysicalApplicationPath + "Template", FsoMethod.Folder);
            DataTable table2 = new DataTable();
            table2.Columns.Add("LabelName");
            table2.Columns.Add("Path");
            foreach (DataRow row in directoryAllInfos.Rows)
            {
                if (row["name"].ToString() == "标签库")
                {
                    foreach (DataRow row2 in FileSystemObject.GetDirectoryAllInfos(row["path"].ToString() + "标签库", FsoMethod.File).Rows)
                    {
                        DataRow row3 = table2.NewRow();
                        row3["LabelName"] = row2["name"].ToString();
                        row3["path"] = row2["path"].ToString() + row2["name"].ToString();
                        table2.Rows.Add(row3);
                    }
                    continue;
                }
            }
            return table2;
        }

        private static string[] GetNoDecodeColumn()
        {
            return new string[] { 
                "PE_CommonProduct|ProductExplain", "PE_Trademark|TrademarkIntro", "PE_Model|Field", "PE_Producer|ProducerIntro", "PE_Nodes|Description", "PE_Nodes|Settings", "PE_Model|ChargeTips", "PE_Model|Field", "PE_Specials|Description", "PE_Advertisement|ADIntro", "PE_Author|Intro", "PE_Source|Intro", "PE_UserGroups|GroupSetting", "PE_CommentPK|Content", "PE_Votes|VoteItem", "PE_Users|UserSetting", 
                "PE_AdminProfile|PersonalSetting", "PE_ModelTemplates|Field", "PE_CollectionExclosion|ExclosionString", "PE_CollectionFieldRule|BeginCode", "PE_CollectionFieldRule|EndCode", "PE_CollectionFieldRule|PrivateFilter", "PE_CollectionFieldRule|FilterRuleID", "PE_CollectionFieldRule|SpecialSetting", "PE_CollectionFilterRule|BeginCode", "PE_CollectionFilterRule|EndCode", "PE_CollectionFilterRule|Replace", "PE_CollectionHistory|NewsUrl", "PE_CollectionItem|Url", "PE_CollectionItem|Intro", "PE_CollectionListRule|ListBeginCode", "PE_CollectionListRule|ListEndCode", 
                "PE_CollectionListRule|LinkBeginCode", "PE_CollectionListRule|LinkEndCode", "PE_CollectionListRule|RedirectUrl", "PE_CollectionPagingRule|PagingBeginCode", "PE_CollectionPagingRule|PagingEndCode", "PE_CollectionPagingRule|LinkBeginCode", "PE_CollectionPagingRule|LinkEndCode", "PE_CollectionPagingRule|DesignatedUrl", "PE_CollectionPagingRule|PagingUrlList", "PE_Present|PresentExplain", "PE_IncludeFile|Template"
             };
        }

        private static string[] GetNoDecodeTable()
        {
            return new string[] { 
                "PE_StatAddress", "PE_StatBrowser", "PE_StatColor", "PE_StatDay", "PE_StatInfoList", "PE_StatIp", "PE_StatIpInfo", "PE_StatKeyword", "PE_StatMonth", "PE_StatMozilla", "PE_StatOnline", "PE_StatRefer", "PE_StatScreen", "PE_StatSystem", "PE_StatTimezone", "PE_StatVisit", 
                "PE_StatVisitor", "PE_StatWeburl", "PE_StatWeek", "PE_StatYear", "PE_Stock", "PE_AdminProfile", "PE_Version", "PE_Question", "PE_QuestionType", "PE_QuestionType_Admin", "PE_Reply"
             };
        }

        private ArrayList GetUpgradeScripts(string databaseVersion)
        {
            ArrayList list = new ArrayList();
            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"), "*.sql");
            string str2 = @"2005\.";
            if (this.DropSqlVersion.SelectedValue == "2000")
            {
                str2 = @"2000\.";
            }
            Regex regex = new Regex(str2 + @"[0123456789]{1,}\.[0123456789]{1,}\.[0123456789]{1,}\.[0123456789]{1,}");
            foreach (string str3 in files)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(str3);
                if (regex.IsMatch(fileNameWithoutExtension))
                {
                    fileNameWithoutExtension = fileNameWithoutExtension.Remove(0, 5);
                    if ((string.Compare(fileNameWithoutExtension, databaseVersion, StringComparison.Ordinal) > 0) && (string.Compare(fileNameWithoutExtension, ProductVersion, StringComparison.Ordinal) <= 0))
                    {
                        list.Add(fileNameWithoutExtension);
                    }
                }
            }
            list.Sort(new SrciptComparer());
            return list;
        }

        private void InitDataBaseVersion()
        {
            this.dataBaseVersion = "0.0.0.0";
            try
            {
                SqlConnection connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                try
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT TOP 1 * FROM PE_Version ORDER BY VersionID DESC";
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.dataBaseVersion = reader["Major"].ToString() + "." + reader["Minor"].ToString() + "." + reader["Build"].ToString() + "." + reader["Revision"].ToString();
                        }
                    }
                }
                catch (SqlException)
                {
                    this.dataBaseVersion = "99.99.99.99";
                }
                connection.Close();
                connection.Dispose();
            }
            catch (SqlException)
            {
                this.dataBaseVersion = "9.9.9.9";
            }
            string str = WebConfigurationManager.AppSettings["Version"];
            if (str == "0.9.8.0")
            {
                this.dataBaseVersion = "0.9.8.0";
            }
        }

        private void InitProductName()
        {
            if (string.IsNullOrEmpty(this.ProductName))
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\Site.config");
                if (string.Compare(ReadSiteConfigElement(fileName, "ProductEdition"), "eShop", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.ProductName = "EasyOne SiteFactory eShop";
                }
                if (string.Compare(ReadSiteConfigElement(fileName, "ProductEdition"), "CMS", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.ProductName = "EasyOne SiteFactory CMS";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SiteCache.Remove("CK_System_DataBaseCurrentVersion");
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
            this.InitProductName();
            this.InitDataBaseVersion();
            this.CheckVersionUpgrade();
        }

        private void RCToRelease()
        {
            if (this.dataBaseVersion == "0.9.8.0")
            {
                base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;更新RC版本数据<br/>");
                base.Response.Flush();
                foreach (ModelInfo info in ModelManager.ContentModelList(ModelShowType.None))
                {
                    bool flag = false;
                    bool flag2 = false;
                    bool flag3 = false;
                    int orderId = 0;
                    string str = "";
                    string str2 = "";
                    IList<Model.CommonModel.FieldInfo> fieldListByModelId = ModelManager.GetFieldListByModelId(info.ModelId);
                    foreach (Model.CommonModel.FieldInfo info2 in fieldListByModelId)
                    {
                        if ((info2.FieldLevel == 0) && (info2.Id == "defaultpicurl"))
                        {
                            flag2 = true;
                        }
                        if (((info2.FieldType == FieldType.ContentType) && (info2.Id == "content")) && (info2.Settings[2] == "DefaultPicUrl"))
                        {
                            flag = true;
                            flag3 = true;
                            str = "DefaultPicUrl";
                            str2 = "UploadFiles";
                            orderId = info2.OrderId;
                        }
                        if ((info2.FieldType == FieldType.PictureType) && (info2.Id == "softpicurl"))
                        {
                            flag = true;
                            str = "softpicurl";
                            str2 = "";
                            orderId = info2.OrderId;
                        }
                        if ((info2.FieldType == FieldType.MultiplePhotoType) && (info2.Id == "photourl"))
                        {
                            Collection<string> settings = info2.Settings;
                            settings[0] = info2.Settings[0];
                            settings[1] = info2.Settings[1];
                            settings[2] = info2.Settings[2];
                            info2.CopyToSettings(settings);
                            orderId = info2.OrderId;
                        }
                        if (info2.FieldType == FieldType.ContentType)
                        {
                            if ((info2.Settings.Count == 6) || (info2.Settings.Count == 11))
                            {
                                Collection<string> collection2 = new Collection<string>();
                                collection2.Add(info2.Settings[0]);
                                collection2.Add(info2.Settings[1]);
                                collection2.Add("jpg|gif|jpeg|png|bmp");
                                collection2.Add("swf|fla|wmv|mp3|rm|rmvb|avi");
                                collection2.Add("txt|doc|rar|zip");
                                collection2.Add("false");
                                collection2.Add("false");
                                info2.CopyToSettings(collection2);
                            }
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                                {
                                    SqlCommand command = new SqlCommand();
                                    connection.Open();
                                    command.Connection = connection;
                                    command.CommandText = "SELECT * FROM [" + info.TableName + "]";
                                    command.CommandType = CommandType.Text;
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
                                            {
                                                connection2.Open();
                                                SqlCommand command2 = connection2.CreateCommand();
                                                command2.CommandText = "UPDATE [" + info.TableName + "] SET [" + info2.Id + "] = @Value WHERE ID=" + reader["ID"].ToString();
                                                command2.Parameters.Add(new SqlParameter("@Value", reader[info2.Id].ToString()));
                                                command2.ExecuteNonQuery();
                                                connection2.Close();
                                                continue;
                                            }
                                        }
                                    }
                                    connection.Close();
                                }
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    if (flag)
                    {
                        string str3 = "";
                        if (string.IsNullOrEmpty(str2))
                        {
                            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;&nbsp;&nbsp;正在复制" + info.TableName + "表" + str + "字段数据到PE_CommonModel表中<br/>");
                            base.Response.Flush();
                            str3 = "\r\nDECLARE @DefaultPicUrl nvarchar(255)\r\nDECLARE @GeneralID int\r\n--定义游标\r\nDECLARE cur_DefaultPicUrl CURSOR\r\nFor SELECT " + str + ",ID FROM " + info.TableName + "\r\nOpen cur_DefaultPicUrl\r\n   --移动或提取列值\r\n   Fetch  From  cur_DefaultPicUrl  into  @DefaultPicUrl,@GeneralID\r\n   --利用循环处理游标中的列值\r\n   While  @@Fetch_Status=0\r\n     Begin\r\n\t   UPDATE PE_CommonModel SET DefaultPicUrl=@DefaultPicUrl WHERE GeneralID=@GeneralID\r\n       Fetch From cur_DefaultPicUrl  into  @DefaultPicUrl,@GeneralID\r\n     End\r\n--关闭/释放游标\r\nCLOSE  cur_DefaultPicUrl\r\nDEALLOCATE cur_DefaultPicUrl\r\n";
                        }
                        else
                        {
                            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;&nbsp;&nbsp;正在复制" + info.TableName + "表" + str + "字段数据和" + str2 + "字段数据到PE_CommonModel表中<br/>");
                            base.Response.Flush();
                            str3 = "\r\nDECLARE @DefaultPicUrl nvarchar(255)\r\nDECLARE @UploadFiles nvarchar(4000)\r\nDECLARE @GeneralID int\r\n--定义游标\r\nDECLARE cur_DefaultPicUrl CURSOR\r\nFor SELECT " + str + "," + str2 + ",ID FROM " + info.TableName + "\r\nOpen cur_DefaultPicUrl\r\n   --移动或提取列值\r\n   Fetch  From  cur_DefaultPicUrl  into  @DefaultPicUrl,@UploadFiles,@GeneralID\r\n   --利用循环处理游标中的列值\r\n   While  @@Fetch_Status=0\r\n     Begin\r\n\t   UPDATE PE_CommonModel SET DefaultPicUrl=@DefaultPicUrl,UploadFiles=@UploadFiles WHERE GeneralID=@GeneralID\r\n       Fetch From cur_DefaultPicUrl  into  @DefaultPicUrl,@UploadFiles,@GeneralID\r\n     End\r\n--关闭/释放游标\r\nCLOSE  cur_DefaultPicUrl\r\nDEALLOCATE cur_DefaultPicUrl\r\n";
                        }
                        SqlConnection connection3 = new SqlConnection(this.ConnectionString);
                        SqlCommand command3 = new SqlCommand();
                        connection3.Open();
                        command3.Connection = connection3;
                        command3.CommandText = str3;
                        command3.ExecuteNonQuery();
                        connection3.Close();
                        connection3.Dispose();
                        try
                        {
                            if ((str == "DefaultPicUrl") && (str2 == "UploadFiles"))
                            {
                                string str4 = "/*\r\n此脚本由 Visual Studio 在 2008/2/27 上的 11:13 处创建。\r\n请在 SiteFactory RC版 上运行此脚本，使其与 SiteFactory正式版 相同。\r\n请在运行此脚本之前备份目标数据库。\r\n*/\r\nSET NUMERIC_ROUNDABORT OFF\r\nSET ANSI_PADDING ON\r\nSET ANSI_WARNINGS ON\r\nSET CONCAT_NULL_YIELDS_NULL ON\r\nSET ARITHABORT ON\r\nSET QUOTED_IDENTIFIER ON\r\nSET ANSI_NULLS ON\r\nIF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors\r\nCREATE TABLE #tmpErrors (Error int)\r\nSET XACT_ABORT ON\r\nSET TRANSACTION ISOLATION LEVEL READ COMMITTED\r\nBEGIN TRANSACTION\r\nPRINT N'正在改变 [dbo].[" + info.TableName + "]'\r\nALTER TABLE [dbo].[" + info.TableName + "] DROP\r\nCOLUMN [DefaultPicUrl],\r\nCOLUMN [UploadFiles]\r\nIF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION\r\nIF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END\r\nIF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION\r\nIF @@TRANCOUNT>0 BEGIN\r\nPRINT '数据库更新成功。'\r\nCOMMIT TRANSACTION\r\nEND\r\nELSE PRINT '数据库更新失败。'\r\nDROP TABLE #tmpErrors\r\n";
                                SqlConnection connection4 = new SqlConnection(this.ConnectionString);
                                SqlCommand command4 = new SqlCommand();
                                connection4.Open();
                                command4.Connection = connection4;
                                command4.CommandText = str4;
                                command4.ExecuteNonQuery();
                                connection4.Close();
                                connection4.Dispose();
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (!flag2)
                    {
                        Model.CommonModel.FieldInfo item = new Model.CommonModel.FieldInfo();
                        item.Id = "DefaultPicUrl";
                        item.FieldName = "DefaultPicUrl";
                        item.FieldLevel = 0;
                        item.FieldType = FieldType.PictureType;
                        item.FieldAlias = "首页图片";
                        item.OrderId = orderId;
                        Collection<string> collection3 = new Collection<string>();
                        collection3.Add("30");
                        collection3.Add("1024");
                        collection3.Add("jpg|gif|bmp");
                        collection3.Add("0");
                        collection3.Add("0");
                        collection3.Add("0");
                        if (flag3)
                        {
                            collection3.Add("False");
                            collection3.Add("True");
                        }
                        else
                        {
                            collection3.Add("True");
                            collection3.Add("False");
                        }
                        item.CopyToSettings(collection3);
                        fieldListByModelId.Add(item);
                    }
                    string fieldList = new Serialize<Model.CommonModel.FieldInfo>().SerializeFieldList(fieldListByModelId);
                    ModelManager.UpdateField(info.ModelId, fieldList);
                    if (str == "photothumb")
                    {
                        Field.Delete("photothumb", info.ModelId);
                    }
                }
                using (SqlConnection connection5 = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand command5 = new SqlCommand();
                    connection5.Open();
                    command5.Connection = connection5;
                    command5.CommandText = "SELECT * FROM PE_CollectionFieldRules WHERE FieldType='ContentType'";
                    command5.CommandType = CommandType.Text;
                    using (SqlDataReader reader2 = command5.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            string str6 = reader2["FieldName"].ToString();
                            if (str6.IndexOf("$$$", StringComparison.Ordinal) > 0)
                            {
                                str6 = str6.Substring(0, str6.IndexOf("$$$", StringComparison.Ordinal));
                                using (SqlConnection connection6 = new SqlConnection(this.ConnectionString))
                                {
                                    connection6.Open();
                                    SqlCommand command6 = connection6.CreateCommand();
                                    command6.CommandType = CommandType.Text;
                                    command6.CommandText = "UPDATE PE_CollectionFieldRules SET FieldName='" + str6 + "' WHERE FieldRuleID=" + reader2["FieldRuleID"].ToString();
                                    command6.ExecuteNonQuery();
                                    connection6.Close();
                                    continue;
                                }
                            }
                        }
                    }
                    connection5.Close();
                }
                using (SqlConnection connection7 = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand command7 = new SqlCommand();
                    connection7.Open();
                    command7.Connection = connection7;
                    command7.CommandText = "SELECT * FROM PE_CommonProduct";
                    command7.CommandType = CommandType.Text;
                    using (SqlDataReader reader3 = command7.ExecuteReader())
                    {
                        while (reader3.Read())
                        {
                            using (SqlConnection connection8 = new SqlConnection(this.ConnectionString))
                            {
                                connection8.Open();
                                SqlCommand command8 = connection8.CreateCommand();
                                command8.CommandText = "UPDATE [PE_CommonProduct] SET [ProductExplain] = @Value WHERE ProductID=" + reader3["ProductID"].ToString();
                                command8.Parameters.Add(new SqlParameter("@Value", reader3["ProductExplain"].ToString()));
                                command8.ExecuteNonQuery();
                                connection8.Close();
                                continue;
                            }
                        }
                    }
                    connection7.Close();
                }
                SiteConfigInfo info4 = SiteConfig.ConfigInfo();
                info4.SiteInfo.EshopWebPartSetting = "";
                info4.UserConfig.UserGetPasswordType = 1;
                info4.UserConfig.MoneyExchangePointByMoney = 1.0;
                info4.UserConfig.MoneyExchangeValidDayByMoney = 1.0;
                info4.UserConfig.UserExpExchangePointByExp = 1.0;
                info4.UserConfig.UserExpExchangeValidDayByExp = 1.0;
                info4.UserConfig.MoneyExchangePointByPoint = 1.0;
                info4.UserConfig.MoneyExchangeValidDayByValidDay = 1.0;
                info4.UserConfig.UserExpExchangePointByPoint = 1.0;
                info4.UserConfig.UserExpExchangeValidDayByValidDay = 1.0;
                info4.ShopConfig.EnablePartPay = true;
                info4.ShopConfig.OrderProductNumber = 0;
                NameValueCollection values = new NameValueCollection();
                values.Add("DynamicPageDefault", "/其他模板/默认动态页模板.html");
                values.Add("ShowAuthorList", "/其他模板/作者列表页模板.html");
                values.Add("ShowAuthor", "/其他模板/显示作者详细信息页模板.html");
                values.Add("ShowCopyFromList", "/其他模板/来源列表页模板.html");
                values.Add("ShowCopyFrom", "/其他模板/显示来源详细信息页模板.html");
                values.Add("GuestWrite", "/其他模板/签写留言页模板.html");
                values.Add("ShowDownload", "/用户中心模板/默认会员中心通用模板.html");
                values.Add("ConfirmRemittance", "/用户中心模板/默认会员中心通用模板.html");
                values.Add("NavContent", "/用户中心模板/默认会员中心信息管理通用模板.html");
                values.Add("AnonymousContent", "/用户中心模板/匿名投稿页模板.html");
                values.Add("AnonymousContent2", "/用户中心模板/匿名投稿页模板.html");
                values.Add("ShowProducer", "/其他模板/显示厂商详细信息页模板.html");
                values.Add("ShowProducerList", "/其他模板/显示厂商列表页模板.html");
                values.Add("ShowTrademark", "/其他模板/显示品牌详细信息页模板.html");
                values.Add("ShowTrademarkList", "/其他模板/显示品牌列表页模板.html");
                foreach (string str7 in values.AllKeys)
                {
                    FrontTemplate template = new FrontTemplate();
                    template.Key = str7;
                    template.Value = values[str7];
                    info4.FrontTemplateList.Add(template);
                }
                SiteConfig config = new SiteConfig();
                try
                {
                    config.Update(info4);
                }
                catch (FileNotFoundException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (ConfigurationErrorsException)
                {
                }
                try
                {
                    System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                    AppSettingsSection section = (AppSettingsSection) configuration.GetSection("appSettings");
                    section.Settings.Add("EasyOne:DefaultUploadSuffix", ".jpg.gif.jpeg.png.bmp.swf.fla.rm.rmvb.mp3.mpeg.avi.mpeg2.wmv.midi");
                    configuration.Save();
                }
                catch (FileNotFoundException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (ConfigurationErrorsException)
                {
                }
            }
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

        private void ResponseEnd()
        {
            base.Response.Write("<br><h2>升级完成</h2><br><br><h2><a href='../Default.aspx'>返回网站首页</a></h2><br><br><div class=\"copy\">" + ProductCopyright + "</div></body></html>");
            base.Response.End();
        }

        private void ResponseHeader()
        {
            string s = "<html>\r\n<head>\r\n  <title>动易软件产品升级程序</title>\r\n  <link rel=\"stylesheet\" type=\"text/css\" href=\"images/upgrade.css\">\r\n</head>\r\n<body>\r\n    <div class=\"top\">\r\n        <form id=\"search\" action=\"http://help.EasyOne.net/search.asp\" method=\"post\">\r\n            <dl>\r\n                <dt class=\"linking\"><a href=\"http://www.EasyOne.net/\" target=\"_blank\" title=\"访问动易官方网站\">\r\n                    EasyOne.net</a> | <a href=\"http://EasyOne.net/soft/\" target=\"_blank\" title=\"免费下载动易系列软件产品\">\r\n                        免费下载</a> | <a href=\"http://EasyOne.net/User/\" target=\"_blank\" title=\"动易官方网站客户自助服务\">\r\n                            客户自助服务</a> | <a href=\"http://bbs.EasyOne.net/\" target=\"_blank\" title=\"今天您上动易论坛了吗？\">\r\n                                动易论坛</a></dt>\r\n                <dt class=\"search\"><span style=\"width: 320px; height: 22px; height: 26px; padding: 6px 0 0 0;\r\n                    padding: 2px 0 0 0; _padding: 4px 0 0 0; overflow: hidden; float: right;\">\r\n                    <span style=\"float: right; padding: 0px 0 0 10px; padding: 2px 0 0 10px;\">\r\n                        <input id=\"Submit\" style=\"border: 0px; width: 47px; height: 20px;\" type=\"image\" src=\"Images/upgrade_search_but.gif\"\r\n                            name=\"Submit\" />\r\n                    </span>\r\n                    <span>\r\n                        <input name=\"Keyword\" id=\"Keyword\" onclick=\"value=''\"  style=\"background:#ebf7ff\" onmouseover=\"this.style.backgroundColor='#ffffff'\"\r\n                            onmouseout=\"this.style.backgroundColor='#ebf7ff'\" value=\"动易全站搜索\" size=\"35\" />\r\n                        <input name=\"ModuleName\" type=\"hidden\" id=\"ModuleName\" value=\"Article\" />\r\n                        <input id=\"Field\" type=\"hidden\" value=\"Title\" name=\"Field\" />\r\n                    </span>\r\n                </span></dt>\r\n            </dl>\r\n        </form>\r\n    </div>\r\n  <br />\r\n<!-- tags excluded on purpose so that installation feedback messages are displayed\r\n</body>\r\n</html>\r\n-->";
            base.Response.Clear();
            base.Response.Write(s);
            base.Response.Flush();
            base.Response.Write("<h1>正在升级" + this.ProductName + "</h1>");
            base.Response.Write("<h2>当前程序集版本：" + ProductVersion + "</h2>");
            base.Response.Write("<h2>当前数据库版本：" + this.dataBaseVersion + "</h2><br>");
            base.Response.Write("<h2>升级状态报告</h2>");
            base.Response.Flush();
        }

        private void SetConfig(string timeout)
        {
            this.UpdateValue(base.Request.PhysicalApplicationPath, @"\web.config", "/configuration/system.web/httpRuntime", "executionTimeout", "120", "executionTimeout", timeout);
        }

        public static bool Update(string content, string xmlfilepath)
        {
            try
            {
                File.WriteAllText(xmlfilepath, content, Encoding.UTF8);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        private void UpdateAdvertiesementDir()
        {
            string str = "DECLARE @ImgUrl nvarchar(255)\r\n                              DECLARE @AdId INT\r\n                              DECLARE @AdDir nvarchar(255)\r\n                              SET @AdDir='" + SiteConfig.SiteOption.AdvertisementDir + "'\r\n                              DECLARE cur_Ad Cursor\r\n                              FOR SELECT ADId,ImgUrl FROM PE_Advertisement\r\n                              OPEN cur_Ad\r\n                              FETCH NEXT FROM cur_Ad INTO @AdId,@ImgUrl\r\n                              WHILE ( @@Fetch_Status=0)   \r\n                              BEGIN\r\n                                    IF LEFT(@ImgUrl,LEN(@AdDir))=@AdDir\r\n                                    BEGIN\r\n                                        SET @ImgUrl=SUBSTRING(@ImgUrl,LEN(@AdDir)+1,LEN(@ImgUrl)-(LEN(@AdDir)+1))\r\n                                        UPDATE PE_Advertisement SET ImgUrl=@ImgUrl WHERE AdId=@AdId\r\n                                    END\r\n                                    FETCH NEXT FROM cur_Ad INTO @AdId,@ImgUrl\r\n                              END\r\n                              CLOSE cur_Ad\r\n                              Deallocate cur_Ad";
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        private void UpdateCompanyPicColumn()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();
                command.Connection = connection;
                if (this.DropSqlVersion.SelectedValue == "2000")
                {
                    command.CommandText = "\r\nIF  EXISTS (SELECT  *  FROM dbo.sysobjects t inner join dbo.syscolumns c inner join dbo.systypes ty on ty.xtype=c.xtype  on t.id=c.id where t.name= 'PE_Company' and c.name='CompamyPic')\r\nBegin\r\nexec sp_rename 'PE_Company.CompamyPic', 'CompanyPic', 'COLUMN'\r\nEnd";
                }
                else
                {
                    command.CommandText = "\r\nIF  EXISTS (SELECT * FROM sys.tables t JOIN sys.columns c ON t.object_id = c.object_id JOIN sys.types ty ON c.system_type_id = ty.system_type_id where t.name='PE_Company' and c.name='CompamyPic')\r\nBegin\r\nexec sp_rename 'PE_Company.CompamyPic', 'CompanyPic', 'COLUMN'\r\nEnd";
                }
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command2 = new SqlCommand();
                connection2.Open();
                command2.Connection = connection2;
                command2.CommandText = "\r\nALTER PROCEDURE [dbo].[PR_Crm_Company_Add] \t\r\n\t(\t\r\n\t@CompanyID Int, \r\n\t@ClientID Int, \r\n\t@CompanyName NVarChar(200), \r\n\t@Country NVarChar(50), \r\n\t@Province NVarChar(30), \r\n\t@City NVarChar(30), \r\n\t@Address NVarChar(255), \r\n\t@ZipCode NVarChar(50), \r\n\t@Phone NVarChar(50), \r\n\t@Fax NVarChar(30), \r\n\t@Homepage NVarChar(255), \r\n\t@BankOfDeposit NVarChar(50), \r\n\t@BankAccount NVarChar(50), \r\n\t@TaxNum NVarChar(50), \r\n\t@StatusInField Int, \r\n\t@CompanySize Int, \r\n\t@BusinessScope NVarChar(200), \r\n\t@AnnualSales NVarChar(50), \r\n\t@ManagementForms Int, \r\n\t@RegisteredCapital NVarChar(50), \r\n\t@CompanyIntro ntext, \r\n\t@CompanyPic NVarChar(255)\r\n\t\r\n\t)\t\r\nAS\r\n\tINSERT INTO [PE_Company]\r\n\t      (\t[CompanyID], [ClientID], [CompanyName], [Country], [Province], [City], [Address], [ZipCode], [Phone], [Fax], [Homepage], [BankOfDeposit], [BankAccount], [TaxNum], [StatusInField], [CompanySize], [BusinessScope], [AnnualSales], [ManagementForms], [RegisteredCapital], [CompanyIntro], [CompanyPic] )\r\n\tVALUES (\t@CompanyID, @ClientID, @CompanyName, @Country, @Province, @City, @Address, @ZipCode, @Phone, @Fax, @Homepage, @BankOfDeposit, @BankAccount, @TaxNum, @StatusInField, @CompanySize, @BusinessScope, @AnnualSales, @ManagementForms, @RegisteredCapital, @CompanyIntro, @CompanyPic )\r\n";
                command2.CommandType = CommandType.Text;
                command2.ExecuteNonQuery();
                connection2.Close();
            }
            using (SqlConnection connection3 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command3 = new SqlCommand();
                connection3.Open();
                command3.Connection = connection3;
                command3.CommandText = "\r\nALTER PROCEDURE [dbo].[PR_Crm_Company_Update]\r\n\t(\t\r\n\t@CompanyID Int, \r\n\t@ClientID Int, \r\n\t@CompanyName NVarChar(200), \r\n\t@Country NVarChar(50), \r\n\t@Province NVarChar(30), \r\n\t@City NVarChar(30), \r\n\t@Address NVarChar(255), \r\n\t@ZipCode NVarChar(50), \r\n\t@Phone NVarChar(50), \r\n\t@Fax NVarChar(30), \r\n\t@Homepage NVarChar(255), \r\n\t@BankOfDeposit NVarChar(50), \r\n\t@BankAccount NVarChar(50), \r\n\t@TaxNum NVarChar(50), \r\n\t@StatusInField Int, \r\n\t@CompanySize Int, \r\n\t@BusinessScope NVarChar(200), \r\n\t@AnnualSales NVarChar(50), \r\n\t@ManagementForms Int, \r\n\t@RegisteredCapital NVarChar(50), \r\n\t@CompanyIntro ntext, \r\n\t@CompanyPic NVarChar(255)\r\n\t\r\n\t)\t\r\nAS\r\n\tUpdate PE_Company\r\n\tSet\r\n\t\tClientID=@ClientID, \r\n\t\tCompanyName=@CompanyName, \r\n\t\tCountry=@Country, \r\n\t\tProvince=@Province, \r\n\t\tCity=@City, \r\n\t\tAddress=@Address, \r\n\t\tZipCode=@ZipCode, \r\n\t\tPhone=@Phone, \r\n\t\tFax=@Fax, \r\n\t\tHomepage=@Homepage, \r\n\t\tBankOfDeposit=@BankOfDeposit, \r\n\t\tBankAccount=@BankAccount, \r\n\t\tTaxNum=@TaxNum, \r\n\t\tStatusInField=@StatusInField, \r\n\t\tCompanySize=@CompanySize, \r\n\t\tBusinessScope=@BusinessScope, \r\n\t\tAnnualSales=@AnnualSales, \r\n\t\tManagementForms=@ManagementForms, \r\n\t\tRegisteredCapital=@RegisteredCapital, \r\n\t\tCompanyIntro=@CompanyIntro, \r\n\t\tCompanyPic=@CompanyPic\r\n    Where CompanyID = @CompanyID";
                command3.CommandType = CommandType.Text;
                command3.ExecuteNonQuery();
                connection3.Close();
            }
        }

        private void UpdateContentStatus()
        {
            string str = "INSERT [dbo].[PE_Status] ([StatusID], [StatusCode], [StatusName], [StatusType]) VALUES (120, 100, N'归档的稿件', 0)";
            try
            {
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        private void UpdateCourierIdColumn()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();
                command.Connection = connection;
                command.CommandText = "ALTER TABLE PE_DeliverItem Add CourierId INT NULL";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command2 = new SqlCommand();
                connection2.Open();
                command2.Connection = connection2;
                command2.CommandText = "INSERT INTO PE_Courier(ShortName) SELECT DISTINCT(ExpressCompany) FROM PE_DeliverItem";
                command2.CommandType = CommandType.Text;
                command2.ExecuteNonQuery();
                connection2.Close();
            }
            using (SqlConnection connection3 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command3 = new SqlCommand();
                connection3.Open();
                command3.Connection = connection3;
                command3.CommandText = "\r\n                   DECLARE @ExpressCompany nvarchar(255)\r\n                    --定义游标\r\n                    DECLARE cur_CourierId CURSOR\r\n                    FOR SELECT DISTINCT(ExpressCompany) FROM PE_DeliverItem\r\n                    Open cur_CourierId\r\n                       --移动或提取列值\r\n                       FETCH From cur_CourierId INTO @ExpressCompany\r\n                       --利用循环处理游标中的列值\r\n                       WHILE  @@Fetch_Status=0\r\n                         Begin\r\n\t                       UPDATE PE_DeliverItem SET CourierId=(SELECT TOP 1 CourierId FROM PE_Courier WHERE ShortName=@ExpressCompany) WHERE ExpressCompany=@ExpressCompany\r\n                           FETCH From cur_CourierId INTO @ExpressCompany\r\n                         End\r\n                    --关闭/释放游标\r\n                    CLOSE  cur_CourierId\r\n                    DEALLOCATE cur_CourierId\r\n                    ";
                command3.CommandType = CommandType.Text;
                command3.ExecuteNonQuery();
                connection3.Close();
            }
            using (SqlConnection connection4 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command4 = new SqlCommand();
                connection4.Open();
                command4.Connection = connection4;
                command4.CommandText = "ALTER TABLE PE_DeliverItem DROP COLUMN ExpressCompany";
                command4.CommandType = CommandType.Text;
                command4.ExecuteNonQuery();
                connection4.Close();
            }
            using (SqlConnection connection5 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command5 = new SqlCommand();
                connection5.Open();
                command5.Connection = connection5;
                command5.CommandText = "DROP PROC PR_Shop_DeliverItem_Add";
                command5.CommandType = CommandType.Text;
                command5.ExecuteNonQuery();
                connection5.Close();
            }
            using (SqlConnection connection6 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command6 = new SqlCommand();
                connection6.Open();
                command6.Connection = connection6;
                command6.CommandText = "           \r\n                    CREATE PROCEDURE [dbo].[PR_Shop_DeliverItem_Add] \t\r\n\t                    (\r\n\t                    @OrderID INT,\r\n\t                    @DeliverDate DateTime,\r\n\t                    @DeliverDirection INT,\r\n\t                    @HandlerName NVarChar(20),\r\n\t                    @CourierId INT,\r\n\t                    @ExpressNumber NVarChar(50),\r\n\t                    @Inputer NVarChar(20),\r\n\t                    @Remark NVarChar(255),\r\n                        @Memo NTEXT\r\n\t                    )\t\r\n                    AS\r\n                       \r\n                       INSERT INTO PE_DeliverItem\r\n\t                          (OrderID,DeliverDate, DeliverDirection, HandlerName, CourierId, ExpressNumber, Inputer, Remark,Memo)\r\n\t                    VALUES (@OrderID,@DeliverDate,@DeliverDirection,@HandlerName,@CourierId,@ExpressNumber,@Inputer,@Remark,@Memo)\r\n                            ";
                command6.CommandType = CommandType.Text;
                command6.ExecuteNonQuery();
                connection6.Close();
            }
        }

        private void UpdateLabel()
        {
            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;对标签进行处理，在此过程中请不要关闭浏览器<br/>");
            base.Response.Flush();
            DataTable labelList = this.GetLabelList();
            string functionPattern = GetFunctionPattern();
            foreach (DataRow row in labelList.Rows)
            {
                string labelByPath = GetLabelByPath(row["path"].ToString());
                if (!string.IsNullOrEmpty(labelByPath))
                {
                    try
                    {
                        foreach (Match match in Regex.Matches(labelByPath, @"<xsl:value-of[\s\S]*?/>", RegexOptions.IgnoreCase))
                        {
                            string input = match.Value;
                            if (!Regex.Match(input, functionPattern, RegexOptions.IgnoreCase).Success)
                            {
                                string newValue = match.Value.Replace(" disable-output-escaping=\"yes\"", "");
                                labelByPath = labelByPath.Replace(input, newValue);
                            }
                            else if (!Regex.Match(input, "\\s*disable-output-escaping=\"yes\"\\s*", RegexOptions.IgnoreCase).Success)
                            {
                                string str5 = match.Value.Replace("value-of", "value-of disable-output-escaping=\"yes\"");
                                labelByPath = labelByPath.Replace(input, str5);
                            }
                        }
                        foreach (Match match4 in Regex.Matches(labelByPath, @"pe:EncodeText\([\s\S]*?,'htmldecode'\)", RegexOptions.IgnoreCase))
                        {
                            string str6 = Regex.Replace(Regex.Replace(match4.Value, @"pe:EncodeText\(", "", RegexOptions.IgnoreCase), @",'htmldecode'\)", "", RegexOptions.IgnoreCase);
                            labelByPath = labelByPath.Replace(match4.Value, str6);
                        }
                        Update(labelByPath, row["path"].ToString());
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;对标签进行处理完成<br/>");
            base.Response.Flush();
        }

        private void UpdateProcDeliverItemAdd()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DROP PROC PR_Shop_DeliverItem_Add";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command2 = new SqlCommand();
                connection2.Open();
                command2.Connection = connection2;
                command2.CommandText = "           \r\n                    CREATE PROCEDURE [dbo].[PR_Shop_DeliverItem_Add] \t\r\n\t                    (\r\n\t                    @OrderID INT,\r\n\t                    @DeliverDate DateTime,\r\n\t                    @DeliverDirection INT,\r\n\t                    @HandlerName NVarChar(20),\r\n\t                    @CourierId INT,\r\n\t                    @ExpressNumber NVarChar(50),\r\n\t                    @Inputer NVarChar(20),\r\n\t                    @Remark NVarChar(255),\r\n                        @Memo NTEXT\r\n\t                    )\t\r\n                    AS\r\n                       \r\n                       INSERT INTO PE_DeliverItem\r\n\t                          (OrderID,DeliverDate, DeliverDirection, HandlerName, CourierId, ExpressNumber, Inputer, Remark,Memo)\r\n\t                    VALUES (@OrderID,@DeliverDate,@DeliverDirection,@HandlerName,@CourierId,@ExpressNumber,@Inputer,@Remark,@Memo)\r\n                            ";
                command2.CommandType = CommandType.Text;
                command2.ExecuteNonQuery();
                connection2.Close();
            }
        }

        private void UpdateValue(string strDir, string fileName, string nKey, string conditionAttributes, string conditionValue, string nAttributes, string nValue)
        {
            try
            {
                FileInfo info = new FileInfo(strDir + fileName);
                if (info.Exists)
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(info.FullName);
                    foreach (XmlNode node in document.SelectNodes(nKey))
                    {
                        if (string.IsNullOrEmpty(conditionAttributes))
                        {
                            node.InnerXml = nValue;
                            document.Save(info.FullName);
                        }
                        else
                        {
                            node.Attributes[nAttributes].Value = nValue;
                            document.Save(info.FullName);
                            return;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void UpgradeAppSettings()
        {
            try
            {
                System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection section = (AppSettingsSection) configuration.GetSection("appSettings");
                section.Settings["Version"].Value = ProductVersion;
                configuration.Save();
            }
            catch (FileNotFoundException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (ConfigurationErrorsException)
            {
            }
        }

        private void UpgradeScripts()
        {
            ArrayList upgradeScripts = this.GetUpgradeScripts(this.dataBaseVersion);
            base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;正在升级到版本：" + ProductVersion + "<br/>");
            if (upgradeScripts.Count == 0)
            {
                this.AddVersion(ProductVersion);
            }
            else
            {
                string str = "2005.";
                if (this.DropSqlVersion.SelectedValue == "2000")
                {
                    str = "2000.";
                }
                foreach (object obj2 in upgradeScripts)
                {
                    base.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " -&nbsp;&nbsp;&nbsp;正在执行数据库脚本：" + obj2.ToString() + ".sql&nbsp;");
                    base.Response.Flush();
                    string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\" + str + obj2.ToString() + ".sql");
                    if (this.ExecuteScripts(fileName))
                    {
                        base.Response.Write(" <font color='green'>执行成功</font><br>");
                    }
                    else
                    {
                        base.Response.Write(" <font color='red'>执行失败</font><br>");
                    }
                    this.AddVersion(obj2.ToString());
                    base.Response.Flush();
                }
                if (string.Compare(upgradeScripts[upgradeScripts.Count - 1].ToString(), ProductVersion, StringComparison.Ordinal) != 0)
                {
                    this.AddVersion(ProductVersion);
                }
            }
        }

        private void UpgradeTablesToDbo()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    string str = "SELECT 'ALTER SCHEMA dbo TRANSFER ' + SCHEMA_NAME(schema_id) + '.' + [name] AS SQLText FROM sys.tables WHERE schema_id != SCHEMA_ID('dbo')";
                    if (this.DropSqlVersion.SelectedValue == "2000")
                    {
                        str = "SELECT 'sp_changeobjectowner ''[' + table_schema + '].[' + table_name + ']'', ''dbo''' AS SQLText from information_schema.tables where Table_schema !='dbo'";
                    }
                    command.CommandType = CommandType.Text;
                    command.CommandText = str;
                    command.CommandTimeout = 300;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string str2 = (string) reader["SQLText"];
                            using (SqlConnection connection2 = new SqlConnection(this.ConnectionString))
                            {
                                using (SqlCommand command2 = new SqlCommand())
                                {
                                    connection2.Open();
                                    command2.Connection = connection2;
                                    command2.CommandType = CommandType.Text;
                                    command2.CommandText = str2;
                                    command2.CommandTimeout = 300;
                                    command2.ExecuteNonQuery();
                                }
                                continue;
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
        }

        private void UpgradeTo1100()
        {
            if (((this.dataBaseVersion == "0.9.8.0") || (this.dataBaseVersion == "1.0.0.0")) || ((this.dataBaseVersion == "1.0.0.1") || (this.dataBaseVersion == "1.0.0.2")))
            {
                this.AddDictionary();
                this.UpdateCourierIdColumn();
                this.UpdateCompanyPicColumn();
                this.DecodeDataBase();
                this.UpdateLabel();
                this.UpdateAdvertiesementDir();
                this.UpdateContentStatus();
                this.SetConfig("120");
            }
        }

        private void UpgradeTo1101()
        {
            if (this.dataBaseVersion == "1.1.0.0")
            {
                this.UpdateProcDeliverItemAdd();
            }
        }
    }
}

