namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class SourceManage : AdminPage
    {

        protected void EBtnAdd_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Source.aspx");
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            string append = string.Empty;
            StringBuilder sb = new StringBuilder(string.Empty);
            for (int i = 0; i < this.GdvSourceList.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.GdvSourceList.Rows[i].Cells[0].FindControl("CheckBoxButton");
                if (box.Checked)
                {
                    append = this.GdvSourceList.DataKeys[i].Value.ToString();
                    StringHelper.AppendString(sb, append);
                }
            }
            if (sb.Length == 0)
            {
                AdminPage.WriteErrMsg("对不起，您还没选择要删除的项！");
            }
            else if (Source.Delete(sb.ToString()))
            {
                this.GdvSourceList.DataBind();
            }
            else
            {
                AdminPage.WriteErrMsg("删除失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SourceList_RowCommand(object sender, CommandEventArgs e)
        {
            bool flag = false;
            string commandName = e.CommandName;
            if (commandName != null)
            {
                SourceInfo sourceInfoById;
                if (!(commandName == "Elite"))
                {
                    if (commandName == "OnTop")
                    {
                        sourceInfoById = Source.GetSourceInfoById(Convert.ToInt32(e.CommandArgument));
                        sourceInfoById.OnTop = !sourceInfoById.OnTop;
                        flag = Source.Update(sourceInfoById);
                        goto Label_00E1;
                    }
                    if (commandName == "Passed")
                    {
                        sourceInfoById = Source.GetSourceInfoById(Convert.ToInt32(e.CommandArgument));
                        sourceInfoById.Passed = !sourceInfoById.Passed;
                        flag = Source.Update(sourceInfoById);
                        goto Label_00E1;
                    }
                    if (commandName == "Deleted")
                    {
                        flag = Source.Delete(Convert.ToString(Convert.ToInt32(e.CommandArgument), (IFormatProvider) null));
                        goto Label_00E1;
                    }
                }
                else
                {
                    sourceInfoById = Source.GetSourceInfoById(Convert.ToInt32(e.CommandArgument));
                    sourceInfoById.Elite = !sourceInfoById.Elite;
                    flag = Source.Update(sourceInfoById);
                    goto Label_00E1;
                }
            }
            flag = false;
        Label_00E1:
            if (flag)
            {
                this.GdvSourceList.DataBind();
            }
        }
    }
}

