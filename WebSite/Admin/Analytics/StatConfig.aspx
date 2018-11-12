<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatConfig" Title="网站统计管理" ValidateRequest="false" Codebehind="StatConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
    function ConfirmModify()
    {
      returun (confirm('强烈建议尽量选择少的统计功能项目，最好一个都不启用！！！'))
    }
    
    var tID=0;
    
    function ShowTabs(ID)
    {
      if(ID!=tID){
        document.getElementById("TabTitle"+ID).className = 'titlemouseover';
        document.getElementById("TabTitle"+tID).className = 'tabtitle';
        document.getElementById("Tabs"+tID).style.display = 'none';
        document.getElementById("Tabs"+ID).style.display = '';
        tID=ID;
      }
    }
    function setFileFileds(num){    
        var str="";
        if (num==1){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=simple'></sc" + "ri" +"pt>";
        }
        else if(num==2){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=common'></sc" + "ri" +"pt>";
        }
        else if(num==3){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=all'></sc" + "ri" +"pt>";
        }
        else if(num==4){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=none'></sc" + "ri" +"pt>";
        }
        document.getElementById("selectKey").value = str;
    }
    function setValue()
    {
        setFileFileds(1);
        document.getElementById("LinkContent").value = "<a href='{PE.SiteConfig.sitepath/}Analytics/ShowOnline.aspx' target='_blank'>网站在线情况详细列表</a>";
    }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="text-align: center;">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                初始化设置</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                功能项目</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td style="width: 30%;" class="tdbgleft">
                    <strong>服务器所在时区：</strong></td>
                <td>
                    <asp:DropDownList ID="DropTimezone" runat="server">
                         		<asp:ListItem>请选择...</asp:ListItem>
	 				    		<asp:ListItem Value="-12">(GMT -12:00) 日界线西</asp:ListItem>
	 				    		<asp:ListItem Value="-11">(GMT -11:00) 中途岛,萨摩亚群岛</asp:ListItem>
	 				    		<asp:ListItem Value="-10">(GMT -10:00) 夏威夷</asp:ListItem>
	 				    		<asp:ListItem Value="-9">(GMT -09:00) 阿拉斯加</asp:ListItem>
	 				    		<asp:ListItem Value="-8">(GMT -08:00) 太平洋时间(美国和加拿大)</asp:ListItem>
	 				    		<asp:ListItem Value="-8">(GMT -08:00) 蒂华纳</asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) 山地时间(美国和加拿大)  </asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) 亚利桑那</asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) 奇瓦瓦,拉巴斯,马扎特兰</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) 萨斯喀彻温</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) 中部时间(美国和加拿大)</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) 中美洲</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) 瓜达拉哈拉,墨西哥城,蒙特雷</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) 波哥大,利马,基多</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) 东部时间(美国和加拿大)</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) 印第安那州(东部)</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) 大西洋时间(加拿大)</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) 加拉加斯,拉巴斯</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) 圣地亚哥</asp:ListItem>
	 				    		<asp:ListItem Value="-3.5">(GMT -03:30) 纽芬兰</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) 巴西利亚</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) 布宜诺斯艾利斯,乔治敦</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) 格陵兰</asp:ListItem>
	 				    		<asp:ListItem Value="-2">(GMT -02:00) 中大西洋</asp:ListItem>
	 				    		<asp:ListItem Value="-1">(GMT -01:00) 佛得角群岛</asp:ListItem>
	 				    		<asp:ListItem Value="-1">(GMT -01:00) 亚速尔群岛</asp:ListItem>
	 				    		<asp:ListItem Value="0">(GMT  00:00) 格林威治时间：都柏林,爱丁堡,伦敦,里斯本</asp:ListItem>
	 				    		<asp:ListItem Value="0">(GMT  00:00) 卡萨布兰卡,蒙罗维亚</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 阿姆斯特丹,柏林,伯尔尼,罗马,斯德哥尔摩,维也纳</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 贝尔格莱德,布拉迪斯拉发,布达佩斯,卢布尔雅那,布拉格</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 布鲁塞尔,哥本哈根,马德里,巴黎</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 萨拉热窝,斯科普里</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 索非亚,维尔纽斯</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 华沙,萨格勒布</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) 中非西部</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 布加勒斯特</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 哈拉雷,比勒陀利亚</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 雅典,伊斯坦布尔</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 明斯克</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 赫尔辛基,基辅,里加,索非亚,塔林,维尔纽斯</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 开罗</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) 耶路撒冷</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 巴格达</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 科威特,利雅得</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 莫斯科</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 圣彼得堡</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 伏尔加格勒</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) 内罗毕</asp:ListItem>
	 				    		<asp:ListItem Value="3.5">(GMT +03:30) 德黑兰</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) 阿布扎比,马斯喀特</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) 巴库</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) 第比利斯</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) 埃里温</asp:ListItem>
	 				    		<asp:ListItem Value="4.5">(GMT +04:30) 喀布尔</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:00) 叶卡捷琳堡</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:00) 伊斯兰堡,卡拉奇,塔什干</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:30) 马德拉斯,加尔各答,孟买,新德里</asp:ListItem>
	 				    		<asp:ListItem Value="5.75">(GMT +05:45) 加德满都</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) 阿拉木图</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) 新西伯利亚</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) 阿斯塔纳,达卡</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) 斯里哈亚华登尼普拉</asp:ListItem>
	 				    		<asp:ListItem Value="6.5">(GMT +06:30) 仰光</asp:ListItem>
	 				    		<asp:ListItem Value="7">(GMT +07:00) 克拉斯诺亚尔斯克</asp:ListItem>
	 				    		<asp:ListItem Value="7">(GMT +07:00) 曼谷,河内,雅加达</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) 北京,重庆,香港特别行政区,乌鲁木齐</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) 吉隆坡,新加坡</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) 珀斯</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) 台北</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) 伊尔库茨克,乌兰巴图</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) 大阪,札幌,东京</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) 汉城</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) 雅库茨克</asp:ListItem>
	 				    		<asp:ListItem Value="9.5">(GMT +09:30) 阿德莱德</asp:ListItem>
	 				    		<asp:ListItem Value="9.5">(GMT +09:30) 达尔文</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) 布里斯班</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) 符拉迪沃斯托克</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) 关岛,莫尔兹比港</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) 霍巴特</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) 堪培拉,墨尔本,悉尼</asp:ListItem>
	 				    		<asp:ListItem Value="11">(GMT +11:00) 马加丹,索罗门群岛,新喀里多尼亚</asp:ListItem>
	 				    		<asp:ListItem Value="12">(GMT +12:00) 奥克兰,惠灵顿</asp:ListItem>
	 				    		<asp:ListItem Value="12">(GMT +12:00) 斐济,堪察加半岛,马绍尔群岛</asp:ListItem>
	 				    		<asp:ListItem Value="13">(GMT +13:00) 努库阿洛法   </asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="ValcTimeZone" runat="server" ControlToValidate="DropTimezone"
                        Display="Dynamic" ErrorMessage="请选择服务器所在的时区" Operator="NotEqual"
                        SetFocusOnError="True" ValueToCompare="请选择..."></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在线用户的保留时间：</strong><br />
                    用户切换页面至其他网站或者关闭浏览器后，在线名单将在上述时间内删除该用户。这个间隔越小，网站统计的当前时刻在线名单越准确；这个间隔越大，网站统计的在线人数越多。
                </td>
                <td>
                    <asp:TextBox ID="TxtOnlineTime" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    秒
                    <asp:CompareValidator ID="ValcOnlineTime" runat="server" ControlToValidate="TxtOnlineTime"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>自动标记在线间隔：</strong><br />
                    客户端浏览器会每隔上述时间向服务器提交一次在线信息，同时服务器将其标记为在线，这个间隔越小，服务器需要处理的请求越多。</td>
                <td>
                    <asp:TextBox ID="TxtInterval" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    秒
                    <asp:CompareValidator ID="ValcInterval" runat="server" ControlToValidate="TxtInterval"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>自动标记在线间隔循环次数：</strong><br />
                    此是为了防止用户打开网页，但长时间无任何活动而设置。客户端浏览器向服务器提交在线信息次数超过此次数，立即停止提交。
                </td>
                <td>
                    <asp:TextBox ID="TxtIntervalNum" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    次
                    <asp:CompareValidator ID="ValcIntervalNum" runat="server" ControlToValidate="TxtIntervalNum"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>保留访问记录数：</strong><br />
                    保存访问明细(最后访问)条目数。</td>
                <td>
                    <asp:TextBox ID="TxtVisitRecord" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    条
                    <asp:CompareValidator ID="ValcVisitRecord" runat="server" ControlToValidate="TxtVisitRecord"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>保留访问IP数(建议大于20小于800的数字)： </strong>
                    <br />
                    当不启用“在线人数统计”功能时，系统将以保留访问者IP的方式来防止刷新，即同一个IP访问多次或者在网站内切换页面，均只计算浏览量而不计算访问量。
                </td>
                <td>
                    <asp:TextBox ID="TxtKillRefresh" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    个
                    <asp:CompareValidator ID="ValcKillRefresh" runat="server" ControlToValidate="TxtKillRefresh"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
        </tbody>
        <tbody id="Tabs1" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 30%;">
                    <strong>开始统计日期：</strong></td>
                <td>
                    <pe:DatePicker ID="DpkStartDate" runat="server" Width="70px"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>使用本系统前的访问量：</strong>
                </td>
                <td>
                    <asp:TextBox ID="TxtOldTotalNum" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    人次
                    <asp:CompareValidator ID="ValcOldTotalNum" runat="server" ControlToValidate="TxtOldTotalNum"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>使用本系统前的浏览量：</strong>
                </td>
                <td>
                    <asp:TextBox ID="TxtOldTotalView" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    人次
                    <asp:CompareValidator ID="ValcOldTotalView" runat="server" ControlToValidate="TxtOldTotalView"
                        Display="Dynamic" ErrorMessage="请输入有效的数字！" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
        </tbody>
        <tbody id="Tabs2" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%;" class="tdbgleft">
                    <strong>功能项目：</strong><br />
                    统计太多的项目会减慢访问速度，耗费太多网站资源，一段时间不想分析的功能项目建议不要起用！<br />
                    <span style="color: #ff0000">强烈建议尽量选择少的功能项目，最好一个都不启用！！！<br />
                    </span>
                </td>
                <td>
                    <asp:CheckBoxList ID="ChklRegFields" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                        Width="100%">
                        <asp:ListItem Value="IsCountOnline">启用“在线人数统计”功能</asp:ListItem>
                        <asp:ListItem Value="FIP">客户端IP地址分析</asp:ListItem>
                        <asp:ListItem Value="FAddress">客户端地址分析</asp:ListItem>
                        <asp:ListItem Value="FRefer">客户端链接页面分析</asp:ListItem>
                        <asp:ListItem Value="FTimezone">客户端时区分析</asp:ListItem>
                        <asp:ListItem Value="FWeburl">客户端来访网站分析</asp:ListItem>
                        <asp:ListItem Value="FBrowser">客户端浏览器分析</asp:ListItem>
                        <asp:ListItem Value="FMozilla">客户端字串分析</asp:ListItem>
                        <asp:ListItem Value="FSystem">客户端操作系统分析</asp:ListItem>
                        <asp:ListItem Value="FScreen">客户端屏幕大小分析</asp:ListItem>
                        <asp:ListItem Value="FColor">客户端屏幕色彩分析</asp:ListItem>
                        <asp:ListItem Value="FKeyword">搜索关键词分析</asp:ListItem>
                        <asp:ListItem Value="FVisit">访问次数统计分析</asp:ListItem>
                        <asp:ListItem Value="FYesterDay">启用昨日统计</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
        </tbody>
    </table>
    <p style="text-align: center">
        <asp:Button ID="BtnSave" runat="server" Text="保存设置" Width="88px" OnClick="BtnSave_Click"
            OnClientClick="ReloadLeft()" />
        &nbsp;
        <asp:HiddenField ID="HdnSaveConfig" runat="server" Value="0" />
    </p>
</asp:Content>
