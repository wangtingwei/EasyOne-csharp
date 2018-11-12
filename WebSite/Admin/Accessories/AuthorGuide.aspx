<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.AuthorGuide" AutoEventWireup="True" Title="作者与来源管理向导" Codebehind="AuthorGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    作者管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        作者管理</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorAdd" IsChecked="true" OperateCode="AuthorManage"
                    href="Author.aspx" runat="server" target="main_right">添加作者</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="AuthorManage"
                    href="AuthorManage.aspx" runat="server" target="main_right">作者管理</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        分类列表</div>
    <div class="guide">
        <ul>
            <li><a href="AuthorManage.aspx" target="main_right">显示全部</a></li>
            <asp:Repeater ID="RptAuthorTypeList" runat="server">
                <ItemTemplate>
                    <li><a onclick="OpenMainRightType('<%# Eval("DataTextField") %>')" href="#">
                        <%# Eval("DataTextField")%>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        作者搜索</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="0" selected="selected">作者名</option>
                    <option value="1">作者地址</option>
                    <option value="2">作者电话</option>
                    <option value="3">作者简介</option>
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
    function OpenMainRightType(value)
    {
        var url= "AuthorManage.aspx?ListType=4&SearchType="+escape(value);
        JumpToMainRight(url);
    }
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
         
        var url = "AuthorManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
