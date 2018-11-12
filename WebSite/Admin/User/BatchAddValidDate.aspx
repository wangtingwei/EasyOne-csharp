<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.BatchAddValidDate" Codebehind="BatchAddValidDate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <asp:Label ID="LblMsg" runat="server" Text="批量添加用户有效期"></asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选择会员：</strong>
            </td>
            <td style="width: 586px">
                <pe:SelectUser ID="SelectUser" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>设置有效期：</strong>
            </td>
            <td align="left" style="width: 586px">
                <asp:RadioButton ID="RadValidType" runat="server" Text="指定期限：" GroupName="ValidType"
                    Checked="true" />
                &nbsp;<asp:TextBox ID="TxtValidNum" runat="server" Width="35px" MaxLength ="10"></asp:TextBox>
                <asp:DropDownList ID="DropValidUnit" runat="server">
                    <asp:ListItem Selected="True" Value="1">天</asp:ListItem>
                    <asp:ListItem Value="2">月</asp:ListItem>
                    <asp:ListItem Value="3">年</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="LabValidDescription" runat="server" Text="Label"></asp:Label>
                <asp:RadioButton ID="RadValidType2" GroupName="ValidType" runat="server" Text="改为无限期" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>请输入<asp:Label ID="Lbltype" runat="server" Text="奖励" />原因：</strong>
            </td>
            <td align="left" style="width: 586px">
                <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrReason" ControlToValidate="TxtReason" runat="server"
                    ErrorMessage="添加有效期原因不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft">
                <strong>内部记录：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                    Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
            </td>
            <td align="left" style="width: 586px">
                <asp:CheckBox ID="ChkSaveItem" runat="server" Checked="True" />
                <strong>为每个会员记录明细记录（推荐）</strong></td>
        </tr>
        <tr class="tdbg">
            <td style="height: 40px;" colspan="2" align="center">
                <asp:Button ID="EBtnSubmit" Text="执行批量操作" OnClick="EBtnSubmit_Click" runat="server" />
                <asp:Button ID="TxtAction" runat="server" Text="" Visible="false"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
