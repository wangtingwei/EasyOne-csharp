<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.CompanyGuide" Title="无标题页" Codebehind="CompanyGuide.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
企业管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        常规操作</div>
    <div class="guide">
        <ul>
            <li><a href="CompanyManage.aspx" target="main_right">企业管理</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        高级查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="CompanyName">企业名称</option>
                    <option value="Province">省/市</option>
                    <option value="City">市/县/区</option>
                    <option value="Address">联系地址</option>
                    <option value="Phone">联系电话</option>
                    <option value="Fax">传真号码</option>
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

    <script type="text/javascript">
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.options.selectedIndex].value;
        var url = "CompanyManage.aspx?Field="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script></asp:Content>
