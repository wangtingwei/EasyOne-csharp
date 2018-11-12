<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Regions" Title="添加行政区划" Codebehind="Region.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="添加行政区划" AlternateText="修改行政区划" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 40%">
                <strong>所属国家：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtCountry" runat="server" Width="200" Text="中华人民共和国"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrCountry" runat="server" ControlToValidate="TxtCountry"
                    ErrorMessage="RequiredFieldValidator" Display="Dynamic">所属国家不能为空！</pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>所属省份：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtProvince" runat="server" Width="200"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrProvince" runat="server" ControlToValidate="TxtProvince"
                    ErrorMessage="RequiredFieldValidator" Display="Dynamic">所属省份不能为空！</pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>所属城市：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtCity" runat="server" Width="200"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>所属县区：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtArea" runat="server" Width="200"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>邮政编码：</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtPostCode" runat="server" Width="200"></asp:TextBox>&nbsp;<pe:RequiredFieldValidator
                    ID="ValrPostCode" runat="server" ControlToValidate="TxtPostCode" ErrorMessage="RequiredFieldValidator"
                    Display="Dynamic">邮政编码不能为空！</pe:RequiredFieldValidator>
                <pe:ZipCodeValidator ID="Vzip" runat="server" ControlToValidate="TxtPostCode" Display="Dynamic"
                    ErrorMessage="请输入有效的邮政编码！" SetFocusOnError="True"></pe:ZipCodeValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>地区区号：</strong>
            </td>
            <td class="tdbg" style="height: 27px">
                <asp:TextBox ID="TxtAreaCode" runat="server" Width="200"></asp:TextBox>&nbsp;<pe:RequiredFieldValidator
                    ID="ValrAreaCode" runat="server" ControlToValidate="TxtAreaCode" ErrorMessage="RequiredFieldValidator"
                    Display="Dynamic">区号不能为空！</pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeAreaCode" runat="server" ControlToValidate="TxtAreaCode"
                    Display="Dynamic" ErrorMessage="请输入有效的区号！" ValidationExpression="\d{3,5}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSubmit_Click" Text="确定" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('RegionManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
