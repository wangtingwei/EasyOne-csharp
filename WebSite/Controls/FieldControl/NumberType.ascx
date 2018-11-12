<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.NumberType" Codebehind="NumberType.ascx.cs" %>
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
            <asp:TextBox ID="TxtNumber" Width="50px" runat="server"></asp:TextBox><asp:Literal
                ID="LitPercent" runat="server" Visible="false"></asp:Literal>&nbsp;<pe:RequiredFieldValidator
                    ID="ReqTxtNumber" runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="数字不能为空"
                    Visible="false" ControlToValidate="TxtNumber"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegTxtNumber" runat="server" SetFocusOnError="true" ControlToValidate="TxtNumber"
                        Display="Dynamic"></asp:RegularExpressionValidator><asp:RangeValidator ID="ValRangeTxtNumber"
                            runat="server" SetFocusOnError="true" ControlToValidate="TxtNumber" Display="Dynamic"
                            Visible="false"></asp:RangeValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
