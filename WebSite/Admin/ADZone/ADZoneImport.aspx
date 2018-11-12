<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneImport" Title="����λ����" Codebehind="ADZoneImport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Wizard ID="WzdAd" runat="server" ActiveStepIndex="0" Width="100%" CellPadding="5"
        Font-Names="Verdana" Font-Size="0.8em" DisplaySideBar="False" OnNextButtonClick="WzdAd_NextButtonClick"
        OnFinishButtonClick="WzdAd_FinishButtonClick" FinishCompleteButtonText=" �� �� " CancelButtonText=" ȡ �� ">
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
                    <tr align="center">
                        <td colspan="2" class="spacingtitle">
                            <strong>����λ���루��һ����</strong></td>
                    </tr>
                    <tr align="left">
                        <td class="tdbg" style="height: 100px" valign="top">
                            <br />
                            <br />
                            ������Ҫ����Ĺ��λ���ݿ���ļ�����
                            <asp:TextBox ID="TxtImportMdb" runat="server" Width="332px">../../Temp/ADZone.mdb</asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td colspan="2" class="spacingtitle">
                            <b>����λ���루�ڶ�����</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="center" style="width: 685px; height: 100px">
                            <br />
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr align="center">
                                    <td>
                                        <strong>��������Ĺ���λ</strong><br />
                                        &nbsp;<asp:ListBox ID="LstImportZoneId" runat="server" Height="300px" Width="250px"
                                            DataTextField="ZoneName" DataValueField="ZoneId" SelectionMode="Multiple"></asp:ListBox></td>
                                    <td style="width: 120px" align="center">
                                        <asp:Button ID="BtnImport" runat="server" Text=" ����&gt;&gt; " OnClick="BtnImport_Click" />
                                        </td>
                                    <td>
                                        <strong>ϵͳ���Ѿ����ڵĹ���λ</strong><br />
                                        &nbsp;<asp:ListBox ID="LstSystemZoneId" runat="server" Height="300px" Width="250px"
                                            DataTextField="ZoneName" DataValueField="ZoneId" SelectionMode="Multiple"></asp:ListBox></td>
                                </tr>
                            </table>
                            <br />
                            <b>��ʾ����ס��Ctrl����Shift�������Զ�ѡ</b><br />
                            &nbsp;
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
