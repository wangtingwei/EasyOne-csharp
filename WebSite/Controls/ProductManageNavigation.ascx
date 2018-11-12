<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ProductManageNavigation" Codebehind="ProductManageNavigation.ascx.cs" %>

<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
    <tr>
        <td style="height: 22px; width: 80px;" align="left" class="tdbg">
            <b>管理导航：</b>
        </td>
        <td class="tdbg">
            <pe:ExtendedNodeLinkButton ID="ELnkContentList" Text="商品列表" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentManage" OnClick="ELnkProductList_Click" runat="server"></pe:ExtendedNodeLinkButton>
           
             <asp:Literal ID="LitSeparator" runat="server">|</asp:Literal>
              <asp:Label ID="LblAddProduct" Style="width: 250px; border: 1px solid #a7a6aa; padding: 4px;
                padding-right: 25px; background: url(../../Admin/images/sitelist.gif) right center no-repeat;"
                runat="server">添加商品</asp:Label>
            <asp:HyperLink runat="server" Visible="false" ID="HlkModel"></asp:HyperLink>
            |
            <pe:ExtendedNodeLinkButton ID="ELnkProductStockManage" Text="库存管理" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="ProductManage" OnClick="ELnkProductStockManage_Click" runat="server"></pe:ExtendedNodeLinkButton>
            |
            <pe:ExtendedNodeLinkButton ID="ELnkProductRecycle" Text="回收站管理" IsChecked="false"
                NodeId='<%# RequestInt32("NodeID")%>' OperateCode="CurrentNodesManage" OnClick="ELnkProductRecycle_Click"
                runat="server" />
            |
            <pe:ExtendedLinkButton ID="ELnkProductBatchModify" Text="批量编缉" IsChecked="false" OperateCode="ProductBatchModify"
                OnClick="ELnkProductBatchModify_Click" runat="server"></pe:ExtendedLinkButton>
            |
             <pe:ExtendedLinkButton ID="ELnkProductImport" Text="批量导入" IsChecked="false" OperateCode="ProductAdd"
                OnClick="ELnkProductImport_Click" runat="server"></pe:ExtendedLinkButton>    
            |
            <pe:ExtendedLinkButton ID="ELnkProductHtml" runat="server" Text=" 生成HTML管理" IsChecked="true"
                    OperateCode="ProductManage" OnClick="ELnkProductHtml_Click" />
            <ajaxToolkit:DropDownExtender runat="server" ID="Adrp" TargetControlID="LblAddProduct"
                DropDownControlID="PnlSelectModelMenu" DropArrowImageUrl="../Admin/images/sitelist.gif">
            </ajaxToolkit:DropDownExtender>
        </td>
    </tr>
</table>
<style type="text/css">
.contextMenuPanel 
{
	border: 1px solid #868686;
	z-index: 1000;
	background: url('../../Admin/Images/menu-bg.gif') repeat-y 0 0 #FAFAFA;
	cursor: default;
	padding: 1px 1px 0px 1px;
	font-size: 11px;
}
a.contextMenuItem
{
	margin: 1px 0 1px 0;
	display: block;
	color: #003399;
	text-decoration: none;
	cursor: pointer;	
	padding: 4px 19px 4px 33px;
}
a.contextMenuItem:hover
{
	background-color: #FFE6A0;
	color: #003399;
	border: 1px solid #D2B47A;
	padding: 3px 18px 3px 32px;
}
</style>
<asp:Panel ID="PnlSelectModelMenu" runat="server" CssClass="contextMenuPanel" Style="display: none;">
    <asp:Repeater ID="RptModelList" runat="server">
        <ItemTemplate>
            <a href='<%# Convert.ToBoolean(Eval("IsEshop")) ? "../Shop/" + Convert.ToString(Eval("AddInfoFilePath")) : "../Shop/" + Convert.ToString(Eval("AddInfoFilePath"))%>?Action=add&modelId=<%#Eval("ModelId")%>&NodeID=<%#RequestInt32("NodeID")%>'
                class='contextMenuItem'>
                <%#Eval("ModelName")%>
            </a>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
