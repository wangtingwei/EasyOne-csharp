<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemListGuide"
    Title="�ʽ���ϸ��ѯ��" Codebehind="BankrollItemListGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ʽ���ϸ��ѯ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ���ٲ���</div>
    <div class="guide">
        <ul>
            <li><a href="BankrollItemList.aspx?SearchType=0" target="main_right">�����ʽ���ϸ��¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=1" target="main_right">10���ڵ��ʽ���ϸ��¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=2" target="main_right">��ǰ�µ��ʽ���ϸ��¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=3" target="main_right">���������¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=4" target="main_right">����֧����¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=5" target="main_right">������ȷ�ϵļ�¼</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=6" target="main_right">����δȷ�ϵļ�¼</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ���Ӳ�ѯ</div>
    <div class="guide">
        <ul>
            <li><a href="BankrollItemListSearch.aspx" target="main_right">���Ӳ�ѯ</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �߼���ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option selected="selected" value="0">�ͻ�����</option>
                    <option value="1">�û�����</option>
                    <option value="2">��������</option>
                    <option value="3">��������</option>
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

    <script type="text/javascript">

   
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        field = objSel.options[objSel.selectedIndex].value;
        if(field=="3" && (!isDate(keyword)))
        {
            alert("��������Ч���ڣ����ڸ�ʽ�磺YYYY-MM-DD");
            return;
        }
        var url = "BankrollItemList.aspx?SearchType=10&Field="+field+"&KeyWord="+escape(keyword);
      JumpToMainRight(url);
    }

  function   isDate(str)
  {   
      if(!str.match(/^\d{4}\-\d\d?\-\d\d?$/)){return   false;}   
      var   ar=str.replace(/\-0/g,"-").split("-");   
      ar=new   Array(parseInt(ar[0]),parseInt(ar[1])-1,parseInt(ar[2]));   
      var   d=new   Date(ar[0],ar[1],ar[2]);   
      return   d.getFullYear()==ar[0]   &&   d.getMonth()==ar[1]   &&   d.getDate()==ar[2];   
  }   
   </script>

</asp:Content>
