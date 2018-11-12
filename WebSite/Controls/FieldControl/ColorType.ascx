<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.ColorType" Codebehind="ColorType.ascx.cs" %>
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
            <pe:ColorPicker ID="PickColor" runat="server"></pe:ColorPicker><pe:RequiredFieldValidator
                ID="ReqPickColor" runat="server" SetFocusOnError="true" Visible="false" Display="Dynamic"
                ControlToValidate="PickColor" ErrorMessage="颜色不能为空"></pe:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PickColor" ValidationExpression="#[0-9a-fA-F]{3}([0-9a-fA-F]{3})?" ErrorMessage="请输入正确的颜色代码"></asp:RegularExpressionValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
