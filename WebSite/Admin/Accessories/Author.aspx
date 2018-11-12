<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Authors" Title="�������" Codebehind="Author.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <pe:AlternateLiteral ID="AltrTitle" Text="���������Ϣ" AlternateText="�޸�������Ϣ" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>������</strong></td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtName" Display="Dynamic"
                    ErrorMessage="��������������" />
            </td>
            <td rowspan="8" colspan="2">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td style="width: 100%;" align="left">
                            <img id="showphoto" src="../../Admin/Images/default.gif" width="150" height="172"
                                alt="����ͼƬ" /></td>
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
                <strong>��Ա����</strong></td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server" Width="169px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ա�</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">��</asp:ListItem>
                    <asp:ListItem Value="0">Ů</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���գ�</strong></td>
            <td>
                <pe:DatePicker ID="TxtBirthDay" runat="server"></pe:DatePicker>
                <pe:DateValidator ID="Vdate" ControlToValidate="TxtBirthDay" runat="server"></pe:DateValidator>
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
                <strong>��λ��</strong></td>
            <td>
                <asp:TextBox ID="TxtCompany" runat="server" MaxLength="20" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���ţ�</strong></td>
            <td>
                <asp:TextBox ID="TxtDepartment" runat="server" MaxLength="20" /></td>
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
                <asp:RadioButtonList ID="RadlAuthorType" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                </asp:RadioButtonList>
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
                <strong>���߼�飺</strong></td>
            <td colspan="3">
                <asp:TextBox ID="TxtIntro" TextMode="MultiLine" runat="server" height="300px" Width="583px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ����ã�</strong>
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ChkPass" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdbg">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('AuthorManage.aspx')" />
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
