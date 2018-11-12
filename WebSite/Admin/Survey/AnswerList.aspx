<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.AnswerList" Title="Untitled Page" Codebehind="AnswerList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
            </td>
            <td style="width: 40%; text-align: right">
                <asp:Label ID="LblTitle" runat="server"></asp:Label>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Repeater ID="RptAnswerList" runat="server" OnItemDataBound="RptAnswerList_ItemDataBound">
        <HeaderTemplate>
            <table width="100%" border="0" style="text-align: center" cellpadding="2" cellspacing="1"
                class="border">
                <tr class="title" align="center" style="height: 25px;">
                    <td style="width: 20%">
                        <b>IP</b></td>
                    <td>
                        <b>»Ø´ð</b></td>
                    <%= (IsThree ? "<td><b>ÆäËü</b></td>":"") %>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <pe:ExtendedLiteral HtmlEncode="false" ID="LtrAnswerList" runat="server"></pe:ExtendedLiteral>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
    <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged" HorizontalAlign="Center"
        Width="100%">
    </pe:AspNetPager>
</asp:Content>
