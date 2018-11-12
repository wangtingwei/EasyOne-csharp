<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Authors" Title="添加作者" Codebehind="Author.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <pe:AlternateLiteral ID="AltrTitle" Text="添加作者信息" AlternateText="修改作者信息" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>姓名：</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtName" Display="Dynamic"
                    ErrorMessage="请输入作者名称" />
            </td>
            <td rowspan="8" colspan="2">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td style="width: 100%;" align="left">
                            <img id="showphoto" src="../../Admin/Images/default.gif" width="150" height="172"
                                alt="作者图片" /></td>
                    </tr>
                    <tr>
                        <td align="left">
                            <pe:FileUploadControl ID="ExtenFileUpload" ModuleName="Author" CustomReturnJSFunction="UpdateImgSrc"
                                runat="server">
                            </pe:FileUploadControl>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员名：</strong></td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server" Width="169px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>性别：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                    <asp:ListItem Value="0">女</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>生日：</strong></td>
            <td>
                <pe:DatePicker ID="TxtBirthDay" runat="server"></pe:DatePicker>
                <pe:DateValidator ID="Vdate" ControlToValidate="TxtBirthDay" runat="server"></pe:DateValidator>
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
                <strong>单位：</strong></td>
            <td>
                <asp:TextBox ID="TxtCompany" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>部门：</strong></td>
            <td>
                <asp:TextBox ID="TxtDepartment" runat="server" MaxLength="20" /></td>
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
                <asp:RadioButtonList ID="RadlAuthorType" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                </asp:RadioButtonList>
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
                <strong>作者简介：</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" TextMode="MultiLine" runat="server" height="300px" Width="583px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用：</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdbg">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('AuthorManage.aspx')" />
            </td>
        </tr>
    </table>
    <br />

    <script type="text/javascript">
    if(document.getElementById("<%=ExtenFileUpload.ClientID %>").value!="")
    {
    document.getElementById("showphoto").src="../../"+document.getElementById("<%=ExtenFileUpload.ClientID %>").value;
    }
    function UpdateImgSrc(path,size,id)
    {
        document.getElementById("showphoto").src = "<%= m_ImgPath %>/" + path;
        
    }
    </script>

</asp:Content>
