<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" Inherits="EasyOne.WebSite.Admin.Analytics.CounterGuide"
    Title="访问统计分析向导" Codebehind="CounterGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    访问统计分析
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        快速统计</div>
    <div class="guide">
        <ul>
            <li><a href="StatInfoListReport.aspx" target="main_right">综合统计</a></li>
            <li><a href="StatVisitorReport.aspx" target="main_right">访问记录</a></li>
            <li><a href="StatTimeReport.aspx?Action=Year" target="main_right">年 报 表</a></li>
            <li><a href="StatTimeReport.aspx?Action=Year&Type=All" target="main_right">全 部 年</a></li>
            <li><a href="StatTimeReport.aspx?Action=Month" target="main_right">月 报 表</a></li>
            <li><a href="StatTimeReport.aspx?Action=Month&Type=All" target="main_right">全 部 月</a></li>
            <li><a href="StatTimeReport.aspx?Action=Week" target="main_right">周 报 表</a></li>
            <li><a href="StatTimeReport.aspx?Action=Week&Type=All" target="main_right">全 部 周</a></li>
            <li><a href="StatTimeReport.aspx?Action=Day" target="main_right">日 报 表</a></li>
            <li><a href="StatTimeReport.aspx?Action=Day&Type=All" target="main_right">全 部 日</a></li>
        </ul>
    </div>
    <div class="guidecollapse" onclick="Switch(this)">
        访问分析</div>
    <div class="guide" style="display: none">
        <ul>
            <asp:PlaceHolder ID="PlhCounter" runat="server"></asp:PlaceHolder>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        访问统计查询</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="type" name="type" style="width: 140px" onchange="change_type()">
                    <option selected="selected" value="1">日报表</option>
                    <option value="2">月报表</option>
                    <option value="3">年报表</option>
                </select>
            </li>
            <li>
                <%= GetYearSelect %>
            </li>
            <li>
                <select id="qmonth" name="qmonth" style="width: 48px" onchange="change_it()">
                    <option value="01">1</option>
                    <option value="02">2</option>
                    <option value="03">3</option>
                    <option value="04">4</option>
                    <option value="05">5</option>
                    <option value="06">6</option>
                    <option value="07">7</option>
                    <option value="08">8</option>
                    <option value="09">9</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                </select>
                月
                <select id="qday" name="qday" style="width: 48px">
                    <option value="01">1</option>
                    <option value="02">2</option>
                    <option value="03">3</option>
                    <option value="04">4</option>
                    <option value="05">5</option>
                    <option value="06">6</option>
                    <option value="07">7</option>
                    <option value="08">8</option>
                    <option value="09">9</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                    <option value="17">17</option>
                    <option value="18">18</option>
                    <option value="19">19</option>
                    <option value="20">20</option>
                    <option value="21">21</option>
                    <option value="22">22</option>
                    <option value="23">23</option>
                    <option value="24">24</option>
                    <option value="25">25</option>
                    <option value="26">26</option>
                    <option value="27">27</option>
                    <option value="28">28</option>
                    <option value="29">29</option>
                    <option value="30">30</option>
                    <option value="31">31</option>
                </select>
                日 </li>
            <li>
                <input id="Search" type="button" class="inputbutton" value="查询" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    
    var form = document.forms[0];
    
    function change_type()
    {
        select_type=form.type.options[form.type.selectedIndex].text;
        switch(select_type)
        { 
            case '日报表' :form.qmonth.disabled=0;form.qday.disabled=0;break;
            case '月报表' :form.qmonth.disabled=0;form.qday.disabled=1;break;
            case '年报表' :form.qmonth.disabled=1;form.qday.disabled=1;break;
        } 
    }

    function change_it()
    { 
        select_type=form.type.options[form.type.selectedIndex].text;
        if (select_type=='日报表')
        {
            var select_item_y=form.qyear.options[form.qyear.selectedIndex].text;
            month29=select_item_y%4;
            select_item_m=form.qmonth.options[form.qmonth.selectedIndex].text;
            switch(select_item_m)
            { 
                case '2' :if (month29==0) {MD(29)}  else {MD(28)};break;
                case '4' : 
                case '6' : 
                case '9' : 
                case '11' : MD(30);break; 
                default : MD(31);break; 
            }
        }
    }
    
        function MD(days)
        { 
            j=form.qday.options.length; 
            for(k=0;k<j;k++) form.qday.options.remove(0); 
            for(i=0;i<days;i++)
            { 
                var day=document.createElement('OPTION'); 
                if(i<9)
                {
                    day.value='0'+(i+1).toString();                  
                }
                else
                {
                    day.value=i+1;
                }
                day.text=i+1;
                form.qday.options.add(day); 
                day.innerText=i+1; 
                form.qday.selectedIndex=0
            } 
        }
        
        function OpenMainRight()
        {
            var select_type=form.type.options[form.type.selectedIndex].text;
            var year = form.qyear.options[form.qyear.selectedIndex].value;
            var month = form.qmonth.options[form.qmonth.selectedIndex].value;
            var day = form.qday.options[form.qday.selectedIndex].value;
            var url;
            switch(select_type)
            { 
                case '日报表' :url ="StatTimeReport.aspx?Action=Day&Search="+year+"-"+month+"-"+day;break;
                case '月报表' :url ="StatTimeReport.aspx?Action=Month&Search="+year+"-"+month;break;
                case '年报表' :url ="StatTimeReport.aspx?Action=Year&Search="+year;break;
            }
            JumpToMainRight(url);
        }
        
        document.body.onload = new function()
        {
            var today = new Date();
            var year = today.getFullYear();          
            var month = today.getMonth()+1;
            var day = today.getDate();
            for(i=0;i<form.qyear.options.length;i++)
            {
                if(form.qyear.options[i].value == year)
                {
                    form.qyear.options[i].selected = true;
                    break;
                }
            }
            for(i=0; i<form.qmonth.options.length;i++)
            {
                if(form.qmonth.options[i].value == month)
                {
                    form.qmonth.options[i].selected = true;
                    break;
                }
            }
            
            for(i=0; i<form.qday.options.length;i++)
            {
                if(form.qday.options[i].value == day)
                {
                    form.qday.options[i].selected = true;
                    break;
                }
            }
        };        

    </script>

</asp:Content>
