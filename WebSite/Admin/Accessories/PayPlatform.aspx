<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatforms" Codebehind="PayPlatform.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="添加支付平台" AlternateText="修改支付平台" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                平台名称：</td>
            <td>
                <asp:TextBox ID="TxtPlatformName" runat="server" MaxLength="50"></asp:TextBox>
                <span style="color: #000000">
                    <pe:RequiredFieldValidator ID="ValrPlatformName" runat="server" ControlToValidate="TxtPlatformName"
                        ErrorMessage="平台名称不能为空" Display="Dynamic"></pe:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                商户ID：</td>
            <td>
                <asp:TextBox ID="TxtAccountsID" runat="server" MaxLength="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrAccountsID" runat="server" ControlToValidate="TxtAccountsID"
                    ErrorMessage="商户ID不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                MD5密钥：</td>
            <td>
                <asp:TextBox ID="TxtMD5" runat="server" MaxLength="255"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrMD5" runat="server" ControlToValidate="TxtMD5"
                    ErrorMessage="MD5密钥不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td width="30%" align="right">
                手续费率：</td>
            <td>
                <asp:TextBox ID="TxtRate" runat="server" Width="59px" MaxLength="6"></asp:TextBox>%
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" width="30%">
                设为默认：</td>
            <td>
                <input type="checkbox" id="ChkIsDefault" runat="server" onclick="DefalutChange()" />
                &nbsp; &nbsp; 禁用：<input type="checkbox" id="ChkIsDisabled" runat="server" /></td>
        </tr>
        <tr align="center" class="tdbg">
            <td height="50" colspan="2">
                <pe:AlternateButton ID="BtnSubmit" runat="server" Text="添加支付平台" AlternateText="修改支付平台"
                    OnClick="BtnSubmit_Click" />
                <input type="button" id="return" onclick="window.location.href='PayPlatformManage.aspx';" class="inputbutton" value="返回" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function DefalutChange()
        {
            document.getElementById('<%=ChkIsDisabled.ClientID %>').disabled = document.getElementById('<%=ChkIsDefault.ClientID %>').checked;
            if(document.getElementById('<%=ChkIsDefault.ClientID %>').checked)
            {
                document.getElementById('<%=ChkIsDisabled.ClientID %>').checked = false;
            }
        }
    </script>

</asp:Content>
