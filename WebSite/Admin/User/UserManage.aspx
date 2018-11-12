<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserManage" Title="�û�����" Codebehind="UserManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvUser" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="UserId" DataSourceID="OdsUser"
        EmptyDataText="�����κ��û����ݣ�" ItemName="��Ա" ItemUnit="λ" OnRowDataBound="EgvUser_RowDataBound"
        CheckBoxFieldHeaderWidth="3%" SerialText="" RowDblclickUrl="UserShow.aspx?UserID={$Field}"
        RowDblclickBoundField="UserId">
        <Columns>
            <pe:TemplateField HeaderText="��Ա��" SortExpression="UserName">
                <ItemTemplate>
                    <a href='UserShow.aspx?UserID=<%#DataBinder.Eval(Container.DataItem,"UserID").ToString()%>'>
                        <%#DataBinder.Eval(Container.DataItem,"UserName").ToString()%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��Ա����" SortExpression="UserType">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="LblUserType">
                    </asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="������Ա��" SortExpression="GroupName">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("GroupName") %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Balance" HeaderText="�ʽ����" DataFormatString="{0:N2}" SortExpression="Balance"
                HtmlEncode="False">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:BoundField DataField="UserPoint" HeaderText="���õ�ȯ" SortExpression="UserPoint">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="ʣ������" SortExpression="Status">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblValidNum" />
                    ��
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </pe:TemplateField>
            <pe:BoundField DataField="UserExp" HeaderText="���û���" SortExpression="UserExp">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����¼IP&lt;br/&gt;����¼ʱ��" SortExpression="Status">
                <HeaderStyle Width="18%" />
                <ItemTemplate>
                    <%#Eval("LastLogOnIP")%>
                    <br />
                    <%#Eval("LastLogOnTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="LogOnTimes" HeaderText="��¼����" SortExpression="LogOnTimes">
                <HeaderStyle Width="4%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="״̬" SortExpression="Status">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblStatus">
                    </pe:ExtendedLabel>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsUser" runat="server" SelectMethod="GetAllUsers" SelectCountMethod="GetNumberOfUsersOnline"
        TypeName="EasyOne.UserManage.Users" DeleteMethod="Delete" EnablePaging="True"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="groupId" QueryStringField="GroupId"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
        <tr>
            <td style="width: 170px;">
                <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />ѡ�б�ҳ��ʾ�������û�</td>
            <td>
                <pe:ExtendedButton ID="EBtnBatchDelete" Text="����ɾ��" IsChecked="true" OperateCode="UserDelete"
                    OnClientClick="return batchconfirm('�Ƿ�Ҫɾ����Ա��');" OnClick="EBtnBatchDelete_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnBatchLock" Text="��������" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('ȷ��Ҫ����ѡ�еĻ�Ա��');" OnClick="EBtnBatchLock_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserPointAdd" Text="������ȯ" IsChecked="true" OperateCode="userpointmanage"
                    OnClick="EBtnUserPointAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserMoneyAdd" Text="����������" IsChecked="true" OperateCode="usermoneymanage"
                    OnClick="EBtnUserMoneyAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserValidDateAdd" Text="�����Ч��" IsChecked="true" OperateCode="uservaliddatemanage"
                    OnClick="EBtnUserValidDateAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserAuditing" Text="������֤" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('ȷ��Ҫ��ѡ���Ļ�Ա��֤ͨ����');" OnClick="EBtnUserAuditing_Click"
                    CausesValidation="False" runat="server" />
                <br />
                <pe:ExtendedButton ID="EBtnSendEmail" Text="�����ʼ�" IsChecked="true" OperateCode="sendinfomanage"
                    OnClick="EBtnSendEmail_Click" CausesValidation="False" runat="server" />
                
                <pe:ExtendedButton ID="EBtnBatchMove" Text="�����ƶ�" IsChecked="true" OperateCode="usermove"
                    OnClick="EBtnBatchMove_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnBatchUnLock" Text="��������" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('ȷ��Ҫ��ѡ���Ļ�Ա������');" OnClick="EBtnBatchunLock_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserPointMinus" Text="�۳���ȯ" IsChecked="true" OperateCode="userpointmanage"
                    OnClick="EBtnUserPointMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserMoneyMinus" Text="�����۽���" IsChecked="true" OperateCode="usermoneymanage"
                    OnClick="EBtnUserMoneyMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserValidDateMinus" Text="������Ч��" IsChecked="true" OperateCode="uservaliddatemanage"
                    OnClick="EBtnUserValidDateMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserNormal" Text="��Ϊ����" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('ȷ��Ҫ��ѡ���Ļ�Ա��Ϊ������');" OnClick="EBtnUserNormal_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnSendTelMessage" Text="���ֻ�����" IsChecked="true" OperateCode="smsmanage"
                    OnClick="EBtnSendTelMessage_Click" CausesValidation="False" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
