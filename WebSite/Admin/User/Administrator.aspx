<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.Administrator"
    Title="����Ա����" Codebehind="Administrator.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="��ӹ���Ա" AlternateText="�޸Ĺ���Ա" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա����</strong></td>
            <td>
                <asp:TextBox ID="TxtAdminName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtAdminName"
                    ErrorMessage="����Ա������Ϊ�գ�" Display="Dynamic" runat="server"></pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeUserName" runat="server" ControlToValidate="TxtAdminName"
                    ErrorMessage="���ܰ��������ַ�  ��@��#��$��%��^��&��*��(��)��'��?��{��}��[��]��;��:��" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="ValgTextMaxLength" Display="Dynamic" ControlToValidate="TxtAdminName"
                    ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5]{2,20}$" SetFocusOnError="true"
                    runat="server" ErrorMessage="����Ա���������2���ַ����Ҳ��ܳ���20���ַ���"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ǰ̨��Ա��</strong>��<pe:ExtendedHyperLink ID="HypAddUser" NavigateUrl="~/Admin/User/User.aspx?Administrator=true" BeginTag="<strong><span style='color:Blue;'>"
                 EndTag="</span></strong>" runat="server">���</pe:ExtendedHyperLink>����</td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtUserName"
                    ErrorMessage="ǰ̨��Ա������Ϊ�գ�" runat="server"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��д���룺</strong></td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox><pe:ExtendedLabel HtmlEncode="false"
                    ID="LabTip" runat="server"></pe:ExtendedLabel>
                <pe:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtPassword"
                    runat="server" ErrorMessage="���벻��Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TxtPassword"
                    StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="BarIndicator_TxtUserPassword"
                    BarBorderCssClass="BarBorder_TxtUserPassword" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                    MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" DisplayPosition="RightSide" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id = "TrPassword">
            <td class="tdbgleft">
                <strong>ȷ�����룺</strong></td>
            <td>
                <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword"
                    ErrorMessage="������������벻һ�£�" runat="server"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ɫ���ã�</strong></td>
            <td>
                <asp:RadioButton ID="RadPurview1" runat="server" GroupName = "AdminPurview"  Text = "" />��������Ա��ӵ������Ȩ�ޡ�ĳЩȨ�ޣ������Ա������վ��Ϣ���á���ɫ����ȹ���Ȩ�ޣ�ֻ�г�������Ա���С�<br />
                <asp:RadioButton ID="RadPurview2" runat="server" GroupName = "AdminPurview" Text = "" Checked ="true" />��ͨ����Ա����Ҫ��ϸָ��ÿһ���ɫȨ��
            </td>
        </tr>
        <tr class="tdbg" runat ="server" id ="RolePurview">
            <td class="tdbgleft"></td>
            <td style="height: 100px">
                <br />
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr align="center">
                        <td valign ="top">
                            <strong>��ѡ��ɫ</strong><br />
                            &nbsp;<asp:ListBox ID="LstNotBelongRole" runat="server" Height="300px" Width="250px"
                                DataTextField="RoleName" DataValueField="RoleId" SelectionMode="Multiple"></asp:ListBox></td>
                        <td style="width: 100px;" align="center">
                            <input type="button" value="���>>" onclick="JavaScript:addItem(<%=LstNotBelongRole.ClientID%>,<%=LstBelongToRole.ClientID%>);delItem(<%=LstNotBelongRole.ClientID%>)" /><br />
                            <br />
                            <input type="button" value="<<�Ƴ�" onclick="JavaScript:addItem(<%=LstBelongToRole.ClientID%>,<%=LstNotBelongRole.ClientID%>);delItem(<%=LstBelongToRole.ClientID%>)" />
                        </td>
                        <td>
                            <strong>������ɫ</strong><br />
                            &nbsp;<asp:ListBox ID="LstBelongToRole" runat="server" Height="300px" Width="250px"
                                DataTextField="RoleName" DataValueField="RoleId" SelectionMode="Multiple"></asp:ListBox>
                            <asp:HiddenField ID="HdnBelongToRole" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                  <strong>ѡ�����ã�</strong>
            </td>
            <td>
                <asp:CheckBox ID="ChkEnableMultiLogin" runat="server" />�������ͬʱʹ�ô��ʺŵ�¼<br />
                <asp:CheckBox ID="ChkEnableModifyPassword" runat="server" Checked="true" />�������Ա�޸�����<br />
                <asp:CheckBox ID="ChkIsLock" runat="server" />�Ƿ�����
            </td>
        </tr>
        <tr class="tdbg" align="center">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="�ύ��Ϣ" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancle" runat="server" Text="ȡ��" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" /></td>
        </tr>
    </table>
<script type ="text/javascript" >
<!--

function RadPurview(roleType)
{
    if (roleType == 0)
    {
        document.getElementById("<%=RolePurview.ClientID%>").style.display="none";
    }
    else
    {
        document.getElementById("<%=RolePurview.ClientID%>").style.display="";
    }
}
        
/**
 * add one option of a select to another select.
 *
 * @author  Chunsheng Wang <wwccss@263.net>
 */
function addItem(ItemList,Target)
{
    for(var x = 0; x < ItemList.length; x++)
    {
        var opt = ItemList.options[x];
        if (opt.selected)
        {
            flag = true;
            for (var y=0;y<Target.length;y++)
            {
                var myopt = Target.options[y];
                if (myopt.value == opt.value)
                {
                    flag = false;
                }
            }
            if(flag)
            {
                Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
            }
        }
    }
}

/**
 * move one selected option from a select.
 *
 * @author  Chunsheng Wang <wwccss@263.net>
 */
function delItem(ItemList)
{
    for(var x=ItemList.length-1;x>=0;x--)
    {
        var opt = ItemList.options[x];
        if (opt.selected)
        {
            ItemList.options[x] = null;
        }
    }
}

function GetBelongToRole(ItemList)
{
    var adminId = "";
    for(var x = 0; x < ItemList.length; x++)
    {
        if (adminId == "")
        {
            adminId = ItemList.options[x].value;
        }
        else
        {
            adminId += "," + ItemList.options[x].value;
        }
    }
    var belongToRole= document.getElementById("<%=HdnBelongToRole.ClientID%>");
    belongToRole.value = adminId;
}
//-->
    </script>
</asp:Content>
