namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Xml;

    public partial class ShowDownloadUrl : DynamicPage
    {
        private int dayHits;
        protected string downloadurl;
        private int hits;
        private int monthHits;
        private int weekHits;

        private void DownloadSoft(string newurl)
        {
            try
            {
                string str3;
                string str = Path.GetExtension(newurl).Replace(".", "");
                string attributeValue = "";
                bool flag = false;
                XmlDocument document = new XmlDocument();
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    str3 = current.Server.MapPath("~/Common/ShowDownlondUrl.xml");
                }
                else
                {
                    str3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/ShowDownlondUrl.xml");
                }
                try
                {
                    document.Load(str3);
                }
                catch (XmlException exception)
                {
                    DynamicPage.WriteErrMsg("ShowDownlondUrl.xml配置文件不符合XML规范，具体错误信息：" + exception.Message);
                }
                XmlNode node = document.SelectSingleNode("Players");
                if (node == null)
                {
                    DynamicPage.WriteErrMsg("ShowDownlondUrl.xml配置文件不存在Players根元素");
                }
                if (node.HasChildNodes)
                {
                    foreach (XmlNode node2 in node)
                    {
                        if (flag)
                        {
                            break;
                        }
                        if (node2 != null)
                        {
                            attributeValue = this.GetAttributeValue(node2, "Extension");
                            if (!string.IsNullOrEmpty(attributeValue))
                            {
                                if (attributeValue.IndexOf(',') > 0)
                                {
                                    foreach (string str4 in attributeValue.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        if (str == str4)
                                        {
                                            this.LitPlayer.Text = node2.InnerText.Replace("{$downloadurl}", this.downloadurl);
                                            flag = true;
                                            break;
                                        }
                                    }
                                    continue;
                                }
                                if (str == attributeValue)
                                {
                                    this.LitPlayer.Text = node2.InnerText.Replace("{$downloadurl}", this.downloadurl);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(this.LitPlayer.Text))
                {
                    base.Response.Redirect(this.downloadurl);
                }
            }
            catch
            {
                throw new CustomException("下载地址文件格式不正确！");
            }
        }

        private string GetAttributeValue(XmlNode xmlNode, string attributeName)
        {
            string str = "";
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[attributeName];
                if (attribute != null)
                {
                    str = attribute.Value;
                }
            }
            return str;
        }

        private int GetDays(DayOfWeek dy)
        {
            switch (dy)
            {
                case DayOfWeek.Sunday:
                    return 0;

                case DayOfWeek.Monday:
                    return 1;

                case DayOfWeek.Tuesday:
                    return 2;

                case DayOfWeek.Wednesday:
                    return 3;

                case DayOfWeek.Thursday:
                    return 4;

                case DayOfWeek.Friday:
                    return 5;

                case DayOfWeek.Saturday:
                    return 6;
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int urlid = BasePage.RequestInt32("urlid");
                int id = BasePage.RequestInt32("id", 0);
                int serverid = BasePage.RequestInt32("serverid", 0);
                if (id <= 0)
                {
                    DynamicPage.WriteErrMsg("错误的下载参数！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
                }
                ChargeManage manage = new ChargeManage();
                if (manage.CheckPermission())
                {
                    manage.ExecuteContentCharge();
                }
                if (!string.IsNullOrEmpty(manage.ErrMsg))
                {
                    DynamicPage.WriteErrMsg(manage.ErrMsg, SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
                }
                string newurl = this.ParseDownLoadPath(urlid, id, serverid);
                this.UpdateHits(id);
                this.DownloadSoft(newurl);
            }
        }

        private string ParseDownLoadPath(int urlid, int id, int serverid)
        {
            DataTable contentDataById = ContentManage.GetContentDataById(id);
            if (contentDataById.Rows.Count <= 0)
            {
                DynamicPage.WriteErrMsg("此资源不存在！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
            string str = "";
            DownServerInfo downServerById = DownServer.GetDownServerById(serverid);
            string weburl = "";
            if (contentDataById.Rows.Count > 0)
            {
                str = contentDataById.Rows[0]["DownloadUrl"].ToString();
                int num = 0;
                foreach (string str3 in str.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] strArray2 = str3.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (num == urlid)
                    {
                        weburl = strArray2[1];
                    }
                    num++;
                }
            }
            if (downServerById.IsNull)
            {
                this.downloadurl = Utility.ConvertAbsolutePath(SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.UploadDir + "/", weburl, true);
                return weburl;
            }
            string str4 = downServerById.ServerUrl.ToString();
            if (!str4.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                str4 = "http://" + str4;
            }
            if (!str4.EndsWith("/", StringComparison.Ordinal))
            {
                str4 = str4 + "/";
            }
            if (weburl.StartsWith("/", StringComparison.Ordinal) || weburl.Contains("://"))
            {
                this.downloadurl = weburl;
                return weburl;
            }
            this.downloadurl = str4 + weburl;
            return weburl;
        }

        private void UpdateHits(int id)
        {
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(id);
            if (!commonModelInfoById.IsNull)
            {
                DateTime time;
                if (!commonModelInfoById.LastHitTime.HasValue)
                {
                    time = DateTime.Now;
                }
                else
                {
                    time = commonModelInfoById.LastHitTime.Value;
                }
                this.hits = commonModelInfoById.Hits + 1;
                this.dayHits = commonModelInfoById.DayHits;
                this.weekHits = commonModelInfoById.WeekHits;
                this.monthHits = commonModelInfoById.MonthHits;
                DateTime now = DateTime.Now;
                if (string.Compare(time.ToShortDateString(), DateTime.Now.ToShortDateString(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.dayHits++;
                }
                else
                {
                    this.dayHits = 1;
                }
                DateTime time3 = now.AddDays((double) -this.GetDays(now.DayOfWeek));
                DateTime time4 = time3.AddDays(7.0);
                if ((DateTime.Compare(time, time3) >= 0) && (DateTime.Compare(time, time4) <= 0))
                {
                    this.weekHits++;
                }
                else
                {
                    this.weekHits = 1;
                }
                if ((string.Compare(time.Year.ToString(), now.Year.ToString(), StringComparison.Ordinal) == 0) && (string.Compare(time.Month.ToString(), now.Month.ToString(), StringComparison.Ordinal) == 0))
                {
                    this.monthHits++;
                }
                else
                {
                    this.monthHits = 1;
                }
                time = DateTime.Now;
                ContentManage.UpdateHits(id, this.hits, this.dayHits, this.weekHits, this.monthHits, time);
            }
        }
    }
}

