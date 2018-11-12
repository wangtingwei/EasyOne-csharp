<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownServerManage" Title="下载服务器管理" Codebehind="DownServerManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有下载服务器" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvDownServer" runat="server" AutoGenerateColumns="false"
        AutoGenerateCheckBoxColumn="True" DataSourceID="OdsDownServer" DataKeyNames="ServerID"
        RowDblclickBoundField="ServerID" RowDblclickUrl="DownServer.aspx?Action=Modify&ServerID={$Field}">
        <Columns>
            <pe:BoundField DataField="ServerID" HeaderText="ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="ServerName" HeaderText="服务器名" SortExpression="ServerName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="服务器LOGO" SortExpression="ServerLogo">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("ServerLogo").ToString() == "" ? "" : "<img src="+ Eval("ServerLogo") + " style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="显示方式" SortExpression="ShowType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# Eval("ShowType").ToString() == "0" ? "显示名称" : "显示LOGO"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="ServerUrl" HeaderText="服务器地址" SortExpression="ServerUrl">
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="17%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahDownServerModify" IsChecked="true" OperateCode="DownServerManage"
                        href='<%# "DownServer.aspx?Action=Modify&ServerID=" + Eval("ServerID")%>' runat="server">修改</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahDownServerDelete" IsChecked="true" OperateCode="DownServerManage"
                        href='<%# AppendSecurityCode("DownServerManage.aspx?Action=Delete&ServerID=" + Eval("ServerID"))%>'
                        onclick="return confirm('确定要删除此服务器信息吗？');" runat="server">删除</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页所有下载服务器
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropShowType" runat="server">
        <asp:ListItem Selected="True" Value="0">显示名称</asp:ListItem>
        <asp:ListItem Value="1">显示LOGO</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="EBtnShowType" Text="设置选中的服务器的显示方式"  OnClick="EBtnShowType_Click" runat="server" />&nbsp;&nbsp;
    <div style="text-align: left">
        <br />
        <br />
        <span style="color: #ff0000"><strong>注意：</strong>删除某个镜像服务器信息时，与之相关的下载错误信息也将一起被删除掉。<br />
        </span>
        <br />
        <br />
        <span style="color: blue"><strong>温馨提示：</strong>要想改变前台下载地址显示方式请分两步操作：首先在这里设置下载服务器的显示方式，然后找到你想修改显示方式的软件重新选择下载服务器即可。前台镜像地址显示的个数和样式完全可以在每个软件里设置，如果你不想为每个软件都设置显示方式请使用默认值。<br />
        </span>
    </div>
    <asp:ObjectDataSource ID="OdsDownServer" runat="server" DataObjectTypeName="EasyOne.Model.Accessories.DownServerInfo"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDownServerList"
        TypeName="EasyOne.Accessories.DownServer" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="SubModuleID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
