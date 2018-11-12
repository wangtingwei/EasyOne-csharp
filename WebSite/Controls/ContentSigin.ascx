<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ContentSigin" Codebehind="ContentSigin.ascx.cs" %>
<tr class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 150px;">
        <strong>文档类型：&nbsp;</strong>
    </td>
    <td class='tdbg' align='left'>
        <asp:Label ID="LblSigninType" runat="server"></asp:Label>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 150px;">
        <strong>优先级：&nbsp;</strong><br />
    </td>
    <td class='tdbg' align='left'>
        <asp:Label ID="LblPriority" runat="server"></asp:Label>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 150px;">
        <strong>签收截止日期：&nbsp;</strong><br />
    </td>
    <td class='tdbg' align='left'>
        <asp:Label ID="LblEndTime" runat="server"></asp:Label>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 150px;">
        <strong>签收状态：&nbsp;</strong><br />
    </td>
    <td class='tdbg' align='left'>
        <asp:Label ID="LblStatus" runat="server"></asp:Label>
    </td>
</tr>
<tr class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 150px;">
        <strong>签收记录：&nbsp;</strong>
    </td>
    <td class='tdbg' align='left'>
        <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
            <tr class='tdbgleft'>
                <td align="center">
                    签收用户</td>
                <td align="center">
                    是否签收</td>
                <td align="center">
                    签收时间</td>
                <td align="center">
                    签收IP</td>
            </tr>
            <asp:Repeater ID="RptSigninLog" runat="server">
                <ItemTemplate>
                    <tr class='tdbg'>
                        <td align="left">
                            <%#Eval("UserName") %>
                        </td>
                        <td align="center">
                            <%#(bool)Eval("IsSignin") ? "<span style=\"color:blue\"><b>√</b></span>" : "<span style=\"color:Red\"><b>×</b></span>"%>
                        </td>
                        <td align="center">
                            <%#(bool)Eval("IsSignin") ? Eval("SigninTime").ToString() : "" %>
                        </td>
                        <td align="center">
                            <%#Eval("IP") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </td>
</tr>
