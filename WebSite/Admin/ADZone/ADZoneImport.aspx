<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneImport" Title="广告版位导入" Codebehind="ADZoneImport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Wizard ID="WzdAd" runat="server" ActiveStepIndex="0" Width="100%" CellPadding="5"
        Font-Names="Verdana" Font-Size="0.8em" DisplaySideBar="False" OnNextButtonClick="WzdAd_NextButtonClick"
        OnFinishButtonClick="WzdAd_FinishButtonClick" FinishCompleteButtonText=" 完 成 " CancelButtonText=" 取 消 ">
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
                    <tr align="center">
                        <td colspan="2" class="spacingtitle">
                            <strong>广告版位导入（第一步）</strong></td>
                    </tr>
                    <tr align="left">
                        <td class="tdbg" style="height: 100px" valign="top">
                            <br />
                            <br />
                            请输入要导入的广告位数据库的文件名：
                            <asp:TextBox ID="TxtImportMdb" runat="server" Width="332px">../../Temp/ADZone.mdb</asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td colspan="2" class="spacingtitle">
                            <b>广告版位导入（第二步）</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="center" style="width: 685px; height: 100px">
                            <br />
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr align="center">
                                    <td>
                                        <strong>将被导入的广告版位</strong><br />
                                        &nbsp;<asp:ListBox ID="LstImportZoneId" runat="server" Height="300px" Width="250px"
                                            DataTextField="ZoneName" DataValueField="ZoneId" SelectionMode="Multiple"></asp:ListBox></td>
                                    <td style="width: 120px" align="center">
                                        <asp:Button ID="BtnImport" runat="server" Text=" 导入&gt;&gt; " OnClick="BtnImport_Click" />
                                        </td>
                                    <td>
                                        <strong>系统中已经存在的广告版位</strong><br />
                                        &nbsp;<asp:ListBox ID="LstSystemZoneId" runat="server" Height="300px" Width="250px"
                                            DataTextField="ZoneName" DataValueField="ZoneId" SelectionMode="Multiple"></asp:ListBox></td>
                                </tr>
                            </table>
                            <br />
                            <b>提示：按住“Ctrl”或“Shift”键可以多选</b><br />
                            &nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
