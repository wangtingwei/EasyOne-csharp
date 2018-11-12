<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.KeyWord" Title="关键字管理" Codebehind="KeyWord.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="添加关键字" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>关键字名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtKeywordText" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtKeywordText"
                    runat="server" ErrorMessage="关键字名称不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>关键字类别：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadlKeywordType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">常规关键字</asp:ListItem>
                    <asp:ListItem Value="1">搜索关键字</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>关键字权重：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Columns="5"></asp:TextBox>
                <span style="color: blue">数字越大权重越高越被优先</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="关键字权重不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtPriority" runat="server"
                    Display="Dynamic" ErrorMessage="只能输入数字！"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('KeyWordManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnKeywordText" runat="server" />
</asp:Content>
