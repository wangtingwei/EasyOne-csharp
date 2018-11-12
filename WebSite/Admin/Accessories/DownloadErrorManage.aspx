<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownloadErrorManage" Title="���ر������" Codebehind="DownloadErrorManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="�������ر���" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvDownloadError" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ErrorId" DataSourceID="OdsDownloadError"
        ItemName="��ַ" ItemUnit="��" OnRowCommand="EgvDownloadError_RowCommand">
        <Columns>
            <pe:BoundField DataField="InfoID" HeaderText="���ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�����" SortExpression="InfoID">
                <HeaderStyle Width="17%" />
                <ItemTemplate>
                    <%#GetSoftName(Convert.ToInt32(Eval("InfoID")))%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="ErrorUrl" HeaderText="���ص�ַ">
            </pe:BoundField>
            <pe:BoundField DataField="ErrorTimes" HeaderText="�����˴�">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a id="EahDownloadErrorTest" href='<%#Eval("ErrorUrl")%>'
                        target="_blank" runat="server">����</a>
                    <asp:LinkButton ID="ELbtnDel" Text="ɾ��" runat="server" CommandArgument='<%# Bind("ErrorID") %>'
                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('ȷ��Ҫɾ����')" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />
    ѡ�б�ҳ�������ص�ַ������Ϣ &nbsp;&nbsp;&nbsp;&nbsp;
    <pe:ExtendedButton ID="EBtnDelete" Text="ɾ��ѡ�е����ص�ַ������Ϣ" OnClick="EBtnDelete_Click" OnClientClick="return batchconfirm('ȷʵҪɾ��ѡ�е����ص�ַ������Ϣ��');"
        CausesValidation="False" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="EBtnClear" Text="ɾ���������ص�ַ������Ϣ" OnClick="EBtnClear_Click" OnClientClick="return confirm('ȷʵҪɾ�����е����ص�ַ������Ϣ��');"
        UseSubmitBehavior="True" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ObjectDataSource ID="OdsDownloadError" runat="server" SelectCountMethod="GetTotalOfDownloadError"
        DeleteMethod="Delete" SelectMethod="GetDownloadErrorList" TypeName="EasyOne.Accessories.DownloadError"
        EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxiNumRows">
        <DeleteParameters>
            <asp:Parameter Name="errorId" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="searchType" QueryStringField="Field"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
