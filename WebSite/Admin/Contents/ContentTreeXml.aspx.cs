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
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class ContentTreeXml : AdminPage
    {
        private bool m_IsCurrentNodesManage = true;
        private bool m_IsNodeInput = true;
        private bool m_IsNodeManage = true;
        private bool m_IsNodeShow = true;
        private bool m_IsShow = true;

        private void AddXTreeItem(XTreeCollection xTreeList, NodeInfo nodeInfo)
        {
            this.CheckPermissions(nodeInfo);
            if ((this.m_IsShow || this.m_IsNodeShow) || (this.m_IsNodeInput || this.m_IsNodeManage))
            {
                XTreeItem item = new XTreeItem();
                item.Text = nodeInfo.NodeName;
                bool flag = false;
                StringBuilder sb = new StringBuilder();
                string str = "";
                string str2 = "";
                string str3 = string.Concat(new object[] { "ContentManage.aspx?NodeID=", nodeInfo.NodeId, "&NodeName=", HttpContext.Current.Server.UrlEncode(DataSecurity.XmlEncode(nodeInfo.NodeName)) });
                foreach (DataRow row in ModelManager.GetContentModelListByNodeId(nodeInfo.NodeId, true).Rows)
                {
                    if (this.m_IsNodeInput || this.m_IsNodeManage)
                    {
                        StringHelper.AppendString(sb, row["ModelId"].ToString());
                        if (string.IsNullOrEmpty(str))
                        {
                            str = "添加" + row["ItemName"].ToString() + "||" + row["IsEshop"].ToString() + "||" + row["AddInfoFilePath"].ToString();
                            str = str + "||管理" + row["ItemName"].ToString() + "||" + row["IsEshop"].ToString() + "||" + row["ManageInfoFilePath"].ToString();
                        }
                        else
                        {
                            str = str + "$$$添加" + row["ItemName"].ToString() + "||" + row["IsEshop"].ToString() + "||" + row["AddInfoFilePath"].ToString();
                            str = str + "||管理" + row["ItemName"].ToString() + "||" + row["IsEshop"].ToString() + "||" + row["ManageInfoFilePath"].ToString();
                        }
                        if (this.m_IsCurrentNodesManage)
                        {
                            str2 = "1";
                        }
                    }
                    flag = true;
                }
                item.ArrModelId = sb.ToString();
                item.ArrModelName = str;
                item.ArrPurview = str2;
                item.Title = "小贴士：您可以在节点名称上点击鼠标右键，从弹出菜单中选择相关操作。";
                item.Action = str3;
                item.NodeId = nodeInfo.NodeId.ToString();
                item.Target = "main_right";
                item.Expand = "0";
                if (nodeInfo.Child > 0)
                {
                    item.XmlSrc = "ContentTreeXml.aspx?NodeID=" + nodeInfo.NodeId;
                }
                string str4 = "";
                switch (nodeInfo.PurviewType)
                {
                    case 0:
                        str4 = "Container";
                        break;

                    case 1:
                        str4 = "HalfOpen";
                        break;

                    case 2:
                        str4 = "Purview";
                        break;

                    default:
                        str4 = "Container";
                        break;
                }
                if (this.m_IsCurrentNodesManage)
                {
                    item.ArrPurview = "AllowSetNode";
                }
                else
                {
                    item.ArrPurview = "NoAllowSetNode";
                }
                if (!flag)
                {
                    str4 = "Forbid";
                    if (this.Administrator || this.m_IsCurrentNodesManage)
                    {
                        item.AnchorType = "3";
                        item.Title = "该节点没有绑定内容模型，请在右键弹出菜单中选择[设置节点]绑定内容模型";
                    }
                    else
                    {
                        item.AnchorType = "0";
                        item.Title = "您没有该节点的管理权限。";
                    }
                }
                else if ((this.m_IsNodeShow || this.m_IsNodeInput) || this.m_IsNodeManage)
                {
                    item.AnchorType = "2";
                }
                else
                {
                    str4 = "Forbid";
                    if (this.m_IsCurrentNodesManage)
                    {
                        item.AnchorType = "3";
                        item.Title = "该节点没有绑定内容模型，请在右键弹出菜单中选择[设置节点]绑定内容模型";
                    }
                    else
                    {
                        item.AnchorType = "0";
                        item.Title = "您没有该节点的管理权限。";
                    }
                }
                item.Icon = str4;
                xTreeList.Add(item);
            }
        }

        private void CheckPermissions(NodeInfo nodeInfo)
        {
            string checkStr = "";
            string roleNodeId = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if (!this.Administrator)
            {
                this.m_IsNodeShow = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentPreview, nodeInfo.NodeId);
                this.m_IsNodeInput = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, nodeInfo.NodeId);
                this.m_IsNodeManage = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, nodeInfo.NodeId);
                this.m_IsCurrentNodesManage = RolePermissions.AccessCheckNodePermission(OperateCode.CurrentNodesManage, nodeInfo.NodeId);
                checkStr = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentPreview);
                roleNodeId = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentInput);
                str3 = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentCheck);
                str4 = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.NodeContentManage);
                str5 = RolePermissions.GetRoleNodeId(PEContext.Current.Admin.Roles, OperateCode.CurrentNodesManage);
                string findStr = nodeInfo.NodeId.ToString();
                if ((nodeInfo.ArrChildId.IndexOf(",", StringComparison.Ordinal) > 0) && !this.m_IsNodeShow)
                {
                    if (!string.IsNullOrEmpty(nodeInfo.ArrChildId))
                    {
                        findStr = nodeInfo.ArrChildId;
                    }
                    if ((StringHelper.FoundCharInArr(checkStr, findStr) || StringHelper.FoundCharInArr(roleNodeId, findStr)) || (StringHelper.FoundCharInArr(str3, findStr) || StringHelper.FoundCharInArr(str4, findStr)))
                    {
                        this.m_IsShow = true;
                    }
                }
                if (nodeInfo.ParentId > 0)
                {
                    findStr = nodeInfo.ParentPath + "," + nodeInfo.NodeId.ToString();
                    if (!this.m_IsNodeShow)
                    {
                        this.m_IsNodeShow = StringHelper.FoundCharInArr(checkStr, findStr);
                    }
                    if (!this.m_IsNodeInput)
                    {
                        this.m_IsNodeInput = StringHelper.FoundCharInArr(roleNodeId, findStr);
                    }
                    if (!this.m_IsNodeManage)
                    {
                        this.m_IsNodeManage = StringHelper.FoundCharInArr(str4, findStr);
                    }
                    if (!this.m_IsCurrentNodesManage)
                    {
                        this.m_IsCurrentNodesManage = StringHelper.FoundCharInArr(str5, findStr);
                    }
                }
            }
        }

        private void ContentXml(XTreeCollection xTreeList)
        {
            foreach (NodeInfo info in EasyOne.Contents.Nodes.GetNodesListByParentId(DataConverter.CLng(HttpContext.Current.Request.QueryString["NodeID"])))
            {
                if (info.NodeType == NodeType.Container)
                {
                    this.AddXTreeItem(xTreeList, info);
                    this.m_IsShow = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            this.ContentXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private bool Administrator
        {
            get
            {
                return PEContext.Current.Admin.IsSuperAdmin;
            }
        }
    }
}

