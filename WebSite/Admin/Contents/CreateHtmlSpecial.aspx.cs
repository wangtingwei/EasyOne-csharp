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

    public partial class CreateHtmlSpecial : AdminPage
    {

        protected void CreateAllSpecialList_Click(object sender, EventArgs e)
        {
            HtmlSpecial special = new HtmlSpecial();
            string createHtmlSpecialList = this.GetCreateHtmlSpecialList(0);
            special.SpecialIdArray = createHtmlSpecialList;
            special.CommonCreateHtml();
            Special.UpdateNeedCreateHtml(createHtmlSpecialList, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + special.CreateId);
        }

        protected void CreateNeedCreateHtmlList_Click(object sender, EventArgs e)
        {
            string createHtmlSpecialList = this.GetCreateHtmlSpecialList(1);
            if (string.IsNullOrEmpty(createHtmlSpecialList))
            {
                AdminPage.WriteErrMsg("请选择要生成的待发布专题页！", "CreateHtmlSpecial.aspx");
            }
            HtmlSpecial special = new HtmlSpecial();
            special.SpecialIdArray = createHtmlSpecialList;
            special.CommonCreateHtml();
            Special.UpdateNeedCreateHtml(createHtmlSpecialList, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + special.CreateId);
        }

        protected void CreateSpecialListById_Click(object sender, EventArgs e)
        {
            string createHtmlSpecialList = this.GetCreateHtmlSpecialList(2);
            if (string.IsNullOrEmpty(createHtmlSpecialList))
            {
                AdminPage.WriteErrMsg("请选择要生成的专题页！", "CreateHtmlSpecial.aspx");
            }
            HtmlSpecial special = new HtmlSpecial();
            special.SpecialIdArray = createHtmlSpecialList;
            special.CommonCreateHtml();
            Special.UpdateNeedCreateHtml(createHtmlSpecialList, false);
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + special.CreateId);
        }

        protected string GetCreateHtmlSpecialList(int selectType)
        {
            StringBuilder sb = new StringBuilder();
            switch (selectType)
            {
                case 0:
                    for (int i = 0; i < this.LstSpecials.Items.Count; i++)
                    {
                        StringHelper.AppendString(sb, this.LstSpecials.Items[i].Value, ",");
                    }
                    break;

                case 1:
                    for (int j = 0; j < this.LstSpecials.Items.Count; j++)
                    {
                        if (this.LstSpecials.Items[j].Text.EndsWith("(需要重新生成)", StringComparison.Ordinal))
                        {
                            StringHelper.AppendString(sb, this.LstSpecials.Items[j].Value, ",");
                        }
                    }
                    break;

                case 2:
                    for (int k = 0; k < this.LstSpecials.Items.Count; k++)
                    {
                        if (this.LstSpecials.Items[k].Selected)
                        {
                            StringHelper.AppendString(sb, this.LstSpecials.Items[k].Value, ",");
                        }
                    }
                    break;
            }
            return sb.ToString();
        }

        public void ListDataBind(ListControl dropName)
        {
            dropName.Items.Clear();
            foreach (SpecialInfo info in Special.GetSpecialList())
            {
                ListItem item = new ListItem();
                if (info.NeedCreateHtml)
                {
                    item.Text = info.SpecialName + "(需要重新生成)";
                }
                else
                {
                    item.Text = info.SpecialName;
                }
                item.Value = info.SpecialId.ToString();
                item.Enabled = info.IsCreateListPage;
                dropName.Items.Add(item);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
                RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
                this.ListDataBind(this.LstSpecials);
            }
        }
    }
}

