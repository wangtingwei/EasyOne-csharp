<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.Producer" Codebehind="Producer.ascx.cs" %>
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
        <pe:ProducerControl ID="PrcTxtProducer" runat="server"></pe:ProducerControl><pe:RequiredFieldValidator
            ID="ReqPrcTxtProducer" runat="server" SetFocusOnError="true" Visible="false"
            Display="Dynamic" ErrorMessage="厂商不能为空" ControlToValidate="PrcTxtProducer"></pe:RequiredFieldValidator>
        <span style="color: Green">
            <%= Description %>
        </span>
        </div>
    </td>
</tr>
