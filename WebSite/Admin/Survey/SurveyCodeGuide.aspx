<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyCodeGuide"
    Title="�ʾ���������" Codebehind="SurveyCodeGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ʾ�������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ���ٲ���</div>
    <div class="guide">
        <ul>
            <li><a href="SurveyCode.aspx" target="main_right">�ʾ�������</a></li>
        </ul>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        �߼�����</div>
    <div class="guidesearch">
        <ul>
            <li>
                <select id="Field" name="Field" style="width: 140px">
                    <option selected="selected" value="0">�ʾ�����</option>
                    <option value="1">��������</option>
                    <option value="2">��ֹ����</option>
                </select>
            </li>
            <li>
                <input maxlength="50" id="keyword" name="keyword" type="text" onfocus="select()" style="width: 134px;"
                    class="inputtext" />
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value="��ѯ" onclick="OpenMainRight()" />
            </li>
        </ul>
    </div>

    <script type="text/javascript">
    function OpenMainRight()
    {
        var Field = document.getElementById("Field").options[document.getElementById("Field").selectedIndex].value;
        var keyword = document.getElementById("keyword").value;
        var url = "SurveyCode.aspx?Action=Search&SearchType="+Field+"&Keyword="+escape(keyword);
         JumpToMainRight(url);
    }
    </script>

</asp:Content>
