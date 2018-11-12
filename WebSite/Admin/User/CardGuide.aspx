<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardGuide"
    Title="��ֵ��������" Codebehind="CardGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ��ֵ������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        �������</div>
    <div class="guide">
        <ul>
            <li><a href="CardAdd.aspx" target="main_right">��ӳ�ֵ��</a></li>
            <li><a href="cardbatchadd.aspx" target="main_right">�������ɳ�ֵ��</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ���ٲ���</div>
    <div class="guide">
        <ul>
            <li><a href="CardsManage.aspx?CardType=-1" target="main_right">���г�ֵ��</a></li>
            <li><a href="CardsManage.aspx?CardStatus=1" target="main_right">����δʹ�õĳ�ֵ��</a></li>
            <li><a href="CardsManage.aspx?CardStatus=2" target="main_right">������ʹ�õĳ�ֵ��</a></li>
            <li><a href="CardsManage.aspx?CardStatus=3" target="main_right">������ʧЧ�ĳ�ֵ��</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ��ֵ������</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="CardType" name="CardType" style="width: 140px">
                    <option selected="selected" value="-1">=��ֵ������=</option>
                    <option value="0">��վ��ֵ��</option>
                    <option value="1">������˾��</option>
                </select>
            </li>
            <li>
                <select id="CardStatus" name="CardStatus" style="width: 140px">
                    <option selected="selected" value="-1">=��ֵ��״̬=</option>
                    <option value="1">δʹ��</option>
                    <option value="2">��ʹ��</option>
                    <option value="3">��ʧЧ</option>
                </select>
            </li>
            <li>
                <select id="Field" name="Field" style="width: 140px">
                    <option selected="selected" value="1">����</option>
                    <option value="2">��ֵ</option>
                    <option value="3">������</option>
                    <option value="4">ʹ����</option>
                </select>
            </li>
            <li>
                <input id="keyword" maxlength="50" name="keyword" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="����" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var CardType = document.getElementById("CardType").options[document.getElementById("CardType").selectedIndex].value;
        var CardStatus = document.getElementById("CardStatus").options[document.getElementById("CardStatus").selectedIndex].value;
        var Field = document.getElementById("Field").options[document.getElementById("Field").selectedIndex].value;
        var keyword = document.getElementById("keyword").value;
        if(keyword=='')
        {
            alert('�������ѯ�ؼ��֣�');
            return;
        }
        if(Field =="2")
        {
           if(isNaN(keyword)) 
           {
               alert("��������Ч����ֵ��"); 
               return;
           }
        }
        var url = "CardsManage.aspx?Action=Search&CardType="+CardType+"&CardStatus="+CardStatus+"&Field="+Field+"&Keyword="+escape(keyword);
        JumpToMainRight(url);
    }
    function changeColor(obj)
    {
        if(obj.value =="")
        {
            obj.style.backgroundColor="#DDD";
        }
    }
    </script>

</asp:Content>
