namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class MailListExport : AdminPage
    {

        protected void BtnSaveAccess_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                string exportFileName = MailList.GetExportFileName(this.TxtExportToAccess.Text);
                if (FileSystemObject.IsExist(exportFileName, FsoMethod.File))
                {
                    try
                    {
                        int num = Users.ExportDataToAccess(exportFileName, Convert.ToInt32(this.DropGroup1.SelectedValue));
                        if (num > 0)
                        {
                            AdminPage.WriteSuccessMsg("操作成功：共导出 " + num.ToString() + " 个会员Email地址到" + this.TxtExportToAccess.Text + "文件。<a href=" + exportFileName + ">点击这里将文件下载回本地</a>", "MailListExport.aspx");
                        }
                        else
                        {
                            AdminPage.WriteErrMsg("该会员组没有可供导出的数据！");
                        }
                    }
                    catch (OleDbException exception)
                    {
                        AdminPage.WriteErrMsg("<li>数据库操作失败，错误原因：" + exception.Message + "</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("文件不存在！");
                }
            }
        }

        protected void BtnSaveText_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                string exportFileName = MailList.GetExportFileName(this.TxtExportToText.Text);
                int num = this.ExportDataToText(exportFileName, Convert.ToInt32(this.DropGroup2.SelectedValue));
                if (num > 0)
                {
                    AdminPage.WriteSuccessMsg("<p align='center'>操作成功：共导出 " + num.ToString() + " 个会员Email地址到" + this.TxtExportToText.Text + "文件。<br/><br/><a style='color:#00F;' href='MailListExport.aspx?Action=LoadText&FileName=" + this.TxtExportToText.Text.Trim() + "'>点击这里将文件下载回本地</a></p>");
                }
                else
                {
                    AdminPage.WriteErrMsg("该会员组没有可供导出的数据！");
                }
            }
        }

        private void DownLoadTextFile()
        {
            string str = BasePage.RequestString("FileName");
            if (!string.IsNullOrEmpty(str))
            {
                string s = string.Empty;
                try
                {
                    s = File.ReadAllText(Path.Combine(base.Server.MapPath("~"), "Temp/" + str));
                }
                catch (IOException exception)
                {
                    AdminPage.WriteErrMsg(exception.Message, "MailListSend.aspx");
                }
                base.Response.Clear();
                base.Response.ContentType = "text/plain";
                base.Response.AppendHeader("content-disposition", "attachment;filename=" + str);
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(s);
                base.Response.Flush();
                base.Response.End();
            }
            else
            {
                AdminPage.WriteErrMsg("没有找到文件！", "MailListSend.aspx");
            }
        }

        private void DropDownListBind()
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.DropGroup1.DataSource = userGroupList;
            this.DropGroup2.DataSource = userGroupList;
            this.DropGroup1.DataTextField = "GroupName";
            this.DropGroup1.DataValueField = "GroupId";
            this.DropGroup2.DataTextField = "GroupName";
            this.DropGroup2.DataValueField = "GroupId";
            this.DropGroup1.DataBind();
            this.DropGroup2.DataBind();
            ListItem item = new ListItem("全部会员", "0");
            this.DropGroup1.Items.Insert(0, item);
            this.DropGroup2.Items.Insert(0, item);
        }

        private int ExportDataToText(string filename, int groupId)
        {
            IList<string> userMailByGroupId = Users.GetUserMailByGroupId(groupId);
            if (userMailByGroupId.Count <= 0)
            {
                return 0;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in userMailByGroupId)
            {
                builder.AppendLine(str);
            }
            FileSystemObject.WriteFile(filename, builder.ToString());
            return userMailByGroupId.Count;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (BasePage.RequestString("Action") == "LoadText")
            {
                this.DownLoadTextFile();
            }
            if (!base.IsPostBack)
            {
                this.DropDownListBind();
            }
        }
    }
}

