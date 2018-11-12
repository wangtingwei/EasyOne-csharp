<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.CollectionFilter"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集过滤" ValidateRequest="false" Codebehind="CollectionFilter.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 300px">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="添加采集过滤" AlternateText="修改采集过滤" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>过滤名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtFilterName" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValeFilterName" ControlToValidate="TxtFilterName"
                    ErrorMessage="过滤名称不能为空！" runat="server" ValidationGroup="Filter"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" style="height: 300px" class="tdbg">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 300px">
                    <tr class="tdbg">
                        <td style="width: 45%; height: 350px" valign="top">
                            <asp:TextBox ID="TxtShowCode" runat="server" Height="320px" TextMode="MultiLine"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td style="height: 300px" valign="top">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                                <tr class="tdbg">
                                    <td style="height: 40%" valign="top">
                                        过滤指定代码↓<br />
                                        <asp:RadioButton ID="RadFilterType1" Text="简单过滤" GroupName="RadlPaingType" runat="server"
                                            Checked="true" OnCheckedChanged="RadFilterType_SelectedIndexChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="RadFilterType2" Text="高级过滤" GroupName="RadlPaingType" runat="server"
                                            OnCheckedChanged="RadFilterType_SelectedIndexChanged" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr class="tdbg" runat="server">
                                    <td style="height: 40%" valign="top">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                                            <tr class="tdbg">
                                                <td>
                                                    <asp:Label ID="LblFilterBegin" runat="server" Text="要过滤的代码：" />↓
                                                </td>
                                            </tr>
                                            <tr class="tdbg">
                                                <td>
                                                    <asp:TextBox ID="TxtFilterBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                        Width="80%"></asp:TextBox><br />
                                                    <pe:RequiredFieldValidator ID="ValeFilterBegin" ControlToValidate="TxtFilterBegin"
                                                        ErrorMessage="要过滤的开始代码不能为空！" runat="server" ValidationGroup="Filter"></pe:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tbody id="PnlFilterEnd" visible="false" runat="server">
                                                <tr class="tdbg">
                                                    <td>
                                                        要过滤的结束代码：↓
                                                    </td>
                                                </tr>
                                                <tr class="tdbg">
                                                    <td>
                                                        <asp:TextBox ID="TxtFilterEnd" runat="server" Height="80px" TextMode="MultiLine"
                                                            Width="80%"></asp:TextBox>
                                                        <br />
                                                        <pe:RequiredFieldValidator ID="ValeFilterEnd" ControlToValidate="TxtFilterEnd" ErrorMessage="要过滤的结束代码不能为空！"
                                                            runat="server" ValidationGroup="Filter"></pe:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tr class="tdbg">
                                                <td>
                                                    要替换的代码：↓
                                                </td>
                                            </tr>
                                            <tr class="tdbg">
                                                <td>
                                                    <asp:TextBox ID="TxtReplace" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Button ID="BtnReplace" runat="server" Text=" 预览 " OnClick="BtnReplace_Click"
                                            ValidationGroup="Filter" />
                                        <asp:Button ID="BtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" ValidationGroup="Filter" />
                                        &nbsp;&nbsp;
                                        <input name="Cancel" type="button" class="inputbutton" id="Cancel2" value="取消" onclick="Redirect('CollectionFilterManage.aspx')" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnFilter" runat="server" />
    <asp:HiddenField ID="HdnFilterName" runat="server" />
</asp:Content>
