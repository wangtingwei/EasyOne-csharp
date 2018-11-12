<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.MultipleHtmlTextType" Codebehind="MultipleHtmlTextType.ascx.cs" %>
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
            <pe:PEeditor ID="EditorMultipleHtml" runat="server">
            </pe:PEeditor>
            <pe:FckEditorValidator ID="FckEditVal" runat="server" SetFocusOnError="true" ControlToValidate="EditorMultipleHtml" Display="Dynamic"
                ErrorMessage="必填项不能为空" Visible="false"></pe:FckEditorValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
