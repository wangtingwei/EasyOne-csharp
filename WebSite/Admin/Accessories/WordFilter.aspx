<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.WordFilter"
    MasterPageFile="~/Admin/MasterPage.master" Title="字符过滤管理" ValidateRequest="false" Codebehind="WordFilter.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <pe:AlternateLiteral ID="AltrTitle" Text="添加字符过滤信息" AlternateText="修改字符过滤信息" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>替换目标：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtSourceWord" runat="server" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSourceWord" ControlToValidate="TxtSourceWord"
                    runat="server" ErrorMessage="替换目标不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>替换结果：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTargetWord" runat="server" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrTargetWord" ControlToValidate="TxtTargetWord"
                    runat="server" ErrorMessage="替换结果不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>优先级别：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Columns="5" MaxLength ="5"></asp:TextBox>
                <span style="color: blue">数字越大权重越高越被优先替换</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="优先级别不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtPriority" runat="server"
                    Display="Dynamic" ErrorMessage="只能输入数字！"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>是否启用：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadioIsEnabled" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">启用</asp:ListItem>
                    <asp:ListItem Value="False">禁用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('WordFilterManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSource" runat="server" />
</asp:Content>
