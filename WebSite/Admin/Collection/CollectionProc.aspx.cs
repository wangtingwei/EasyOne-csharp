namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.AccessManage;
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Collection;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CollectionProc : AdminPage
    {
        protected string m_UrlReferrer = "";

        protected void BtnStopCreate_Click(object sender, EventArgs e)
        {
            string nodeValue = XmlManage.Instance("Config/CreateCollectionWork.config", XmlType.File).GetNodeValue("CollectionWork/WorkId");
            if (base.Application[nodeValue] != null)
            {
                CollectionProgress progress = base.Application[nodeValue] as CollectionProgress;
                progress.CreateThread.Abort();
                base.Application.Remove(nodeValue);
                if (DataConverter.CBoolean(this.HdnIsCreateHtml.Value))
                {
                    string nodeIds = "";
                    string str3 = BasePage.RequestString("ItemIds");
                    if (!string.IsNullOrEmpty(str3))
                    {
                        if (str3.Contains(","))
                        {
                            foreach (string str4 in str3.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                nodeIds = GetNodeIs(nodeIds, str4);
                            }
                        }
                        else
                        {
                            nodeIds = GetNodeIs(nodeIds, str3);
                        }
                    }
                    HtmlContent content = new HtmlContent();
                    content.CreateMethod = CreateContentType.CreateByNotCreate;
                    content.NodeIdArray = nodeIds;
                    content.CommonCreateHtml();
                }
            }
        }

        private static string GetNodeIs(string nodeIds, string itemIds)
        {
            CollectionItemInfo infoById = CollectionItem.GetInfoById(DataConverter.CLng(itemIds));
            if (!infoById.IsNull)
            {
                nodeIds = nodeIds + infoById.NodeId.ToString() + "," + infoById.InfoNodeId;
            }
            return nodeIds;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
            this.HdnIsCreateHtml.Value = BasePage.RequestString("isCreateHtml");
            string nodeValue = XmlManage.Instance("Config/CreateCollectionWork.config", XmlType.File).GetNodeValue("CollectionWork/WorkId");
            this.m_UrlReferrer = base.Request.UrlReferrer.ToString();
            if (base.Application[nodeValue] != null)
            {
                this.BtnStopCreate.Visible = true;
            }
            else
            {
                this.BtnStopCreate.Visible = false;
            }
        }
    }
}

