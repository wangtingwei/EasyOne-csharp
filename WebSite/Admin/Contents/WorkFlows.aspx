<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Contents.WorkFlows" ValidateRequest="false"
    Title="添加流程" Codebehind="WorkFlows.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center" class="title">
            <td colspan="2">
                <pe:AlternateLiteral ID="AlternateLiteral1" Text="添加流程" AlternateText="修改流程" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="width: 15%;" class="tdbgleft">
                流程名称：</td>
            <td>
                <asp:TextBox ID="TxtFlowName" runat="server" Text="" Columns="50"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrFlowName" ControlToValidate="TxtFlowName" runat="server"
                    ErrorMessage="流程名称不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                流程描述：</td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" Height="90px" TextMode="MultiLine"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="height: 40px;" colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存流程" runat="server" OnClick="EBtnSubmit_Click" />
            </td>
        </tr>
        <asp:HiddenField ID="HdnFlowName" runat="server" />
        <asp:HiddenField ID="HdnAction" runat="server" />
    </table>
</asp:Content>
