<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Template.TemplateManageGuide"
    Title="模板管理向导" Codebehind="TemplateManageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    模板管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        模板管理</div>
    <div>
        <pe:FileTreeView ID="TrvTemplateDir" NodeNavigateUrl="TemplateManage.aspx?Dir=" DirectoriesXmlUrl="TemplateDirectoriesXML.aspx?Dir="
            RootAction="TemplateManage.aspx" runat="server">
        </pe:FileTreeView>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        模板查询</div>
    <div class="guidesearch">
        <ul>
            <li style="text-align: left; margin-left: 18px;">搜索类型： </li>
            <li>
                <asp:DropDownList ID="DropSearch" runat="server" Width="140px">
                    <asp:ListItem Selected="True" Value="0">模板文件名</asp:ListItem>
                    <asp:ListItem Value="1">模板内容</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li style="text-align: left; margin-left: 18px;">搜索内容： </li>
            <li>
                <asp:TextBox ID="TxtSearch" Width="134px" runat="server"></asp:TextBox>
            </li>
            <li style="text-align: left; margin-left: 18px;">搜索范围： </li>
            <li>
                <asp:DropDownList ID="DropSearchFile" runat="server" Width="140px">
                    <asp:ListItem Text="/" Value="/"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value=" 搜索 " onclick="return OpenMainRight()" />
            </li>
        </ul>
    </div>
    <script type="text/javascript">
    function OpenMainRight()
    {       
        var dropSearch = document.getElementById("<%=DropSearch.ClientID %>").options[document.getElementById("<%=DropSearch.ClientID %>").options.selectedIndex].value;
        var txtSearch = document.getElementById("<%=TxtSearch.ClientID %>").value;
        var dropSearchFile = document.getElementById("<%=DropSearchFile.ClientID %>").options[document.getElementById("<%=DropSearchFile.ClientID %>").options.selectedIndex].value;
        
        if (txtSearch =="")
        {
            alert("请输入要查询的条件！"); 
            return false; 
        }
        
        if (dropSearchFile =="")
        {
            alert("请输入要查询的范围！"); 
            return false; 
        }
        var url = "TemplateManage.aspx?Action=TemplateSearch&DropSearch="+escape(dropSearch)+"&TxtSearch="+escape(txtSearch)+"&Dir="+escape(dropSearchFile);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
