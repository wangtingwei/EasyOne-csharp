<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.LinkType" Codebehind="LinkType.ascx.cs" %>
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
            <asp:TextBox ID="TxtLinkUrl" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                ID="ReqTxtLinkUrl" runat="server" SetFocusOnError="true" Display="Dynamic" ErrorMessage="URL地址不能为空"
                Visible="false" ControlToValidate="TxtLinkUrl"></pe:RequiredFieldValidator><pe:UrlValidator
                    ID="ValUrlTxtLink" runat="server" ControlToValidate="TxtLinkUrl" Display="Dynamic"></pe:UrlValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
