<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Sources" Title="�����Դ" Codebehind="Source.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong>
                    <asp:Label ID="LblPTitle" runat="server" Text="�����Դ��Ϣ"></asp:Label></strong></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���ƣ�</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrTxtName" runat="server" ControlToValidate="TxtName"
                    ErrorMessage="���Ʋ���Ϊ�գ�"></pe:RequiredFieldValidator>
            </td>
            <td rowspan="6" colspan="2">
                <table width="180" border="1">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <img id="showphoto" src="../../Admin/Images/default.gif" width="150" height="172"
                                alt="��ԴͼƬ" /></td>
                    </tr>
                </table>
                <pe:FileUploadControl ID="ExtenFileUpload" ModuleName="Source" CustomReturnJSFunction="UpdateImgSrc"
                    runat="server">
                </pe:FileUploadControl>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ַ��</strong></td>
            <td>
                <asp:TextBox ID="TxtAddress" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�绰��</strong></td>
            <td>
                <asp:TextBox ID="TxtTel" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���棺</strong></td>
            <td>
                <asp:TextBox ID="TxtFax" runat="server" MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ϵ�ˣ�</strong></td>
            <td>
                <asp:TextBox ID="TxtContacterName" runat="server" MaxLength="20" Width="157px" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳ��</strong></td>
            <td>
                <asp:TextBox ID="TxtHomePage" runat="server" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ʱࣺ</strong></td>
            <td>
                <asp:TextBox ID="TxtZipCode" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>�ʼ���</strong></td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ͨѶ��</strong></td>
            <td>
                <asp:TextBox ID="TxtMail" runat="server" MaxLength="20" /></td>
            <td class="tdbgleft">
                <strong>IM��</strong></td>
            <td>
                <asp:TextBox ID="TxtIm" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���ࣺ</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtType" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropAuthorType" runat="server" DataTextField="name" DataValueField="name"
                    Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���ԣ�</strong></td>
            <td colspan="3">
                <asp:CheckBox ID="ChkElite" runat="server" />�Ƽ�
                <asp:CheckBox ID="ChkOnTop" runat="server" />�ö�</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Դ��飺</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" runat="server" Height="300px" Width="583px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdbg">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('SourceManage.aspx')" /></td>
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
