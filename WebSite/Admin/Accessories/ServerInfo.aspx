<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.ServerInfo"
    AspCompat="true" Title="服务器信息" Codebehind="ServerInfo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:ScriptManager ID="JavaScripts" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
      function ShowTabs(ID){
           for (i=0;i<=3;i++){
                if(i == ID){
                    $get("TabTitle" + i).className="titlemouseover";
                    $get("Tab" + i).style.display="";
                }
                else{
                    $get("TabTitle" + i).className="tabtitle";
                    $get("Tab" + i).style.display="none";
                }
           }
      }
      
      function CallServer(arg, context)
      {
        <%= ClientScript.GetCallbackEventReference(this, "arg", "receiveServerData", "context")%>;
      }
      
      function receiveServerData(result, context){
        $get("checkResult").innerHTML = result;
        $get("CustomCheckRow").style.display = "";
      }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                服务器概况</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" style="width: 80px">
                组件支持</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                环境变量</td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                空间占用
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <div id="Tab0">
                    <div class="title" style="float: left; width: 30%; text-indent: 30px;">
                        项目</div>
                    <div class="title" style="margin-left: 30%; text-indent: 30px; border-left: solid 1px #fff;
                        width: auto;">
                        值</div>
                    <div style="clear: both;">
                    </div>
                    <table cellpadding="3" cellspacing="1" style="width: 100%; margin: auto;" class="border">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="width: 30%">
                                <strong>服务器名称/主机域名：</strong></td>
                            <td>
                                <asp:Label ID="LblServerName" runat="server"></asp:Label>
                                /
                                <asp:Label ID="LblSiteDomain" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>服务器IP及端口：</strong></td>
                            <td>
                                <asp:Label ID="LblSiteIP" runat="server"></asp:Label>:<asp:Label ID="LblSitePort"
                                    runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>服务器操作系统：</strong></td>
                            <td>
                                <asp:Label ID="LblOsVersion" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>信息服务软件版本：</strong></td>
                            <td>
                                <asp:Label ID="LblServerSoft" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>服务器时间及时区：</strong></td>
                            <td>
                                时区：<asp:Label ID="LblTimeZone" runat="server"></asp:Label>当前时间：<asp:Label ID="LblNowTime"
                                    runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>服务器CPU种类（型号）：</strong></td>
                            <td>
                                类型：<asp:Label ID="LblCpuType" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>服务器CPU个数：</strong></td>
                            <td>
                                <asp:Label ID="LblCpuNumber" runat="server"></asp:Label>
                                个</td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>脚本超时时间：</strong></td>
                            <td>
                                <asp:Label ID="LblScriptTimeout" runat="server"></asp:Label>
                                秒</td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>站点占用内存：</strong></td>
                            <td>
                                <asp:Label ID="LblSiteMemory" runat="server"></asp:Label>
                                M</td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>当前虚拟目录：</strong></td>
                            <td style="height: 17px">
                                <asp:Label ID="LabelVitualPath" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>脚本虚拟路径：</strong></td>
                            <td style="height: 17px">
                                <asp:Label ID="LblScriptPath" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>脚本物理路径：</strong></td>
                            <td>
                                <asp:Label ID="LblScriptPhysicalPath" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>Application个数：</strong></td>
                            <td>
                                <asp:Label ID="LblApplicationCount" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>Session个数：</strong></td>
                            <td>
                                <asp:Label ID="LblSessionCount" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div id="Tab1" style="display: none;">
                    <div class="title" style="padding-left: 30px;">
                        IIS自带组件</div>
                    <table style="width: 100%; margin: auto;" cellpadding="3" cellspacing="1" class="border">
                        <tbody>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    MSWC.AdRotator</td>
                                <td class="tdbg" style="width: 20%;">
                                    <%= IsObjectInstalled("MSWC.AdRotator") %>
                                </td>
                                <td class="tdbgleft" style="width: 30%;">
                                    MSWC.BrowserType</td>
                                <td class="tdbg" style="width: 20%;">
                                    <%= IsObjectInstalled("MSWC.BrowserType")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    MSWC.NextLink</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSWC.NextLink") %>
                                </td>
                                <td class="tdbgleft">
                                    MSWC.Tools</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSWC.Tools")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    MSWC.Status</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSWC.Status") %>
                                </td>
                                <td class="tdbgleft">
                                    MSWC.Counters</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSWC.Counters")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    IISSample.ContentRotator</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("IISSample.ContentRotator") %>
                                </td>
                                <td class="tdbgleft">
                                    IISSample.PageCounter</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("IISSample.PageCounter")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    MSWC.PermissionChecker</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSWC.PermissionChecker") %>
                                </td>
                                <td class="tdbgleft">
                                    WScript.Shell</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("WScript.Shell")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    Scripting.FileSystemObject(FSO)</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("Scripting.FileSystemObject")%>
                                </td>
                                <td class="tdbgleft">
                                    ActiveX Data Object [ADO]</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("ADODB.Connection")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    Collaboration Data Object [CDONTS]</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("CDONTS.NewMail")%>
                                </td>
                                <td class="tdbgleft">
                                    MSXML2.XMLHTTP(MSXML 3.0)</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("MSXML2.XMLHTTP")%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="title" style="padding-left: 30px;">
                        动易ASP版本组件</div>
                    <table style="width: 100%; margin: auto;" cellpadding="3" cellspacing="1" class="border">
                        <tr>
                            <td class="tdbgleft" style="width: 30%">
                                动易4.03版COM组件：</td>
                            <td class="tdbg">
                                <%= IsObjectInstalled("EasyOne.GetVersion") %>
                            </td>
                            <td class="tdbgleft" style="width: 30%">
                                动易2005版COM组件：</td>
                            <td class="tdbg">
                                <%= IsObjectInstalled("PE_Common.GetVersion")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft">
                                动易2006版COM组件：</td>
                            <td class="tdbg">
                                <%= IsObjectInstalled("PE_CMS6.GetVersion")%>
                            </td>
                            <td class="tdbgleft">
                                &nbsp;</td>
                            <td class="tdbg">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <div class="title" style="padding-left: 30px;">
                        常用上传组件</div>
                    <table style="width: 100%; margin: auto;" cellpadding="3" cellspacing="1" class="border">
                        <tbody>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    SA-FileUp 文件上传</td>
                                <td class="tdbg" style="width: 20%;">
                                    <%= IsObjectInstalled("SoftArtisans.FileUp")%>
                                </td>
                                <td class="tdbgleft" style="width: 30%;">
                                    SA-FM 文件管理</td>
                                <td class="tdbg" style="width: 20%;">
                                    <%= IsObjectInstalled("SoftArtisans.FileManager")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    LyfUpload 文件上传</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("LyfUpload.UploadFile")%>
                                </td>
                                <td class="tdbgleft">
                                    ASPUpload 文件上传</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("Persits.Upload.1")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    w3 upload 文件上传</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("w3.upload")%>
                                </td>
                                <td class="tdbgleft">
                                    动网上传组件</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("DvFile.Upload")%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="title" style="padding-left: 30px;">
                        常用图像组件</div>
                    <table style="width: 100%; margin: auto;" cellpadding="3" cellspacing="1" class="border">
                        <tbody>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    CreatePreviewImage</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("CreatePreviewImage.cGvbox")%>
                                </td>
                                <td class="tdbgleft" style="width: 30%;">
                                    AspJpeg</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("Persits.Jpeg")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    SoftArtisans ImgWriter</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("SoftArtisans.ImageGen")%>
                                </td>
                                <td class="tdbgleft">
                                    sjCatSoft.Thumbnail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("sjCatSoft.Thumbnail")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    Dimac 的图像读写组件</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("W3Image.Image")%>
                                </td>
                                <td class="tdbgleft">
                                </td>
                                <td class="tdbg">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="title" style="padding-left: 30px;">
                        常用邮件组件</div>
                    <table style="width: 100%; margin: auto;" cellpadding="3" cellspacing="1" class="border">
                        <tbody>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    IISemail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("iismail.iismail.1")%>
                                </td>
                                <td class="tdbgleft" style="width: 30%;">
                                    w3 Jmail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("JMail.SMTPMail")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    ASPemail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("Persits.MailSender")%>
                                </td>
                                <td class="tdbgleft">
                                    Geocel</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("Geocel.Mailer")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft">
                                    SMTPmail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("SmtpMail.SmtpMail.1")%>
                                </td>
                                <td class="tdbgleft">
                                    dkQmail</td>
                                <td class="tdbg">
                                    <%= IsObjectInstalled("dkQmail.Qmail")%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="title" style="padding-left: 30px;">
                        自定义组件</div>
                    <table style="width: 100%;" cellpadding="3" cellspacing="1" class="border">
                        <tbody>
                            <tr id="CustomCheckRow" style="display: none;">
                                <td class="tdbg" colspan="2" id="checkResult" style="height: 25px; line-height: 25px;
                                    text-indent: 30px;">
                                    正在检测，请稍候...</td>
                            </tr>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    输入组件名称：</td>
                                <td class="tdbg">
                                    <input type="text" size="40" id="objToCheck" class="inputtext" />&nbsp;<input type="button"
                                        class="inputbutton" id="BtnCheckObject" runat="server" value="检测" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div id="Tab2" style="display: none;">
                    <asp:Repeater ID="RptServerVeriable" runat="server">
                        <HeaderTemplate>
                            <div class="title" style="float: left; width: 30%; text-indent: 30px;">
                                项目</div>
                            <div class="title" style="margin-left: 30%; text-indent: 30px; border-left: solid 1px #fff;
                                width: auto;">
                                值</div>
                            <div style="clear: both;">
                            </div>
                            <table cellpadding="3" cellspacing="1" style="width: 100%;" class="border">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    <%# Eval("key","<strong>{0}</strong>") %>
                                </td>
                                <td class="tdbg">
                                    <%# Eval("value","{0}") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
                <div id="Tab3" style="display: none;">
                    <asp:Repeater ID="RptSpaceUsage" runat="server">
                        <HeaderTemplate>
                            <div class="title" style="float: left; width: 30%; text-indent: 30px;">
                                项目</div>
                            <div class="title" style="margin-left: 30%; text-indent: 30px; border-left: solid 1px #fff;
                                width: auto;">
                                空间占用量</div>
                            <div style="clear: both;">
                            </div>
                            <table cellpadding="3" cellspacing="1" style="width: 100%;" class="border">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tdbgleft" style="width: 30%;">
                                    <%# Eval("Name","<strong>{0}</strong>") %>
                                </td>
                                <td class="tdbg">
                                    <%# Eval("SpaceUsage","{0}") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
