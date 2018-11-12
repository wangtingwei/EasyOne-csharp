<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.CountType" Codebehind="CountType.ascx.cs" %>
<tr id='Tab' runat="server" class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 20%;">
        <div class="DivWordBreak">
            <strong>
                <%= FieldAlias %>
                £º&nbsp;</strong><br />
            <%= Tips %>
        </div>
    </td>
    <td class='tdbg' align='left'>
        <div class="DivWordBreak">
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
