<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.MyWorktable" Codebehind="MyWorktable.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ҵĹ���̨</title>
</head>
<body id="MasterPagebody">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptMyWorktable" runat="server" EnablePartialRendering="true">
            <Services>
                <asp:ServiceReference Path="~/WebServices/GetSystemDiagnostics.asmx" />
            </Services>
        </asp:ScriptManager>
        <pe:AdminWebPartManager ID="WpmMyWorktable" runat="server">
        </pe:AdminWebPartManager>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td>
                    <pe:ExtendedLiteral HtmlEncode="false"  ID="LitHelp" runat="server"></pe:ExtendedLiteral></td>
                <td align="right">
                    <asp:LinkButton ID="LnkBrowseDisplayMode" runat="server" OnClick="LnkBrowseDisplayMode_Click">���</asp:LinkButton>
                    <asp:LinkButton ID="LnkDesignDisplayMode" runat="server" OnClick="LnkDesignDisplayMode_Click">�Ű�</asp:LinkButton>
                    <asp:LinkButton ID="LnkEditDisplayMode" runat="server" OnClick="LnkEditDisplayMode_Click">�༭ģ������</asp:LinkButton>
                    <asp:LinkButton ID="LnkCatalogDisplayMode" runat="server" OnClick="LnkCatalogDisplayMode_Click">���ģ��</asp:LinkButton></td>
            </tr>
        </table>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td valign="top" style="width: 50%">
                    <asp:WebPartZone ID="WpzLeft" Height="100%" PartChromeType="TitleOnly"  HeaderText="���"
                        Width="100%" runat="server">
                        <ZoneTemplate>
                            <pe:SystemDiagnostics ID="SystemDiagnostics1" OperateCode="administratormanage" Title="ϵͳ������Ϣ" runat="server" />
                            <pe:Content ID="Content" OperateCode="contentmanage" runat="server"></pe:Content>
<pe:SigninContent ID="SigninContent1" OperateCode="none" runat="server" Title="��ǩ������">
                            </pe:SigninContent>
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td valign="top">
                    <asp:WebPartZone ID="WpzRight" HeaderStyle-CssClass="title" Height="100%" PartChromeType="TitleOnly"
                        HeaderText="�Ҳ�" Width="100%" runat="server">
                        <ZoneTemplate>
                            <pe:AjaxCalendar ID="AjaxCal1" OperateCode="none" runat="server" Title="����"></pe:AjaxCalendar>
                            <pe:AjaxNote ID="AjaxNote1" OperateCode="none" runat="server" Title="����¼"></pe:AjaxNote>
                            <pe:MyMessage ID="MyMessage" OperateCode="none" runat="server" Title="�ҵĶ�����Ϣ"></pe:MyMessage>
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td valign="top">
                    <asp:EditorZone ID="EdzMyWorktable" runat="server">
                        <ZoneTemplate>
                            <asp:PropertyGridEditorPart ID="PgepEditor" Title="����" runat="server" />
                        </ZoneTemplate>
                    </asp:EditorZone>
                    <asp:CatalogZone ID="CtzMyWorktable" runat="server">
                        <ZoneTemplate>
                            <asp:PageCatalogPart ID="PcpPage" runat="server" Title="ҳ���ѹرյ�ģ��" />
                            <asp:DeclarativeCatalogPart ID="DcpMyWorktable" runat="server" Title="ģ��">
                                <WebPartsTemplate>
                                    <pe:MyMessage ID="MyMessage" OperateCode="none" runat="server" Title="�ҵĶ�����Ϣ"></pe:MyMessage>
                                    <pe:Content ID="Content" OperateCode="contentmanage" runat="server" Title="������Ϣ"></pe:Content>
                                    <pe:Orders ID="Orders" OperateCode="OrderView" runat="server" Title="������Ķ���" />
                                    <pe:StockAlarm ID="StockAlarm" OperateCode="ProductManage" runat="server" Title="��ⱨ������Ʒ" />
                                    <pe:SigninContent ID="SigninContent1" OperateCode="none" runat="server" Title="��ǩ������">
                                    </pe:SigninContent>
                                    <pe:AjaxNote ID="AjaxNote1" OperateCode="none" runat="server" Title="����¼"></pe:AjaxNote>
                                    <pe:AjaxCalendar ID="AjaxCal1" OperateCode="none" runat="server" Title="����"></pe:AjaxCalendar>
                                    <pe:SystemDiagnostics ID="SystemDiagnostics1" OperateCode="administratormanage" Title="ϵͳ������Ϣ" runat="server" />
                                </WebPartsTemplate>
                            </asp:DeclarativeCatalogPart>
                        </ZoneTemplate>
                    </asp:CatalogZone>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function EndRequestHandler(sender, args)
{
   if (args.get_error() != undefined)
   {
       args.set_errorHandled(true);
   }
}

        function OpenLink(FileName_Left,FileName_Right)
        {
            if(parent.document.getElementById("left").src != FileName_Left)
            {
                parent.document.getElementById("left").src=FileName_Left;
            }
            parent.document.getElementById("main_right").src=FileName_Right;
        }
        </script>

    </form>
</body>
</html>
