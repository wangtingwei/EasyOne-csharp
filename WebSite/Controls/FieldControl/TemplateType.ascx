<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.TemplateType" Codebehind="TemplateType.ascx.cs" %>
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
            <pe:TemplateSelectControl ID="FscTemplate" runat="server"></pe:TemplateSelectControl><pe:RequiredFieldValidator
                ID="ReqFscTemplate" ControlToValidate="FscTemplate" runat="server" Display="Dynamic"
                SetFocusOnError="true" Visible="false" ErrorMessage="内容页模板不能为空"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
