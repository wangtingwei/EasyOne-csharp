<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGuide"
    Title="��Ա������" Codebehind="UserGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ��Ա����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="UserManage.aspx" target="main_right">��Ա������ҳ</a></li>
            <li>
                <pe:ExtendedAnchor ID="EahUserAdd" IsChecked="true" OperateCode="UserAdd" href="User.aspx"
                    runat="server" target="main_right">����»�Ա</pe:ExtendedAnchor></li>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        �������</div>
    <div class="guide" style="display: none">
        <ul>
            <li><a href="UserManage.aspx?listType=0&GroupID=0&GroupName=���л�Ա" target="main_right">
                ���л�Ա</a></li>
            <asp:Repeater ID="RptGroups" runat="server">
                <ItemTemplate>
                    <li><a href="UserManage.aspx?listType=10&GroupID=<%#Eval("GroupId")%>&GroupName=<%#Server.UrlEncode(Eval("GroupName").ToString())%>"
                        target="main_right">
                        <%#Eval("GroupName")%>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        ���ٲ���</div>
    <div class="guide" style="display: none">
        <ul>
            <li><a href="userManage.aspx?ListType=1" target="main_right">�������TOP100</a> </li>
            <li><a href="userManage.aspx?ListType=2" target="main_right">�������ٵ�100����Ա</a> </li>
            <li><a href="userManage.aspx?ListType=3" target="main_right">���24Сʱ�ڵ�¼�Ļ�Ա</a> </li>
            <li><a href="userManage.aspx?ListType=4" target="main_right">���24Сʱ��ע��Ļ�Ա</a> </li>
            <li><a href="userManage.aspx?ListType=5" target="main_right">���б���ס�Ļ�Ա</a> </li>
            <li runat="server" id="ListType6"><a href="userManage.aspx?ListType=6" target="main_right">
                ��ȯ������0�Ļ�Ա</a> </li>
            <li runat="server" id="ListType7"><a href="userManage.aspx?ListType=7" target="main_right">
                ���ִ���0�Ļ�Ա</a> </li>
            <li runat="server" id="ListType8"><a href="userManage.aspx?ListType=8" target="main_right">
                �ʽ�������0�Ļ�Ա</a> </li>
            <li runat="server" id="ListType9"><a href="userManage.aspx?ListType=9" target="main_right">
                �ʽ����С�ڵ���0�Ļ�Ա</a> </li>
            <li><a href="userManage.aspx?ListType=14" target="main_right">δͨ���ʼ���֤�Ļ�Ա</a> </li>
            <li><a href="userManage.aspx?ListType=15" target="main_right">δͨ������Ա��֤�Ļ�Ա</a> </li>
            <li runat="server" id="ListType16"><a href="userManage.aspx?ListType=16" target="main_right">
                ���ѽ��TOP100</a> </li>
            <li runat="server" id="ListType17"><a href="userManage.aspx?ListType=17" target="main_right">
                ���ѵ�ȯTOP100</a> </li>
            <li runat="server" id="ListType18"><a href="userManage.aspx?ListType=18" target="main_right">
                ���ѻ���TOP100</a> </li>
            <li runat="server" id="ListType19"><a href="userManage.aspx?ListType=19" target="main_right">
                ��Ч��ʣ��5��Ļ�Ա</a> </li>
            <li><a href="userManage.aspx?ListType=36" target="main_right">�����˵���Ȩ�޵Ļ�Ա</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��Ա��ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option value="11">��ԱID</option>
                    <option value="12" selected="selected">��Ա����</option>
                    <option value="13">�����ʼ�</option>
                    <option value='20'>������ҳ</option>
                    <option value='21'>��ʵ����</option>
                    <option value='22'>���֤����</option>
                    <option value='23'>��λ����</option>
                    <option value='24'>��ϵ��ַ</option>
                    <option value='25'>�ֻ�����</option>
                    <option value='26'>�칫�绰</option>
                    <option value='27'>��ͥ�绰</option>
                    <option value='28'>С��ͨ</option>
                    <option value='29'>�������</option>
                    <option value='30'>QQ��</option>
                    <option value='31'>ICQ��</option>
                    <option value='32'>MSN�ʺ�</option>
                    <option value='33'>Yahoo</option>
                    <option value='34'>UC��</option>
                    <option value='35'>Aim�ʺ�</option> 
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
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
        
        if (field == "11"){
            if(checknumber(keyword)) 
            { 
                alert("ֻ�����������֣�"); 
                return false; 
            } 
        }
        if (field == "20"){
            var regexp = /http:\/\//gi;
            keyword = keyword.replace(regexp,"");
        }
        
        var url = "userManage.aspx?ListType="+field+"&KeyWord="+escape(keyword);
       JumpToMainRight(url);
    }
   
    function checknumber(String) 
    { 
        var Letters = "1234567890"; 
        var i;
        var c; 
        for( i = 0; i < String.length; i ++ ) 
        { 
            c = String.charAt( i ); 
            if (Letters.indexOf( c ) ==-1) 
            { 
                return true; 
            } 
        } 
        return false; 
    } 
    </script>

</asp:Content>
