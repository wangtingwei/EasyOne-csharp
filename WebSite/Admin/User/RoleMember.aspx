<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.User.RoleMember"
    Title="角色成员管理" Codebehind="RoleMember.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>角色权限设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px;">
                <strong>角色名：</strong></td>
            <td>
                <asp:Label ID="LblRoleName" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="height: 79px">
                <strong>角色描述：</strong></td>
            <td style="height: 79px">
                <asp:Label ID="LblDescription" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle">
                <b>
                    <asp:Label ID="LblRoleName2" runat="server" Text=""></asp:Label>角色成员管理</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" style="height: 100px">
                <br />
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr align="center">
                        <td>
                            <strong>未属于该角色的管理员</strong><br />
                            &nbsp;<asp:ListBox ID="LstNotBelongRole" runat="server" Height="300px" Width="250px"
                                DataTextField="AdminName" DataValueField="AdminId" SelectionMode="Multiple"></asp:ListBox></td>
                        <td style="width: 100px;" align="center">
                            <input type="button" value="添加>>" onclick="JavaScript:addItem(<%=LstNotBelongRole.ClientID%>,<%=LstBelongToRole.ClientID%>);delItem(<%=LstNotBelongRole.ClientID%>)" /><br />
                            <br />
                            <input type="button" value="<<移除" onclick="JavaScript:addItem(<%=LstBelongToRole.ClientID%>,<%=LstNotBelongRole.ClientID%>);delItem(<%=LstBelongToRole.ClientID%>)" />
                        </td>
                        <td>
                            <strong>已属于该角色的管理员</strong><br />
                            &nbsp;<asp:ListBox ID="LstBelongToRole" runat="server" Height="300px" Width="250px"
                                DataTextField="AdminName" DataValueField="AdminId" SelectionMode="Multiple"></asp:ListBox>
                            <asp:HiddenField ID="HdnBelongToRole" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="BtnConfirm" runat="server" Text=" 确定 " OnClick="BtnConfirm_Click" /><br />
                &nbsp;
                <br />
            </td>
        </tr>
    </table>

    <script language="JavaScript">
<!--
/**
 * add one option of a select to another select.
 *
 * @author  Chunsheng Wang <wwccss@263.net>
 */
function addItem(ItemList,Target)
{
    for(var x = 0; x < ItemList.length; x++)
    {
        var opt = ItemList.options[x];
        if (opt.selected)
        {
            flag = true;
            for (var y=0;y<Target.length;y++)
            {
                var myopt = Target.options[y];
                if (myopt.value == opt.value)
                {
                    flag = false;
                }
            }
            if(flag)
            {
                Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
            }
        }
    }
}

/**
 * move one selected option from a select.
 *
 * @author  Chunsheng Wang <wwccss@263.net>
 */
function delItem(ItemList)
{
    for(var x=ItemList.length-1;x>=0;x--)
    {
        var opt = ItemList.options[x];
        if (opt.selected)
        {
            ItemList.options[x] = null;
        }
    }
}

function GetBelongToRole(ItemList)
{
    var adminId = "";
    for(var x = 0; x < ItemList.length; x++)
    {
        if (adminId == "")
        {
            adminId = ItemList.options[x].value;
        }
        else
        {
            adminId += "," + ItemList.options[x].value;
        }
    }
    var belongToRole= document.getElementById("<%=HdnBelongToRole.ClientID%>");
    belongToRole.value = adminId;
}
//-->
    </script>

</asp:Content>
