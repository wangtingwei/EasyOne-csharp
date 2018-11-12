namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AuthorManage : AdminPage
    {

        protected void EBtnAdd_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("Author.aspx");
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            string str = this.GdvAuthorList.SelectList.ToString();
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的作者！</li>");
            }
            else if (Author.Delete(str))
            {
                this.GdvAuthorList.DataBind();
            }
            else
            {
                AdminPage.WriteErrMsg("删除失败！");
            }
        }

        protected void GdvAuthorList_RowCommand(object sender, CommandEventArgs e)
        {
            bool flag = false;
            string commandName = e.CommandName;
            if (commandName != null)
            {
                AuthorInfo authorInfoById;
                if (!(commandName == "Elite"))
                {
                    if (commandName == "OnTop")
                    {
                        authorInfoById = Author.GetAuthorInfoById(Convert.ToInt32(e.CommandArgument));
                        authorInfoById.OnTop = !authorInfoById.OnTop;
                        flag = Author.Update(authorInfoById);
                        goto Label_00E1;
                    }
                    if (commandName == "Passed")
                    {
                        authorInfoById = Author.GetAuthorInfoById(Convert.ToInt32(e.CommandArgument));
                        authorInfoById.Passed = !authorInfoById.Passed;
                        flag = Author.Update(authorInfoById);
                        goto Label_00E1;
                    }
                    if (commandName == "Deleted")
                    {
                        flag = Author.Delete(Convert.ToString(Convert.ToInt32(e.CommandArgument), (IFormatProvider) null));
                        goto Label_00E1;
                    }
                }
                else
                {
                    authorInfoById = Author.GetAuthorInfoById(Convert.ToInt32(e.CommandArgument));
                    authorInfoById.Elite = !authorInfoById.Elite;
                    flag = Author.Update(authorInfoById);
                    goto Label_00E1;
                }
            }
            flag = false;
        Label_00E1:
            if (flag)
            {
                this.GdvAuthorList.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

