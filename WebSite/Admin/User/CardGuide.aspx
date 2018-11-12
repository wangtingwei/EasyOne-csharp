<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardGuide"
    Title="充值卡管理向导" Codebehind="CardGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    充值卡管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        常规操作</div>
    <div class="guide">
        <ul>
            <li><a href="CardAdd.aspx" target="main_right">添加充值卡</a></li>
            <li><a href="cardbatchadd.aspx" target="main_right">批量生成充值卡</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        快速查找</div>
    <div class="guide">
        <ul>
            <li><a href="CardsManage.aspx?CardType=-1" target="main_right">所有充值卡</a></li>
            <li><a href="CardsManage.aspx?CardStatus=1" target="main_right">所有未使用的充值卡</a></li>
            <li><a href="CardsManage.aspx?CardStatus=2" target="main_right">所有已使用的充值卡</a></li>
            <li><a href="CardsManage.aspx?CardStatus=3" target="main_right">所有已失效的充值卡</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        充值卡搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="CardType" name="CardType" style="width: 140px">
                    <option selected="selected" value="-1">=充值卡类型=</option>
                    <option value="0">本站充值卡</option>
                    <option value="1">其他公司卡</option>
                </select>
            </li>
            <li>
                <select id="CardStatus" name="CardStatus" style="width: 140px">
                    <option selected="selected" value="-1">=充值卡状态=</option>
                    <option value="1">未使用</option>
                    <option value="2">已使用</option>
                    <option value="3">已失效</option>
                </select>
            </li>
            <li>
                <select id="Field" name="Field" style="width: 140px">
                    <option selected="selected" value="1">卡号</option>
                    <option value="2">面值</option>
                    <option value="3">代理商</option>
                    <option value="4">使用者</option>
                </select>
            </li>
            <li>
                <input id="keyword" maxlength="50" name="keyword" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="搜索" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var CardType = document.getElementById("CardType").options[document.getElementById("CardType").selectedIndex].value;
        var CardStatus = document.getElementById("CardStatus").options[document.getElementById("CardStatus").selectedIndex].value;
        var Field = document.getElementById("Field").options[document.getElementById("Field").selectedIndex].value;
        var keyword = document.getElementById("keyword").value;
        if(keyword=='')
        {
            alert('请输入查询关键字！');
            return;
        }
        if(Field =="2")
        {
           if(isNaN(keyword)) 
           {
               alert("请输入有效的数值！"); 
               return;
           }
        }
        var url = "CardsManage.aspx?Action=Search&CardType="+CardType+"&CardStatus="+CardStatus+"&Field="+Field+"&Keyword="+escape(keyword);
        JumpToMainRight(url);
    }
    function changeColor(obj)
    {
        if(obj.value =="")
        {
            obj.style.backgroundColor="#DDD";
        }
    }
    </script>

</asp:Content>
