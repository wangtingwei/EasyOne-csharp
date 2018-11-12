<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.LabelProperty" Title="Untitled Page" Codebehind="LabelProperty.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmProperty" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>���ñ�ǩ����</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <asp:UpdatePanel ID="UpPropertys" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GdvPropertys" runat="server" DataSourceID="OdsPropertys" AutoGenerateColumns="False"
                            OnRowCommand="GdvPropertys_RowCommand" OnRowUpdating="GdvProperty_RowUpdating" Style="width: 100%;">
                            <Columns>
                                <pe:BoundField DataField="AttributeName" SortExpression="AttributeName" HeaderText="��������">
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:BoundField DataField="DefaultValue" SortExpression="DefaultValue" HeaderText="Ĭ��ֵ">
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:BoundField DataField="Intro" SortExpression="Intro" HeaderText="����˵��" >
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:TemplateField HeaderText="����">
                                    <headerstyle horizontalalign="Center" width="10%" />
                                    <itemtemplate>
                                        <asp:LinkButton ID="LbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="�༭"></asp:LinkButton>
                                        <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            CommandArgument='<%# Bind("AttributeName") %>'>ɾ��</asp:LinkButton>
                                    </itemtemplate>
                                    <edititemtemplate>
                                        <asp:LinkButton ID="LbtnUpdate" runat="server" CausesValidation="False" CommandName="Update">�޸�</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="ȡ��"></asp:LinkButton>
                                    </edititemtemplate>
                                </pe:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="OdsPropertys" runat="server" TypeName="EasyOne.Templates.LabelManage"
                            SelectMethod="GetAttributeList" UpdateMethod="UpdateAttribute" DeleteMethod="DeleteAttribute">
                            <SelectParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                                <asp:Parameter Name="oldAttributeName" Type="String" />
                                <asp:Parameter Name="attributename" Type="String" />
                                <asp:Parameter Name="defaultvalue" Type="String" />
                                <asp:Parameter Name="intro" Type="String" />
                            </UpdateParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                                <asp:Parameter Name="attributename" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <table cellspacing="0" border="1" id="ctl00_CphContent_GridView2" style="width: 100%;border-collapse: collapse;">
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtAttributeName" runat="server" Width="180px" Text="��������"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtDefaultValue" runat="server" Width="180px" Text="Ĭ��ֵ"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtIntro" runat="server" Width="180px" Text="����˵��"></asp:TextBox></td>
                                <td style="width: 10%;">
                                    <asp:Button ID="BtnAddProperty" OnClick="BtnAddProperty_Click" runat="server" Text="���" EnableViewState="False">
                                    </asp:Button></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <asp:Button ID="BtnPrv" OnClick="BtnPrv_Click" runat="server" Style="cursor: pointer;
                    cursor: hand; width: 88px;" Text="��һ��" />&nbsp;&nbsp;<asp:Button ID="BtnNext" runat="server"
                        Text="��һ��" OnClick="BtnNext_Click" Style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;<asp:Button
                            ID="BtnSave" runat="server" Text="������" OnClick="BtnSave_Click" Style="cursor: pointer;
                            cursor: hand; width: 88px;" Visible="False" />&nbsp;&nbsp;<input id="BtnCancel" type="button"
                                class="inputbutton" value="ȡ����" onclick="Redirect('LabelManage.aspx')" style="cursor: pointer;
                                cursor: hand; width: 88px;" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>���˵��</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="��ǩ����">
                        <ContentTemplate>
                            <p>
                                ��ǩ���������Ǳ�ǩ�п����ɶ���ı�����������������Ա�ǩ���Ӳ��������Ӻ�Ĳ������ڱ�ǩ����ʱ��{$PE id="��ǩ��" ������="����ֵ"/}�ķ�ʽ���ã�ϵͳ֧�ֶ��������������˲�����û����ģ��༭�б༭��ǩʱ��������ֵ����������Ĭ��ֵ����������ʾ��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="������;">
                        <ContentTemplate>
                            <p>
                                ��ǩ��������������SQL��ѯ����ģ���У�����SQL���ʱ�Ķ�Ӧ��ϵΪ�����趨���˲�����selectnum������SQL�����ʹ�����·������ã�select top
                                @selectnum from database<br />
                                ��������ڱ�ǩģ�壬���÷�ʽ��Ϊ&lt;xsl:value-of select=&quot;selectnum&quot;/&gt;��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="����ö��">
                        <ContentTemplate>
                            <p>
                                ������Ĭ��ֵ֧��ö�ٷ��������ö��ֵ��ʹ��"|||"���ŷָ�����磺p1|||p2|||p3��һ������Ϊö�٣�����÷���Ϊ{$PE id="��ǩ��" ������="ö��λ��"/}������ö��λ��Ϊ�������ö���б�������˳���������0��ʼ��ö�ٵ����һλ���������磺{$PE
                                id="��ǩ��" ������="1"/}����ò����õ������ݼ�Ϊ"p2"��<br />
                                <br />
                                ע�⣺һ������Ϊö�����ͣ����������������øñ�ǩ������ֵ���κγ�Խ��׼������ֵ�������ض���Ϊ0</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
