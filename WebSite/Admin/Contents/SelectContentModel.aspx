<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SelectContentModel" Codebehind="SelectContentModel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr align="center" class="spacingtitle">
            <td colspan="2">
                <strong>请选择模型</strong></td>
        </tr>
        <asp:Repeater ID="RptModelList" runat="server">
            <ItemTemplate>
                <tr align="center">
                    <td class="tdbg" valign="top">
                        <a href='<%#Eval("AddInfoFilePath")%>?Action=add&modelId=<%#Eval("ModelId")%>&NodeID=<%#RequestInt32("NodeID")%>'
                            class="contextMenuItem">
                            <%#Eval("ModelName")%>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
