<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Contents.FlowProcessUI" ValidateRequest="false"
    Title="添加流程步骤" Codebehind="FlowProcess.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <pe:AlternateLiteral ID="AlternateLiteral1" Text="添加流程步骤" AlternateText="修改流程步骤"
                    runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>所属流程：</strong></td>
            <td>
                <asp:Label ID="LblWorkFlows" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>流程的步骤名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtProcessName" runat="server" Width="300px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrProcessName" ControlToValidate="TxtProcessName"
                    runat="server" ErrorMessage="流程的步骤名称不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>流程步骤描述：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtDescription" runat="server" Height="90px" TextMode="MultiLine"
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>可以执行本步骤的角色：</strong></td>
            <td>
                <pe:ExtendedCheckBoxList ID="EChklProcessGroup" RepeatColumns="5" runat="server">
                </pe:ExtendedCheckBoxList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>可执行本操作的状态码：</strong><br />
                注意：可以按住Ctrl键进行多选</td>
            <td>
                <asp:ListBox ID="ListProcessStatusCode" runat="server" Height="161px" Width="206px"
                    SelectionMode="Multiple" DataTextField="StatusName" DataValueField="StatusCode">
                </asp:ListBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>审核通过的操作名：</strong></td>
            <td>
                <asp:TextBox ID="TxtPassActionName" runat="server" Width="200px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrPassActionName" ControlToValidate="TxtPassActionName"
                    runat="server" ErrorMessage="审核通过的操作名不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>审核通过的状态码：</strong></td>
            <td>
                <asp:DropDownList ID="DropPassActionStatus" runat="server" Width="206px" DataTextField="StatusName"
                    DataValueField="StatusCode">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>未通过审核的操作名：</strong></td>
            <td>
                <asp:TextBox ID="TxtRejectActionName" runat="server" Width="200px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrRejectActionName" ControlToValidate="TxtRejectActionName"
                    runat="server" ErrorMessage="未通过审核的操作名不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>未通过审核的状态码：</strong></td>
            <td>
                <asp:DropDownList ID="DropRejectActionStatus" Width="206px" runat="server" DataTextField="StatusName"
                    DataValueField="StatusCode">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存流程步骤" runat="server" OnClick="EBtnSubmit_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnProcessId" runat="server" />
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnFlowId" runat="server" />
</asp:Content>
