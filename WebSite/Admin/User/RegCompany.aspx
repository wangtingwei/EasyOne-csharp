<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RegCompany"
    Title="无标题页" Codebehind="RegCompany.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
            </td>
            <td style="text-align: right">
                <asp:Label ID="LblUserName" runat="server"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">

    <script type="text/javascript">
        function SelectCompany()
        {
            window.open('../Crm/CompanyList.aspx?InputControl=<%=TxtName.ClientID %>&HiddenControl=<%=HdnCompanyId.ClientID %>','','width=600,height=450,resizable=0,scrollbars=yes');
        }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center" class="title">
            <td colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text=" 方式一：将会员加入已有企业 " Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 40%;" class="tdbgleft">
                请选择要加入的企业：</td>
            <td align="left">
                <asp:TextBox ID="TxtName" MaxLength="200" ReadOnly="true" Width="220px" runat="server"></asp:TextBox>
                <asp:HiddenField ID="HdnCompanyId" runat="server" />
                <input type="button" class="inputbutton" name="Submit" value="..." onclick="SelectCompany();" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 40%;" class="tdbgleft">
                加入后的成员级别：</td>
            <td align="left">
                <asp:RadioButtonList ID="RadlUserType" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Value="2">管理员&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="3" Selected="true">普通成员&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="4">待审核成员</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td colspan="2">
                <asp:Button ID="BtnRegister" runat="server" Text="加入此企业" CausesValidation="false"
                    OnClick="BtnRegister_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center" class="title">
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Text=" 方式二：创建新企业并加入 " Font-Bold="True"></asp:Label></td>
        </tr>
        <pe:Company ID="Company1" runat="server" />
        <tr align="center" class="tdbg">
            <td colspan="4">
                <asp:Button ID="BtnAppend" runat="server" Text="加入此企业" OnClick="BtnAppend_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
            </td>
        </tr>
    </table>
</asp:Content>
