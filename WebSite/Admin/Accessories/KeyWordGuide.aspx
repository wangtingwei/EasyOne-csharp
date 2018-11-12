<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.KeyWordGuide" Title="关键字管理向导" AutoEventWireup="True" Codebehind="KeyWordGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    关键字管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahKeyWordAdd" IsChecked="true" OperateCode="KeyWordManage"
                    href="KeyWord.aspx" runat="server" target="main_right">添加关键字</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahKeyWordManage" IsChecked="true" OperateCode="KeyWordManage"
                    href="KeyWordManage.aspx" runat="server" target="main_right">管理关键字</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        快捷筛选</div>
    <div class="guide">
        <ul>
            <li><a href="KeyWordManage.aspx?listType=0" target="main_right">所有关键字</a> </li>
            <li><a href="KeyWordManage.aspx?listType=1&SearchType=0" target="main_right">常规关键字</a>
            </li>
            <li><a href="KeyWordManage.aspx?listType=1&SearchType=1" target="main_right">搜索关键字</a>
            </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        关键字搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="" selected="selected">所有关键字</option>
                    <option value="0">常规关键字</option>
                    <option value="1">搜索关键字</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="关键字" class="inputtext"
                    onfocus="select()" />
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
        var listType
        if (keyword =="")
        {
            alert("请输入要查询的条件！"); 
            return false; 
        }
        field = objSel.options[objSel.options.selectedIndex].value;

        if(field=="0"||field=="1")
        {
            listType=3;
        }else{
            listType=2;
        }
        var url = "KeyWordManage.aspx?SearchType="+ field +"&ListType="+ listType +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
