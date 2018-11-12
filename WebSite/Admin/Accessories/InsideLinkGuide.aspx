<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.InsideLinkGuide" AutoEventWireup="True"
    Title="站内链接管理向导" Codebehind="InsideLinkGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    站内链接管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahInsideLinkAdd" IsChecked="true" OperateCode="InsideLinkManage"
                    href="InsideLink.aspx" runat="server" target="main_right">添加站内链接</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahInsideLinkManage" IsChecked="true" OperateCode="InsideLinkManage"
                    href="InsideLinkManage.aspx" runat="server" target="main_right">管理站内链接</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        站内链接搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">目标</option>
                    <option value="3">地址</option>
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
        
        if (keyword =="")
        {
            alert("请输入要查询的条件！"); 
            return false; 
        }
        field = objSel.options[objSel.options.selectedIndex].value;
        var url = "InsideLinkManage.aspx?ListType="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
