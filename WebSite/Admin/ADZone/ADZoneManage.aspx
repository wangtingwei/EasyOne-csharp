<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneManage" Title="����λ����" Codebehind="ADZoneManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.AD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvADZone" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsAdZone" ItemName="��λ" ItemUnit="��" AutoGenerateCheckBoxColumn="True"
        DataKeyNames="ZoneId" OnRowDataBound="GdvADZone_RowDataBound" CheckBoxFieldHeaderWidth="3%"
        RowDblclickBoundField="ZoneId" 
        RowDblclickUrl="ADZone.aspx?Action=Modify&amp;ZoneId={$Field}"
        SerialText="">
        <Columns>
            <pe:BoundField DataField="ZoneId" HeaderText="���">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="��λ����">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkZoneName" NavigateUrl='<%# Eval("ZoneId", "ADManage.aspx?ZoneId={0}&listType=3") %>' Text='<%#DataBinder.Eval(Container.DataItem,"ZoneName").ToString()%>'
                        runat="server"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LabZoneType" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��ʾ����">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <asp:Label ID="LabShowType" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�ߴ�">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem,"ZoneWidth").ToString()%>
                    x
                    <%#DataBinder.Eval(Container.DataItem,"ZoneHeight").ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("Active") == false ? "<span style=\"color: #ff0033\">��</span>" : "��"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <a id="EahAdd" href='<%# "Advertisement.aspx?Action=Add&ZoneId=" + Eval("ZoneId")%>'>
                        ���</a> | <a id="EahModify" href='<%# "ADZone.aspx?Action=Modify&ZoneId=" + Eval("ZoneId")%>'>
                            �޸�</a> | <a id="EahCopy" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Copy&ZoneId=" + Eval("ZoneId"))%>'>
                                ����</a><br /><a id="EahDelete" onclick="return confirm('ȷ��Ҫɾ���˰�λ��');" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Delete&ZoneId=" + Eval("ZoneId"))%>'>ɾ��</a> | <a id="EahClear" onclick="return confirm('ȷ��Ҫ��մ˰�λ����պ�ԭ�������ڴ˰�λ�Ĺ�潫�������ڰ�λ��');" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Clear&ZoneId=" + Eval("ZoneId"))%>'>
                            ���</a> | <a href="ADZoneManage.aspx?Action=<%# (bool)Eval("Active") == false ? "Active" : "Pause"%>&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                                <%# (bool)Eval("Active") == false ? "�" : "��ͣ"%>
                            </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��λJS">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <a href="ADZoneManage.aspx?Action=Refurbish&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                        ˢ��</a>&nbsp;&nbsp;<a href="PreviewAD.aspx?Type=Zone&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">Ԥ��</a><br />
                    <a href="ShowJSCode.aspx?ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                        JS���ô���</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="EBtnDelete" runat="server" Text="����ɾ��ѡ����λ" OnClientClick="return confirm('ȷʵҪɾ��ѡ�еİ�λ��')"
                    OnClick="EBtnDelete_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnActive" runat="server" Text="��Ϊ���λ" OnClick="EBtnActive_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnPause" runat="server" Text="��ͣ��λ��ʾ" OnClick="EBtnPause_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnRefurbish" runat="server" Text="ˢ�°�λJS" OnClick="EBtnRefurbish_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <asp:ObjectDataSource ID="OdsAdZone" runat="server" SelectMethod="GetADZoneList"
        SelectCountMethod="GetTotalOfADZone" EnablePaging="True" TypeName="EasyOne.AD.ADZone">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
