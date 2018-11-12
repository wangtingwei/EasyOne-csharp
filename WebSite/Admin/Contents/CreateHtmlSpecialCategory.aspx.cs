namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CreateHtmlSpecialCategory : AdminPage
    {

        protected void CreateAllSpecialList_Click(object sender, EventArgs e)
        {
            HtmlSpecialCategory category = new HtmlSpecialCategory();
            category.SpecialCategoryIdArray = this.GetCreateHtmlSpecialCategoryList(0);
            category.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected void CreateNeedCreateHtmlList_Click(object sender, EventArgs e)
        {
            string createHtmlSpecialCategoryList = this.GetCreateHtmlSpecialCategoryList(1);
            if (string.IsNullOrEmpty(createHtmlSpecialCategoryList))
            {
                AdminPage.WriteErrMsg("请选择要生成待发布的专题类别页！", "CreateHtmlSpecial.aspx");
            }
            HtmlSpecialCategory category = new HtmlSpecialCategory();
            category.SpecialCategoryIdArray = createHtmlSpecialCategoryList;
            category.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected void CreateSpecialListById_Click(object sender, EventArgs e)
        {
            string createHtmlSpecialCategoryList = this.GetCreateHtmlSpecialCategoryList(2);
            if (string.IsNullOrEmpty(createHtmlSpecialCategoryList))
            {
                AdminPage.WriteErrMsg("请选择要生成的专题类别页！", "CreateHtmlSpecial.aspx");
            }
            HtmlSpecialCategory category = new HtmlSpecialCategory();
            category.SpecialCategoryIdArray = createHtmlSpecialCategoryList;
            category.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + category.CreateId);
        }

        protected string GetCreateHtmlSpecialCategoryList(int selectType)
        {
            StringBuilder sb = new StringBuilder();
            switch (selectType)
            {
                case 0:
                    for (int i = 0; i < this.LstSpecialsCategory.Items.Count; i++)
                    {
                        StringHelper.AppendString(sb, this.LstSpecialsCategory.Items[i].Value, ",");
                    }
                    break;

                case 1:
                    for (int j = 0; j < this.LstSpecialsCategory.Items.Count; j++)
                    {
                        if (this.LstSpecialsCategory.Items[j].Text.EndsWith("(需要重新生成)", StringComparison.Ordinal))
                        {
                            StringHelper.AppendString(sb, this.LstSpecialsCategory.Items[j].Value, ",");
                        }
                    }
                    break;

                case 2:
                    for (int k = 0; k < this.LstSpecialsCategory.Items.Count; k++)
                    {
                        if (this.LstSpecialsCategory.Items[k].Selected)
                        {
                            StringHelper.AppendString(sb, this.LstSpecialsCategory.Items[k].Value, ",");
                        }
                    }
                    break;
            }
            return sb.ToString();
        }

        public void ListDataBind(ListControl dropName)
        {
            dropName.Items.Clear();
            foreach (SpecialCategoryInfo info in Special.GetSpecialCategoryList())
            {
                ListItem item = new ListItem();
                if (info.NeedCreateHtml)
                {
                    item.Text = info.SpecialCategoryName + "(需要重新生成)";
                }
                else
                {
                    item.Text = info.SpecialCategoryName;
                }
                item.Value = info.SpecialCategoryId.ToString();
                item.Enabled = info.IsCreateHtml;
                dropName.Items.Add(item);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
                RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
                this.ListDataBind(this.LstSpecialsCategory);
            }
        }
    }
}

