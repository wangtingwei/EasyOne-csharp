<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADManage" Title="��վ������" Codebehind="ADManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.AD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvAD" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsAd" ItemName="���" ItemUnit="��" AutoGenerateCheckBoxColumn="True"
        DataKeyNames="AdID" OnRowDataBound="GdvAD_RowDataBound"
        RowDblclickBoundField="ADId" 
        RowDblclickUrl="Advertisement.aspx?Action=Modify&amp;ADId={$Field}">
        <Columns>
            <pe:BoundField DataField="ADId" HeaderText="���">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="Ԥ��">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:HyperLink ID="HypPreview" runat="server" Text="Ԥ��">
                    </asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
            <HeaderStyle />
                <ItemTemplate>
                    <a href="Advertisement.aspx?Action=Modify&ADId=<%#Eval("ADId")%>">
                        <%# DataBinder.Eval(Container.DataItem,"ADName")%>
                    </a>
                </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%#Advertisement.GetADType()[Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ADType"))-1].ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Priority" HeaderText="Ȩ��">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�����">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LabClicks"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LabViews" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="ʣ������">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LabDays" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <asp:Label ID="LabRate" runat="server"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("Passed") == false ? "<span style=\"color: #ff0033\">��</span>" : "��"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="19%" />
                <ItemTemplate>
                    <a href='<%# "Advertisement.aspx?Action=Modify&ADId=" + Eval("ADId")%>'>�޸�</a> <a
                        href='<%# AppendSecurityCode("ADManage.aspx?Action=Copy&ADId=" + Eval("ADId"))%>'>
                        ����</a> <a href='<%# AppendSecurityCode("ADManage.aspx?Action=Delete&ADId=" + Eval("ADId"))%>'
                            onclick="return confirm('ȷ��Ҫɾ���˹����');">ɾ��</a> <a href="ADManage.aspx?Action=<%# (bool)Eval("Passed") == false ? "Passed" : "CancelPassed"%>&ADId=<%#Eval("ADId")%>">
                                <%# (bool)Eval("Passed") == false ? "ͨ�����" : "ȡ�����"%>
                            </a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="EBtnDelete" runat="server" Text="ɾ��ѡ�����" OnClientClick="return confirm('ȷ��Ҫɾ��ѡ�еĹ����')"
                    OnClick="EBtnDelete_Click" />
                <asp:Button ID="EBtnPassed" runat="server" Text="���ͨ��ѡ�����" OnClick="EBtnPassed_Click" />
                <asp:Button ID="EBtnCancelPased" runat="server" Text="ȡ�����ѡ�����" OnClick="EBtnCancelPased_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:ObjectDataSource ID="OdsAd" runat="server" SelectMethod="GetAdvertisementList"
        TypeName="EasyOne.AD.Advertisement" EnablePaging="True" SelectCountMethod="GetTotalOfAdvertisements">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="zoneId" QueryStringField="ZoneId"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div id="dHTMLADPreview" style='z-index: 1000; left: 0px; visibility: hidden; width: 10px;
        position: absolute; top: 0px; height: 10px'>
    </div>

    <script src="<%=BasePath %>Admin/JS/Popup.js" language="javascript" type="text/javascript"></script>

</asp:Content>
