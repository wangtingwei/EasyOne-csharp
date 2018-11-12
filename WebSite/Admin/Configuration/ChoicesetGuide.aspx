<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.ChoicesetGuide"
    Title="数据字典管理向导" Codebehind="ChoicesetGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    数据字典管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <asp:Repeater ID="RptMenu" runat="server" OnItemDataBound="RptMenu_ItemDataBound">
        <ItemTemplate>
            <div class="guidecollapse" onclick="Switch(this)">
                <asp:Label ID="LblTableName" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="guide" style="display: none">
                <ul>
                    <asp:Repeater ID="RptChoicesetTitle" runat="server" OnItemDataBound="RptChoicesetTitle_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="LnkTitle" runat="server"></asp:HyperLink>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
