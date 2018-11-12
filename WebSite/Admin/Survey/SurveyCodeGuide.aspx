<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyCodeGuide"
    Title="问卷代码管理向导" Codebehind="SurveyCodeGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    问卷代码管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        快速查找</div>
    <div class="guide">
        <ul>
            <li><a href="SurveyCode.aspx" target="main_right">问卷代码调用</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        高级搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="Field" name="Field" style="width: 140px">
                    <option selected="selected" value="0">问卷名称</option>
                    <option value="1">创建日期</option>
                    <option value="2">截止日期</option>
                </select>
            </li>
            <li>
                <input maxlength="50" id="keyword" name="keyword" type="text" onfocus="select()" style="width: 134px;"
                    class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="查询" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var Field = document.getElementById("Field").options[document.getElementById("Field").selectedIndex].value;
        var keyword = document.getElementById("keyword").value;
        var url = "SurveyCode.aspx?Action=Search&SearchType="+Field+"&Keyword="+escape(keyword);
         JumpToMainRight(url);
    }
    </script>

</asp:Content>
