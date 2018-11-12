<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.IPManage" Title="ͳ��IP�����" Codebehind="IPManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.Analytics" %>
<%@ Import Namespace="EasyOne.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%; height: 19px">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td style="width: 40%; height: 19px; text-align: right">
                ��
                <asp:Label ID="LblCount" runat="server" ForeColor="Red"></asp:Label>
                ��IP�μ�¼&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="ExtendedGridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsStatIPInfo" OnRowCommand="ExtendedGridView1_RowCommand" OnDataBound="ExtendedGridView1_DataBound">
        <Columns>
            <pe:TemplateField HeaderText="��ʼ IP">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="LblStartIP" runat="server" Text='<%# StringHelper.DecodeIP(Convert.ToInt64(Eval("StartIp"))) %>'></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��β IP">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="LblEndIP" Text='<%# StringHelper.DecodeIP(Convert.ToInt64(Eval("EndIp"))) %>'
                        runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Address" HeaderText="��Դ��ϸ��ַ">
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:LinkButton  ID="LbtnEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                        CommandName="EditIP">�༭</asp:LinkButton>&nbsp; |
                    <asp:LinkButton    ID="LbtnDel" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                        CommandName="DelIP" OnClientClick="return confirm('�Ƿ�Ҫɾ����IP��¼��')">ɾ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    &nbsp;
    <table border="0" cellpadding="2" cellspacing="1" class="border" style="width: 100%;
        text-align: center">
        <tr class="tdbg">
            <td style="width: 120px; height: 52px">
                <strong>ͳ��IP��������</strong></td>
            <td style="height: 52px">
                IP ��ַ��</td>
            <td style="height: 52px">
                <asp:TextBox ID="TxtSearchIP" runat="server" Width="120px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ValeStartIP" runat="server" ControlToValidate="TxtSearchIP"
                    Display="Dynamic" ErrorMessage="������Ч��IP��ַ" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator>
            </td>
            <td style="height: 52px">
                ��Դ��ϸ��ַ��</td>
            <td style="height: 52px">
                <asp:TextBox ID="TxtSearchAddress" runat="server" Width="120px"></asp:TextBox>&nbsp;</td>
            <td style="height: 52px">
                &nbsp;<asp:Button ID="BtnSearch" runat="server" Text="����" />
            </td>
            <td style="width: 110px; height: 52px">
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="OdsStatIPInfo" runat="server" EnablePaging="True" MaximumRowsParameterName="maxiNumRows"
        SelectCountMethod="GetTotal" SelectMethod="GetList" StartRowIndexParameterName="startRowIndexId"
        TypeName="EasyOne.Analytics.IPStorage">
        <SelectParameters>
            <asp:ControlParameter ControlID="TxtSearchIP" Name="searchIP" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="TxtSearchAddress" Name="searchAddress" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
