<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Title="内容管理" Inherits="EasyOne.WebSite.Admin.Contents.ContentMessage" Codebehind="ContentMessage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvContent" runat="server" AllowPaging="True" 
        AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" 
        CheckBoxFieldHeaderWidth="3%" DataKeyNames="GeneralId" 
        DataSourceID="OdsContents" IsHoldState="True" SerialText="" 
        onrowdatabound="EgvContent_RowDataBound">
        <Columns>
            <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                <HeaderStyle Width="12%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="Title">
                <ItemTemplate>
                    <asp:Label ID="LTitle" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnIDList" runat="server" />
    <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetCommonModelListByGeneralID" TypeName="EasyOne.Contents.ContentManage">
        <SelectParameters>
            <asp:ControlParameter ControlID="HdnIDList" Name="itemIDList" 
                PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">选中本页显示的所有项目</label>&nbsp;&nbsp;
    <asp:Button ID="EBtnPass" runat="server" Text="确定发送" onclick="EBtnPass_Click" />
    <asp:Button ID="EBtnCancelPass" runat="server" Text="取消发送" 
        onclick="EBtnCancelPass_Click" />
</asp:Content>
