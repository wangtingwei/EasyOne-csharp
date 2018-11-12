<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AdministratorGuide"
    Title="无标题页" Codebehind="AdministratorGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    管理员管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        管理员管理</div>
    <div class="guide">
        <ul>
            <li><a href="AdministratorManage.aspx" target="main_right">管理员管理</a></li>
            <li><a href="Administrator.aspx" target="main_right">添加管理员</a> </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        按角色查询</div>
    <div class="guide">
        <ul>
            <asp:Repeater ID="RptRoles" runat="server">
                <ItemTemplate>
                    <li><a href="AdministratorManage.aspx?RoleId=<%# Eval("RoleId") %>" target="main_right"><%# Eval("RoleName")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        快速查找</div>
    <div class="guide" style="display: none">
        <ul>
            <li><a href="AdministratorManage.aspx?ListType=1" target="main_right">最近一个月未修改密码管理员</a></li>
        </ul>
    </div>
</asp:Content>
