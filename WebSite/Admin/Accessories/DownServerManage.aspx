<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownServerManage" Title="���ط���������" Codebehind="DownServerManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="�������ط�����" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvDownServer" runat="server" AutoGenerateColumns="false"
        AutoGenerateCheckBoxColumn="True" DataSourceID="OdsDownServer" DataKeyNames="ServerID"
        RowDblclickBoundField="ServerID" RowDblclickUrl="DownServer.aspx?Action=Modify&ServerID={$Field}">
        <Columns>
            <pe:BoundField DataField="ServerID" HeaderText="ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="ServerName" HeaderText="��������" SortExpression="ServerName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="������LOGO" SortExpression="ServerLogo">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# Eval("ServerLogo").ToString() == "" ? "" : "<img src="+ Eval("ServerLogo") + " style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��ʾ��ʽ" SortExpression="ShowType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# Eval("ShowType").ToString() == "0" ? "��ʾ����" : "��ʾLOGO"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="ServerUrl" HeaderText="��������ַ" SortExpression="ServerUrl">
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="17%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahDownServerModify" IsChecked="true" OperateCode="DownServerManage"
                        href='<%# "DownServer.aspx?Action=Modify&ServerID=" + Eval("ServerID")%>' runat="server">�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahDownServerDelete" IsChecked="true" OperateCode="DownServerManage"
                        href='<%# AppendSecurityCode("DownServerManage.aspx?Action=Delete&ServerID=" + Eval("ServerID"))%>'
                        onclick="return confirm('ȷ��Ҫɾ���˷�������Ϣ��');" runat="server">ɾ��</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />ѡ�б�ҳ�������ط�����
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropShowType" runat="server">
        <asp:ListItem Selected="True" Value="0">��ʾ����</asp:ListItem>
        <asp:ListItem Value="1">��ʾLOGO</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="EBtnShowType" Text="����ѡ�еķ���������ʾ��ʽ"  OnClick="EBtnShowType_Click" runat="server" />&nbsp;&nbsp;
    <div style="text-align: left">
        <br />
        <br />
        <span style="color: #ff0000"><strong>ע�⣺</strong>ɾ��ĳ�������������Ϣʱ����֮��ص����ش�����ϢҲ��һ��ɾ������<br />
        </span>
        <br />
        <br />
        <span style="color: blue"><strong>��ܰ��ʾ��</strong>Ҫ��ı�ǰ̨���ص�ַ��ʾ��ʽ������������������������������ط���������ʾ��ʽ��Ȼ���ҵ������޸���ʾ��ʽ���������ѡ�����ط��������ɡ�ǰ̨�����ַ��ʾ�ĸ�������ʽ��ȫ������ÿ����������ã�����㲻��Ϊÿ�������������ʾ��ʽ��ʹ��Ĭ��ֵ��<br />
        </span>
    </div>
    <asp:ObjectDataSource ID="OdsDownServer" runat="server" DataObjectTypeName="EasyOne.Model.Accessories.DownServerInfo"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetDownServerList"
        TypeName="EasyOne.Accessories.DownServer" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="SubModuleID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
