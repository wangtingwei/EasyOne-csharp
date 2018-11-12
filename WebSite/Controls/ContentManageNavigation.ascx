<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ContentManageNavigation" Codebehind="ContentManageNavigation.ascx.cs" %>
<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
    <tr>
        <td style="height: 22px; width: 80px;" align="left" class="tdbg">
            <b>管理导航：</b>
        </td>
        <td class="tdbg">
            <pe:ExtendedNodeLinkButton ID="ELnkContentList" Text="内容列表" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentManage" OnClick="ELnkContentList_Click" runat="server"></pe:ExtendedNodeLinkButton>
            |
            <asp:Label ID="LblAddContent" Style="width: 250px; border: 1px solid #a7a6aa; padding: 4px;
                padding-right: 25px; background: url(../../Admin/images/sitelist.gif) right center no-repeat;"
                runat="server">添加内容</asp:Label>
            <asp:HyperLink runat="server" Visible="false" ID="HlkModel"></asp:HyperLink><asp:Literal
                ID="LitSeparator" runat="server"> | </asp:Literal>
            <pe:ExtendedNodeLinkButton ID="ELnkCheckContent" Text="审核内容" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentCheck" OnClick="ELnkCheckContent_Click" runat="server" />
            |
            <pe:ExtendedNodeLinkButton ID="ELnkContentRecycle" Text="回收站管理" IsChecked="false"
                NodeId='<%# RequestInt32("NodeID")%>' OperateCode="NodeContentManage" OnClick="ELnkContentRecycle_Click"
                runat="server" />
            |
            <pe:ExtendedLinkButton ID="ELnkHtmlManage" Text="生成HTML管理" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentManage" OnClick="ELnkHtmlManage_Click" runat="server"></pe:ExtendedLinkButton>
            |
            <pe:ExtendedLinkButton ID="ELnkSiginManage" Text="签收管理" IsChecked="false" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentManage" OnClick="ELnkSiginManage_Click" runat="server"></pe:ExtendedLinkButton>
            | 
            <pe:ExtendedLinkButton ID="ELnkArchiving" Text="归档内容" IsChecked="false" OperateCode="NodeContentManage"
                OnClick="ELnkArchiving_Click" runat="server"></pe:ExtendedLinkButton>
            | 
            <pe:ExtendedLinkButton ID="ELnkReplace" Text="批量替换" IsChecked="false" OperateCode="NodeContentManage"
                OnClick="ELnkReplaceManage_Click" runat="server" Visible = "false" ></pe:ExtendedLinkButton>
            <ajaxToolkit:DropDownExtender runat="server" ID="Adrp" TargetControlID="LblAddContent"
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
            <a href='<%# Convert.ToBoolean(Eval("IsEshop")) ? "../Shop/" + Convert.ToString(Eval("AddInfoFilePath")) : "../Contents/" + Convert.ToString(Eval("AddInfoFilePath"))%>?Action=add&modelId=<%#Eval("ModelId")%>&NodeID=<%#RequestInt32("NodeID")%>'
                class='contextMenuItem'>
                <%#Eval("ModelName")%>
            </a>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
