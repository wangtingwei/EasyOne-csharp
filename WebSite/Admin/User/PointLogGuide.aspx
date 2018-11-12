<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.PointLogGuide"
    MasterPageFile="~/Admin/Guide.master" Title="��ȯ��ϸ��ѯ��" Codebehind="PointLogGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    <pe:ShowPointName ID="ShowPointName3" runat="server" />��ϸ��ѯ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ���ٲ���</div>
    <div class="guide">
        <ul>
            <li><a href="PointLog.aspx?SearchType=-1" target="main_right">����<pe:ShowPointName ID="ShowPointName" runat="server" />��ϸ��¼</a></li>
            <li><a href="PointLog.aspx?SearchType=0" target="main_right">ʮ����<pe:ShowPointName ID="ShowPointName1" runat="server" />��ϸ��¼</a></li>
            <li><a href="PointLog.aspx?SearchType=1" target="main_right">һ����<pe:ShowPointName ID="ShowPointName2" runat="server" />��ϸ��¼</a></li>
            <li><a href="PointLog.aspx?SearchType=2" target="main_right">���������¼</a></li>
            <li><a href="PointLog.aspx?SearchType=3" target="main_right">����֧����¼</a></li>
        </ul>
    </div>
<%--    <div class="guideexpand" onclick="Switch(this)">
        ���Ӳ�ѯ</div>
    <div class="guide">
        <ul>
            <li><a href="LogSearch.aspx?Action=PointLog&LogType=1" target="main_right">���Ӳ�ѯ</a></li>
        </ul>
    </div>--%>
    <div class="guideexpand" onclick="Switch(this)">
        �߼���ѯ</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="SelField" style="width: 140px" onchange="ChanageSelect(this)">
                    <option value="1" selected="selected">�û�����</option>
                    <option value="2">����ʱ��</option>
                </select>
            </li>
            <li id="key" style="display: ">
                <input id="TxtKeyWord" style="width: 134px;" type="text" class="inputtext" />
            </li>
            <li id="dpk" style="display: none">
                <pe:DatePicker ID="Dpk" runat="server" Width="115px"></pe:DatePicker>
                <br />
                <pe:DateValidator ID="Vdate" ControlToValidate="Dpk" Display="Dynamic" SetFocusOnError="true"
                    runat="server"></pe:DateValidator>
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="return OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    
    function ChanageSelect(select)
    {
        if(select.options[select.options.selectedIndex].value==1)
        {
            document.getElementById("dpk").style.display="none";
            document.getElementById("key").style.display="";
        }else{
            document.getElementById("dpk").style.display="";
            document.getElementById("key").style.display="none";
        }
    }
    ChanageSelect(document.getElementById("SelField"));
    function OpenMainRight()
    {
        var field=0;
        var keyword = document.getElementById("TxtKeyWord").value.trim();
        var objSel = document.getElementById("SelField");
        if(objSel.options[objSel.options.selectedIndex].value==1)
        {
            keyword=document.getElementById("TxtKeyWord").value.trim();
        }else{
            keyword=document.getElementById("<%=Dpk.ClientID %>").value
            if(!isVbDate(keyword)){
                return false;
            }
        }
        field=objSel.options[objSel.options.selectedIndex].value;
        var url = "PointLog.aspx?SearchType=10&Field="+field+"&KeyWord="+escape(keyword);
        JumpToMainRight(url);
    }
        
    function isVbDate(str)
    {
         var reg = /^(\d{4})(-|\/|\.)(\d{1,2})\2(\d{1,2})$/;
         result = str.match(reg);
         if(result == null)
         {
             return false;
         }
         var y, m, d;

         //����û�����֮���
         y = result[1];

         //����û�����֮�·�
         m = parseInt(result[3], 10);

         //����û�����֮��
         d = parseInt(result[4], 10);

         if ((m < 1) || (m > 12) || (d < 1) || (d > 31)) return false;
         if (((m == 4) || (m == 6) || (m == 9) || (m == 11)) && (d > 30)) return false;
         if((y % 4) == 0)
         {
             if ((m == 2) && (d > 29)) return false;
         }else
         {
             if ((m == 2) && (d > 28)) return false;
         }
         return true;
    }
    </script>

</asp:Content>
