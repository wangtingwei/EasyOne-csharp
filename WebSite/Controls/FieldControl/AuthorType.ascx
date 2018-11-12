<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.AuthorType" Codebehind="AuthorType.ascx.cs" %>
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
            <pe:AuthorControl ID="Author" runat="server"></pe:AuthorControl>
            
            <pe:RequiredFieldValidator
                ID="ReqAuthor" runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="作者不能为空"
                Visible="false" ControlToValidate="Author"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>