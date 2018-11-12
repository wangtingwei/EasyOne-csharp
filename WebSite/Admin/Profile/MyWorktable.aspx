<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.MyWorktable" Codebehind="MyWorktable.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的工作台</title>
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
                    <asp:LinkButton ID="LnkBrowseDisplayMode" runat="server" OnClick="LnkBrowseDisplayMode_Click">浏览</asp:LinkButton>
                    <asp:LinkButton ID="LnkDesignDisplayMode" runat="server" OnClick="LnkDesignDisplayMode_Click">排版</asp:LinkButton>
                    <asp:LinkButton ID="LnkEditDisplayMode" runat="server" OnClick="LnkEditDisplayMode_Click">编辑模块属性</asp:LinkButton>
                    <asp:LinkButton ID="LnkCatalogDisplayMode" runat="server" OnClick="LnkCatalogDisplayMode_Click">添加模块</asp:LinkButton></td>
            </tr>
        </table>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td valign="top" style="width: 50%">
                    <asp:WebPartZone ID="WpzLeft" Height="100%" PartChromeType="TitleOnly"  HeaderText="左侧"
                        Width="100%" runat="server">
                        <ZoneTemplate>
                            <pe:SystemDiagnostics ID="SystemDiagnostics1" OperateCode="administratormanage" Title="系统运行信息" runat="server" />
                            <pe:Content ID="Content" OperateCode="contentmanage" runat="server"></pe:Content>
<pe:SigninContent ID="SigninContent1" OperateCode="none" runat="server" Title="待签收内容">
                            </pe:SigninContent>
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td valign="top">
                    <asp:WebPartZone ID="WpzRight" HeaderStyle-CssClass="title" Height="100%" PartChromeType="TitleOnly"
                        HeaderText="右侧" Width="100%" runat="server">
                        <ZoneTemplate>
                            <pe:AjaxCalendar ID="AjaxCal1" OperateCode="none" runat="server" Title="日历"></pe:AjaxCalendar>
                            <pe:AjaxNote ID="AjaxNote1" OperateCode="none" runat="server" Title="备忘录"></pe:AjaxNote>
                            <pe:MyMessage ID="MyMessage" OperateCode="none" runat="server" Title="我的短信消息"></pe:MyMessage>
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td valign="top">
                    <asp:EditorZone ID="EdzMyWorktable" runat="server">
                        <ZoneTemplate>
                            <asp:PropertyGridEditorPart ID="PgepEditor" Title="属性" runat="server" />
                        </ZoneTemplate>
                    </asp:EditorZone>
                    <asp:CatalogZone ID="CtzMyWorktable" runat="server">
                        <ZoneTemplate>
                            <asp:PageCatalogPart ID="PcpPage" runat="server" Title="页中已关闭的模块" />
                            <asp:DeclarativeCatalogPart ID="DcpMyWorktable" runat="server" Title="模块">
                                <WebPartsTemplate>
                                    <pe:MyMessage ID="MyMessage" OperateCode="none" runat="server" Title="我的短信消息"></pe:MyMessage>
                                    <pe:Content ID="Content" OperateCode="contentmanage" runat="server" Title="内容信息"></pe:Content>
                                    <pe:Orders ID="Orders" OperateCode="OrderView" runat="server" Title="待处理的订单" />
                                    <pe:StockAlarm ID="StockAlarm" OperateCode="ProductManage" runat="server" Title="存库报警的商品" />
                                    <pe:SigninContent ID="SigninContent1" OperateCode="none" runat="server" Title="待签收内容">
                                    </pe:SigninContent>
                                    <pe:AjaxNote ID="AjaxNote1" OperateCode="none" runat="server" Title="备忘录"></pe:AjaxNote>
                                    <pe:AjaxCalendar ID="AjaxCal1" OperateCode="none" runat="server" Title="日历"></pe:AjaxCalendar>
                                    <pe:SystemDiagnostics ID="SystemDiagnostics1" OperateCode="administratormanage" Title="系统运行信息" runat="server" />
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
