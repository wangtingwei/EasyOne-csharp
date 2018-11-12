namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using EasyOne.Model.WorkFlow;
    using EasyOne.WorkFlows;

    public partial class Content : BaseWebPart, IWebEditable, IWebPartPermissibility
    {
        private bool m_Administrator;
        private string m_arrNodeIdManage = "";
        protected int m_ListType;
        protected int m_NodeId;
        protected Dictionary<int, NodeInfo> m_NodeInfoDictionary = new Dictionary<int, NodeInfo>();
        protected string m_OperateCode;
        protected int m_PageSize = 10;
        protected int m_Status;

        public EditorPartCollection CreateEditorParts()
        {
            ArrayList editorParts = new ArrayList();
            ContentEditorPart part = new ContentEditorPart();
            {
                ID = this.ID + "_editorPart1";
            }
            editorParts.Add(part);
            return new EditorPartCollection(editorParts);
        }

        protected void EgvContent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CommonModelInfo dataItem = (CommonModelInfo) e.Row.DataItem;
                int nodeId = dataItem.NodeId;
                string s = "";
                if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                {
                    s = this.m_NodeInfoDictionary[dataItem.NodeId].NodeName;
                }
                else
                {
                    NodeInfo cacheNodeById = Nodes.GetCacheNodeById(dataItem.NodeId);
                    if (!cacheNodeById.IsNull)
                    {
                        s = cacheNodeById.NodeName;
                        this.m_NodeInfoDictionary.Add(dataItem.NodeId, cacheNodeById);
                    }
                }
                HyperLink link = e.Row.FindControl("LnkNodeLink") as HyperLink;
                link.Text = s;
                link.NavigateUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Contents/ContentManage.aspx?NodeID=" + dataItem.NodeId.ToString() + "&NodeName=" + base.Server.UrlEncode(s);
                HyperLink link2 = (HyperLink) e.Row.FindControl("HypTitle");
                link2.ToolTip = dataItem.Title;
                link2.Text = StringHelper.SubString(dataItem.Title, 0x19, "...");
                string str2 = "javascript:OpenLink(\"Contents/NodeTree.aspx\",\"Contents/ContentView.aspx?";
                if (dataItem.IsEshop)
                {
                    link2.NavigateUrl = str2 + "IsEschop=true&GeneralID=" + dataItem.GeneralId.ToString() + "\")";
                }
                else
                {
                    link2.NavigateUrl = str2 + "GeneralID=" + dataItem.GeneralId.ToString() + "\")";
                }
                HyperLink link3 = (HyperLink) e.Row.FindControl("ContentModify");
                link3.Text = "修改";
                link3.NavigateUrl = string.Concat(new object[] { base.BasePath, SiteConfig.SiteOption.ManageDir, "/Contents/Content.aspx?Action=Modify&NodeID=", nodeId.ToString(), "&GeneralID=", dataItem.GeneralId, "&ModelID=", dataItem.ModelId.ToString(), "&LinkType=", dataItem.LinkType.ToString() });
                if (!this.m_Administrator)
                {
                    string checkStr = nodeId.ToString();
                    NodeInfo info3 = new NodeInfo();
                    if (this.m_NodeInfoDictionary.ContainsKey(dataItem.NodeId))
                    {
                        info3 = this.m_NodeInfoDictionary[dataItem.NodeId];
                    }
                    else
                    {
                        info3 = Nodes.GetCacheNodeById(nodeId);
                    }
                    if (info3.ParentId > 0)
                    {
                        checkStr = checkStr + "," + info3.ParentPath;
                    }
                    if (!StringHelper.FoundCharInArr(checkStr, this.m_arrNodeIdManage))
                    {
                        link3.Enabled = false;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_Administrator = PEContext.Current.Admin.IsSuperAdmin;
            if (!this.m_Administrator)
            {
                this.m_arrNodeIdManage = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, EasyOne.Enumerations.OperateCode.NodeContentManage);
            }
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "待审核内容";
            }
            this.EgvContent.DataSource = ContentManage.GetCommonModelInfoList(0, this.PageSize, this.NodeId, (ContentSortType) this.ListType, this.Status);
            this.EgvContent.DataBind();
            base.Subtitle = "共" + ContentManage.GetTotalOfCommonModelInfo(this.NodeId, (ContentSortType) this.ListType, this.Status).ToString() + "条";
            base.TitleUrl = base.BasePath + SiteConfig.SiteOption.ManageDir + "/Contents/ContentManage.aspx?Status=0";
        }

        [Personalizable(PersonalizationScope.User)]
        public int ListType
        {
            get
            {
                return this.m_ListType;
            }
            set
            {
                this.m_ListType = value;
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public int NodeId
        {
            get
            {
                return this.m_NodeId;
            }
            set
            {
                this.m_NodeId = value;
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public string OperateCode
        {
            get
            {
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
            }
        }

        [WebBrowsable, Personalizable(PersonalizationScope.User), WebDescription("显示项目数"), WebDisplayName("显示项目数")]
        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public int Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        public object WebBrowsableObject
        {
            get
            {
                return this;
            }
        }
    }
    public class ContentEditorPart : EditorPart
    {
        private DropDownList m_DrpListType;
        private DropDownList m_DrpNodeId;
        private DropDownList m_DrpStatus;

        public ContentEditorPart()
        {
            this.Title = "编辑查询内容条件";
        }

        public override bool ApplyChanges()
        {
            EasyOne.WebSite.Controls.WebPart.Content webBrowsableObject = (EasyOne.WebSite.Controls.WebPart.Content)base.WebPartToEdit.WebBrowsableObject;
            webBrowsableObject.NodeId = DataConverter.CLng(this.DrpNodeId.SelectedValue);
            webBrowsableObject.ListType = DataConverter.CLng(this.DrpListType.SelectedValue);
            webBrowsableObject.Status = DataConverter.CLng(this.DrpStatus.SelectedValue);
            return true;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.m_DrpNodeId = new DropDownList();
            this.m_DrpNodeId.AppendDataBoundItems = true;
            this.m_DrpNodeId.Items.Add(new ListItem("所有节点", "0"));
            IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
            this.m_DrpNodeId.DataTextField = "NodeName";
            this.m_DrpNodeId.DataValueField = "NodeId";
            this.m_DrpNodeId.DataSource = nodeNameForContainerItems;
            this.m_DrpNodeId.DataBind();
            this.Controls.Add(this.m_DrpNodeId);
            this.m_DrpListType = new DropDownList();
            this.m_DrpListType.Items.Add(new ListItem("按ID升序", "-2"));
            this.m_DrpListType.Items.Add(new ListItem("按ID降序", "0"));
            this.m_DrpListType.Items.Add(new ListItem("按推荐级别降序", "1"));
            this.m_DrpListType.Items.Add(new ListItem("按推荐级别升序", "2"));
            this.m_DrpListType.Items.Add(new ListItem("按优先级别降序", "3"));
            this.m_DrpListType.Items.Add(new ListItem("按优先级别升序", "4"));
            this.m_DrpListType.Items.Add(new ListItem("按日点击数降序", "5"));
            this.m_DrpListType.Items.Add(new ListItem("按日点击数升序", "6"));
            this.m_DrpListType.Items.Add(new ListItem("按周点击数降序", "7"));
            this.m_DrpListType.Items.Add(new ListItem("按周点击数升序", "8"));
            this.m_DrpListType.Items.Add(new ListItem("按月点击数降序", "9"));
            this.m_DrpListType.Items.Add(new ListItem("按月点击数升序", "10"));
            this.m_DrpListType.Items.Add(new ListItem("按总点击数降序", "11"));
            this.m_DrpListType.Items.Add(new ListItem("按总点击数升序", "12"));
            this.Controls.Add(this.m_DrpListType);
            this.m_DrpStatus = new DropDownList();
            this.m_DrpStatus.AppendDataBoundItems = true;
            this.m_DrpStatus.DataTextField = "StatusName";
            this.m_DrpStatus.DataValueField = "StatusCode";
            this.m_DrpStatus.Items.Add(new ListItem("所有内容", "100"));
            this.m_DrpStatus.Items.Add(new ListItem("所有审核中内容", "101"));
            IList<StatusInfo> statusList = Status.GetStatusList();
            this.m_DrpStatus.DataSource = statusList;
            this.m_DrpStatus.DataBind();
            this.Controls.Add(this.m_DrpStatus);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("选择一个节点，来显示该节点的内容");
            writer.WriteBreak();
            this.m_DrpNodeId.RenderControl(writer);
            writer.WriteBreak();
            writer.WriteBreak();
            writer.Write("选择查询排序方式");
            writer.WriteBreak();
            this.m_DrpListType.RenderControl(writer);
            writer.WriteBreak();
            writer.WriteBreak();
            writer.Write("选择内容审核状态");
            this.m_DrpStatus.RenderControl(writer);
            writer.WriteBreak();
        }

        public void SetListControlsSelect(ListControl control, string value)
        {
            foreach (ListItem item in control.Items)
            {
                if (item.Value == value)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        public override void SyncChanges()
        {
            EasyOne.WebSite.Controls.WebPart.Content webBrowsableObject = (EasyOne.WebSite.Controls.WebPart.Content)base.WebPartToEdit.WebBrowsableObject;
            this.SetListControlsSelect(this.DrpNodeId, webBrowsableObject.NodeId.ToString());
            this.SetListControlsSelect(this.DrpListType, webBrowsableObject.ListType.ToString());
            this.SetListControlsSelect(this.DrpStatus, webBrowsableObject.Status.ToString());
        }

        private DropDownList DrpListType
        {
            get
            {
                this.EnsureChildControls();
                return this.m_DrpListType;
            }
        }

        private DropDownList DrpNodeId
        {
            get
            {
                this.EnsureChildControls();
                return this.m_DrpNodeId;
            }
        }

        private DropDownList DrpStatus
        {
            get
            {
                this.EnsureChildControls();
                return this.m_DrpStatus;
            }
        }
    }
}

