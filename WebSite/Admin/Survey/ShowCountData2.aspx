<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.ShowCountData2" Title="Untitled Page" Codebehind="ShowCountData2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <asp:Repeater ID="RptShowCountList" runat="server" OnItemDataBound="RptShowCountList_ItemDataBound">
            <ItemTemplate>
                <asp:Repeater ID="RptShowCountData" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" style="text-align: center" cellpadding="2" cellspacing="1"
                            class="border">
                            <tr class="title">
                                <td style="text-align: center" colspan="2" align="center">
                                    <strong>卡片式查看</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%" align="right" class="tdbgleft">
                                    IP：
                                </td>
                                <td align="left">
                                    <asp:Label ID="LblIP" runat="server" Text="IP"></asp:Label></td>
                            </tr>
                            <tr class="tdbg">
                                <td style="width: 30%" align="right" class="tdbgleft">
                                    提交时间：
                                </td>
                                <td align="left">
                                    <asp:Label ID="LblSubmitTime" runat="server" Text="LblSubmitTime"></asp:Label></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tdbg">
                            <td style="width: 30%" align="right" class="tdbgleft">
                                <%#Eval("QuestionContent")%>
                                ：
                            </td>
                            <td align="left">
                                <asp:Label ID="LblAnswer" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
        <span id="showPage" runat="server">
            <pe:AspNetPager ID="Pager" ShowPageSize="false" runat="server" OnPageChanged="Pager_PageChanged">
            </pe:AspNetPager>
        </span>
        <br />
        <span id="showButton" visible="false" runat="server">
            <asp:Button ID="BtnReturn" runat="server" Text="返回列表" OnClick="BtnReturn_Click" />&nbsp;&nbsp
            <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnDelete" runat="server"
                Text="删除记录" OnClick="BtnDelete_Click" />
        </span>
    </div>
</asp:Content>
