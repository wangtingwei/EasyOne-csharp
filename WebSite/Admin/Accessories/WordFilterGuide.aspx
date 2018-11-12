<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.WordFilterGuide" AutoEventWireup="True"
    Title="字符过滤管理向导" Codebehind="WordFilterGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    字符过滤管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahWordFilterAdd" IsChecked="true" OperateCode="WordFilterManage"
                    href="WordFilter.aspx" runat="server" target="main_right">添加过滤字符</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="WordFilterManage"
                    href="WordFilterManage.aspx" runat="server" target="main_right">管理过滤字符</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        字符过滤搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">替换目标</option>
                    <option value="2">替换内容</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="关键字" class="inputtext" />
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

        var url = "WordFilterManage.aspx?ListType="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
