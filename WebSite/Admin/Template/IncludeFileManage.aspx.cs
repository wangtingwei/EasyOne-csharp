namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Templates;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class IncludeFileManage : AdminPage
    {

        protected void BtnAllCreateIncludeFile_Click(object sender, EventArgs e)
        {
            IncludeFile.CreateAllIncludeFile();
            AdminPage.WriteSuccessMsg("刷新所有成功", "IncludeFileManage.aspx");
        }

        protected void BtnBatchCreateIncludeFile_Click(object sender, EventArgs e)
        {
            foreach (string str in this.EgvIncludeFileList.SelectList.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                IncludeFile.CreateIncludeFile(DataConverter.CLng(str));
            }
            AdminPage.WriteSuccessMsg("批量刷新成功", "IncludeFileManage.aspx");
        }

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            foreach (string str in this.EgvIncludeFileList.SelectList.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                IncludeFile.Delete(DataConverter.CLng(str));
            }
            AdminPage.WriteSuccessMsg("批量删除成功", "IncludeFileManage.aspx");
        }

        protected void EgvIncludeFileList_RowCommand(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            if (commandName != null)
            {
                if (!(commandName == "CreateIncludeFile"))
                {
                    if (!(commandName == "DeleteIncludeFile"))
                    {
                        return;
                    }
                }
                else
                {
                    IncludeFile.CreateIncludeFile(DataConverter.CLng(e.CommandArgument));
                    this.EgvIncludeFileList.DataBind();
                    return;
                }
                IncludeFile.Delete(DataConverter.CLng(e.CommandArgument));
                this.EgvIncludeFileList.DataBind();
            }
        }

        protected void EgvIncludeFileList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IncludeFileInfo dataItem = (IncludeFileInfo) e.Row.DataItem;
                TextBox box = (TextBox) e.Row.FindControl("TxtIncludeCode");
                ExtendedLiteral literal = (ExtendedLiteral) e.Row.FindControl("LitFileName");
                HyperLink link = (HyperLink) e.Row.FindControl("HlnkPreview");
                string includeFilePath = SiteConfig.SiteOption.IncludeFilePath;
                includeFilePath = "~/" + includeFilePath + "/" + dataItem.FileName;
                FileInfo info2 = new FileInfo(HttpContext.Current.Request.MapPath(includeFilePath));
                if (!info2.Exists)
                {
                    ((LinkButton) e.Row.FindControl("LnkCreateIncludeFile")).Text = "生成";
                    literal.BeginTag = "<span style='color:red'>";
                    literal.Text = dataItem.FileName;
                    literal.EndTag = "</span>";
                    link.Enabled = false;
                }
                else
                {
                    literal.Text = dataItem.FileName;
                }
                switch (dataItem.IncludeType)
                {
                    case IncludeType.JSWriteHtml:
                    case IncludeType.JS:
                        box.Text = "<script type=\"text/javascript\" src=\"{PE.SiteConfig.ApplicationPath /}{PE.SiteConfig.includefilepath /}/" + dataItem.FileName + "\"></script>";
                        return;

                    case IncludeType.Html:
                        box.Text = "<!--#include File=\"{PE.SiteConfig.ApplicationPath /}{PE.SiteConfig.includefilepath /}/" + dataItem.FileName + "\"-->";
                        return;

                    default:
                        return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

