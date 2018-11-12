<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.Trademark" Codebehind="Trademark.ascx.cs" %>
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
            <pe:TrademarkControl ID="TmcTrademark" runat="server"></pe:TrademarkControl><pe:RequiredFieldValidator
                ID="ReqTmcTrademark" runat="server" SetFocusOnError="true" ControlToValidate="TmcTrademark"
                Display="Dynamic" ErrorMessage="品牌不能为空" Visible="false"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
