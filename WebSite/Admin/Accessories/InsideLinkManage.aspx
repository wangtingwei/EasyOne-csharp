<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.InsideLinkManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="վ�����ӹ���"
    ValidateRequest="false" Codebehind="InsideLinkManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="����վ������" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvInsideLink" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ItemID" DataSourceID="OdsInsideLink"
        ItemName="��Ŀ" ItemUnit="��" OnRowDataBound="EgvInsideLink_RowDataBound"
        RowDblclickBoundField="ItemID" RowDblclickUrl="InsideLink.aspx?Action=Modify&ItemID={$Field}">
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="SourceWord" HeaderText="����Ŀ��" SortExpression="SourceWord">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="���ӵ�ַ" SortExpression="TargetWord">
                <ItemTemplate>
                    <a href='<%# (string)Eval("TargetWord") %>' target="_blank">
                        <%# (string)Eval("TargetWord") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Priority" HeaderText="���ȼ�" SortExpression="Priority">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="ReplaceTimes" HeaderText="�滻����" SortExpression="ReplaceTimes">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�򿪷�ʽ" SortExpression="OpenType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (bool)Eval("OpenType") ? "ԭ����" : "�´���"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="״̬" SortExpression="IsEnabled">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsEnabled") == true ? "<b>��</b>" : "<span style=\"color:red;\"><b>��</b></span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahInsideLinkEnabled" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=runInsideLink&ItemID=" + Eval("ItemID"))%>'
                        runat="server">����</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkDisable" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=disInsideLink&ItemID=" + Eval("ItemID"))%>'
                        runat="server">����</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkModify" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# "InsideLink.aspx?Action=Modify&ItemID=" + Eval("ItemID")%>' runat="server">�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkDelete" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=Delete&ItemID=" + Eval("ItemID"))%>'
                        onclick="return confirm('�Ƿ�ɾ����վ�����ӣ�');" runat="server">ɾ��</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsInsideLink" runat="server" SelectMethod="GetInsideList"
        TypeName="EasyOne.Accessories.WordReplace" DeleteMethod="Delete" EnablePaging="True"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HdnlistType" runat="server" Value="0" />
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">ѡ�б�ҳ��ʾ������վ������</label>
    &nbsp;&nbsp;
    <pe:ExtendedButton ID="EBtnBatchDelete" Text="ɾ��ѡ�е�վ������" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('�Ƿ�Ҫɾ��վ�����ӣ�');" OnClick="EBtnBatchDelete_Click"
        CausesValidation="False" runat="server" />
    <pe:ExtendedButton ID="EBtnBatchEnable" Text="����ѡ�е�վ������" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('�Ƿ�Ҫ����վ�����ӣ�');" OnClick="EBtnBatchEnable_Click"
        CausesValidation="False" runat="server" />
    <pe:ExtendedButton ID="EBtnBatchDisable" Text="����ѡ�е�վ������" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('�Ƿ�Ҫ����վ�����ӣ�');" OnClick="EBtnBatchDisable_Click"
        CausesValidation="False" runat="server" />
    <br />
    <br />
    <br />
</asp:Content>
