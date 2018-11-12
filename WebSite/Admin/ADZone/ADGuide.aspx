<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.AD.ADGuide" Title="��������" Codebehind="ADGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ������</div>
    <div class="guide">
        <ul>
            <li><a href="ADManage.aspx" id="EahADManage" target="main_right">��վ������</a> </li>
            <li><a href="Advertisement.aspx" id="EahADAdd" target="main_right">����¹��</a> </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="1" selected="selected">�������</option>
                    <option value="2">�����</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" value="" class="inputtext" />
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
     
       var url = "ADManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
    </script>

</asp:Content>
