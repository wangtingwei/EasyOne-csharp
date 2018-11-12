<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZones" Title="添加广告版位" ValidateRequest="false" Codebehind="ADZone.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table class="border" width="100%" border="0" cellpadding="2" cellspacing="1">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <pe:AlternateLiteral ID="LblTitle" Text="添加广告版位" AlternateText="修改广告版位" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 30%">
                <strong>版位名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneName" TextMode="SingleLine" MaxLength="100" runat="server"
                    EnableViewState="False" Width="180" />
                <pe:RequiredFieldValidator ID="ValrZoneName" runat="server" ControlToValidate="TxtZoneName"
                    ErrorMessage="版位名称不能为空！" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>生成JS文件名：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneJSName" TextMode="singleLine" MaxLength="100" runat="server"
                    EnableViewState="False" Width="180" />
                <pe:RequiredFieldValidator ID="ValrtZoneJSName" runat="server" ControlToValidate="TxtZoneJSName"
                    ErrorMessage="生成JS文件名不能为空！" SetFocusOnError="true" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeZoneJSName" ControlToValidate="TxtZoneJSName"
                    runat="server" ErrorMessage="JS文件名不正确或者为空！" ValidationExpression="[0-9]{6}\/[0-9]{1,}\.js"
                    SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版位描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneIntro" TextMode="multiline" MaxLength="255" runat="server"
                    EnableViewState="False" Height="63px" Width="280px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版位类型：</strong><br />
                选择放置于此版位的广告类型。
            </td>
            <td>
                <asp:RadioButtonList ID="RadlZoneType" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" EnableViewState="true" AutoPostBack="false" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版位设置：</strong><br />
                对版位的详细参数进行设置。
            </td>
            <td>
                <asp:RadioButtonList ID="RadlDefaultSetting" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" AutoPostBack="false" Height="30" />
                <div id="ZoneTypeSetting" runat="server">
                    <table id="ZoneTypeSetting1" runat="server" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td colspan="2" align="center" style="height: 23px">
                                            此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting2" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            弹出方式：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopType" runat="server">
                                                <asp:ListItem Value="1">前置窗口</asp:ListItem>
                                                <asp:ListItem Value="2">后置窗口</asp:ListItem>
                                                <asp:ListItem Value="3">网页对话框</asp:ListItem>
                                                <asp:ListItem Value="4">背投广告</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            弹出位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopLeft" runat="server" Text="100" MaxLength="4" TextMode="SingleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopTop" runat="server" Text="100" MaxLength="4" TextMode="singleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            时间间隔：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopCookieHour" Text="0" MaxLength="2" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                            小时 在时间间隔内不重复弹出，设为0时总是弹出
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting3" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            广告位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropMovePosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            移动平滑度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveDelay" MaxLength="7" Text="0.015" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                            （取值在0.001至1之间）
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlMoveShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtMoveCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting4" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            广告位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFixedPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedLeft" MaxLength="4" Text="100" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFixedShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtFixedCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting5" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            漂浮类型：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatType" runat="server">
                                                <asp:ListItem Value="1">变速漂浮</asp:ListItem>
                                                <asp:ListItem Value="2">匀速漂浮</asp:ListItem>
                                                <asp:ListItem Value="3">上下漂浮</asp:ListItem>
                                                <asp:ListItem Value="4">左右漂浮</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            开始位置：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatPosition" runat="server">
                                                <asp:ListItem Value="1" Text="左上" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="左下"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="右下"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatLeft">左</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatLeft" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatTop">上</span>：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFloatShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtFloatCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting6" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td align="center">
                                            此类型无版位参数设置！</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting7" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            左右边距：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            上边距：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            移动平滑度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletDelay" MaxLength="7" Text="0.015" TextMode="singleLine"
                                                runat="server" Width="36px" />
                                            （取值在0.001至1之间）
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            是否显示关闭标签：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlCoupletShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">是</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            关闭标签的颜色：
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtCoupletCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版位尺寸：</strong><br />
                IAB：互联网广告联合会标准尺寸。<br />
                带*号的为新增加的标准广告尺寸。
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropAdZoneSize" runat="server" AutoPostBack="false" EnableViewState="False">
                                <asp:ListItem Value="468x60">IAB - 468 x 60 IMU (横幅广告)</asp:ListItem>
                                <asp:ListItem Value="234x60">IAB - 234 x 60 IMU (半幅广告)</asp:ListItem>
                                <asp:ListItem Value="88x31">IAB - 88 x 31 IMU (小按钮)</asp:ListItem>
                                <asp:ListItem Value="120x90">IAB - 120 x 90 IMU (按钮一)</asp:ListItem>
                                <asp:ListItem Value="120x60">IAB - 120 x 60 IMU (按钮二)</asp:ListItem>
                                <asp:ListItem Value="728x90">IAB - 728 x 90 IMU (通栏广告) *</asp:ListItem>
                                <asp:ListItem Value="120x240">IAB - 120 x 240 IMU (竖幅广告)</asp:ListItem>
                                <asp:ListItem Value="125x125">IAB - 125 x 125 IMU (方形按钮)</asp:ListItem>
                                <asp:ListItem Value="180x150">IAB - 180 x 150 IMU (长方形) *</asp:ListItem>
                                <asp:ListItem Value="300x250">IAB - 300 x 250 IMU (中长方形) *</asp:ListItem>
                                <asp:ListItem Value="336x280">IAB - 336 x 280 IMU (大长方形)</asp:ListItem>
                                <asp:ListItem Value="240x400">IAB - 240 x 400 IMU (竖长方形)</asp:ListItem>
                                <asp:ListItem Value="250x250">IAB - 250 x 250 IMU (正方形弹出)</asp:ListItem>
                                <asp:ListItem Value="120x600">IAB - 120 x 600 IMU (摩天大楼)</asp:ListItem>
                                <asp:ListItem Value="160x600">IAB - 160 x 600 IMU (宽摩天大楼) *</asp:ListItem>
                                <asp:ListItem Value="300x600">IAB - 300 x 600 IMU (半页广告) *</asp:ListItem>
                                <asp:ListItem Value="0x0">自定义大小</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            宽度：
                            <asp:TextBox ID="TxtZoneWidth" MaxLength="4" Width="36px" Text="468" TextMode="singleLine"
                                runat="server" />
                            <pe:RequiredFieldValidator ID="ValrTxtZoneWidth" runat="server" ControlToValidate="TxtZoneWidth"
                                SetFocusOnError="true" ErrorMessage="" Display="Dynamic" />
                            &nbsp;&nbsp;&nbsp;&nbsp;高度：
                            <asp:TextBox ID="TxtZoneHeight" MaxLength="4" Width="36px" Text="60" TextMode="singleLine"
                                runat="server" />
                            <pe:RequiredFieldValidator ID="ValrTxtZoneHeight" runat="server" ControlToValidate="TxtZoneHeight"
                                SetFocusOnError="true" ErrorMessage="" Display="Dynamic" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>显示方式：</strong><br />
                当版位中有多个广告时按照此设定进行显示（依据广告的权重）。</td>
            <td>
                <asp:RadioButtonList ID="RadlShowType" runat="server">
                    <asp:ListItem Value="1" Selected="true">按权重随机显示，权重越大显示机会越大。</asp:ListItem>
                    <asp:ListItem Value="2">按权重优先显示，显示权重值最大的广告。</asp:ListItem>
                    <asp:ListItem Value="3">按顺序循环显示，此方式仅对矩形横幅有效。</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版位状态：</strong><br />
                设为活动的版位才能在前台显示。</td>
            <td>
                <asp:CheckBox ID="ChkActive" Checked="true" runat="server" EnableViewState="False" />
                活动版位
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnAdZone" runat="server" Text="保存" OnClick="EBtnAdZone_Click" />&nbsp;&nbsp;
                <input name="Cancel" type="button" onclick="Redirect('ADZoneManage.aspx')" class="inputbutton"
                    id="Cancel" value="取消" style="cursor: pointer; cursor: hand;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnZoneId" runat="server" />

    <script language="javascript" type="text/javascript">
  
    function $(value)
    {
        return document.getElementById(value);
    }
  
    function ShowZoneTypePanel()
    {
        
       
         if($('<%=RadlZoneType.ClientID %>_0').checked==false)
        {
            $('<%=RadlShowType.ClientID %>_2').disabled = true;
            
            $('<%=RadlShowType.ClientID %>_0').checked=true;//$('<%=RadlShowType.ClientID %>_2').checked;
        } else{
            $('<%=RadlShowType.ClientID %>_2').disabled=false;
            $('<%=RadlShowType.ClientID %>_0').checked=true;
           
        }
        
        Zone_DisableSize($('<%=RadlZoneType.ClientID %>_5').checked);
        
        var obj=document.getElementsByName('<%=RadlZoneType.UniqueID %>');
        for (var j=0;j<obj.length;j++)
        {
            var ot = eval($('<%=ZoneTypeSetting.ClientID %>' + (j + 1)))
            if($('<%=RadlZoneType.ClientID %>_'+j).checked && $('<%=RadlDefaultSetting.ClientID %>_1').checked)
            {
                ot.style.display = '';
                
            }else{ 
                ot.style.display = 'none';
            }
        } 
         
     }
     
     function Zone_DisableSize(value)
     {
        $('<%=TxtZoneWidth.ClientID %>').disabled=value;
        $('<%=TxtZoneHeight.ClientID %>').disabled=value;
        $('<%=DropAdZoneSize.ClientID %>').disabled=value;
     }
   
    function Zone_SelectSize(o)
    {
        size = o.options[o.selectedIndex].value;
        if (size != '0x0')
        {
            sarray = size.split('x');
            height = sarray.pop();
            width  = sarray.pop();
            $('<%=TxtZoneWidth.ClientID %>').value=width;
            $('<%=TxtZoneHeight.ClientID %>').value=height;
        }else{
            $('<%=TxtZoneHeight.ClientID %>').value=100;
            $('<%=TxtZoneWidth.ClientID %>').value=100;
        }
     }
    
    function ChangePositonShow(drop)
    {
        var text=drop.options[drop.options.selectedIndex].text;
        var name=drop.id.replace("ctl00_CphContent_Drop","");
        name=name.replace("Position","");
        $(name+"Left").innerHTML=text.substring(0,1);
        $(name+"Top").innerHTML=text.substring(1,2);
    }
     if($('<%=HdnAction.ClientID %>').value=="Modify")
     {
         if($('<%=RadlZoneType.ClientID %>_0').checked==false)
        {
            $('<%=RadlShowType.ClientID %>_2').disabled = true;
        } else{
            $('<%=RadlShowType.ClientID %>_2').disabled=false;
        }
     }
    </script>

</asp:Content>
