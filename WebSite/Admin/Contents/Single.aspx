<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.Single" ValidateRequest="false" Title="��ҳ���" Codebehind="Single.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/javascript">
    <!-- 
     function ChangeElementValue(elementId,Value)
        {
            if(Value != "-1")
            {
                document.getElementById(elementId).value = Value;
            }
        }
    //-->
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="��ӵ�ҳ�ڵ�" />
            </td>
        </tr>
        <tr id="TrNodeId" class="tdbg" runat="server" visible="false">
            <td class="tdbgleft">
                <strong>�ڵ�ID��</strong>
            </td>
            <td>
                <span style="color: Red">
                    <asp:Literal runat="server" ID="LitNodeId"></asp:Literal></span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�����ڵ㣺</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropParentNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="���ڵ�" Value="0" />
                </asp:DropDownList>
                <asp:Label ID="LblNodeName" runat="server" Text="" />
                <asp:Label ID="LblNodePermissions" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳ���ƣ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server" />
                <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="��ҳ���Ʋ���Ϊ�գ�"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳ��ʶ����</strong><br />
                ����ǰ̨����ʱ����ֱ���ñ�ʶ��ȡ��ID
            </td>
            <td>
                <asp:TextBox ID="TxtNodeIdentifier" runat="server" />
                <pe:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="��ʶ������Ϊ�գ�"
                    ControlToValidate="TxtNodeIdentifier" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����HTML��</strong><br />
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsCreate" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="��" Selected="true" Value="True" />
                    <asp:ListItem Text="��" Value="False" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�򿪷�ʽ��</strong>
            </td>
            <td>
                <asp:RadioButton ID="RadOpenType0" Checked="true" GroupName="OpenType" runat="server" />��ԭ���ڴ�
                <asp:RadioButton ID="RadOpenType1" GroupName="OpenType" runat="server" />���´��ڴ�
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ��ڶ����˵�����ʾ��</strong><br />
                ��ѡ��ֻ��һ����Ŀ��Ч��
            </td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnMenu" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="��" Selected="True" Value="True" />
                    <asp:ListItem Text="��" Value="False" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <%--    <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�λ�õ�������ʾ��</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="��" Selected="True" Value="True" />
                    <asp:ListItem Text="��" Value="False" />
                </asp:RadioButtonList></td>
        </tr>--%>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong><span>��Ŀ�б��ļ�������׺��</span></strong><br />
            </td>
            <td align="left">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="LblPageHtmlDir" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPageHtmlDir" runat="server" />
                        </td>
                        <td>
                            .
                        </td>
                        <td>
                            <pe:ComboBox ID="PagePostfix" runat="server">
                                <Items>
                                    <asp:ListItem>html</asp:ListItem>
                                    <asp:ListItem>htm</asp:ListItem>
                                    <asp:ListItem>shtml</asp:ListItem>
                                    <asp:ListItem>shtm</asp:ListItem>
                                </Items>
                            </pe:ComboBox>
                        </td>
                        <td>
                            <pe:RequiredFieldValidator ID="ValrPageHtmlDir" ControlToValidate="TxtPageHtmlDir"
                                runat="server" ErrorMessage="��ҳ���Ʋ���Ϊ��" Display="Dynamic" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ָ����ҳģ�壺</strong>
            </td>
            <td align="left">
                <pe:TemplateSelectControl ID="FileCdefaultListTmeplate" Width="300px" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳͼƬ��ַ��</strong><br />
                �����ڵ�ҳҳ��ʾָ����ͼƬ
            </td>
            <td>
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Width="360px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳ��ʾ��</strong><br />
                ���������ҳ������ʱ����ʾ�趨����ʾ���֣���֧��HTML��
            </td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="65px" Width="360px"
                    Rows="2" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳ˵����</strong><br />
                �����ڵ�ҳҳ��ϸ���ܵ�ҳ��Ϣ��֧��HTML
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="65px" Width="360px"
                    Rows="2" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳMETA�ؼ��ʣ�</strong><br />
                ��������������õĹؼ���<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaKeywords" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ҳMETA��ҳ������</strong><br />
                ��������������õ���ҳ����<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaDescription" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="ȡ��"
                    onclick="Redirect('CategoryManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
