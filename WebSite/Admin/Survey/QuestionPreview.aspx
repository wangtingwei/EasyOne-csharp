<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.QuestionPreview" Codebehind="QuestionPreview.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" class="border" cellpadding="2" cellspacing="0">
        <tr class="title">
            <td align="center" colspan="2">
                <asp:Label ID="LblTitle" Text="����Ԥ��" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2">
                <%=questionHTML%>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" style="width: 50%">
            
                <div runat="server" id="DivModify" style="float: right; text-align: right">
                    <asp:Button ID="BtnModify" runat="server" Text="�޸�" OnClick="BtnModify_Click" />
                </div>
            </td>
            <td>
                <div style="float: left; width: 50%; text-align: left">
                    &nbsp;&nbsp;<asp:Button ID="BtnReturn" runat="server" Text="����" OnClick="BtnReturn_Click" />
                    </div>
            </td>
        </tr>
    </table>
</asp:Content>
