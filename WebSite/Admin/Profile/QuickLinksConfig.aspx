<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.QuickLinksConfig" Codebehind="QuickLinksConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    
    <style type="text/css">
    div.AlertStyle
    {
      background-color: #FFC080;
      top:90%;
      left: 1%;
      height: 20px;
      width: 270px;
      position: absolute;
      visibility: hidden;
    }
    .Linked
    {
     background-color: #FFC080;
    }
	</style>
        <asp:ScriptManager ID="SmQuickLinksConfig" runat="server">
            <Services>
                <asp:ServiceReference Path="~/WebServices/QuickLinksConfig.asmx" />
            </Services>
        </asp:ScriptManager>

        <script type="text/javascript" language="javascript">
    var tempId = '';
    var tempLeftUrl = '';
    var tempRightUrl = '';
    var tempUrlTitle = '';
    
    function ActivateAlertDiv(visstring, elem, msg)
    {
        var adiv = $get(elem);
        adiv.style.visibility = visstring;
        adiv.innerHTML = msg;                     
    }
                
    function AddLink(id,leftUrl,rightUrl,urlTitle)
    {
        ActivateAlertDiv('visible', 'AlertDiv',  '正在加载...');
        tempId = id;
        tempLeftUrl = leftUrl;
        tempRightUrl = rightUrl;
        tempUrlTitle = urlTitle;
        EasyOne.WebSite.Admin.Profile.QuickLinksConfig.AddLink(id,onAddLinkCompleted);
    }
    
    function DeleteLink(id,leftUrl,rightUrl,urlTitle)
    {
        ActivateAlertDiv('visible', 'AlertDiv',  '正在加载...');
        tempId = id;
        tempLeftUrl = leftUrl;
        tempRightUrl = rightUrl;
        tempUrlTitle = urlTitle;
        EasyOne.WebSite.Admin.Profile.QuickLinksConfig.DeleteLink(id,onDeleteLinkCompleted);
    }
                
    function onDeleteLinkCompleted(value)
    {
        ActivateAlertDiv('hidden', 'AlertDiv', '');
        if(value == 'true')
        {
            var linkli = document.getElementById(tempId+'Link');
            url = "javascript:AddLink('"+tempId+"','"+tempLeftUrl+"','"+tempRightUrl+"','"+tempUrlTitle+"');";
            linkli.setAttribute("href",url);
            var linkStatus = document.getElementById(tempId+'LinkStatus');
            linkStatus.innerHTML = '';
            try
            {
                parent.frames['left'].DeleteLink(tempId);
            }
            catch(err)
            {}
        }
    }
                
    function onAddLinkCompleted(value)
    {
        ActivateAlertDiv('hidden', 'AlertDiv', '');
        if(value == 'true')
        {
            var linkli = document.getElementById(tempId+'Link');
            url = "javascript:DeleteLink('"+tempId+"','"+tempLeftUrl+"','"+tempRightUrl+"','"+tempUrlTitle+"');";
            linkli.setAttribute("href",url);
            var linkStatus = document.getElementById(tempId+'LinkStatus');
            linkStatus.innerHTML = '<b>√</b>';
            try
            {
            parent.frames['left'].AddLink(tempId,tempLeftUrl,tempRightUrl,tempUrlTitle);
            }
            catch(err)
            {}
        }
    }
        </script>

        <table border="0" cellpadding="2" style="text-align: center" cellspacing="1" width="100%"
            class="border">
            <tr align="center">
                <td class="spacingtitle">
                    快捷导航配置</td>
            </tr>
            <asp:Repeater ID="RptQuickLinks" runat="server" OnItemDataBound="RptQuickLinks_ItemDataBound">
                <ItemTemplate>
                    <tr align="left" class="tdbgleft">
                        <td height="30">
                            <strong>
                                <asp:Literal ID="LitModuleDescription" runat="server"></asp:Literal></strong>
                        </td>
                    </tr>
                    <tr align="left" class="tdbg">
                        <td>
                            <asp:Repeater ID="RptQuickMainLink" runat="server">
                                <ItemTemplate>
                                    <asp:PlaceHolder ID="PlhQuickLink" runat="server" Visible="false">
                                        <div class="quickLink">
                                            <ul>
                                                <pe:ExtendedLiteral HtmlEncode="false" ID="LitQuickLink" runat="server"></pe:ExtendedLiteral>
                                            </ul>
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:Repeater ID="RptQuickLink" runat="server">
                                        <HeaderTemplate>
                                            <div style="clear: both;">
                                                <fieldset>
                                                    <legend><span style="color: Green">
                                                        <pe:ExtendedLiteral HtmlEncode="false" ID="LitMainLink" runat="server"></pe:ExtendedLiteral></span></legend>
                                                    <div class="quickLink">
                                                        <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <pe:ExtendedLiteral HtmlEncode="false" ID="LitLink" runat="server"></pe:ExtendedLiteral>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul></div></fieldset></div></FooterTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div id="AlertDiv" class="AlertStyle">
        </div>
</asp:Content>