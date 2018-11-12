namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    public partial class Item : TemplatePage
    {
        private int m_GeneralId;
        private CommonModelInfo m_ItemInfo;
        private ModelInfo m_ModelInfo;
        private NodeInfo m_NodeInfo;

        protected string ItemTemplateFilePath()
        {
            if (!string.IsNullOrEmpty(this.m_ItemInfo.TemplateFile))
            {
                return this.m_ItemInfo.TemplateFile;
            }
            NodesModelTemplateRelationShipInfo nodesModelTemplateRelationShip = ModelManager.GetNodesModelTemplateRelationShip(this.m_NodeInfo.NodeId, this.m_ModelInfo.ModelId);
            if (!nodesModelTemplateRelationShip.IsNull && !string.IsNullOrEmpty(nodesModelTemplateRelationShip.DefaultTemplateFile))
            {
                return nodesModelTemplateRelationShip.DefaultTemplateFile;
            }
            if (!string.IsNullOrEmpty(this.m_ModelInfo.DefaultTemplateFile))
            {
                return this.m_ModelInfo.DefaultTemplateFile;
            }
            return "";
        }

        public override void OnInitTemplateInfo(EventArgs e)
        {
            TemplateInfo info = new TemplateInfo();
            info.QueryList = base.Request.QueryString;
            NameValueCollection queryString = base.Request.QueryString;
            StringBuilder builder = new StringBuilder();
            if (queryString.Count > 3)
            {
                for (int i = 2; i < queryString.Count; i++)
                {
                    builder.Append(queryString[i]);
                    builder.Append("&");
                }
            }
            info.PageName = this.m_GeneralId.ToString() + "_{$pageid/}.aspx";
            if (!string.IsNullOrEmpty(builder.ToString()))
            {
                info.PageName = info.PageName + "?" + builder.ToString();
            }
            info.PageType = 0;
            info.TemplateContent = Template.GetTemplateContent(this.ItemTemplateFilePath());
            info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
            info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
            info.IsDynamicPage = true;
            info.PageType = 0;
            base.TemplateInfo = info;
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            this.m_GeneralId = BasePage.RequestInt32("id");
            if (this.m_GeneralId <= 0)
            {
                TemplatePage.WriteErrMsg("您访问的内容信息不存在", base.BasePath + "Default.aspx");
            }
            this.m_ItemInfo = ContentManage.GetCommonModelInfoById(this.m_GeneralId);
            if (this.m_ItemInfo.IsNull)
            {
                TemplatePage.WriteErrMsg("您访问的内容信息不存在", base.BasePath + "Default.aspx");
            }
            else
            {
                this.m_ModelInfo = ModelManager.GetModelInfoById(this.m_ItemInfo.ModelId);
                if (!this.m_ModelInfo.IsEshop && (this.m_ItemInfo.Status != 0x63))
                {
                    TemplatePage.WriteErrMsg("您访问的内容信息需要经过审核才能浏览", base.BasePath + "Default.aspx");
                }
            }
            this.m_NodeInfo = Nodes.GetCacheNodeById(this.m_ItemInfo.NodeId);
        }
    }
}

