<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.RegionGuide"
    Title="行政区划管理向导" Codebehind="RegionGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    行政区划管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li><a href="RegionManage.aspx" target="main_right">行政区划管理</a></li>
            <li><a href="Region.aspx" target="main_right">添加行政区划</a></li>
        </ul>
    </div>
        <div class="guideexpand" onclick="Switch(this)">
        行政区划搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="Province" selected="selected">所属省份</option>
                    <option value="City">所属城市</option>
                    <option value="Area">所属县区</option>
                    <option value="PostCode">邮政编码</option>
                    <option value="AreaCode">区号</option>
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
    <asp:ObjectDataSource ID="Ods2" runat="server" SelectMethod="GetSourceTypeList" TypeName="EasyOne.Accessories.Source">
    </asp:ObjectDataSource>

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
     
        var url = "RegionManage.aspx?SearchType="+ field +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>
</asp:Content>
