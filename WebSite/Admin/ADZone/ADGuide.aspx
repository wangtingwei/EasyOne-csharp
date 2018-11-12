<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.AD.ADGuide" Title="广告管理向导" Codebehind="ADGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    广告管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        广告管理</div>
    <div class="guide">
        <ul>
            <li><a href="ADManage.aspx" id="EahADManage" target="main_right">网站广告管理</a> </li>
            <li><a href="Advertisement.aspx" id="EahADAdd" target="main_right">添加新广告</a> </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        广告搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">广告名称</option>
                    <option value="2">广告简介</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="" class="inputtext" />
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
     
       var url = "ADManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
    </script>

</asp:Content>
