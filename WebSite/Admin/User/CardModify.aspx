<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardModify" Title="修改充值卡" Codebehind="CardModify.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>修改充值卡</strong></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 40%">
                <b>所属商品：</b></td>
            <td id="TdProductName" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <b>充值卡卡号：</b></td>
            <td>
                <asp:TextBox ID="TxtCardNum" runat="server" MaxLength="20" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <b>充值卡密码：</b></td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <b>充值卡面值：</b></td>
            <td>
                <asp:TextBox ID="TxtMoney" runat="server" MaxLength="10" OnTextChanged="TxtMoney_TextChanged"></asp:TextBox>
                元<asp:RequiredFieldValidator ID="ValrMoney" runat="server" ControlToValidate="TxtMoney"
                    Display="Dynamic" ErrorMessage="面值不能为空！"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="ValcMoney" runat="server" ControlToValidate="TxtMoney"
                    Display="Dynamic" ErrorMessage="请指定充值卡的面值！" Operator="GreaterThan" Type="Currency"
                    ValueToCompare="0"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <b>充值卡点数：</b></td>
            <td>
                <asp:TextBox ID="TxtValidNum" runat="server" MaxLength="9" Width="79px"></asp:TextBox>&nbsp;<asp:DropDownList
                    ID="DropValidUnit" runat="server">
                    <asp:ListItem Value="0">点</asp:ListItem>
                    <asp:ListItem Value="1">天</asp:ListItem>
                    <asp:ListItem Value="2">月</asp:ListItem>
                    <asp:ListItem Value="3">年</asp:ListItem>
                    <asp:ListItem Value="4">元</asp:ListItem>
                    <asp:ListItem Value="5">组</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropUserGroup" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <b>充值截止日期：</b></td>
            <td>
                <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right">
                <strong>代理商：</strong></td>
            <td style="width: 60%">
                <asp:TextBox ID="TxtAgentName" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr align="center" class="tdbg">
            <td colspan="2" style="height: 30px">
                <asp:Button ID="BtnSave" runat="server" Text="保存修改结果" OnClick="BtnSave_Click" />
                &nbsp; &nbsp;<input id="Cancel" name="Cancel" onclick="javascript:history.go(-1)"
                    style="cursor: hand" type="button" class="inputbutton" value="取消" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
var dropUserGroup = document.getElementById('<%=DropUserGroup.ClientID %>');
var dropValidUnit = document.getElementById('<%=DropValidUnit.ClientID %>');
var txtValidNum =document.getElementById('<%=TxtValidNum.ClientID %>');

function selectGroup()
{
    if(dropValidUnit.value == 5)
    {
        dropUserGroup.style.display='';
        txtValidNum.value = dropUserGroup.value;
        txtValidNum.disabled = true;
    }
    else
    {
        dropUserGroup.style.display="none";
        txtValidNum.disabled = false;
        txtValidNum.value="500";
    }

}

function selectValue()
{
    txtValidNum.value = dropUserGroup.value;
}

document.body.onload=function()
{ 
    if(dropValidUnit.value == 5)
    {
        dropUserGroup.style.display='';
        txtValidNum.disabled = true;
    }
    else
    {
        dropUserGroup.style.display="none";
        txtValidNum.disabled = false;
    }
};
  
    </script>

</asp:Content>
