<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.OperatingType" Codebehind="OperatingType.ascx.cs" %>
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
            <pe:OperatingSystemControl ID="OscTxtOperat" runat="server"></pe:OperatingSystemControl><pe:RequiredFieldValidator
                ID="ReqOscTxtOperat" runat="server" Display="Dynamic" Visible="false" SetFocusOnError="true"
                ControlToValidate="OscTxtOperat" ErrorMessage="运行平台不能为空"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
