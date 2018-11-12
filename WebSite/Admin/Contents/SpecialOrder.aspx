<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Contents.SpecialOrder"
    Title="专题排序" Codebehind="SpecialOrder.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
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
      document.getElementById("<%=HdnList.ClientID %>").value=  junkdrawer.serializeList(document.getElementById('phoneticlong'));
    
    }
	//-->
    </script>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="OdsSpecial">
        <HeaderTemplate>
            <div class="ol_wrapper">
                <dl>
                    <dt>专题名称 [注意：鼠标放到栏目名上点击可拖动排序]</dt>
                    <dd id="phoneticlong">
                        <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li itemid="<%#Eval("SpecialID") %>"><a  href="#" title="点击拖动">
                <asp:Label ID="SpecialName" runat="server" Text='<%#Eval("SpecialName") %>'></asp:Label></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul> </dd> </dl></div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="HdnList" runat="server" />
   
      <br />
    <div style="text-align: center;">
        <asp:Button ID="EBtnSetOrderId" Text="保存排序" OnClick="EBtnSetOrderId_Click" OnClientClick="OrderList()" runat="server" />
        &nbsp;&nbsp;<asp:Button ID="BtnBack" runat="server" Text="恢复设置" OnClick="BtnBack_Click" />
        </div>
    <asp:ObjectDataSource ID="OdsSpecial" runat="server" SelectMethod="GetSpecialList"
        TypeName="EasyOne.Contents.Special" EnablePaging="False">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="specialCategoryId" QueryStringField="SpecialCategoryId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
