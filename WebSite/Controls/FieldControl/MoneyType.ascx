<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.MoneyType" Codebehind="MoneyType.ascx.cs" %>
<tr id='Tab' runat="server" class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 20%;">
        <div class="DivWordBreak">
            <strong>
                <%= FieldAlias %>
                ：&nbsp;</strong><br />
            <%= Tips %>
        </div>
    </td>
    <td class='tdbg' align='left'>
        <div class="DivWordBreak">
            <asp:TextBox ID="TxtMoney" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                ID="ReqTxtMoney" runat="server" ControlToValidate="TxtMoney" SetFocusOnError="true"
                Display="Dynamic" ErrorMessage="货币文本框长度不能为空" Visible="false"></pe:RequiredFieldValidator><asp:RangeValidator
                    ID="ValRangeTxtMoney" ControlToValidate="TxtMoney" Display="Dynamic" SetFocusOnError="true" Visible="false"
                    runat="server"></asp:RangeValidator><pe:MoneyValidator ID="MoneyValTextMoney" runat="server" ControlToValidate="TxtMoney" Display="Dynamic" SetFocusOnError="true"></pe:MoneyValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
