namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Web.UI;
    using EasyOne.ModelControls;
    using System.Collections.ObjectModel;

    public partial class NodeType : BaseFieldControl
    {
        protected string GetUploadPath = "";
        private int m_NodeInfoType = 1;
        protected string path = "";

        private void InitInfoType()
        {
            int num = DataConverter.CLng(HttpContext.Current.Request["ModelID"]);
            StringBuilder builder = new StringBuilder();
            switch (this.m_NodeInfoType)
            {
                case 1:
                {
                    int generalId = DataConverter.CLng(HttpContext.Current.Request["GeneralId"]);
                    if (generalId > 0)
                    {
                        IList<CommonModelInfo> infoList = ContentManage.GetInfoList(generalId);
                        StringBuilder sb = new StringBuilder();
                        foreach (CommonModelInfo info in infoList)
                        {
                            builder.Append("<span id='NodeSpanId" + info.NodeId + "'>");
                            builder.Append(EasyOne.Contents.Nodes.ShowNodesAndRootNavigation(info.NodeId));
                            builder.Append(" <input type='button' onclick=\"javascript:Del('" + info.NodeId + "')\" value='删除此节点' /><br /></span>");
                            StringHelper.AppendString(sb, info.NodeId.ToString());
                        }
                        this.SelectedNodes.InnerHtml = builder.ToString();
                        this.HdnInfoIds.Value = sb.ToString();
                    }
                    break;
                }
                case 2:
                {
                    int id = DataConverter.CLng(HttpContext.Current.Request["ItemId"]);
                    if (id > 0)
                    {
                        string infoNodeId = CollectionItem.GetInfoById(id).InfoNodeId;
                        foreach (string str2 in infoNodeId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            builder.Append("<span id='NodeSpanId" + str2 + "'>");
                            builder.Append(EasyOne.Contents.Nodes.ShowNodesAndRootNavigation(DataConverter.CLng(str2)));
                            builder.Append(" <input type='button' onclick=\"javascript:Del('" + str2 + "')\" value='删除此节点' /><br /></span>");
                        }
                        this.SelectedNodes.InnerHtml = builder.ToString();
                        this.HdnInfoIds.Value = infoNodeId;
                    }
                    break;
                }
            }
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("<script type=\"text/javascript\">\n");
            builder3.Append("<!--\n");
            builder3.Append("  function AddInfo(){\n");
            builder3.Append(string.Concat(new object[] { "      var strUrl = \"", this.path, "/Contents/NodesList.aspx?ModelId=", num, "&ClientID=", this.HdnInfoIds.ClientID, "\";\n" }));
            builder3.Append("           window.open(strUrl+'&Action=AddInfo','newWin','modal=yes,width=250,height=400,resizable=yes,scrollbars=yes'); \n");
            builder3.Append("  }\n");
            builder3.Append("  function UpdateTheInfoNodes(arr){\n");
            builder3.Append("       var arrNodes=arr.split('$$$');\n");
            builder3.Append("       var hdnNodeId= document.getElementById('" + this.HdnInfoIds.ClientID + "');\n");
            builder3.Append("       var SelectedNodeId = hdnNodeId.value.split(\",\");\n");
            builder3.Append("       var isExist = false;\n");
            builder3.Append("       for(i=0;i<SelectedNodeId.length;i++) {if(SelectedNodeId[i] == arrNodes[0]){isExist = true;}}\n");
            builder3.Append("       if(!isExist){ if(hdnNodeId.value != '') {hdnNodeId.value = hdnNodeId.value + ',';} \n");
            builder3.Append("       hdnNodeId.value = hdnNodeId.value + arrNodes[0];  \n");
            builder3.Append("       var newli = document.createElement(\"SPAN\");  \n");
            builder3.Append("       newli.setAttribute(\"id\",\"NodeSpanId\"+arrNodes[0]);\n");
            builder3.Append("       newli.innerHTML =arrNodes[1] + \" \";\n");
            builder3.Append("       var newlink = document.createElement(\"INPUT\");");
            builder3.Append("       newlink.onclick = function() { Del(arrNodes[0]);};\n");
            builder3.Append("       newlink.setAttribute(\"type\", \"button\");\n");
            builder3.Append("       newlink.setAttribute(\"class\", \"button\");\n");
            builder3.Append("       newlink.setAttribute(\"value\", \"删除此节点\");\n");
            builder3.Append("       newli.appendChild(newlink);\n");
            builder3.Append("       var newbr = document.createElement(\"BR\");  \n");
            builder3.Append("       newli.appendChild(newbr);\n");
            builder3.Append("       var links = document.getElementById(\"" + this.SelectedNodes.ClientID + "\");\n");
            builder3.Append("       links.appendChild(newli);}\n");
            builder3.Append("  }\n");
            builder3.Append("  function Del(nodeId){\n");
            builder3.Append("  var li = document.getElementById(\"NodeSpanId\" + nodeId);\n");
            builder3.Append("  li.parentNode.removeChild(li);\n");
            builder3.Append("  var hdnNodeId = document.getElementById('" + this.HdnInfoIds.ClientID + "');\n");
            builder3.Append("  var SelectedNodeId = hdnNodeId.value.split(\",\");\n");
            builder3.Append("  var newselected = '';\n");
            builder3.Append("  for(i=0;i<SelectedNodeId.length;i++)\n");
            builder3.Append("  {\n");
            builder3.Append("    if(SelectedNodeId[i] != nodeId){ if(newselected != ''){newselected = newselected + ',';} newselected = newselected+SelectedNodeId[i]; }\n");
            builder3.Append("  }\n");
            builder3.Append("  hdnNodeId.value = newselected;\n");
            builder3.Append("  }\n");
            builder3.Append(" //-->\n");
            builder3.Append("</script>\n");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("AddInfo"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "AddInfo", builder3.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.path = base.BasePath;
            this.path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.path;
            if (base.IsAdminManage)
            {
                this.path = this.path + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                this.PhAddInfo.Visible = false;
                this.PhAddInfo2.Visible = false;
                this.path = this.path + "User";
            }
            this.GetUploadPath = this.path + "/Accessories/GetUploadPath.aspx?NodeId=";
            if (!base.IsPostBack)
            {
                this.InitInfoType();
                int modelId = DataConverter.CLng(HttpContext.Current.Request["ModelID"]);
                int nodeId = DataConverter.CLng(HttpContext.Current.Request["NodeID"]);
                this.HdnNodeId.Value = nodeId.ToString();
                if (nodeId > 0)
                {
                    this.LblNavigation.Text = EasyOne.Contents.Nodes.ShowNodesAndRootNavigation(nodeId);
                    this.PnlChange.Visible = true;
                    this.PnlSelect.Visible = false;
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<script type=\"text/javascript\">\n");
                    builder.Append("<!--\n");
                    builder.Append("  function ShowWindow(){\n");
                    builder.Append(string.Concat(new object[] { "      var strUrl = \"", this.path, "/Contents/NodesList.aspx?ModelId=", modelId, "&ClientID=", this.HdnNodeId.ClientID, "\";\n" }));
                    builder.Append("           window.open(strUrl+'&Action=SetNode','newWin','modal=yes,width=250,height=400,resizable=yes,scrollbars=yes'); \n");
                    builder.Append("  }\n");
                    builder.Append("  function SetNode(nodes){\n");
                    builder.Append("       var arrnodes=nodes.split('$$$');\n");
                    builder.Append("       document.getElementById('" + this.HdnNodeId.ClientID + "').value = arrnodes[0];  \n");
                    builder.Append("       document.getElementById('" + this.LblNavigation.ClientID + "').innerHTML = arrnodes[1];  \n");
                    builder.Append("       document.getElementById(\"UploadPath\").src = \"" + this.path + "/Accessories/GetUploadPath.aspx?NodeId=\" + arrnodes[0]; \n");
                    builder.Append("  }\n");
                    builder.Append(" //-->\n");
                    builder.Append("</script>\n");
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ShowWindow(strUrl)"))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ShowWindow(strUrl)", builder.ToString());
                    }
                    this.GetUploadPath = this.GetUploadPath + nodeId.ToString();
                }
                else
                {
                    this.PnlChange.Visible = false;
                    this.PnlSelect.Visible = true;
                    DataTable nodeNameByModelId = EasyOne.Contents.Nodes.GetNodeNameByModelId(modelId, EasyOne.Enumerations.NodeType.Container);
                    if (nodeNameByModelId.Rows.Count > 0)
                    {
                        this.DrpNodeList.DataSource = nodeNameByModelId;
                        this.HdnNodeId.Value = nodeNameByModelId.Rows[0]["NodeId"].ToString();
                    }
                    else
                    {
                        IList<NodeInfo> nodeNameForContainerItems = EasyOne.Contents.Nodes.GetNodeNameForContainerItems();
                        this.DrpNodeList.DataSource = nodeNameForContainerItems;
                        if (nodeNameForContainerItems.Count > 0)
                        {
                            this.HdnNodeId.Value = nodeNameForContainerItems[0].NodeId.ToString();
                        }
                    }
                    this.DrpNodeList.DataBind();
                    if (nodeId > 0)
                    {
                        this.DrpNodeList.SelectedValue = nodeId.ToString();
                        this.HdnNodeId.Value = nodeId.ToString();
                    }
                    this.DrpNodeList.Attributes.Add("onchange", "SetNodeId(this.options[this.options.selectedIndex].value)");
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("<script type=\"text/javascript\">\n");
                    builder2.Append("<!--\n");
                    builder2.Append("  function SetNodeId(value){\n");
                    builder2.Append("       document.getElementById('" + this.HdnNodeId.ClientID + "').value = value;  \n");
                    builder2.Append("       document.getElementById(\"UploadPath\").src = \"" + this.path + "/Accessories/GetUploadPath.aspx?NodeId=\" + value; \n");
                    builder2.Append("  }\n");
                    builder2.Append(" //-->\n");
                    builder2.Append("</script>\n");
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("SetNodeId(value)"))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "SetNodeId(value)", builder2.ToString());
                    }
                    if (base.EnableNull)
                    {
                        this.ReqDrpNodeList.Visible = true;
                    }
                    this.GetUploadPath = this.GetUploadPath + this.HdnNodeId.Value;
                }
            }
            else
            {
                this.FieldValue = this.HdnNodeId.Value;
                this.GetUploadPath = this.GetUploadPath + this.HdnNodeId.Value;
            }
        }

        public string InfoNodeId
        {
            get
            {
                return this.HdnInfoIds.Value;
            }
        }

        public int NodeInfoType
        {
            get
            {
                return this.m_NodeInfoType;
            }
            set
            {
                this.m_NodeInfoType = value;
            }
        }
    }
}

