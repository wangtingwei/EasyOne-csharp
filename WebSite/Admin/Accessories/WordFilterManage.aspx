<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.WordFilterManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="�ַ����˹���"
    ValidateRequest="false" Codebehind="WordFilterManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvWordFilter" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ItemID" DataSourceID="OdsWordFilter"
        ItemName="��Ŀ" ItemUnit="��" OnRowDataBound="EgvWordFilter_RowDataBound"
        RowDblclickBoundField="ItemID" RowDblclickUrl="WordFilter.aspx?Action=Modify&ItemID={$Field}">
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="SourceWord" HeaderText="�滻Ŀ��" SortExpression="SourceWord">
                <HeaderStyle Width="35%" />
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="TargetWord" HeaderText="�滻����" SortExpression="TargetWord">
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="Priority" HeaderText="���ȼ�" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="״̬" SortExpression="IsEnabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsEnabled") == true ? "<b>��</b>" : "<span style=\"color:red;\"><b>��</b></span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <a id="EahWordFilterEnabled" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=runWordFilter&ItemID=" + Eval("ItemID"))%>'
                        runat="server">����</a> <a id="EahWordFilterDisable" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=disWordFilter&ItemID=" + Eval("ItemID"))%>'
                            runat="server">����</a> <a id="EahWordFilterModify" href='<%# "WordFilter.aspx?Action=Modify&ItemID=" + Eval("ItemID")%>'
                                runat="server">�޸�</a> <a id="EahWordFilterDelete" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=Delete&ItemID=" + Eval("ItemID"))%>'
                                    onclick="return confirm('�Ƿ�ɾ�����ַ����ˣ�');" runat="server">ɾ��</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsWordFilter" runat="server" SelectMethod="GetWordFilterList"
        SelectCountMethod="GetCountNumber" TypeName="EasyOne.Accessories.WordReplace"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="keyword" QueryStringField="Keyword" Type="string" />
            <asp:QueryStringParameter Name="listType" QueryStringField="ListType" Type="int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />ѡ�б�ҳ��ʾ�������ַ�����
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="ɾ��ѡ�е��ַ�����" OnClientClick="return batchconfirm('�Ƿ�Ҫɾ���ַ����ˣ�');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />
    <asp:Button ID="EBtnBatchEnable" Text="����ѡ�е��ַ�����" OnClientClick="return batchconfirm('�Ƿ�Ҫ�����ַ����ˣ�');"
        OnClick="EBtnBatchEnable_Click" CausesValidation="False" runat="server" />
    <asp:Button ID="EBtnBatchDisable" Text="����ѡ�е��ַ�����" OnClientClick="return batchconfirm('�Ƿ�Ҫ�����ַ����ˣ�');"
        OnClick="EBtnBatchDisable_Click" CausesValidation="False" runat="server" />
</asp:Content>
