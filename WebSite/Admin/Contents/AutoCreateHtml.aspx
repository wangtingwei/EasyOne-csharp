<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.AutoCreateHtml"
    Title="自动生成配置" Codebehind="AutoCreateHtml.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                自动生成配置
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 50%">
                <strong>开启自动生成：</strong>
            </td>
            <td>
                <asp:CheckBox ID="ChkIsEnable" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>异常时允许自动关闭：</strong>
            </td>
            <td>
                <asp:CheckBox ID="ChkIsShutDown" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>程序自动执行的间隔时间：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtMinutes" runat="server"></asp:TextBox>分
                <pe:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtMinutes"
                    runat="server" ErrorMessage="请填写执行间隔时间！" SetFocusOnError="true" Display="Dynamic" />
                <pe:NumberValidator ID="NumberValidator1" ControlToValidate="TxtMinutes" runat="server"
                    ErrorMessage="执行时间必须大于等于零！" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>每次自动生成的篇数：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtNumber" runat="server"></asp:TextBox>篇
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtNumber"
                    runat="server" ErrorMessage="请填写每次自动生成的篇数！" SetFocusOnError="true" Display="Dynamic" />
                <pe:NumberValidator ID="Vnum" ControlToValidate="TxtNumber" runat="server" ErrorMessage="自动生成的篇数必须大于零！"
                    SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选择栏目：</strong>
                <br />
                <br />
                如果不选，则从所有信息中生成最新未生成的指定信息数。
                <br />
                如果选择，则只生成所选栏目的最新未生成的指定信息数。
                <br />
                按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择。
            </td>
            <td>
                <asp:CheckBox ID="ChkCateogry" runat="server" />同时自动生成选中的栏目页<br />
                <br />
                <asp:ListBox ID="LstNodes" runat="server" Width="200px" Height="200px" SelectionMode="Multiple"
                    DataTextField="NodeName" DataValueField="NodeId" ToolTip="按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否自动生成首页：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadLIndexPage" RepeatDirection="horizontal" runat="server">
                    <asp:ListItem Value="true" Selected="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="CreateCategoryListById" runat="server" Text="保存配置" OnClick="CreateCategoryListById_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
