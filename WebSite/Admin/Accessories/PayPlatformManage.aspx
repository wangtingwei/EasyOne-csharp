<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatformManage" Codebehind="PayPlatformManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="��������֧��ƽ̨" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <pe:ExtendedGridView ID="EgvPayPlatform" runat="server" AutoGenerateColumns="False"
        DataSourceID="OdsPayPlatform" DataKeyNames="PayPlatformId" OnRowCommand="EgvPayPlatform_RowCommand"
        ItemName="ƽ̨" ItemUnit="��" OnRowDataBound="EgvPayPlatform_RowDataBound" CheckBoxFieldHeaderWidth="3%"
        RowDblclickBoundField="PayPlatformId" RowDblclickUrl="PayPlatform.aspx?Action=Modify&amp;ID={$Field}"
        SerialText="">
        <Columns>
            <pe:BoundField DataField="PayPlatformId" HeaderText="ID" SortExpression="PayPlatformId">
                <HeaderStyle Width="4%" />
            </pe:BoundField>
            <pe:BoundField DataField="PayPlatformName" HeaderText="����" SortExpression="PayPlatformName">
            </pe:BoundField>
            <pe:BoundField DataField="AccountsId" HeaderText="�̻�ID" SortExpression="AccountsId">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField SortExpression="Rate" HeaderText="��������">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# Eval("Rate") + "%"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�Ƿ�Ĭ��" SortExpression="Disabled">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDefault") == true ? "��":""%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="������" SortExpression="Disabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDisabled") == true ? "<font color=red>��</font>" : "<font color=green>��</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="23%" />
                <ItemTemplate>
                    <asp:HyperLink ID="HlnkApply" runat="server" Target="_blank">�����̻�</asp:HyperLink>
                    <asp:LinkButton ID="LbtnSetDefault" runat="server" CommandName='<%# (bool)Eval("IsDefault") == false ? "SetDefault" : ""%>'
                        CommandArgument='<%# Eval("PayPlatformID") %>'>Ĭ��</asp:LinkButton>
                    <asp:LinkButton ID="LbtnDisabled" runat="server" CommandName='<%# (bool)Eval("IsDisabled")? "Enabled" : "Disabled"%>'
                        CommandArgument='<%# Eval("PayPlatformID") %>'><%# (bool)Eval("IsDisabled") == true ? "����" : "����"%></asp:LinkButton>
                    <a href='<%# Eval("PayPlatformID","PayPlatform.aspx?Action=Modify&ID={0}") %>'>�޸�</a>
                    <asp:LinkButton ID="LbtnDel" CommandName="Del" CommandArgument='<%# Eval("PayPlatformId") %>'
                        runat="server">ɾ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������" SortExpression="Disabled">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    &nbsp;<asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:ObjectDataSource ID="OdsPayPlatform" runat="server" SelectCountMethod="Count"
        SelectMethod="GetList" TypeName="EasyOne.Accessories.PayPlatform"></asp:ObjectDataSource>
    <div style="text-align: center;">
        <asp:Button ID="BtnSaveSort" runat="server" OnClick="BtnSaveSort_Click" Text="��������" />
    </div>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <strong>���˵��</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="180px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="ɾ��֧���ӿ�˵��">
                        <ContentTemplate>
                            <p>
                                ϵͳ���õ�֧���ӿڲ�����ɾ���������ʹ�õĻ�����������Ϊ�����á��������á�״̬��֧���ӿڣ�ǰ̨������ʾ��
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="�����֧���ӿ�˵��">
                        <ContentTemplate>
                            <p>
                                ��֧��ƽ̨������������µ�֧��ƽ̨�󣬻���Ҫ�Լ����֧���ӿڳ���</p>
                            <p>
                                �����֧���ӿڲ���˵����
                                <li>���ƽ̨����֧��ƽ̨����������µ�֧��ƽ̨��ע����Ӻ��ƽ̨ID�����ID�ڳ������ж����ĸ�ƽ̨ʱ���õ���</li>
                                <li>��ӷ��ͽӿڳ��򣺷��ͽӿڳ�����PayOnline.aspx����վĿ¼/PayOnline/PayOnline.aspx���ļ��У���������֧�����ṩ�Ľӿڿ����ĵ��ڸ��ļ�����Ӷ�Ӧ�Ľӿڳ���</li>
                                <li>��ӽ��սӿڳ��򣺽��սӿ���Ҫ��PayOnlineĿ¼�����һ�����ļ��������ļ�������Ͻ��ճ���������õķ������Բ���ϵͳ����֧��ƽ̨�Ľ����ļ����磺��Ǯ�Ľ����ļ���PayResult99bill.aspx����</li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
