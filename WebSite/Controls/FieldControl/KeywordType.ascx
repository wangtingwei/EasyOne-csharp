<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.KeywordType" Codebehind="KeywordType.ascx.cs" %>
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
            <pe:KeyWordControl ID="TxtKeyWord" runat="server"></pe:KeyWordControl><pe:RequiredFieldValidator
                ID="ReqTxtKeyWord" runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="关键字不能为空"
                Visible="false" ControlToValidate="TxtKeyWord"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
