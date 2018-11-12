<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.OutLink" ValidateRequest="false" Title="单页外部链接" Codebehind="OutLink.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="添加外部链接" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>所属节点：</strong></td>
            <td>
                <asp:DropDownList ID="DropParentNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="根节点" Value="0"></asp:ListItem>
                </asp:DropDownList><asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label>
                <asp:Label ID="LblNodePermissions" runat="server" Text=""></asp:Label>
             
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="外部链接名称不能为空！"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接标识符：</strong><br />
                用于前台调用时可以直接用标识符取代ID</td>
            <td>
                <asp:TextBox ID="TxtNodeIdentifier" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="标识符不能为空！"
                    ControlToValidate="TxtNodeIdentifier" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接地址：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtLinkUrl" runat="server" Width="289px"></asp:TextBox><pe:UrlValidator
                    ID="Vurl" ControlToValidate="TxtLinkUrl" runat="server"></pe:UrlValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>打开方式：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlOpenType" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="在原窗口打开" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="在新窗口打开" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接图片地址：</strong><br />
                用于在外部链接页显示指定的图片</td>
            <td style="width: 498px">
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Width="289px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>外部链接提示：</strong><br />
                鼠标移至外部链接名称上时将显示设定的提示文字（不支持HTML）</td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="56px" Width="289px"
                    Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否位置导航处显示：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="是" Selected="True" Value="True"></asp:ListItem>
                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消"
                    onclick="Redirect('CategoryManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
