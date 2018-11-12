<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.LogManagerGuide"
    Title="网站日志管理导航" Codebehind="LogManagerGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    网站日志管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        快速查找</div>
    <div class="guide">
        <ul>
            <li><a href="../Accessories/LogManager.aspx?Category=4" target="main_right">越权操作</a></li>
            <li><a href="../Accessories/LogManager.aspx?Category=5" target="main_right">异常记录</a></li>
            <li><a href="../Accessories/LogManager.aspx?Category=2" target="main_right">登录失败</a></li>
            <li><a href="../Accessories/LogManager.aspx?Category=1" target="main_right">登录成功</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        高级查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select name="SelSearchType" id="SelSearchType" style="width: 140px">
                    <option value="UserName">操作人</option>
                    <option value="Title">标 题</option>
                    <option value="UserIP">IP地址</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="查询" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var searchType=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelSearchType");
        searchType = objSel.options[objSel.options.selectedIndex].value;
        var url = "LogManager.aspx?SearchType="+searchType+"&KeyWord="+escape(keyword);
      JumpToMainRight(url);
    }
    </script>

</asp:Content>
