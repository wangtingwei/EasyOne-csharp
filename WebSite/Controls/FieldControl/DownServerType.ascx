<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.DownServerType" Codebehind="DownServerType.ascx.cs" %>
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
            <pe:DownServerControl ID="TxtDownServer" runat="server"></pe:DownServerControl><pe:RequiredFieldValidator
                ID="ReqTxtDownServer" runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="下载服务器不能为空"
                Visible="false" ControlToValidate="TxtDownServer"></pe:RequiredFieldValidator>
                <pe:DownServerControl runat="server" ID="thisdown"></pe:DownServerControl>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
