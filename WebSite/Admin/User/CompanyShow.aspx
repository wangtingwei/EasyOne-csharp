<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.CompanyShow" Title="无标题页" Codebehind="CompanyShow.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                单位信息</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                单位成员</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td>
                    <pe:CompanyInfo ID="CompanyInfo1" runat="server" />
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs1" style="display: none">
            <tr class="tdbg">
                <td>
                    <pe:CompanyMemberManage ID="CompanyMemberManage1" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <script language="javascript" type="text/javascript">
      function ShowTabs(ID){
           for (i=0;i< 2;i++){
                if(i == ID){
                    document.getElementById("TabTitle" + i).className="titlemouseover";
                    document.getElementById("Tabs" + i).style.display="";
                }
                else{
                    document.getElementById("TabTitle" + i).className="tabtitle";
                    document.getElementById("Tabs" + i).style.display="none";
                }
           }
      } 
    </script>
 </asp:Content>
