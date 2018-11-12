<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlContent"
    ValidateRequest="false" Title="生成内容" Codebehind="CreateHtmlContent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2" align="center">
                生成内容页
            </td>
        </tr>
        <tr>
            <td rowspan="8" class="tdbg" align="left" valign="top">
                <table>
                    <tr>
                        <td align="right">
                            是否同时生成子栏目下的信息：</td>
                        <td>
                            <asp:RadioButtonList ID="RdlIsCreateChild" RepeatDirection="horizontal" RepeatLayout="flow"
                                runat="server">
                                <asp:ListItem Selected="true" Text="是" Value="true"></asp:ListItem>
                                <asp:ListItem Text="否" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        
        </tr>
        <tr>
            <td align="right">
                请选择所属栏目：<br />
                <span style="color: Blue">（如未选择则指定为所有栏目）</span>
                </td><td>
                <asp:ListBox ID="LstNodes" runat="server" Height="275px" Width="200px" SelectionMode="Multiple"
                    DataTextField="NodeName" DataValueField="NodeId" ToolTip="按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                
            </td>
        </tr>
    </table>
    </td>
    <td class="tdbg">
        生成最新
        <asp:TextBox ID="TxtTopNew" runat="server" Width="58px" ValidationGroup="TopNew" />个项目&nbsp;&nbsp;
        <asp:Button ID="EBtnTopNew" Text="开始生成 >> " OnClick="EBtnTopNew_Click" runat="server"
            ValidationGroup="TopNew" />
        <pe:RequiredFieldValidator ID="ValrTopNew" ControlToValidate="TxtTopNew" ValidationGroup="TopNew"
            runat="server" ErrorMessage="生成最新不能为空" ShowRequiredText="false"></pe:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
        <td class="tdbg">
            生成更新时间从
            <pe:DatePicker ID="DpkBeginDate" runat="server" Width="80px" ValidationGroup="Date"></pe:DatePicker>
            到
            <pe:DatePicker ID="DpkEndDate" runat="server" Width="80px" ValidationGroup="Date"></pe:DatePicker>
            的项目
            <asp:Button ID="EBtnDate" Text="开始生成 >> " OnClick="EBtnDate_Click" runat="server"
                ValidationGroup="Date" />
            <pe:RequiredFieldValidator ID="ValrBeginDate" ControlToValidate="DpkBeginDate" ValidationGroup="Date"
                Display="Dynamic" runat="server" ErrorMessage="开始时间不能为空" ShowRequiredText="false" />
            <pe:RequiredFieldValidator ID="ValrEndDate" ControlToValidate="DpkEndDate" ValidationGroup="Date"
                Display="Dynamic" runat="server" ErrorMessage="结束时间不能为空" ShowRequiredText="false" />
            <asp:CompareValidator ID="CompareValidator1" Operator="GreaterThan" ValidationGroup="Date"
                Type="Date" ControlToValidate="DpkEndDate" ControlToCompare="DpkBeginDate" Display="Dynamic"
                runat="server" ErrorMessage="开始时间不能小于结束时间！" />
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            生成ID号从
            <asp:TextBox ID="TxtBeginId" runat="server" Width="59px" ValidationGroup="BeginId" />
            到
            <asp:TextBox ID="TxtEndId" runat="server" Width="60px" ValidationGroup="BeginId" />
            的项目
            <asp:Button ID="EBtnBoundId" Text="开始生成 >> " OnClick="EBtnBoundId_Click" runat="server"
                ValidationGroup="BeginId" />
            <pe:RequiredFieldValidator ID="ValrBeginId" ControlToValidate="TxtBeginId" ValidationGroup="BeginId"
                Display="Dynamic" runat="server" ErrorMessage="起始ID不能为空" ShowRequiredText="false"></pe:RequiredFieldValidator>
            <pe:RequiredFieldValidator ID="ValrEndId" ControlToValidate="TxtEndId" ValidationGroup="BeginId"
                Display="Dynamic" runat="server" ErrorMessage="结束ID不能为空" ShowRequiredText="false"></pe:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" Operator="GreaterThanEqual" Type="Integer"
                ControlToValidate="TxtEndId" ControlToCompare="TxtBeginId" runat="server" ErrorMessage="开始ID不能小于结束ID！"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            生成指定ID的项目（多个ID可以用逗号隔开）
            <asp:TextBox ID="TxtAppointId" runat="server" ValidationGroup="AppointId" />
            <asp:Button ID="EBtnAppointId" Text="开始生成 >> " OnClick="EBtnAppointId_Click" runat="server"
                ValidationGroup="AppointId" />
            <pe:RequiredFieldValidator ID="ValrAppointId" ControlToValidate="TxtAppointId" ValidationGroup="AppointId"
                Display="Dynamic" runat="server" ErrorMessage="指定ID不能为空" ShowRequiredText="false"></pe:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            生成所有未生成的项目
            <asp:Button ID="EBtnNotCreate" Text="开始生成 >> " OnClick="EBtnNotCreate_Click" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            生成所有项目
            <asp:Button ID="EBtnAll" Text="开始生成 >> " OnClick="EBtnAll_Click" runat="server" />
        </td>
    </tr>
    </table>
    <br />
    <span style="color: Blue"><strong>注意：</strong></span>如果选择了栏目，则只生成选择栏目下的内容；如果不选择直接生成的，则生成全站的内容。
    <%--    <script type="text/javascript" language="javascript">
    function StartCreate()
    {
        document.getElementById("create").height=100;
    }
    </script>--%>
</asp:Content>
