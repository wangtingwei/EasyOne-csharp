<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelTemplateImport"
    Title="模型模板管理" Codebehind="ModelTemplateImport.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Wizard ID="WzdSkin" runat="server" ActiveStepIndex="0" Width="100%" CellPadding="5"
        Font-Names="Verdana" Font-Size="0.8em" DisplaySideBar="False" OnNextButtonClick="WzdModelTemplate_NextButtonClick"
        OnFinishButtonClick="WzdModelTemplate_FinishButtonClick" FinishCompleteButtonText=" 完 成 ">
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
                    <tr align="center">
                        <td colspan="2" class="title">
                            <b>模型模板导入（第一步）</b>
                        </td>
                    </tr>
                    <tr align="left">
                        <td class="tdbg" valign="top" style="height: 100px">
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;请输入要导入的模型模板数据库的文件名：
                            <asp:TextBox ID="TxtModelTemplateMdb" runat="server" Width="332px">../../Temp/ModelTemplate.mdb</asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td class="title">
                            <b>模型模板导入（第二步）</b>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="center" style="width: 685px; height: 100px">
                            <br />
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr align="center">
                                    <td>
                                        <strong>将被导入的模型模板</strong><br />
                                        &nbsp;<asp:ListBox ID="LstImportTemplateId" runat="server" Height="300px" Width="250px"
                                            DataTextField="TemplateName" DataValueField="TemplateId" SelectionMode="Multiple">
                                        </asp:ListBox></td>
                                    <td style="width: 100px;" align="center">
                                        &nbsp;<asp:Button ID="BtnImport" runat="server" Text=" 导入>> " OnClick="BtnImport_Click" />
                                    </td>
                                    <td>
                                        <strong>系统中已经存在的模型模板</strong><br />
                                        &nbsp;<asp:ListBox ID="LstSystemTemplateId" runat="server" Height="300px" Width="250px"
                                            DataTextField="TemplateName" DataValueField="TemplateId" SelectionMode="Multiple">
                                        </asp:ListBox></td>
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
