<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.RegionGuide"
    Title="��������������" Codebehind="RegionGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ������������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="RegionManage.aspx" target="main_right">������������</a></li>
            <li><a href="Region.aspx" target="main_right">�����������</a></li>
        </ul>
    </div>
        <div class="guideexpand" onclick="Switch(this)">
        ������������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="Province" selected="selected">����ʡ��</option>
                    <option value="City">��������</option>
                    <option value="Area">��������</option>
                    <option value="PostCode">��������</option>
                    <option value="AreaCode">����</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="�ؼ���" class="inputtext"
                    onfocus="select()" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="return OpenMainRight()" />
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
            alert("������Ҫ��ѯ��������"); 
            return false; 
        }
        field = objSel.options[objSel.options.selectedIndex].value;
     
        var url = "RegionManage.aspx?SearchType="+ field +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>
</asp:Content>
