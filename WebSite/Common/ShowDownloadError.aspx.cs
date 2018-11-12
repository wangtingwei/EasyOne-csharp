namespace EasyOne.WebSite
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class ShowDownloadError : DynamicPage
    {

        protected void EBtnCancle_Click(object sender, EventArgs e)
        {
            int num = BasePage.RequestInt32("id");
            base.Response.Redirect(string.Concat(new object[] { SiteConfig.SiteInfo.VirtualPath, "Item/", num, ".aspx" }));
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            bool flag = true;
            int num = BasePage.RequestInt32("type");
            int generalId = BasePage.RequestInt32("id");
            DataTable contentDataById = ContentManage.GetContentDataById(generalId);
            if (contentDataById == null)
            {
                DynamicPage.WriteErrMsg("对不起，错误的参数！");
            }
            if (contentDataById.Rows.Count > 0)
            {
                string str = contentDataById.Rows[0]["DownloadUrl"].ToString();
                string str2 = contentDataById.Rows[0]["arrServerName"].ToString();
                StringBuilder builder = new StringBuilder();
                if (this.EgvDownloadError.SelectList.Length == 0)
                {
                    DynamicPage.WriteErrMsg("对不起，您还没选择要报错的信息！");
                }
                for (int i = 0; i < this.EgvDownloadError.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox) this.EgvDownloadError.Rows[i].Cells[this.EgvDownloadError.CheckBoxColumnIndex].FindControl("CheckBoxButton");
                    if (box.Checked)
                    {
                        DownloadErrorInfo downloadErrorInfo = new DownloadErrorInfo();
                        if (num == 1)
                        {
                            if (string.IsNullOrEmpty(str))
                            {
                                downloadErrorInfo.InfoId = generalId;
                                downloadErrorInfo.ErrorUrl = new InsideStaticLabel().GetInfoPath(contentDataById.Rows[0]["NodeId"].ToString(), contentDataById.Rows[0]["GeneralId"].ToString(), contentDataById.Rows[0]["InputTime"].ToString(), contentDataById.Rows[0]["Title"].ToString());
                                downloadErrorInfo.ErrorDate = DateTime.Now;
                                downloadErrorInfo.ErrorTimes = 1;
                            }
                            else if (!string.IsNullOrEmpty(str2))
                            {
                                int serverId = DataConverter.CLng(((HiddenField) this.EgvDownloadError.Rows[i].Cells[1].FindControl("HdnUrlID")).Value);
                                int urlId = DataConverter.CLng(((HiddenField) this.EgvDownloadError.Rows[i].Cells[1].FindControl("HdnServerID")).Value);
                                downloadErrorInfo.InfoId = generalId;
                                downloadErrorInfo.ErrorUrl = DownloadError.GetDownloadurlById(str, urlId, serverId);
                                downloadErrorInfo.ErrorDate = DateTime.Now;
                                downloadErrorInfo.ErrorTimes = 1;
                            }
                            else
                            {
                                downloadErrorInfo.InfoId = generalId;
                                downloadErrorInfo.ErrorUrl = DownloadError.GetDownloadurlById(str, i, 0);
                                downloadErrorInfo.ErrorDate = DateTime.Now;
                                downloadErrorInfo.ErrorTimes = 1;
                            }
                        }
                        else
                        {
                            downloadErrorInfo.InfoId = generalId;
                            downloadErrorInfo.ErrorUrl = new InsideStaticLabel().GetInfoPath(contentDataById.Rows[0]["NodeId"].ToString(), contentDataById.Rows[0]["GeneralId"].ToString(), contentDataById.Rows[0]["InputTime"].ToString(), contentDataById.Rows[0]["Title"].ToString());
                            downloadErrorInfo.ErrorDate = DateTime.Now;
                            downloadErrorInfo.ErrorTimes = 1;
                        }
                        if (!DownloadError.Add(downloadErrorInfo))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    DynamicPage.WriteSuccessMsg("感谢您的参与，提交成功！", SiteConfig.SiteInfo.VirtualPath + "Category_" + contentDataById.Rows[0]["NodeId"].ToString() + "/index.aspx");
                }
                else
                {
                    DynamicPage.WriteErrMsg("对不起，提交失败！");
                }
            }
            else
            {
                DynamicPage.WriteErrMsg("对不起，错误的参数！");
            }
        }

        private void ErrorUrlData(int type, int id)
        {
            if (type == 1)
            {
                DataTable contentDataById = ContentManage.GetContentDataById(id);
                if (contentDataById == null)
                {
                    DynamicPage.WriteErrMsg("对不起，错误的参数！");
                }
                if (contentDataById.Rows.Count > 0)
                {
                    string str = contentDataById.Rows[0]["DownloadUrl"].ToString();
                    string str2 = contentDataById.Rows[0]["arrServerName"].ToString();
                    DataTable table2 = new DataTable();
                    table2.Columns.Add("urlId", typeof(int));
                    table2.Columns.Add("infoId", typeof(int));
                    table2.Columns.Add("urlname", typeof(string));
                    table2.Columns.Add("serverId", typeof(int));
                    if (string.IsNullOrEmpty(str))
                    {
                        CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(id);
                        DataRow row = table2.NewRow();
                        row["urlId"] = 0;
                        row["infoId"] = id;
                        row["urlname"] = commonModelInfoById.Title;
                        row["serverId"] = 0;
                        table2.Rows.Add(row);
                    }
                    else
                    {
                        int num = 0;
                        if (string.IsNullOrEmpty(str2))
                        {
                            foreach (string str3 in str.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                string[] strArray2 = str3.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                                DataRow row2 = table2.NewRow();
                                row2["urlId"] = num;
                                row2["infoId"] = id;
                                row2["urlname"] = strArray2[0];
                                row2["serverId"] = 0;
                                table2.Rows.Add(row2);
                                num++;
                            }
                        }
                        else
                        {
                            string[] strArray3 = str.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                            num = 0;
                            string[] strArray10 = strArray3;
                            for (int i = 0; i < strArray10.Length; i++)
                            {
                                string text1 = strArray10[i];
                                foreach (string str4 in str2.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] strArray5 = str4.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                                    DataRow row3 = table2.NewRow();
                                    row3["urlId"] = num;
                                    row3["infoId"] = id;
                                    row3["urlname"] = strArray5[0];
                                    row3["serverId"] = strArray5[1];
                                    table2.Rows.Add(row3);
                                }
                                num++;
                            }
                        }
                    }
                    this.EgvDownloadError.DataSource = table2;
                    this.EgvDownloadError.DataBind();
                }
                else
                {
                    DynamicPage.WriteErrMsg("对不起，错误的参数！");
                }
            }
            else
            {
                CommonModelInfo info2 = ContentManage.GetCommonModelInfoById(id);
                if (info2.IsNull)
                {
                    DynamicPage.WriteErrMsg("对不起，错误的参数！");
                }
                DataTable table3 = new DataTable();
                table3.Columns.Add("urlId", typeof(int));
                table3.Columns.Add("infoId", typeof(int));
                table3.Columns.Add("urlname", typeof(string));
                table3.Columns.Add("serverId", typeof(int));
                DataRow row4 = table3.NewRow();
                row4["urlId"] = 0;
                row4["infoId"] = id;
                row4["urlname"] = info2.Title;
                row4["serverId"] = 0;
                table3.Rows.Add(row4);
                this.EgvDownloadError.DataSource = table3;
                this.EgvDownloadError.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int type = BasePage.RequestInt32("type");
                int id = BasePage.RequestInt32("id");
                this.ErrorUrlData(type, id);
            }
        }
    }
}

