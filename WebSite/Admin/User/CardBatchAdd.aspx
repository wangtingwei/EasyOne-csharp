<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardBatchAdd" Title="��ӱ�ǩ" Codebehind="CardBatchAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Panel ID="PnlAdd" runat="server" Width="100%">
        <table style="text-align: center; width: 100%" border="0" cellpadding="2" cellspacing="1"
            class="border">
            <tr align="center">
                <td class="spacingtitle" colspan="2">
                    <strong>�������ɳ�ֵ��</strong>
                </td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td style="width: 40%">
                    <strong>��ֵ��������Ʒ��</strong><br />
                    �̵��е�ĳ�ŵ㿨����Ʒ���Զ�Ӧ����ʵ�ʵĳ�ֵ������Ա�ڹ���㿨����Ʒ�󣬿���ͨ������ȡ�����ֵ�������õ���������Ŀ��ź����롣</td>
                <td style="width: 60%">
                    <asp:DropDownList ID="DropProductId" runat="server" Width="115px">
                    </asp:DropDownList></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>��ֵ��������</strong></td>
                <td>
                    <asp:TextBox ID="TxtNums" runat="server" MaxLength="5" Width="100px">100</asp:TextBox>
                    ��</td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>�Զ����ֵ�����룺</strong>
                    <br />
                    <span style="color: #0000ff">˵����ÿ��?����һ��Ӣ����ĸ��#����һ�����֣�
                        <br />
                        *����һ��Ӣ����ĸ������(�Զ�����ű����ǰ��) </span>
                </td>
                <td>
                    <asp:TextBox ID="TxtCardNum" runat="server" MaxLength="30" Width="98px">PE???###?#*</asp:TextBox>
                    ������Ϊ10--15λ</td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>�Զ����ֵ�����룺</strong>
                    <br />
                    <span style="color: #0000ff">˵����ÿ��?����һ��Ӣ����ĸ��#����һ�����֣�
                        <br />
                        *����һ��Ӣ����ĸ������(�Զ�����ű����ǰ��) </span>
                </td>
                <td>
                    <asp:TextBox ID="TxtPassword" runat="server" MaxLength="50" Width="100px">PE??#?#*</asp:TextBox>
                    ������Ϊ6--10λ</td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>��ֵ����ֵ��</strong><br />
                    ����������Ҫ���ѵ�ʵ�ʽ��</td>
                <td>
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="10" Width="100px">50</asp:TextBox>
                    Ԫ <span style="color: #ff0000">ע��Ҫ��������Ʒ����������ֵ��ͬ��</span></td>
            </tr>
            <tr class="tdbg" style="text-align: left; color: #000000;">
                <td>
                    <strong>��ֵ���������ʽ����Ч�ڣ�</strong><br />
                    �����˿��Եõ��ĵ������ʽ����Ч��
                </td>
                <td>
                    <asp:TextBox ID="TxtValidNum" runat="server" MaxLength="10" Width="100px">500</asp:TextBox>
                    <asp:DropDownList ID="DropValidUnit" runat="server">
                        <asp:ListItem Value="0">��</asp:ListItem>
                        <asp:ListItem Value="1">��</asp:ListItem>
                        <asp:ListItem Value="2">��</asp:ListItem>
                        <asp:ListItem Value="3">��</asp:ListItem>
                        <asp:ListItem Value="4">Ԫ</asp:ListItem>
                        <asp:ListItem Value="5">��</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropUserGroup" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="LblMsg" runat="server" ForeColor="red" Text="ע��Ҫ��������Ʒ�������ĵ�����ͬ��"></asp:Label></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>��ֵ��ֹ���ޣ�</strong><br />
                    �����˱����ڴ�����ǰ���г�ֵ�������Զ�ʧЧ</td>
                <td class="tdbg">
                    <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg" style="text-align: left">
                <td>
                    <strong>�����̣�</strong></td>
                <td>
                    <asp:TextBox ID="TxtAgentName" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" style="width: 100%; text-align: center">
                    <asp:Button ID="BtnBatchAdd" runat="server" Text="���" OnClick="BtnBatchAdd_Click" />
                    &nbsp;&nbsp;
                    <input id="Cancel" name="Cancel" onclick="Redirect('CardsManage.aspx')" style="cursor: hand"
                        type="button" class="inputbutton" value="ȡ��" />
                </td>
            </tr>
        </table>
        <br />
        ע�⣺�����ݿ��п��ŵ��ظ��ĳ�ֵ�����������ɡ�</asp:Panel>
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
        lblMsg.innerText="��ѡ���ֵ����Ӧ�Ļ�Ա�顣";
    }
    else
    {
        dropUserGroup.style.display="none";
        txtValidNum.disabled = false;
        txtValidNum.value="500";
        lblMsg.innerText="ע��Ҫ��������Ʒ�������ĵ�����ͬ��";
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
                lblMsg.innerText="��ѡ���ֵ����Ӧ�Ļ�Ա�顣";
        }
        else
        {
                dropUserGroup.style.display="none";
                txtValidNum.disabled = false;
                lblMsg.innerText="ע��Ҫ��������Ʒ�������ĵ�����ͬ��";
        }
     
    }
};
    </script>

    <asp:Panel ID="PnlShow" runat="server" Width="100%" HorizontalAlign="Center" Visible="False">
        <table border="0" cellpadding="2" cellspacing="1" class="border" width="300" align="Center">
            <tr>
                <td align="center" class="title" colspan="2">
                    �������ɵĵ㿨��Ϣ���£�</td>
            </tr>
            <tr class="tdbg">
                <td style="width: 100px" align="left">
                    ��ֵ��������Ʒ��</td>
                <td id="TdCardType" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    ��ֵ��������</td>
                <td id="TdCount" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    ��ֵ����ֵ��</td>
                <td class="tdbg" id="TdMoney" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    ��ֵ��������</td>
                <td id="TdValidNum" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    ��ֵ��ֹ���ڣ�</td>
                <td id="TdEndDate" runat="server" align="left">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left">
                    �����̣�</td>
                <td id="TdAgentName" runat="server" align="left">
                </td>
            </tr>
        </table>
        <br />
        <asp:Table ID="TbCardList" runat="server" BorderWidth="0px" CellPadding="2" CellSpacing="1"
            CssClass="border" HorizontalAlign="Center" Width="300px">
            <asp:TableRow runat="server" CssClass="title">
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="50%">�� ��</asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="50%">�� ��</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <input id="Button1" type="button" value="�� ��" onclick="javascript:history.go(-1)" /></asp:Panel>
</asp:Content>
