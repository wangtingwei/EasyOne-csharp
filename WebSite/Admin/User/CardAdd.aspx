<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardAdd" Title="充值卡管理" Codebehind="CardAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>添加充值卡</strong></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>充值卡类型：</strong></td>
            <td style="width: 60%">
                &nbsp;&nbsp;
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadlCardType" runat="server" RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True">本站充值卡</asp:ListItem>
                                <asp:ListItem Value="1">其他公司卡</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                            <span style="color: blue">购买者得到卡号和密码后，可以直接在本站进行充值<br />
                                购买者得到卡号和密码后，需要去相关公司或网站进行充值</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>充值卡所属商品：</b><br />
                商店中的某张点卡类商品可以对应多张实际的充值卡，会员在购买点卡类商品后，可以通过“获取虚拟充值卡”来得到这里输入的卡号和密码。</td>
            <td style="width: 60%">
                <asp:DropDownList ID="DropProductId" runat="server" Width="111px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>添加方式：</strong></td>
            <td style="width: 60%">
                <asp:RadioButton ID="RadAddTypeSingle" runat="server" Checked="True" GroupName="AddType"
                    Text="单张充值卡" />
                <asp:RadioButton ID="RadAddTypeBatch" runat="server" GroupName="AddType" Text="批量添加充值卡" /></td>
        </tr>
        <tr class="tdbg" id="trSingle1">
            <td style="width: 40%">
                <b>充值卡卡号：</b></td>
            <td>
                <asp:TextBox ID="TxtCardNum" runat="server" MaxLength="30"></asp:TextBox>
                <span style="color: blue">建议设为10--15位</span></td>
        </tr>
        <tr class="tdbg" id="trSingle2">
            <td style="width: 40%">
                <b>充值卡密码：</b></td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="127px" MaxLength="50"></asp:TextBox>
                <span style="color: blue">建议设为6--10位</span>
            </td>
        </tr>
        <tr class="tdbg" id="trBatch" style="display: none">
            <td style="width: 40%">
                <b>格式文本：</b><br />
                <span style="color: red">请按照每行一张卡，每张卡按“卡号＋分隔符＋密码”的格式录入</span><br />
                例1：734534759*kSo94Sf4Xs（以“*”作为分隔符）<br />
                例2：98273305834|lo23ji6x（以“|”作为分隔符）</td>
            <td>
                <asp:TextBox ID="TxtCardText" runat="server" Height="160px" TextMode="MultiLine"
                    Width="415px"></asp:TextBox><br />
                分隔符：<asp:TextBox ID="TxtSplit" runat="server" MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>充值卡面值：</b><br />
                即购买人需要花费的实际金额</td>
            <td>
                <asp:TextBox ID="TxtMoney" runat="server" MaxLength="10" Width="67px"></asp:TextBox>
                元
                <asp:RequiredFieldValidator ID="ValrMoney" runat="server" ControlToValidate="TxtMoney"
                    Display="Dynamic" ErrorMessage="请输入充值卡面值！"></asp:RequiredFieldValidator><asp:CompareValidator
                        ID="ValcMoney" runat="server" ControlToValidate="TxtMoney" Display="Dynamic"
                        ErrorMessage="请输入正确数值！" Operator="GreaterThan" Type="Double"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>充值卡点数、资金或有效期：</b><br />
                购买人可以得到的点数、资金或有效期</td>
            <td>
                <asp:TextBox ID="TxtValidNum" runat="server" MaxLength="9" Width="67px">500</asp:TextBox>
                <asp:DropDownList ID="DropValidUnit" runat="server">
                    <asp:ListItem Value="0">点</asp:ListItem>
                    <asp:ListItem Value="1">天</asp:ListItem>
                    <asp:ListItem Value="2">月</asp:ListItem>
                    <asp:ListItem Value="3">年</asp:ListItem>
                    <asp:ListItem Value="4">元</asp:ListItem>
                    <asp:ListItem Value="5">组</asp:ListItem>
                </asp:DropDownList>&nbsp;<asp:DropDownList ID="DropUserGroup" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>充值截止日期：</b><br />
                购买人必须在此日期前进行充值，否则自动失效</td>
            <td>
                <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>代理商：</strong></td>
            <td style="width: 60%">
                <asp:TextBox ID="TxtAgentName" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="height: 30px" colspan="2">
                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="添加" /></td>
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
    
    if(document.getElementById("<%=RadAddTypeBatch.ClientID %>").checked)
    {
        document.getElementById("trBatch").style.display ='';
    }

 };
    
    </script>

</asp:Content>
