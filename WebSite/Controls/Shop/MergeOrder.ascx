<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.MergeOrder" Codebehind="MergeOrder.ascx.cs" %>
<div style="text-align: center">
    <table width='100%' border='0' cellpadding='2' cellspacing='1' class='border'>
        <tr align='center' class='title'>
            <td colspan='2'>
                合并订单</td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                主订单：</td>
            <td align='left'>
                <asp:UpdatePanel runat="server" ID="UpnlPrincipalOrder" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtPrincipalOrder" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="DropPrincipalOrder" DataTextField="value" DataValueField="key"
                            AutoPostBack="true" OnSelectedIndexChanged="DropPrincipalOrder_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <pe:RequiredFieldValidator ShowRequiredText="false" ID="ValrPrincipalOrder" runat="server"
                    Display="dynamic" ControlToValidate="TxtPrincipalOrder" ErrorMessage="请输入要合并的主订单！"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                从订单：</td>
            <td align='left'>
                <asp:UpdatePanel runat="server" ID="UpnlSubordinateOrder" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtSubordinateOrder" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="DropSubordinateOrder" DataTextField="value" DataValueField="key"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropSubordinateOrder_SelectedIndexChanged">
                        </asp:DropDownList>
                        合并后从订单将被直接删除
                    </ContentTemplate>
                </asp:UpdatePanel>
                <pe:RequiredFieldValidator ShowRequiredText="false" ID="ValrSubordinateOrder" runat="server"
                    Display="dynamic" ControlToValidate="TxtSubordinateOrder" ErrorMessage="请输入要合并的从订单！"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                合并方式：</td>
            <td align='left'>
                <asp:RadioButtonList ID="RadlMergeType" runat="server">
                    <asp:ListItem Value="0" Selected="true">保存主订单的备注留言和内部记录</asp:ListItem>
                    <asp:ListItem Value="1">保存从订单的备注留言和内部记录</asp:ListItem>
                    <asp:ListItem Value="2">保存主订单、从订单的备注留言和内部记录</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2">
                <span style="color:blue">注意：只能对未确认的订单进行合并</span>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="合并" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
            </td>
        </tr>
    </table>
</div>
