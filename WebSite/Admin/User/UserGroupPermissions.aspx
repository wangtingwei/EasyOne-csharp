<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.UserGroupPermissions" Title="用户会员组权限设置" Codebehind="UserGroupPermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <asp:Label ID="LblTitle" runat="server" Text="设置会员组权限"></asp:Label>
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员组名称：</strong></td>
            <td>
                <asp:Label ID="LblGroupName" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员组类型：</strong></td>
            <td>
                <asp:Label ID="LblGropType" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员组说明：</strong></td>
            <td>
                <asp:Label ID="LblDescription" runat="server" Text="" />
            </td>
        </tr>
        <pe:UserIndividuation ID="UserIndividuation" runat="server" />
    </table>
    <br />
    <asp:Button ID="BtnSetNode" runat="server" Text="设置节点权限" OnClientClick="ShowWindow(1);return false;"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtnSpecial" runat="server" Text="设置专题权限" OnClientClick="ShowWindow(2);return false;"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtnField" runat="server" Text="设置字段权限" OnClientClick="ShowWindow(5);return false;"
        UseSubmitBehavior="False" />
    <br />
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存" OnClick="ButtonSubmit_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="ButtonCancel_Click" />
    </center>
    <br />
    <asp:HiddenField ID="HdnGroupName" runat="server" />
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnGroupId" runat="server" />

    <script language="javascript" id="check" type="text/javascript"> 
    
    function ShowWindow(type){
        var strUrl = "";
        
        switch (type)
        {
            case 1:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Content&RoleId=<%=Request["GroupID"]%>&IdType=1";
                break ;   
            case 2:
                strUrl = "SpecialPermissions.aspx?PermissionsType=User&RoleId=<%=Request["GroupID"]%>&IdType=1";
                break ;    
            case 3:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Comment&RoleId=<%=Request["GroupID"]%>&IdType=1";
                break ;
            case 4:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Node&RoleId=<%=Request["GroupID"]%>&IdType=1";
                break ;
            case 5:
                strUrl = "FieldPermissions.aspx?PermissionsType=User&RoleId=<%=Request["GroupID"]%>&IdType=1";
                break ;
            default:
                break ;
        }
        
        var arr= window.open(strUrl,'newWin','modal=yes,width=700px,height=400px,resizable=yes,scrollbars=yes'); 
        if (arr != null) {

        }
    }
    </script>

</asp:Content>
