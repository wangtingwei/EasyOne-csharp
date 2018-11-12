<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.SpecialUnite" Title="专题合并" Codebehind="SpecialUnite.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                专题合并
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%">
                <br />
                &nbsp;&nbsp;将专题
                <asp:DropDownList ID="DropFromSpecial" DataValueField="SpecialId" DataTextField="SpecialName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                &nbsp;&nbsp;合并到&nbsp;&nbsp;
                <asp:DropDownList ID="DropToSpecial" DataValueField="SpecialId" DataTextField="SpecialName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%; height: 72px;" align="center">
                <asp:Button ID="EBtnUnite" Text="合并专题" OnClick="EBtnUnite_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%">
                <strong>注意事项：</strong> &nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;&nbsp; 所有操作不可逆，请慎重操作！ &nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;&nbsp; 不能在同一个专题内进行操作。 &nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;&nbsp; 合并后您所指定的专题将被删除，所有内容将转移到目标专题中。
            </td>
        </tr>
    </table>
</asp:Content>
