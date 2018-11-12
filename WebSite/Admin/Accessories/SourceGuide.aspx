<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.SourceGuide" Codebehind="SourceGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    来源管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        来源管理</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahSourceAdd" IsChecked="true" OperateCode="SourceManage"
                    href="Source.aspx" runat="server" target="main_right">添加来源</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahSourceManage" IsChecked="true" OperateCode="SourceManage"
                    href="SourceManage.aspx" runat="server" target="main_right">来源管理</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        分类列表</div>
    <div class="guide">
        <ul>
            <li><a href="SourceManage.aspx" target="main_right">显示全部</a></li>
            <asp:Repeater ID="RptSourceTypeList" runat="server" DataSourceID="Ods2">
                <ItemTemplate>
                    <li><a href="SourceManage.aspx?SearchType=5&SourceType=<%# Eval("Name") %>" target="main_right">
                        <%# Eval("Name") %></li>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        来源搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="0" selected="selected">来源名称</option>
                    <option value="1">来源地址</option>
                    <option value="2">来源电话</option>
                    <option value="3">来源简介</option>
                    <option value="4">联系人</option>
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
     
        var url = "SourceManage.aspx?SearchType="+ field +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
