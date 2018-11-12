<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.ShowCountData3" Title="查看统计结果" Codebehind="ShowCountData3.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <asp:Repeater ID="DlstSurveyRecord" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="1" cellpadding="1" class="border">
                    <tr>
            </HeaderTemplate>
            <ItemTemplate>
                <td align="center">
                    <a href='ShowCountData2.aspx?SurveyID=<%=m_SurveyId%>&RecordID=<%#Eval("RecordId")%>'>
                        <%# Eval("IP")%>
                    </a>
                </td>
                <% 
                    i++; %>
                <% if (i % 6 == 0 && i > 1)
               {%>
                </tr><tr>
                    <%} %>
            </ItemTemplate>
            <FooterTemplate>
                </tr></table>
            </FooterTemplate>
        </asp:Repeater>
        <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
        </pe:AspNetPager>
    </div>
</asp:Content>
