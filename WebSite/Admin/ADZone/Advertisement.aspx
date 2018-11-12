<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADAdd" Title="添加广告" ValidateRequest="false" Codebehind="Advertisement.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="javascript" type="text/javascript">
function CheckSelect()
{
   var s=document.getElementById("<%= LstZoneName.ClientID %>");
   for(var i=0;i<s.length;i++)
   {
     if(s.options[i].selected)
     {
       return true;
     }
   }
   return false;
}
function CheckUploadFile()
{
    if(document.getElementById("<%=RadlADType.ClientID %>_0").checked)
    {
        var s= document.getElementById("<%=FileUploadControl1.ClientID %>");
        if(s.value=="")
        {
            alert("还没有上传图片！");
            return false;
        }
    }
   if(document.getElementById("<%=RadlADType.ClientID %>_1").checked)
   {
     s=document.getElementById("<%=ExtenFileUpload.ClientID %>");
     if(s.value=="")
     {
        alert("还没有上传Flash");
        return false;
     }
   }
   return true;
}
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="3" class="spacingtitle">
                <pe:AlternateLiteral ID="LblTitle" Text="添加广告" AlternateText="修改广告" runat="Server" /></td>
        </tr>
        <tr align="center" class="tdbg">
            <td class="tdbg" rowspan="7" valign="top">
                <strong>所属版位</strong><br />
                <asp:ListBox ID="LstZoneName" runat="server" Height="313px" Width="220px" SelectionMode="multiple">
                </asp:ListBox>
            </td>
            <td class="tdbgleft" align="Left">
                <strong>广告名称：</strong></td>
            <td align="Left">
                <asp:TextBox ID="TxtADName" Width="400px" MaxLength="150" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrADName" runat="server" ControlToValidate="TxtADName"
                    Display="dynamic" ErrorMessage="广告名称不能为空！"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 84px">
                <strong>广告类型：</strong></td>
            <td align="Left">
                <asp:RadioButtonList ID="RadlADType" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 84px">
                <strong>广告内容：</strong></td>
            <td valign="top" align="Left">
                <div runat="server" id="ADContent">
                    <table id="ADContent_1" width="100%" border="0" runat="server" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr align="center" class="tdbg">
                            <td colspan="2">
                                <strong>广告内容设置--图片</strong></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                图片上传：</td>
                            <td>
                                <pe:FileUploadControl ID="FileUploadControl1" ModuleName="ADZone" runat="server">
                                </pe:FileUploadControl>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                图片尺寸：</td>
                            <td>
                                宽：<asp:TextBox ID="TxtImgWidth" MaxLength="5" Style="width: 40px" runat="server"></asp:TextBox>
                                像素&nbsp;&nbsp;&nbsp;&nbsp; 高：<asp:TextBox ID="TxtImgHeight" MaxLength="5" Style="width: 40px"
                                    runat="server"></asp:TextBox>
                                像素
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                链接地址：</td>
                            <td>
                                <asp:TextBox ID="TxtLinkUrl" Text="http://" runat="server" Width="341px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                链接提示：</td>
                            <td>
                                <asp:TextBox ID="TxtLinkAlt" runat="server" MaxLength="255" Width="341px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                链接目标：</td>
                            <td>
                                <asp:RadioButtonList ID="RadlLinkTarget" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">新窗口</asp:ListItem>
                                    <asp:ListItem Value="0">原窗口</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                广告简介：</td>
                            <td style="height: 95px">
                                <asp:TextBox ID="TxtADIntro" runat="server" TextMode="MultiLine" Height="87px" Width="341px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="ADContent_2" runat="server" width="100%" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr align="center" class="tdbg">
                            <td colspan="2">
                                <strong>广告内容设置--动画</strong></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                动画上传：</td>
                            <td>
                                <pe:FileUploadControl ID="ExtenFileUpload" ModuleName="ADZone" runat="server">
                                </pe:FileUploadControl>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                动画尺寸：</td>
                            <td>
                                宽：<asp:TextBox ID="TxtFlashWidth" runat="server" Style="width: 40px" MaxLength="5"></asp:TextBox>
                                像素&nbsp;&nbsp;&nbsp;&nbsp;高：<asp:TextBox ID="TxtFlashHeight" Style="width: 40px"
                                    runat="server" MaxLength="5"></asp:TextBox>
                                像素
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                背景透明：</td>
                            <td>
                                <asp:RadioButtonList ID="RadlFlashMode" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">不透明</asp:ListItem>
                                    <asp:ListItem Value="1">透明</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table id="ADContent_3" runat="server" width="380px" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr align="center" class="tdbg">
                            <td colspan="2">
                                <strong>广告内容设置--文本</strong></td>
                        </tr>
                        <tr class="tdbg">
                            <td colspan="2" align="center" style="height: 90px">
                                <asp:TextBox ID="TxtADText" TextMode="multiLine" runat="server" Height="90px" Width="380px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="ADContent_4" runat="server" width="380px" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr align="center" class="tdbg">
                            <td>
                                <strong>广告内容设置--代码</strong></td>
                        </tr>
                        <tr class="tdbg">
                            <td align="center" style="width: 380px">
                                <asp:TextBox ID="TxtADCode" TextMode="multiLine" runat="server" Height="90px" Width="380px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="ADContent_5" runat="server" width="100%" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr align="center" class="tdbg">
                            <td colspan="2">
                                <strong>广告内容设置--页面</strong>
                                
                                </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="height: 90px">
                                页面地址：</td>
                            <td align="left" style="height: 90px">
                                <asp:TextBox ID="TxtWebFileUrl" TextMode="MultiLine" runat="server" Height="90px"
                                    Width="300px"></asp:TextBox><br />
                                    <span style="color:Blue;">注意：</span>页面地址需要加上http://
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 84px">
                <strong>广告权重：</strong></td>
            <td>
                <asp:TextBox ID="TxtPriority" runat="server" TextMode="singleLine" MaxLength="3"
                    Text="1" Style="width: 20px"></asp:TextBox>
                * 此项为版位广告随机显示时的优先权，权重越大显示机会越大。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 84px">
                <strong>广告统计：</strong></td>
            <td align="left">
                <asp:CheckBox ID="ChkCountView" runat="server" />
                统计浏览数 浏览数：<asp:TextBox ID="TxtViews" MaxLength="5" runat="server" Width="32px" Text="0"></asp:TextBox><br />
                <asp:CheckBox ID="ChkCountClick" runat="server" />
                统计点击数 点击数：<asp:TextBox ID="TxtClicks" runat="server" MaxLength="5" Width="35px" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 94px">
                <strong>广告过期时间：</strong></td>
            <td align="left">
                <pe:DatePicker ID="DpkOverdueDate" runat="server"></pe:DatePicker></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 84px; height: 24px;">
                <strong>审核状态：</strong></td>
            <td style="height: 24px" align="left">
                <asp:CheckBox ID="ChkPassed" Checked="true" runat="server" />
                通过审核
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1">
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="EBtnSubmit" runat="server" Text="保存" OnClick="EBtnSubmit_Click" />&nbsp;&nbsp;
                <input name="Cancel" type="button" onclick="Redirect('ADZoneManage.aspx')" class="inputbutton"
                    id="Cancel" value="取消" style="cursor: pointer; cursor: hand;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnAdId" runat="server" />
</asp:Content>
