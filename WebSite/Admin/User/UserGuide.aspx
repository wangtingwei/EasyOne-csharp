<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGuide"
    Title="会员管理向导" Codebehind="UserGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    会员管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="UserManage.aspx" target="main_right">会员管理首页</a></li>
            <li>
                <pe:ExtendedAnchor ID="EahUserAdd" IsChecked="true" OperateCode="UserAdd" href="User.aspx"
                    runat="server" target="main_right">添加新会员</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        分组管理</div>
    <div class="guide" style="display: none">
        <ul>
            <li><a href="UserManage.aspx?listType=0&GroupID=0&GroupName=所有会员" target="main_right">
                所有会员</a></li>
            <asp:Repeater ID="RptGroups" runat="server">
                <ItemTemplate>
                    <li><a href="UserManage.aspx?listType=10&GroupID=<%#Eval("GroupId")%>&GroupName=<%#Server.UrlEncode(Eval("GroupName").ToString())%>"
                        target="main_right">
                        <%#Eval("GroupName")%>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        快速查找</div>
    <div class="guide" style="display: none">
        <ul>
            <li><a href="userManage.aspx?ListType=1" target="main_right">文章最多TOP100</a> </li>
            <li><a href="userManage.aspx?ListType=2" target="main_right">文章最少的100个会员</a> </li>
            <li><a href="userManage.aspx?ListType=3" target="main_right">最近24小时内登录的会员</a> </li>
            <li><a href="userManage.aspx?ListType=4" target="main_right">最近24小时内注册的会员</a> </li>
            <li><a href="userManage.aspx?ListType=5" target="main_right">所有被锁住的会员</a> </li>
            <li runat="server" id="ListType6"><a href="userManage.aspx?ListType=6" target="main_right">
                点券数大于0的会员</a> </li>
            <li runat="server" id="ListType7"><a href="userManage.aspx?ListType=7" target="main_right">
                积分大于0的会员</a> </li>
            <li runat="server" id="ListType8"><a href="userManage.aspx?ListType=8" target="main_right">
                资金余额大于0的会员</a> </li>
            <li runat="server" id="ListType9"><a href="userManage.aspx?ListType=9" target="main_right">
                资金余额小于等于0的会员</a> </li>
            <li><a href="userManage.aspx?ListType=14" target="main_right">未通过邮件验证的会员</a> </li>
            <li><a href="userManage.aspx?ListType=15" target="main_right">未通过管理员认证的会员</a> </li>
            <li runat="server" id="ListType16"><a href="userManage.aspx?ListType=16" target="main_right">
                消费金额TOP100</a> </li>
            <li runat="server" id="ListType17"><a href="userManage.aspx?ListType=17" target="main_right">
                消费点券TOP100</a> </li>
            <li runat="server" id="ListType18"><a href="userManage.aspx?ListType=18" target="main_right">
                消费积分TOP100</a> </li>
            <li runat="server" id="ListType19"><a href="userManage.aspx?ListType=19" target="main_right">
                有效期剩余5天的会员</a> </li>
            <li><a href="userManage.aspx?ListType=36" target="main_right">设置了单独权限的会员</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        会员查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="11">会员ID</option>
                    <option value="12" selected="selected">会员名称</option>
                    <option value="13">电子邮件</option>
                    <option value='20'>个人主页</option>
                    <option value='21'>真实姓名</option>
                    <option value='22'>身份证号码</option>
                    <option value='23'>单位名称</option>
                    <option value='24'>联系地址</option>
                    <option value='25'>手机号码</option>
                    <option value='26'>办公电话</option>
                    <option value='27'>家庭电话</option>
                    <option value='28'>小灵通</option>
                    <option value='29'>传真号码</option>
                    <option value='30'>QQ号</option>
                    <option value='31'>ICQ号</option>
                    <option value='32'>MSN帐号</option>
                    <option value='33'>Yahoo</option>
                    <option value='34'>UC号</option>
                    <option value='35'>Aim帐号</option> 
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="查询" onclick="return OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        
        if (keyword =="")
        {
            alert("请输入要查询的条件！"); 
            return false; 
        }
       field = objSel.options[objSel.options.selectedIndex].value;
        
        if (field == "11"){
            if(checknumber(keyword)) 
            { 
                alert("只允许输入数字！"); 
                return false; 
            } 
        }
        if (field == "20"){
            var regexp = /http:\/\//gi;
            keyword = keyword.replace(regexp,"");
        }
        
        var url = "userManage.aspx?ListType="+field+"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
   
    function checknumber(String) 
    { 
        var Letters = "1234567890"; 
        var i;
        var c; 
        for( i = 0; i < String.length; i ++ ) 
        { 
            c = String.charAt( i ); 
            if (Letters.indexOf( c ) ==-1) 
            { 
                return true; 
            } 
        } 
        return false; 
    } 
    </script>

</asp:Content>
