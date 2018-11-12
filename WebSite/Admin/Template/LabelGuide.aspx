<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="True"
    Inherits="EasyOne.WebSite.Admin.Accessories.LabelGuide"
    Title="标签管理向导" Codebehind="LabelGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    标签管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        内容标签</div>
    <div class="guide">
        <ul>
            <li><a href="Label.aspx" target="main_right">添加标签</a></li>
            <li><a href="LabelManage.aspx" target="main_right">标签管理</a></li>
            <li><a href="LabelBatch.aspx" target="main_right">标签批量设置</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        标签查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option selected="selected" value="0">标签名包含</option>
                    <option value="1">标签内容包含</option>
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
    <div class="guideexpand" onclick="Switch(this)">
        分页标签</div>
    <div class="guide">
        <ul>
            <li><a href="Pager.aspx" target="main_right">添加分页标签</a></li>
            <li><a href="PagerManage.aspx" target="main_right">分页标签管理</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        分页标签查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SearchType" style="width: 140px">
                    <option selected="selected" value="0">标签名包含</option>
                    <option value="1">标签内容包含</option>
                </select>
            </li>
            <li>
                <input id="TxtPagerKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="Button1" type="button" class="inputbutton" value="查询" onclick="OpenPagerMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript"> 
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.selectedIndex].value;
        var url = "LabelManage.aspx?type=1&field="+field+"&keyword="+escape(keyword);
        JumpToMainRight(url);
    }
    function OpenPagerMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtPagerKeyWord").value;
        if(keyword == '')
        {
        alert("关键字不能为空！");
        }
        else
        {
        var objSel = document.getElementById("SearchType");
        field = objSel.options[objSel.selectedIndex].value;
        var url = "PagerManage.aspx?type=1&SearchType="+field+"&keyword="+escape(keyword);
        JumpToMainRight(url);
        }
    }
    </script>

</asp:Content>
