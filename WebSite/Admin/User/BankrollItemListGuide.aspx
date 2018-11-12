<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemListGuide"
    Title="资金明细查询向导" Codebehind="BankrollItemListGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    资金明细查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        快速查找</div>
    <div class="guide">
        <ul>
            <li><a href="BankrollItemList.aspx?SearchType=0" target="main_right">所有资金明细记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=1" target="main_right">10天内的资金明细记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=2" target="main_right">当前月的资金明细记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=3" target="main_right">所有收入记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=4" target="main_right">所有支出记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=5" target="main_right">所有已确认的记录</a></li>
            <li><a href="BankrollItemList.aspx?SearchType=6" target="main_right">所有未确认的记录</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        复杂查询</div>
    <div class="guide">
        <ul>
            <li><a href="BankrollItemListSearch.aspx" target="main_right">复杂查询</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        高级查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px">
                    <option selected="selected" value="0">客户名称</option>
                    <option value="1">用户名称</option>
                    <option value="2">银行名称</option>
                    <option value="3">交易日期</option>
                </select>
            </li>
            <li>
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="查询" onclick="OpenMainRight()" />
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
            alert("请输入有效日期，日期格式如：YYYY-MM-DD");
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
