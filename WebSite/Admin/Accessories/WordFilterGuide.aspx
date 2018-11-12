<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.WordFilterGuide" AutoEventWireup="True"
    Title="�ַ����˹�����" Codebehind="WordFilterGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ַ����˹���
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahWordFilterAdd" IsChecked="true" OperateCode="WordFilterManage"
                    href="WordFilter.aspx" runat="server" target="main_right">��ӹ����ַ�</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="WordFilterManage"
                    href="WordFilterManage.aspx" runat="server" target="main_right">��������ַ�</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �ַ���������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">�滻Ŀ��</option>
                    <option value="2">�滻����</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="�ؼ���" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="return OpenMainRight()" />
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
            alert("������Ҫ��ѯ��������"); 
            return false; 
        }
        field = objSel.options[objSel.options.selectedIndex].value;

        var url = "WordFilterManage.aspx?ListType="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
