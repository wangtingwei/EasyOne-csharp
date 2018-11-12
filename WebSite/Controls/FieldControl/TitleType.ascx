<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.TitleType" Codebehind="TitleType.ascx.cs" %>
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
            <asp:TextBox ID="TxtTitle" runat="server"></asp:TextBox><pe:ExtendedLiteral HtmlEncode="false" ID="LitCheckTitle" Visible="false" runat="server"></pe:ExtendedLiteral>
            <pe:RequiredFieldValidator ID="ReqTxtTitle" runat="server" SetFocusOnError="true"
                Display="Dynamic" ControlToValidate="TxtTitle" Visible="false" ErrorMessage="必填项不能为空"></pe:RequiredFieldValidator>
            <span style="color: Green">
                <%= Description %>
            </span>
            <br />
            <asp:TextBox ID="TxtPinyinTitle" Visible="false"  runat="server"></asp:TextBox>
        </div>
    </td>
</tr>
<script language="javascript" type="text/javascript">
function GetPinyinTitle(value)
{
    EasyOne.WebSite.Admin.Contents.CategoryService.GetPinyinTitles(value,SetPinyinTitle);
}
 function SetPinyinTitle(s){
       $get("<%=TxtPinyinTitle.ClientID %>").value=s;
   }
</script>
