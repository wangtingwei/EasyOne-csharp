<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.KeyWordGuide" Title="�ؼ��ֹ�����" AutoEventWireup="True" Codebehind="KeyWordGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ؼ��ֹ���
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahKeyWordAdd" IsChecked="true" OperateCode="KeyWordManage"
                    href="KeyWord.aspx" runat="server" target="main_right">��ӹؼ���</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahKeyWordManage" IsChecked="true" OperateCode="KeyWordManage"
                    href="KeyWordManage.aspx" runat="server" target="main_right">����ؼ���</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ���ɸѡ</div>
    <div class="guide">
        <ul>
            <li><a href="KeyWordManage.aspx?listType=0" target="main_right">���йؼ���</a> </li>
            <li><a href="KeyWordManage.aspx?listType=1&SearchType=0" target="main_right">����ؼ���</a>
            </li>
            <li><a href="KeyWordManage.aspx?listType=1&SearchType=1" target="main_right">�����ؼ���</a>
            </li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �ؼ�������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="" selected="selected">���йؼ���</option>
                    <option value="0">����ؼ���</option>
                    <option value="1">�����ؼ���</option>
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

    <script type="text/javascript">
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        var listType
        if (keyword =="")
        {
            alert("������Ҫ��ѯ��������"); 
            return false; 
        }
        field = objSel.options[objSel.options.selectedIndex].value;

        if(field=="0"||field=="1")
        {
            listType=3;
        }else{
            listType=2;
        }
        var url = "KeyWordManage.aspx?SearchType="+ field +"&ListType="+ listType +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
