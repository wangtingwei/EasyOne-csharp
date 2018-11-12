namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SpecialInfosManage : AdminPage
    {
        private bool m_Administrator;
        private bool m_IsManage;
        protected Dictionary<int, string> nodeNameDictionary = new Dictionary<int, string>();

        protected void DropSelectedIndex_Changed(object sender, EventArgs e)
        {
            this.HdnListType.Value = this.DropRescentQuery.SelectedValue;
            this.EgvSpecialInfos.PageIndex = 0;
        }

        protected void EBtnAddToSpecial_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder();
            selectList = this.EgvSpecialInfos.SelectList;
            if (selectList.Length <= 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要添加的项！</li>", "SpecialInfosManage.aspx");
            }
            else
            {
                BasePage.ResponseRedirect("AddContentToSpecial.aspx?Action=Special&Id=" + selectList.ToString());
            }
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder selectList = new StringBuilder("");
            selectList = this.EgvSpecialInfos.SelectList;
            if (selectList.Length == 0)
            {
                AdminPage.WriteErrMsg("<li>对不起，您还没选择要删除的项！</li>");
            }
            else if (Special.DeleteSpecialInfoById(selectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除成功！</li>", string.Concat(new object[] { "SpecialInfosManage.aspx?SpecialID=", BasePage.RequestInt32("SpecialID"), "&SpecialCategoryID=", BasePage.RequestInt32("SpecialCategoryID") }));
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EBtnMoveToOtherSpecial_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("");
            BasePage.ResponseRedirect("MoveToOtherSpecial.aspx?SpecialId=" + BasePage.RequestString("SpecialId") + "&Id=" + this.EgvSpecialInfos.SelectList.ToString());
        }

        protected void EgvSpecialInfos_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteSpecialInfoById")
            {
                if (Special.DeleteSpecialInfoById(e.CommandArgument.ToString()))
                {
                    AdminPage.WriteSuccessMsg("<li>删除成功！</li>", string.Concat(new object[] { "SpecialInfosManage.aspx?SpecialID=", BasePage.RequestInt32("SpecialID"), "&SpecialCategoryID=", BasePage.RequestInt32("SpecialCategoryID") }));
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
            }
        }

        protected void EgvSpecialInfos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int length = 0;
                SpecialCommonModelInfo dataItem = (SpecialCommonModelInfo) e.Row.DataItem;
                Label label = e.Row.FindControl("LblIsCreateHtml") as Label;
                if (!dataItem.CreateTime.HasValue || (dataItem.CreateTime.Value <= dataItem.UpdateTime))
                {
                    label.Text = "<span style=\"color:Red\"><b>\x00d7</b></span>";
                }
                else
                {
                    label.Text = "<b>√</b>";
                }
                LinkImage image = e.Row.FindControl("LinkImageModel") as LinkImage;
                string itemIcon = ModelManager.GetCacheModelById(dataItem.ModelId).ItemIcon;
                if (string.IsNullOrEmpty(itemIcon))
                {
                    itemIcon = "Default.gif";
                }
                image.Icon = itemIcon;
                if (dataItem.LinkType != 0)
                {
                    image.IsShowLink = true;
                }
                HyperLink link = e.Row.FindControl("HypTitle") as HyperLink;
                length = 0x25 - length;
                link.Text = StringHelper.SubString(dataItem.Title, length, "...");
                link.ToolTip = dataItem.Title;
                link.NavigateUrl = "ContentView.aspx?GeneralID=" + dataItem.GeneralId.ToString();
                if (!this.m_Administrator)
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("DeleteSpecialInfoById");
                    if (BasePage.RequestInt32("SpecialID") > 0)
                    {
                        button.Enabled = this.m_IsManage;
                    }
                    else if (!RolePermissions.AccessCheckSpecialPermission(OperateCode.SepcialContentManage, dataItem.SpecialId))
                    {
                        button.Enabled = false;
                    }
                }
            }
        }

        protected void ELnkCheckContent_Click(object sender, EventArgs e)
        {
            this.HdnStatus.Value = "101";
            this.EgvSpecialInfos.PageIndex = 0;
        }

        protected void ELnkContentList_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentManage.aspx?NodeID=" + BasePage.RequestInt32("NodeID").ToString());
        }

        protected void ELnkContentRecycle_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ContentRecycle.aspx?NodeID=" + BasePage.RequestInt32("NodeID").ToString());
        }

        protected void ELnkHtmlManage_Click(object sender, EventArgs e)
        {
            int num = BasePage.RequestInt32("NodeID");
            if (num > 0)
            {
                BasePage.ResponseRedirect("ContentHtml.aspx?NodeID=" + num.ToString());
            }
            else
            {
                BasePage.ResponseRedirect("ContentHtml.aspx");
            }
        }

        protected string GetStatusShow(string status)
        {
            int num = DataConverter.CLng(status);
            switch (num)
            {
                case -3:
                    return "回收站中";

                case -2:
                    return "退稿";

                case -1:
                    return "草稿";

                case 0:
                    return "待审核";

                case 0x63:
                    return "终审通过";
            }
            return "审核中";
        }

        private string InitSiteMapCategory(int categoryId)
        {
            return Special.GetSpecialCategoryInfoById(categoryId).SpecialCategoryName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int specialId = BasePage.RequestInt32("SpecialID");
            int categoryId = BasePage.RequestInt32("SpecialCategoryID");
            string str = BasePage.RequestString("SpecialName");
            if (PEContext.Current.Admin.IsSuperAdmin)
            {
                this.m_Administrator = true;
            }
            if (!base.IsPostBack)
            {
                this.DropRescentQuery.SelectedValue = BasePage.RequestStringToLower("ListType");
                this.HdnListType.Value = BasePage.RequestStringToLower("ListType");
                this.HdnStatus.Value = BasePage.RequestStringToLower("status", "100");
                if (!string.IsNullOrEmpty(str))
                {
                    this.SmpNavigator.AdditionalNode = this.InitSiteMapCategory(categoryId) + " >> " + str;
                }
                else
                {
                    this.SmpNavigator.AdditionalNode = this.InitSiteMapCategory(categoryId);
                }
            }
            if (!this.m_Administrator)
            {
                if (specialId > 0)
                {
                    this.m_IsManage = RolePermissions.AccessCheckSpecialPermission(OperateCode.SepcialContentManage, specialId);
                }
                else if (categoryId > 0)
                {
                    foreach (SpecialCommonModelInfo info in ContentManage.GetCommonModelInfoListBySpecialIdOrSpecialCategoryId(0, 0, 0, categoryId, ContentSortType.None, 100))
                    {
                        if (RolePermissions.AccessCheckSpecialPermission(OperateCode.SepcialContentManage, info.SpecialId))
                        {
                            this.m_IsManage = true;
                            break;
                        }
                    }
                }
                else
                {
                    this.m_IsManage = RolePermissions.AccessCheckSpecialPermission(OperateCode.SepcialContentManage, -1);
                }
                if (!this.m_IsManage)
                {
                    this.EBtnDelete.Enabled = false;
                    this.EBtnAddToSpecial.Enabled = false;
                    this.EBtnMoveToOtherSpecial.Enabled = false;
                }
            }
        }

        protected void RadlContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EgvSpecialInfos.PageIndex = 0;
        }
    }
}

