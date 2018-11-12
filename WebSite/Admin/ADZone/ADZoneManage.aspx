<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneManage" Title="广告版位管理" Codebehind="ADZoneManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.AD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvADZone" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsAdZone" ItemName="版位" ItemUnit="个" AutoGenerateCheckBoxColumn="True"
        DataKeyNames="ZoneId" OnRowDataBound="GdvADZone_RowDataBound" CheckBoxFieldHeaderWidth="3%"
        RowDblclickBoundField="ZoneId" 
        RowDblclickUrl="ADZone.aspx?Action=Modify&amp;ZoneId={$Field}"
        SerialText="">
        <Columns>
            <pe:BoundField DataField="ZoneId" HeaderText="序号">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="版位名称">
                <ItemTemplate>
                    <asp:HyperLink ID="LnkZoneName" NavigateUrl='<%# Eval("ZoneId", "ADManage.aspx?ZoneId={0}&listType=3") %>' Text='<%#DataBinder.Eval(Container.DataItem,"ZoneName").ToString()%>'
                        runat="server"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="类型">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LabZoneType" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="显示类型">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <asp:Label ID="LabShowType" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="尺寸">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem,"ZoneWidth").ToString()%>
                    x
                    <%#DataBinder.Eval(Container.DataItem,"ZoneHeight").ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="活动">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("Active") == false ? "<span style=\"color: #ff0033\">×</span>" : "√"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <a id="EahAdd" href='<%# "Advertisement.aspx?Action=Add&ZoneId=" + Eval("ZoneId")%>'>
                        添加</a> | <a id="EahModify" href='<%# "ADZone.aspx?Action=Modify&ZoneId=" + Eval("ZoneId")%>'>
                            修改</a> | <a id="EahCopy" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Copy&ZoneId=" + Eval("ZoneId"))%>'>
                                复制</a><br /><a id="EahDelete" onclick="return confirm('确定要删除此版位吗？');" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Delete&ZoneId=" + Eval("ZoneId"))%>'>删除</a> | <a id="EahClear" onclick="return confirm('确定要清空此版位吗？清空后原来的属于此版位的广告将不再属于版位！');" href='<%# AppendSecurityCode("ADZoneManage.aspx?Action=Clear&ZoneId=" + Eval("ZoneId"))%>'>
                            清空</a> | <a href="ADZoneManage.aspx?Action=<%# (bool)Eval("Active") == false ? "Active" : "Pause"%>&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                                <%# (bool)Eval("Active") == false ? "活动" : "暂停"%>
                            </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="版位JS">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <a href="ADZoneManage.aspx?Action=Refurbish&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                        刷新</a>&nbsp;&nbsp;<a href="PreviewAD.aspx?Type=Zone&ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">预览</a><br />
                    <a href="ShowJSCode.aspx?ZoneId=<%#DataBinder.Eval(Container.DataItem,"ZoneId").ToString()%>">
                        JS调用代码</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="EBtnDelete" runat="server" Text="批量删除选定版位" OnClientClick="return confirm('确实要删除选中的版位？')"
                    OnClick="EBtnDelete_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnActive" runat="server" Text="设为活动版位" OnClick="EBtnActive_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnPause" runat="server" Text="暂停版位显示" OnClick="EBtnPause_Click" />&nbsp;&nbsp;
                <asp:Button ID="EBtnRefurbish" runat="server" Text="刷新版位JS" OnClick="EBtnRefurbish_Click" />&nbsp;&nbsp;
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
