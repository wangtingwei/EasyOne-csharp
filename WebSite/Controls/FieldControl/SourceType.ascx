<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.SourceType" Codebehind="SourceType.ascx.cs" %>
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
            <pe:SourceControl ID="SocTxtSource" runat="server"></pe:SourceControl><pe:RequiredFieldValidator
                ID="ReqSocTxtSource" runat="server" SetFocusOnError="true" Display="Dynamic"
                ControlToValidate="SocTxtSource" Visible="false" ErrorMessage="来源不能为空"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
        </div>
    </td>
</tr>
