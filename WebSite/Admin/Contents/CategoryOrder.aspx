<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryOrder" ValidateRequest="false" Codebehind="CategoryOrder.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/core.js"></script>
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/events.js"></script>
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/css.js"></script>
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/coordinates.js"></script>
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/drag.js"></script>
    <script language="JavaScript" type="text/javascript" src="<%=BasePath %>Admin/JS/tool-man/dragsort.js"></script>
    <script language="JavaScript" type="text/javascript"><!--
	var dragsort = ToolMan.dragsort()
	var junkdrawer = ToolMan.junkdrawer()

	window.onload = function() {
		
		junkdrawer.restoreListOrder("phoneticlong")
		
		dragsort.makeListSortable(document.getElementById("phoneticlong"),verticalOnly)
	}

	function verticalOnly(item) {
		item.toolManDragGroup.verticalOnly()
	}
    function OrderList()
    {
      document.getElementById("<%=HdnSerNodeList.ClientID %>").value=  junkdrawer.serializeList(document.getElementById('phoneticlong'));
    
    }
	//-->
    </script>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="OdsCategoryOrder">
        <HeaderTemplate>
            <div class="ol_wrapper">
                <dl>
                    <dt>节点名 [注意：鼠标放到栏目名上点击可拖动排序]</dt>
                    <dd id="phoneticlong">
                        <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li itemid="<%#Eval("NodeId") %>"><a  href="#" title="点击拖动">
                <asp:Label ID="LabNodeName" runat="server" Text='<%#Eval("NodeName") %>'></asp:Label></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul> </dd> </dl></div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="HdnSerNodeList" runat="server" />
    
    <br />
    <div style="text-align: center;">
        <asp:Button ID="EBtnSetOrderId" Text="保存排序" OnClick="EBtnSetOrderId_Click" OnClientClick="OrderList();BOX_show('RegUser');"
            runat="server" />
        <asp:Button ID="BtnBack" runat="server" Text="恢复设置" OnClick="BtnBack_Click" />
    </div>
    <asp:ObjectDataSource ID="OdsCategoryOrder" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
        SelectMethod="GetNodesListByParentId" TypeName="EasyOne.Contents.Nodes">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="parentId" QueryStringField="NodeId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="BOX_overlay" style="display:none;">
        <div id="RegUser">
            <div>
                <label><font color="#FF0000">数据正在更新中……</font></label><br />
                <img alt="" src="<%=BasePath %>admin/Images/progressbar.gif" />
            </div>
        </div>
    </div>
<script src="<%=BasePath %>admin/JS/ModalPopup.js" type="text/javascript"></script>
</asp:Content>
