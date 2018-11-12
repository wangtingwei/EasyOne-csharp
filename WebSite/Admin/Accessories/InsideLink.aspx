<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.InsideLink"
    MasterPageFile="~/Admin/MasterPage.master" Title="站内链接管理" Codebehind="InsideLink.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="添加站内链接" AlternateText="修改站内链接" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>链接目标：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtSourceWord" runat="server" Width="200px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSourceWord" ControlToValidate="TxtSourceWord"
                    runat="server" ErrorMessage="链接目标不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>链接Title：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTitle" runat="server" Width="200px"></asp:TextBox>
                <span style="color: blue">链接地址的Title属性，有利于SEO</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>链接地址：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtTargetWord" runat="server" Width="200px">http://</asp:TextBox>
                <span style="color: blue">请使用绝对地址</span>
                <pe:RequiredFieldValidator ID="ValrTargetWord" ControlToValidate="TxtTargetWord"
                    runat="server" ErrorMessage="链接地址不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>优先级别：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtPriority" runat="server" Width="64px"></asp:TextBox>
                <span style="color: blue">数字越大权重越高越被优先替换</span>
                <pe:RequiredFieldValidator ID="ValrPriority" ControlToValidate="TxtPriority" runat="server"
                    ErrorMessage="优先级别不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>替换次数：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtReplaceTimes" runat="server" Width="64px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrReplaceTimes" ControlToValidate="TxtReplaceTimes"
                    runat="server" ErrorMessage="替换次数不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:NumberValidator ID="ValrNumberValidator" ControlToValidate="TxtReplaceTimes"
                    runat="server" Display="Dynamic" ErrorMessage="只能输入数字！"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
                <strong>打开方式：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadlOpenType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">原窗口</asp:ListItem>
                    <asp:ListItem Value="False">新窗口</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 30%">
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
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('InsideLinkManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSource" runat="server" />
</asp:Content>
