<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardBatchAdd" Title="添加标签" Codebehind="CardBatchAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Panel ID="PnlAdd" runat="server" Width="100%">
        <table style="text-align: center; width: 100%" border="0" cellpadding="2" cellspacing="1"
            class="border">
            <tr align="center">
                <td class="spacingtitle" colspan="2">
                    <strong>批量生成充值卡</strong>
                </td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td style="width: 40%">
                    <strong>充值卡所属商品：</strong><br />
                    商店中的某张点卡类商品可以对应多张实际的充值卡，会员在购买点卡类商品后，可以通过“获取虚拟充值卡”来得到这里输入的卡号和密码。</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="DropProductId" runat="server" Width="115px">
                    </asp:DropDownList></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>充值卡数量：</strong></td>
                <td>
                    <asp:TextBox ID="TxtNums" runat="server" MaxLength="5" Width="100px">100</asp:TextBox>
                    张</td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>自定义充值卡号码：</strong>
                    <br />
                    <span style="color: #0000ff">说明：每个?代表一个英文字母，#代表一个数字，
                        <br />
                        *代表一个英文字母或数字(自定义符号必须是半角) </span>
                </td>
                <td>
                    <asp:TextBox ID="TxtCardNum" runat="server" MaxLength="30" Width="98px">PE???###?#*</asp:TextBox>
                    建议设为10--15位</td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>自定义充值卡密码：</strong>
                    <br />
                    <span style="color: #0000ff">说明：每个?代表一个英文字母，#代表一个数字，
                        <br />
                        *代表一个英文字母或数字(自定义符号必须是半角) </span>
                </td>
                <td>
                    <asp:TextBox ID="TxtPassword" runat="server" MaxLength="50" Width="100px">PE??#?#*</asp:TextBox>
                    建议设为6--10位</td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>充值卡面值：</strong><br />
                    即购买人需要花费的实际金额</td>
                <td>
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="10" Width="100px">50</asp:TextBox>
                    元 <span style="color: #ff0000">注意要与所属商品中描述的面值相同。</span></td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>充值卡点数、资金或有效期：</strong><br />
                    购买人可以得到的点数、资金或有效期
                </td>
                <td>
                    <asp:TextBox ID="TxtValidNum" runat="server" MaxLength="10" Width="100px">500</asp:TextBox>
                    <asp:DropDownList ID="DropValidUnit" runat="server">
                        <asp:ListItem Value="0">点</asp:ListItem>
                        <asp:ListItem Value="1">天</asp:ListItem>
                        <asp:ListItem Value="2">月</asp:ListItem>
                        <asp:ListItem Value="3">年</asp:ListItem>
                        <asp:ListItem Value="4">元</asp:ListItem>
                        <asp:ListItem Value="5">组</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropUserGroup" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="LblMsg" runat="server" ForeColor="red" Text="注意要与所属商品中描述的点数相同。"></asp:Label></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>充值截止期限：</strong><br />
                    购买人必须在此日期前进行充值，否则自动失效</td>
                <td class="tdbg">
                    <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>代理商：</strong></td>
                <td>
                    <asp:TextBox ID="TxtAgentName" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" style="width: 100%; text-align: center">
                    <asp:Button ID="BtnBatchAdd" runat="server" Text="添加" OnClick="BtnBatchAdd_Click" />
                    &nbsp;&nbsp;
                    <input id="Cancel" name="Cancel" onclick="Redirect('CardsManage.aspx')" style="cursor: hand"
                        type="button" class="inputbutton" value="取消" />
                </td>
            </tr>
        </table>
        <br />
        注意：如数据库中卡号的重复的充值卡将不会生成。</asp:Panel>
    &nbsp;

    <script type="text/javascript">
var dropUserGroup = document.getElementById('<%=DropUserGroup.ClientID %>');
var dropValidUnit = document.getElementById('<%=DropValidUnit.ClientID %>');
var txtValidNum =document.getElementById('<%=TxtValidNum.ClientID %>');
var lblMsg = document.getElementById('<%=LblMsg.ClientID %>');

function selectGroup()
{
    if(dropValidUnit.value == 5)
    {
        dropUserGroup.style.display='';
        txtValidNum.value = dropUserGroup.value;
        txtValidNum.disabled = true;
        lblMsg.innerText="请选择充值卡对应的会员组。";
    }
    else
    {
        dropUserGroup.style.display="none";
        txtValidNum.disabled = false;
        txtValidNum.value="500";
        lblMsg.innerText="注意要与所属商品中描述的点数相同。";
    }

}

function selectValue()
{
    txtValidNum.value = dropUserGroup.value;
}

document.body.onload=function()
{
    if(dropValidUnit!=null && dropUserGroup!=null && txtValidNum!=null && lblMsg!=null)
    {
           if(dropValidUnit.value==5)
        {
                dropUserGroup.style.display='';
                txtValidNum.disabled = true;
                lblMsg.innerText="请选择充值卡对应的会员组。";
        }
        else
        {
                dropUserGroup.style.display="none";
                txtValidNum.disabled = false;
                lblMsg.innerText="注意要与所属商品中描述的点数相同。";
        }
     
    }
};
    </script>

    <asp:Panel ID="PnlShow" runat="server" Width="100%" HorizontalAlign="Center" Visible="False">
        <table border="0" cellpadding="2" cellspacing="1" class="border" width="300" align="Center">
            <tr>
                <td align="center" class="title" colspan="2">
                    本次生成的点卡信息如下：</td>
            </tr>
            <tr class="tdbg">
                <td style="width: 100px" align="left">
                    充值卡所属商品：</td>
                <td id="TdCardType" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    充值卡数量：</td>
                <td id="TdCount" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    充值卡面值：</td>
                <td class="tdbg" id="TdMoney" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    充值卡点数：</td>
                <td id="TdValidNum" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    充值截止日期：</td>
                <td id="TdEndDate" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    代理商：</td>
                <td id="TdAgentName" runat="server" align="left">
                </td>
            </tr>
        </table>
        <br />
        <asp:Table ID="TbCardList" runat="server" BorderWidth="0px" CellPadding="2" CellSpacing="1"
            CssClass="border" HorizontalAlign="Center" Width="300px">
            <asp:TableRow runat="server" CssClass="title">
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="50%">卡 号</asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="50%">密 码</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <input id="Button1" type="button" value="返 回" onclick="javascript:history.go(-1)" /></asp:Panel>
</asp:Content>
