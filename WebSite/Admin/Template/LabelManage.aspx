<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.LabelManageUI" Title="��ǩ����" Codebehind="LabelManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
    <br />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td style="width: 88px">
                <strong>�����б�</strong></td>
            <td align="left">
                <a href="LabelManage.aspx">��ʾȫ��</a>
                <asp:Repeater ID="RptLabel" runat="server" DataSourceID="OdsLabelType">
                    <ItemTemplate>
                        | <a href="LabelManage.aspx?type=-1&labelCategory=<%# Server.UrlEncode(Eval("Name").ToString()) %>">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:ObjectDataSource ID="OdsLabelType" runat="server" SelectMethod="GetLabelTypeList"
                    TypeName="EasyOne.Templates.LabelManage"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvLabelList" AutoGenerateColumns="False" runat="server"
        DataSourceID="OdsLabel" DataKeyNames="name" AllowPaging="True" ItemName="��ǩ"
        ItemUnit="��" AutoGenerateCheckBoxColumn="True" OnRowCommand="GdvLabelList_RowCommand"
        PageSize="20" CheckBoxFieldHeaderWidth="3%" SerialText="">
        <Columns>
            <pe:TemplateField HeaderText="����">
                <ItemTemplate>
                    <a href="Label.aspx?name=<%# Server.UrlEncode(Eval("name").ToString()) %>" title='<%# Eval("Intro") %>'>
                        <%# Eval("Name") %>
                    </a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:BoundField DataField="Type" HeaderText="����" SortExpression="Type" >
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="UpDateTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                SortExpression="UpDateTime" HtmlEncode="False">
                <HeaderStyle Width="18%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href="Label.aspx?name=<%# Server.UrlEncode(Eval("name").ToString()) %>">�޸�</a>
                    <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Bind("name") %>'
                        CommandName="Deleted" OnClientClick="return confirm('�Ƿ�ɾ������ǩ��')">ɾ��</asp:LinkButton>
                    <asp:LinkButton ID="LbtnCopy" runat="server" CausesValidation="False" CommandArgument='<%# Bind("name") %>'
                        CommandName="Copy">����</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:Button ID="BtnDel" runat="server" Text="����ɾ��ѡ����ǩ" CausesValidation="False" OnClientClick="return confirm('ȷʵҪɾ��ѡ�еı�ǩ��');"
        OnClick="BtnDel_Click" />&nbsp;&nbsp;<asp:Button ID="BtnAdd" runat="server" Text="����һ���±�ǩ"
            OnClick="BtnAdd_Click" />&nbsp;&nbsp;
    <input id="InputUploadLabel" type="button" class="inputbutton" value="�ϴ���ǩ" onclick="javascript:window.open('LabelUpload.aspx','�ϴ���ǩ','width=600,height=250,resizable=0,scrollbars=yes');" />
    <asp:Button ID="BtnDw" runat="server" Text="����Dreamweaver����Ƭ��" OnClick="BtnDW_Click" />
    <asp:ObjectDataSource ID="OdsLabel" runat="server" TypeName="EasyOne.Templates.LabelManage"
        SelectMethod="GetLabelList">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="type" QueryStringField="type" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="field" Type="Int32" QueryStringField="field" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" Type="string" QueryStringField="keyword" />
            <asp:QueryStringParameter DefaultValue="" Name="labelCategory" Type="string" QueryStringField="LabelCategory" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
