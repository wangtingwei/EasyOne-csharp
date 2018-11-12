<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.DateTimeType" Codebehind="DateTimeType.ascx.cs" %>
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
            <pe:DatePicker ID="PickDate" runat="server"></pe:DatePicker><pe:RequiredFieldValidator
                ID="ReqPickDate" runat="server" SetFocusOnError="true" ControlToValidate="PickDate"
                Display="Dynamic" ErrorMessage="日期不能为空" Visible="false"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
