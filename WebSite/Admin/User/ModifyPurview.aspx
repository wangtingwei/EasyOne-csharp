<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ModifyPurview"
    MasterPageFile="~/Admin/MasterPage.master" EnableEventValidation="false" ViewStateEncryptionMode="never"
    Title="��ԱȨ���޸�" Codebehind="ModifyPurview.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="��ԱȨ���޸�" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�� Ա ����</strong></td>
            <td align="left">
                <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ָ����Ա��Ա�飺</strong></td>
            <td align="left">
                <asp:Label ID="LblGroupName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ԱȨ�ޣ�</strong></td>
            <td align="left">
                <asp:RadioButtonList ID="RadlIsInheritGroupRole" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RadlIsInheritGroupRole_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="True">��Ա��Ĭ��</asp:ListItem>
                    <asp:ListItem Value="False">�������þ���Ȩ��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <pe:UserIndividuation ID="UserIndividuation" runat="server" />
    </table>
    <br />
    <asp:Button ID="BtnSetNode" runat="server" Text="���ýڵ�Ȩ��" OnClientClick="ShowWindow(1);return false;"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtnSpecial" runat="server" Text="����ר��Ȩ��" OnClientClick="ShowWindow(2);return false;"
        UseSubmitBehavior="False" />
    <asp:Button ID="BtnField" runat="server" Text="�����ֶ�Ȩ��" OnClientClick="ShowWindow(5);return false;"
        UseSubmitBehavior="False" />
    <br />
    <br />
    <center>
        <asp:Button ID="BtnSubmit" runat="server" Text="�����޸Ľ��" OnClick="BtnSubmit_Click" /></center>
    <asp:HiddenField ID="HdnUsersId" runat="server" />

    <script language="javascript" id="check" type="text/javascript"> 
    
    function ShowWindow(type){
        var strUrl = "";
        
        switch (type)
        {
            case 1:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Content&RoleId=<%=Request["UserID"]%>&IdType=0";
                break ;   
            case 2:
                strUrl = "SpecialPermissions.aspx?PermissionsType=User&RoleId=<%=Request["UserID"]%>&IdType=0";
                break ;    
            case 3:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Comment&RoleId=<%=Request["UserID"]%>&IdType=0";
                break ;
            case 4:
                strUrl = "NodePermissions.aspx?PermissionsType=User&Type=Node&RoleId=<%=Request["UserID"]%>&IdType=0";
                break ;
            case 5:
                strUrl = "FieldPermissions.aspx?PermissionsType=User&RoleId=<%=Request["UserID"]%>&IdType=0";
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
