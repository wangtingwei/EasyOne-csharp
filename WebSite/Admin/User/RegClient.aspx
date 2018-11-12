<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RegClient" Title="无标题页" Codebehind="RegClient.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider">
                </pe:ExtendedSiteMapPath>
            </td>
            <td style="text-align: right">
                <asp:Label ID="LblUserName" runat="server"></asp:Label>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center" class="title">
            <td colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text=" 方式一：将会员加入已有企业客户的联系人" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 40%;" class="tdbgleft">
                请选择要加入的企业客户：</td>
            <td align="left">
                <pe:CrmSelectControl ID="ClientSelect" ReadOnly="true" runat="server" ButtonText=" ... " FileUrl="~/Admin/Crm/ClientList.aspx" UrlArgs="searchType=5" />
                <pe:RequiredFieldValidator ID="ValrClientId" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="Type1"
                                ControlToValidate="ClientSelect" Display="Dynamic">请选择对应的企业客户</pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td colspan="2">
                <asp:Button ID="BtnRegister" runat="server" Text="加入" ValidationGroup="Type1"
                    OnClick="BtnRegister_Click" />&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
            </td>
        </tr>
    </table>
    <br />
    <table  border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr align="center" class="title">
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Text=" 方式二：将会员升级为客户" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width:12%">
                助记名称：</td>
            <td>
                <asp:TextBox ID="TxtShortedForm" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrShortedForm" runat="server" ControlToValidate="TxtShortedForm"
                    Display="Dynamic" ErrorMessage="客户简称（助记码）不能为空！"></pe:RequiredFieldValidator></td>
            <td align="right" class="tdbgleft" style="width:12%" >
                客户编号：</td>
            <td style="width:38%">
                <asp:TextBox ID="TxtClientNum" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                区域：</td>
            <td>
                <asp:DropDownList ID="DropArea" runat="server" Width="165px">
                </asp:DropDownList></td>
            <td align="right" class="tdbgleft">
                行业：</td>
            <td>
                <asp:DropDownList ID="DropClientField" runat="server" Width="165px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                价值评估：</td>
            <td>
                <asp:DropDownList ID="DropValueLevel" runat="server" Width="165px">
                </asp:DropDownList></td>
            <td align="right" class="tdbgleft">
                信用等级：</td>
            <td>
                <asp:DropDownList ID="DropCreditLevel" runat="server" Width="165px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                重要程度：</td>
            <td>
                <asp:DropDownList ID="DropImportance" runat="server" Width="165px">
                </asp:DropDownList></td>
            <td align="right" class="tdbgleft">
                关系等级：</td>
            <td>
                <asp:DropDownList ID="DropConnectionLevel" runat="server" Width="165px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                客户来源：</td>
            <td>
                <asp:DropDownList ID="DropSourceType" runat="server" Width="165px">
                </asp:DropDownList></td>
            <td align="right" class="tdbgleft">
                阶段：</td>
            <td>
                <asp:DropDownList ID="DropPhaseType" runat="server" Width="165px">
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                客户组别：</td>
            <td>
                <asp:DropDownList ID="DropGroupID" runat="server" Width="165px">
                </asp:DropDownList></td>
            <td align="right" class="tdbgleft">
                客户类别：</td>
            <td>
                <asp:Label ID="LblClientType" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg" style=" height:50px">
            <td align="center" colspan="10">
                <asp:Button ID="BtnSave" runat="server" Text="保存客户信息" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnClientName" runat="server" />
    <asp:HiddenField ID="HdnClientType" runat="server" />
</asp:Content>
