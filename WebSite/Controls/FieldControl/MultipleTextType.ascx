<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.MultipleTextType" Codebehind="MultipleTextType.ascx.cs" %>
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
            <asp:TextBox ID="TxtMultiple" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                ID="ReqTxtMultiple" runat="server" ControlToValidate="TxtMultiple" SetFocusOnError="true"
                ErrorMessage="必填项不能为空" Display="Dynamic" Visible="false"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
