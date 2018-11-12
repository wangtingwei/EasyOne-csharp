<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardAdd" Title="��ֵ������" Codebehind="CardAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>��ӳ�ֵ��</strong></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>��ֵ�����ͣ�</strong></td>
            <td style="width: 60%">
                &nbsp;&nbsp;
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadlCardType" runat="server" RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True">��վ��ֵ��</asp:ListItem>
                                <asp:ListItem Value="1">������˾��</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                            <span style="color: blue">�����ߵõ����ź�����󣬿���ֱ���ڱ�վ���г�ֵ<br />
                                �����ߵõ����ź��������Ҫȥ��ع�˾����վ���г�ֵ</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>��ֵ��������Ʒ��</b><br />
                �̵��е�ĳ�ŵ㿨����Ʒ���Զ�Ӧ����ʵ�ʵĳ�ֵ������Ա�ڹ���㿨����Ʒ�󣬿���ͨ������ȡ�����ֵ�������õ���������Ŀ��ź����롣</td>
            <td style="width: 60%">
                <asp:DropDownList ID="DropProductId" runat="server" Width="111px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>��ӷ�ʽ��</strong></td>
            <td style="width: 60%">
                <asp:RadioButton ID="RadAddTypeSingle" runat="server" Checked="True" GroupName="AddType"
                    Text="���ų�ֵ��" />
                <asp:RadioButton ID="RadAddTypeBatch" runat="server" GroupName="AddType" Text="������ӳ�ֵ��" /></td>
        </tr>
        <tr class="tdbg" id="trSingle1">
            <td style="width: 40%">
                <b>��ֵ�����ţ�</b></td>
            <td>
                <asp:TextBox ID="TxtCardNum" runat="server" MaxLength="30"></asp:TextBox>
                <span style="color: blue">������Ϊ10--15λ</span></td>
        </tr>
        <tr class="tdbg" id="trSingle2">
            <td style="width: 40%">
                <b>��ֵ�����룺</b></td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="127px" MaxLength="50"></asp:TextBox>
                <span style="color: blue">������Ϊ6--10λ</span>
            </td>
        </tr>
        <tr class="tdbg" id="trBatch" style="display: none">
            <td style="width: 40%">
                <b>��ʽ�ı���</b><br />
                <span style="color: red">�밴��ÿ��һ�ſ���ÿ�ſ��������ţ��ָ��������롱�ĸ�ʽ¼��</span><br />
                ��1��734534759*kSo94Sf4Xs���ԡ�*����Ϊ�ָ�����<br />
                ��2��98273305834|lo23ji6x���ԡ�|����Ϊ�ָ�����</td>
            <td>
                <asp:TextBox ID="TxtCardText" runat="server" Height="160px" TextMode="MultiLine"
                    Width="415px"></asp:TextBox><br />
                �ָ�����<asp:TextBox ID="TxtSplit" runat="server" MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>��ֵ����ֵ��</b><br />
                ����������Ҫ���ѵ�ʵ�ʽ��</td>
            <td>
                <asp:TextBox ID="TxtMoney" runat="server" MaxLength="10" Width="67px"></asp:TextBox>
                Ԫ
                <asp:RequiredFieldValidator ID="ValrMoney" runat="server" ControlToValidate="TxtMoney"
                    Display="Dynamic" ErrorMessage="�������ֵ����ֵ��"></asp:RequiredFieldValidator><asp:CompareValidator
                        ID="ValcMoney" runat="server" ControlToValidate="TxtMoney" Display="Dynamic"
                        ErrorMessage="��������ȷ��ֵ��" Operator="GreaterThan" Type="Double"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>��ֵ���������ʽ����Ч�ڣ�</b><br />
                �����˿��Եõ��ĵ������ʽ����Ч��</td>
            <td>
                <asp:TextBox ID="TxtValidNum" runat="server" MaxLength="9" Width="67px">500</asp:TextBox>
                <asp:DropDownList ID="DropValidUnit" runat="server">
                    <asp:ListItem Value="0">��</asp:ListItem>
                    <asp:ListItem Value="1">��</asp:ListItem>
                    <asp:ListItem Value="2">��</asp:ListItem>
                    <asp:ListItem Value="3">��</asp:ListItem>
                    <asp:ListItem Value="4">Ԫ</asp:ListItem>
                    <asp:ListItem Value="5">��</asp:ListItem>
                </asp:DropDownList>&nbsp;<asp:DropDownList ID="DropUserGroup" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <b>��ֵ��ֹ���ڣ�</b><br />
                �����˱����ڴ�����ǰ���г�ֵ�������Զ�ʧЧ</td>
            <td>
                <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%">
                <strong>�����̣�</strong></td>
            <td style="width: 60%">
                <asp:TextBox ID="TxtAgentName" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="height: 30px" colspan="2">
                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="���" /></td>
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
