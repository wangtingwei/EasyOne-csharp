namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CompareFilesOnline : AdminPage
    {
        private const string CannotReadServerFiles = "<li><span style='Color:#f00'>获取官方数据失败，可能是您的服务器不支持 XMLHTTP 组件或者是通过代理服务器访问网络。</span></li>";
        private const string FileExt = "asax|ascs|ashx|asmx|asp|aspx|axd|cdx|cer|idc|licx|rem|resources|resx|soap|stm|vsdisco|webinfo";
        private bool m_ItemCssClass;
        private int m_LocalNotExsis;
        private int m_OnlyDateDiffNum;
        private int m_ServerNotExsis;
        private int m_SizeAndDataDiffNum;
        private int m_SizeDiffNum;
        private const string ServerNoResponse = "由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。";

        protected void BtnCompareTogether_Click(object sender, EventArgs e)
        {
            this.notes.Visible = false;
            this.PnlCompareFiles.Visible = true;
            this.CompareFile(this.IsShowAll(), this.ReadLocalFileInfo().Select());
            this.LblSame.Text = this.m_SizeAndDataDiffNum.ToString();
            this.LblSizeDiff.Text = this.m_SizeDiffNum.ToString();
            this.LblOnlyDateDiff.Text = this.m_OnlyDateDiffNum.ToString();
            this.LblServerNoExists.Text = this.m_ServerNotExsis.ToString();
            this.lblLocalNoExists.Text = this.m_LocalNotExsis.ToString();
        }

        private void CompareFile(bool isShowAll, DataRow[] dataRowList)
        {
            foreach (DataRow row in dataRowList)
            {
                if (string.IsNullOrEmpty(row["ServerSize"].ToString()))
                {
                    DateTime time3 = (DateTime) row["LocalTime"];
                    this.NewRow(this.NewCell(""), this.NewCell(""), this.NewCell(""), this.NewFileCell(row["FileName"].ToString(), Color.Blue), this.NewCell(string.Format("{0:N0}", row["LocalSize"]), Color.Blue), this.NewCell(time3.ToString("yyyy-MM-dd HH:mm:ss"), Color.Blue), this.NewCell(""));
                }
                else if (string.IsNullOrEmpty(row["LocalSize"].ToString()))
                {
                    this.NewRow(this.NewFileCell(row["FileName"].ToString(), Color.Blue), this.NewCell(string.Format("{0:N0}", row["ServerSize"]), Color.Blue), this.NewCell(DataConverter.CDate(row["ServerTime"]).ToString("yyy-MM-dd HH:mm:ss"), Color.Blue), this.NewCell(""), this.NewCell(""), this.NewCell(""), this.NewCell(""));
                    this.m_LocalNotExsis++;
                }
                else
                {
                    Color color2;
                    Color color3;
                    Color color4;
                    Color color5;
                    Color color6;
                    Color gray;
                    Color color = color2 = color3 = color4 = color5 = color6 = gray = Color.Black;
                    bool flag = false;
                    string text = "=";
                    string str2 = row["FileName"].ToString();
                    long num = DataConverter.CLng(row["ServerSize"]);
                    long num2 = DataConverter.CLng(row["LocalSize"]);
                    DateTime time = DataConverter.CDate(row["ServerTime"]);
                    DateTime time2 = DataConverter.CDate(row["LocalTime"]);
                    if (num != num2)
                    {
                        text = "≠";
                        this.m_SizeDiffNum++;
                        if (time > time2)
                        {
                            color = color2 = color3 = gray = Color.Red;
                            color4 = color5 = color6 = Color.Gray;
                        }
                        else
                        {
                            color4 = color5 = color6 = gray = color = color2 = color3 = Color.Red;
                        }
                    }
                    else if (time != time2)
                    {
                        color6 = color3 = Color.Red;
                        gray = Color.Gray;
                        text = "≈";
                        this.m_OnlyDateDiffNum++;
                    }
                    else
                    {
                        text = "=";
                        this.m_SizeAndDataDiffNum++;
                        flag = true;
                    }
                    if (isShowAll || !flag)
                    {
                        this.NewRow(this.NewFileCell(str2, color), this.NewCell(num.ToString(), color2), this.NewCell(time.ToString("yyyy-MM-dd HH:mm:ss"), color3), this.NewFileCell(row["FileName"].ToString(), color4), this.NewCell(num2.ToString(), color5), this.NewCell(time2.ToString("yyyy-MM-dd HH:mm:ss"), color6), this.NewCell(text, gray));
                    }
                }
            }
        }

        private bool IsShowAll()
        {
            return (BasePage.RequestInt32("IsShowAll") == 0);
        }

        protected void LbtnSort_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                button.CommandArgument = (button.CommandArgument == "DESC") ? "ASC" : "DESC";
                this.CompareFile(this.IsShowAll(), this.ReadLocalFileInfo().Select("", button.CommandName + " " + button.CommandArgument));
            }
        }

        private TableCell NewCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            return cell;
        }

        private TableCell NewCell(string text, Color color)
        {
            TableCell cell = this.NewCell(text);
            cell.ForeColor = color;
            return cell;
        }

        private TableCell NewFileCell(string text, Color color)
        {
            TableCell cell = new TableCell();
            cell.Text = "<b>\x00b7</b>" + ((text.Length > 0x19) ? (text.Substring(0, 0x19) + "...") : text);
            cell.ToolTip = text;
            cell.ForeColor = color;
            return cell;
        }

        private void NewRow(TableCell sFile, TableCell sSize, TableCell sTime, TableCell cFile, TableCell cSize, TableCell cTime, TableCell centerTc)
        {
            TableRow row = new TableRow();
            row.CssClass = this.m_ItemCssClass ? "compare_tdbg1" : "compare_tdbg";
            this.m_ItemCssClass = !this.m_ItemCssClass;
            sSize.HorizontalAlign = cSize.HorizontalAlign = HorizontalAlign.Right;
            sTime.HorizontalAlign = cTime.HorizontalAlign = centerTc.HorizontalAlign = HorizontalAlign.Center;
            centerTc.CssClass = "compare_tdinter";
            centerTc.Text = "<b>" + centerTc.Text + "</b>";
            row.Cells.Add(sFile);
            row.Cells.Add(sSize);
            row.Cells.Add(sTime);
            row.Cells.Add(centerTc);
            row.Cells.Add(cFile);
            row.Cells.Add(cSize);
            row.Cells.Add(cTime);
            this.TbCompareFiles.Rows.Add(row);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BasePage.RequestString("IsShowAll")))
            {
                this.BtnCompareTogether_Click(sender, e);
            }
        }

        private DataTable ReadLocalFileInfo()
        {
            DataTable table = this.ReadServerFileInfo();
            string path = base.Server.MapPath("~");
            DirectoryInfo info = new DirectoryInfo(path);
            string[] strArray = "asax|ascs|ashx|asmx|asp|aspx|axd|cdx|cer|idc|licx|rem|resources|resx|soap|stm|vsdisco|webinfo".Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            int num = 0;
            foreach (string str2 in strArray)
            {
                FileInfo[] files = info.GetFiles("*." + str2, SearchOption.AllDirectories);
                if (files.Length != 0)
                {
                    num += files.Length;
                    foreach (FileInfo info2 in files)
                    {
                        if (string.Compare(info2.Extension, "." + str2, true, CultureInfo.CurrentCulture) == 0)
                        {
                            string str3 = info2.FullName.Substring(path.Length).Replace('\\', '/').TrimStart(new char[] { '/' });
                            DataRow[] rowArray = table.Select("FileName='" + str3 + "'");
                            if (rowArray.Length == 0)
                            {
                                DataRow row = table.NewRow();
                                row["FileName"] = str3;
                                row["LocalSize"] = info2.Length;
                                row["LocalTime"] = info2.LastWriteTime;
                                table.Rows.Add(row);
                                this.m_ServerNotExsis++;
                            }
                            else
                            {
                                rowArray[0]["localSize"] = info2.Length;
                                rowArray[0]["LocalTime"] = info2.LastWriteTime;
                            }
                        }
                    }
                }
            }
            this.LblLocalFiles.Text = num.ToString();
            return table;
        }

        private DataTable ReadServerFileInfo()
        {
            string str;
            if (BasePage.IseShop)
            {
                str = "http://www.EasyOne.net/FileList/SiteFactory/eShop_1.0.txt";
            }
            else
            {
                str = "http://www.EasyOne.net/FileList/SiteFactory/CMS_1.0.txt";
            }
            Uri requestUri = new Uri(str);
            StreamReader reader = null;
            Stream responseStream = null;
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
                request.AllowAutoRedirect = false;
                request.MaximumAutomaticRedirections = 1;
                response = (HttpWebResponse) request.GetResponse();
                if (response.ResponseUri.ToString() != requestUri.ToString())
                {
                    throw new WebException();
                }
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.Default);
            }
            catch (SocketException)
            {
                AdminPage.WriteErrMsg("由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。", "CompareFilesOnline.aspx");
            }
            catch (ProtocolViolationException)
            {
                AdminPage.WriteErrMsg("由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。", "CompareFilesOnline.aspx");
            }
            catch (WebException)
            {
                AdminPage.WriteErrMsg("由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。", "CompareFilesOnline.aspx");
            }
            catch (InvalidOperationException)
            {
                AdminPage.WriteErrMsg("由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。", "CompareFilesOnline.aspx");
            }
            catch (NotSupportedException)
            {
                AdminPage.WriteErrMsg("由于官方在一段时间后没有正确答复或连接的官方主机没有反应，连接尝试失败，请稍后再试。", "CompareFilesOnline.aspx");
            }
            DataTable table = new DataTable("ServerFiles");
            table.Columns.Add("FileName", typeof(string));
            table.Constraints.Add("PK_FileName", table.Columns["FileName"], true);
            table.Columns.Add("ServerSize", typeof(long));
            table.Columns.Add("ServerTime", typeof(DateTime));
            table.Columns.Add("LocalSize", typeof(long));
            table.Columns.Add("LocalTime", typeof(DateTime));
            if (reader != null)
            {
                string str2;
                if (string.IsNullOrEmpty(SiteConfig.SiteOption.AdvertisementDir))
                {
                    while ((str2 = reader.ReadLine()) != null)
                    {
                        string[] strArray2 = str2.Split(new char[] { '|' });
                        if (strArray2.Length >= 3)
                        {
                            DataRow row2 = table.NewRow();
                            row2["FileName"] = strArray2[0];
                            row2["ServerSize"] = DataConverter.CLng(strArray2[1]);
                            row2["ServerTime"] = DataConverter.CDate(strArray2[2]);
                            table.Rows.Add(row2);
                        }
                    }
                }
                else
                {
                    while ((str2 = reader.ReadLine()) != null)
                    {
                        string[] strArray = str2.Split(new char[] { '|' });
                        if (strArray.Length >= 3)
                        {
                            DataRow row = table.NewRow();
                            row["FileName"] = Regex.Replace(strArray[0], "^(AD/)", SiteConfig.SiteOption.AdvertisementDir + "/");
                            row["ServerSize"] = DataConverter.CLng(strArray[1]);
                            row["ServerTime"] = DataConverter.CDate(strArray[2]);
                            table.Rows.Add(row);
                        }
                    }
                }
                response.Close();
                responseStream.Dispose();
                reader.Dispose();
            }
            if (table.Rows.Count == 0)
            {
                AdminPage.WriteErrMsg("<li><span style='Color:#f00'>获取官方数据失败，可能是您的服务器不支持 XMLHTTP 组件或者是通过代理服务器访问网络。</span></li>");
            }
            this.LblServerFiles.Text = table.Rows.Count.ToString();
            return table;
        }
    }
}

