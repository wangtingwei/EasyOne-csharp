<%@ Page Language="C#" AutoEventWireup="True" Inherits="EasyOne.WebSite.Admin.Accessories.DownloadErrorGuide"
    MasterPageFile="~/Admin/Guide.master"
    Title="���ر��������" Codebehind="DownloadErrorGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ���ر������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahDownloadErrorManage" IsChecked="true" OperateCode="DownloadErrorManage"
                    href="DownloadErrorManage.aspx" runat="server" target="main_right">���ر������</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ���ر����ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select name="SelField" id="SelField" style="width: 140px">
                    <option value="">��������</option>
                    <option value="InfoID">�������</option>
                    <option value="ErrorDate">�������</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px" type="text" value="�ؼ���" class="inputtext"
                    onfocus="select()" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="OpenMainRight()" />
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
