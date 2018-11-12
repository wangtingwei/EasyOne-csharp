namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class AnonymousContent : DynamicPage
    {

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int num = DataConverter.CLng(this.DropNodeId.SelectedValue);
                int num2 = DataConverter.CLng(this.DropModelId.SelectedValue);
                if (num == 0)
                {
                    DynamicPage.WriteErrMsg("<li>您没有指定要匿名投稿的所属栏目</li>");
                }
                if (num2 == 0)
                {
                    DynamicPage.WriteErrMsg("<li>您没有指定要匿名投稿的所属模型</li>");
                }
                BasePage.ResponseRedirect("AnonymousContent2.aspx?NodeID=" + num.ToString() + "&ModelID=" + num2.ToString());
            }
        }

        protected void DropNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetModelList(DataConverter.CLng(this.DropNodeId.SelectedValue));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.DropNodeId.Items.Clear();
                IList<NodeInfo> anonymousNodeId = Nodes.GetAnonymousNodeId(-2, OperateCode.NodeContentInput);
                if (anonymousNodeId.Count <= 0)
                {
                    DynamicPage.WriteErrMsg("<li>没有允许匿名发表的栏目！</li>");
                }
                this.DropNodeId.DataSource = anonymousNodeId;
                this.DropNodeId.DataBind();
                this.SetModelList(DataConverter.CLng(this.DropNodeId.Items[0].Value));
            }
        }

        protected void SetModelList(int nodeId)
        {
            this.DropModelId.Items.Clear();
            foreach (NodesModelTemplateRelationShipInfo info2 in ModelManager.GetNodesModelTemplateList(nodeId))
            {
                ModelInfo cacheModelById = ModelManager.GetCacheModelById(info2.ModelId);
                if (!cacheModelById.IsNull && !cacheModelById.IsEshop)
                {
                    this.DropModelId.Items.Add(new ListItem("添加" + cacheModelById.ItemName + " (" + cacheModelById.ModelName + ")", cacheModelById.ModelId.ToString()));
                }
            }
        }
    }
}

