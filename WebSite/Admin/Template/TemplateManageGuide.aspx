<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Template.TemplateManageGuide"
    Title="ģ�������" Codebehind="TemplateManageGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    ģ�����
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        ģ�����</div>
    <div>
        <pe:FileTreeView ID="TrvTemplateDir" NodeNavigateUrl="TemplateManage.aspx?Dir=" DirectoriesXmlUrl="TemplateDirectoriesXML.aspx?Dir="
            RootAction="TemplateManage.aspx" runat="server">
        </pe:FileTreeView>
    </div>
    <div class="guideexpand" onclick="Switch(this)">
        ģ���ѯ</div>
    <div class="guidesearch">
        <ul>
            <li style="text-align: left; margin-left: 18px;">�������ͣ� </li>
            <li>
                <asp:DropDownList ID="DropSearch" runat="server" Width="140px">
                    <asp:ListItem Selected="True" Value="0">ģ���ļ���</asp:ListItem>
                    <asp:ListItem Value="1">ģ������</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li style="text-align: left; margin-left: 18px;">�������ݣ� </li>
            <li>
                <asp:TextBox ID="TxtSearch" Width="134px" runat="server"></asp:TextBox>
            </li>
            <li style="text-align: left; margin-left: 18px;">������Χ�� </li>
            <li>
                <asp:DropDownList ID="DropSearchFile" runat="server" Width="140px">
                    <asp:ListItem Text="/" Value="/"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <input id="BtnSearch" type="button" class="inputbutton" value=" ���� " onclick="return OpenMainRight()" />
            </li>
        </ul>
    </div>
    <script type="text/javascript">
    function OpenMainRight()
    {       
        var dropSearch = document.getElementById("<%=DropSearch.ClientID %>").options[document.getElementById("<%=DropSearch.ClientID %>").options.selectedIndex].value;
        var txtSearch = document.getElementById("<%=TxtSearch.ClientID %>").value;
        var dropSearchFile = document.getElementById("<%=DropSearchFile.ClientID %>").options[document.getElementById("<%=DropSearchFile.ClientID %>").options.selectedIndex].value;
        
        if (txtSearch =="")
        {
            alert("������Ҫ��ѯ��������"); 
            return false; 
        }
        
        if (dropSearchFile =="")
        {
            alert("������Ҫ��ѯ�ķ�Χ��"); 
            return false; 
        }
        var url = "TemplateManage.aspx?Action=TemplateSearch&DropSearch="+escape(dropSearch)+"&TxtSearch="+escape(txtSearch)+"&Dir="+escape(dropSearchFile);
        JumpToMainRight(url);
    }
    </script>

</asp:Content>
