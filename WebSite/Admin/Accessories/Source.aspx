<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Sources" Title="添加来源" Codebehind="Source.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong>
                    <asp:Label ID="LblPTitle" runat="server" Text="添加来源信息"></asp:Label></strong></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrTxtName" runat="server" ControlToValidate="TxtName"
                    ErrorMessage="名称不能为空！"></pe:RequiredFieldValidator>
            </td>
            <td rowspan="6" colspan="2">
                <table width="180" border="1">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <img id="showphoto" src="../../Admin/Images/default.gif" width="150" height="172"
                                alt="来源图片" /></td>
                    </tr>
                </table>
                <pe:FileUploadControl ID="ExtenFileUpload" ModuleName="Source" CustomReturnJSFunction="UpdateImgSrc"
                    runat="server">
                </pe:FileUploadControl>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>地址：</strong></td>
            <td>
                <asp:TextBox ID="TxtAddress" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>电话：</strong></td>
            <td>
                <asp:TextBox ID="TxtTel" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>传真：</strong></td>
            <td>
                <asp:TextBox ID="TxtFax" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>联系人：</strong></td>
            <td>
                <asp:TextBox ID="TxtContacterName" runat="server" MaxLength="20" Width="157px" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>主页：</strong></td>
            <td>
                <asp:TextBox ID="TxtHomePage" runat="server" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>邮编：</strong></td>
            <td>
                <asp:TextBox ID="TxtZipCode" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>邮件：</strong></td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>通讯：</strong></td>
            <td>
                <asp:TextBox ID="TxtMail" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>IM：</strong></td>
            <td>
                <asp:TextBox ID="TxtIm" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>分类：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtType" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropAuthorType" runat="server" DataTextField="name" DataValueField="name"
                    Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>属性：</strong></td>
            <td colspan="3">
                <asp:CheckBox ID="ChkElite" runat="server" />推荐
                <asp:CheckBox ID="ChkOnTop" runat="server" />置顶</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>来源简介：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" runat="server" Height="300px" Width="583px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdbg">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('SourceManage.aspx')" /></td>
        </tr>
    </table>
    <br />

    <script type="text/javascript">
    OnChangeCategory(document.getElementById("<%=DropAuthorType.ClientID%>").options[document.getElementById("<%=DropAuthorType.ClientID%>").selectedIndex].text)
    function OnChangeCategory(value)
    {
        document.getElementById("<%=TxtType.ClientID %>").value=value;
    }
    function UpdateImgSrc(path,size,id)
    {
        document.getElementById("showphoto").src = "<%= m_ImgPath %>/" + path;
    }
    </script>

</asp:Content>
