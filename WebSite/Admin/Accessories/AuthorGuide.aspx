<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" Inherits="EasyOne.WebSite.Admin.Accessories.AuthorGuide" AutoEventWireup="True" Title="��������Դ������" Codebehind="AuthorGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ���߹���
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ���߹���</div>
    <div class="guide">
        <ul>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorAdd" IsChecked="true" OperateCode="AuthorManage"
                    href="Author.aspx" runat="server" target="main_right">�������</pe:ExtendedAnchor></li>
            <li>
                <pe:ExtendedAnchor ID="EahAuthorManage" IsChecked="true" OperateCode="AuthorManage"
                    href="AuthorManage.aspx" runat="server" target="main_right">���߹���</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �����б�</div>
    <div class="guide">
        <ul>
            <li><a href="AuthorManage.aspx" target="main_right">��ʾȫ��</a></li>
            <asp:Repeater ID="RptAuthorTypeList" runat="server">
                <ItemTemplate>
                    <li><a onclick="OpenMainRightType('<%# Eval("DataTextField") %>')" href="#">
                        <%# Eval("DataTextField")%>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="0" selected="selected">������</option>
                    <option value="1">���ߵ�ַ</option>
                    <option value="2">���ߵ绰</option>
                    <option value="3">���߼��</option>
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
    function OpenMainRightType(value)
    {
        var url= "AuthorManage.aspx?ListType=4&SearchType="+escape(value);
        JumpToMainRight(url);
    }
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
         
        var url = "AuthorManage.aspx?ListType="+ field +"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
