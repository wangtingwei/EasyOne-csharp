<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.AD.ADZoneGuide"
    Title="广告版位管理" Codebehind="ADZoneGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    广告版位管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        广告版位管理</div>
    <div class="guide">
        <ul>
            <li><a id="EahADZoneManage" href="ADZoneManage.aspx" target="main_right">广告版位管理</a></li>
            <li><a id="EahADZoneAdd" href="ADZone.aspx" target="main_right">添加广告版位</a></li>
            <li><a id="EahADZoneImport" href="ADZoneImport.aspx" target="main_right">广告版位导入</a></li>
            <li><a id="EahADZoneExport" href="ADZoneExport.aspx" target="main_right">广告版位导出</a></li>
            <li><a id="EahADJsManage" href="JSTemplate.aspx" target="main_right">广告JS模板</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        广告版位搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">版位名称</option>
                    <option value="2">版位简介</option>
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
       var url = "ADZoneManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
    </script>

</asp:Content>
