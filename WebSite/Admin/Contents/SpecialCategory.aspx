<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Title="添加专题类别" Inherits="EasyOne.WebSite.Admin.Contents.SpecialCategory" Codebehind="SpecialCategory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg">
            <td class="spacingtitle" colspan="2" align="center">
                <pe:AlternateLiteral ID="AltrTitle" Text="添加专题类别" AlternateText="修改专题类别" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别名称：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtSpecialCategoryName" Width="155px" MaxLength="255" runat="server"
                    Text=""></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSpecialCategoryName" ControlToValidate="TxtSpecialCategoryName"
                    runat="server" Display="dynamic" ErrorMessage="专题类别名称不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别目录名：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtSpecialCategoryDir" MaxLength="20" Width="155px" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtSpecialCategoryDir"
                    Display="dynamic" runat="server" ErrorMessage="目录名不能为空"></pe:RequiredFieldValidator>
                <span style="color: Blue">注意，目录名只能是字母、数字、下划线组成
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtSpecialCategoryDir"
                        Display="Dynamic" ValidationExpression="[_a-zA-Z0-9]*" ErrorMessage="注意，目录名只能是字母、数字、下划线组成"
                        SetFocusOnError="True"></asp:RegularExpressionValidator></span><br />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别描述：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtDescription" runat="server" Height="90px" TextMode="MultiLine"
                    Width="297px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否在新窗口打开：</strong></td>
            <td align="left">
                <asp:RadioButtonList ID="RadOpenType" runat="server" Height="3px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True" Selected="True">在原窗口打开</asp:ListItem>
                    <asp:ListItem Value="False">在新窗口打开</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否生成静态页：</strong></td>
            <td align="left">
                <asp:RadioButtonList ID="RadlCreatHtml" runat="server" Height="3px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="False">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>生成静态页的扩展名：</strong></td>
            <td align="left">
                <pe:ComboBox ID="PagePostfix" runat="server">
                    <Items>
                        <asp:ListItem>html</asp:ListItem>
                        <asp:ListItem>htm</asp:ListItem>
                        <asp:ListItem>shtml</asp:ListItem>
                        <asp:ListItem>shtm</asp:ListItem>
                    </Items>
                </pe:ComboBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题列表页模板：</strong>
            </td>
            <td align="left">
                <pe:TemplateSelectControl ID="FileCSpecialTemplatePath" Width="300px" runat="server"></pe:TemplateSelectControl>
            </td>
        </tr>
                <tr class="tdbg">
            <td class="tdbgleft">
                <strong>专题类别搜索页模板：</strong>
            </td>
            <td align="left">
                <pe:TemplateSelectControl ID="FileCSearchTemplatePath" Width="300px" runat="server"></pe:TemplateSelectControl>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="height: 30px;" colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存专题类别" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消"
                    onclick="Redirect('SpecialCategoryManage.aspx')" />
            </td>
        </tr>
        <asp:HiddenField ID="HdnSpecialCategoryName" runat="server" />
        <asp:HiddenField ID="HdnAction" runat="server" />
        <asp:HiddenField ID="HdnOrderId" runat="server" />
    </table>
</asp:Content>
