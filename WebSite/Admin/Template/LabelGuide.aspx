<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="True"
    Inherits="EasyOne.WebSite.Admin.Accessories.LabelGuide"
    Title="��ǩ������" Codebehind="LabelGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ��ǩ����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ���ݱ�ǩ</div>
    <div class="guide">
        <ul>
            <li><a href="Label.aspx" target="main_right">��ӱ�ǩ</a></li>
            <li><a href="LabelManage.aspx" target="main_right">��ǩ����</a></li>
            <li><a href="LabelBatch.aspx" target="main_right">��ǩ��������</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��ǩ��ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option selected="selected" value="0">��ǩ������</option>
                    <option value="1">��ǩ���ݰ���</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��ҳ��ǩ</div>
    <div class="guide">
        <ul>
            <li><a href="Pager.aspx" target="main_right">��ӷ�ҳ��ǩ</a></li>
            <li><a href="PagerManage.aspx" target="main_right">��ҳ��ǩ����</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��ҳ��ǩ��ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SearchType" style="width: 140px">
                    <option selected="selected" value="0">��ǩ������</option>
                    <option value="1">��ǩ���ݰ���</option>
                </select>
            </li>
            <li>
                <input id="TxtPagerKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="Button1" type="button" class="inputbutton" value="��ѯ" onclick="OpenPagerMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript"> 
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.selectedIndex].value;
        var url = "LabelManage.aspx?type=1&field="+field+"&keyword="+escape(keyword);
        JumpToMainRight(url);
    }
    function OpenPagerMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtPagerKeyWord").value;
        if(keyword == '')
        {
        alert("�ؼ��ֲ���Ϊ�գ�");
        }
        else
        {
        var objSel = document.getElementById("SearchType");
        field = objSel.options[objSel.selectedIndex].value;
        var url = "PagerManage.aspx?type=1&SearchType="+field+"&keyword="+escape(keyword);
        JumpToMainRight(url);
        }
    }
    </script>

</asp:Content>
