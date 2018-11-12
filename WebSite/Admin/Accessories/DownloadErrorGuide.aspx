<%@ Page Language="C#" AutoEventWireup="True" Inherits="EasyOne.WebSite.Admin.Accessories.DownloadErrorGuide"
    MasterPageFile="~/Admin/Guide.master"
    Title="下载报错管理向导" Codebehind="DownloadErrorGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    下载报错管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahDownloadErrorManage" IsChecked="true" OperateCode="DownloadErrorManage"
                    href="DownloadErrorManage.aspx" runat="server" target="main_right">下载报错管理</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        下载报错查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select name="SelField" id="SelField" style="width: 140px">
                    <option value="">搜索类型</option>
                    <option value="InfoID">软件名称</option>
                    <option value="ErrorDate">最近天数</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px" type="text" value="关键字" class="inputtext"
                    onfocus="select()" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="查询" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    <!--
    function OpenMainRight(){
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.options.selectedIndex].value;
        var url = "DownloadErrorManage.aspx?Field="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    //-->
    </script>

</asp:Content>
