<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.PaymentLogGuide"
    Title="在线支付记录向导" Codebehind="PaymentLogGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    在线支付记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        快速查找</div>
    <div class="guide">
        <ul>
            <li><a href="PaymentLogManage.aspx?SearchType=0" target="main_right">所有在线支付记录</a></li>
            <li><a href="PaymentLogManage.aspx?SearchType=1" target="main_right">最近10天内的新记录</a></li>
            <li><a href="PaymentLogManage.aspx?SearchType=2" target="main_right">最近一月内的新记录</a></li>
            <li><a href="PaymentLogManage.aspx?SearchType=3" target="main_right">未提交的在线支付记录</a></li>
            <li><a href="PaymentLogManage.aspx?SearchType=4" target="main_right">未成功的在线支付记录</a></li>
            <li><a href="PaymentLogManage.aspx?SearchType=5" target="main_right">支付成功的在线支付记录</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        高级查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select name="SelField" id="SelField" style="width: 140px">
                    <option value="PaymentNum">在线支付记录编号</option>
                    <option value="UserName">用户名</option>
                    <option value="PayTime">支付时间</option>
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
    <!--
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.options.selectedIndex].value;
        var url = "PaymentLogManage.aspx?SearchType=10&Field="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    //-->
    </script>

</asp:Content>

