<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserManage" Title="用户管理" Codebehind="UserManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvUser" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="UserId" DataSourceID="OdsUser"
        EmptyDataText="暂无任何用户数据！" ItemName="会员" ItemUnit="位" OnRowDataBound="EgvUser_RowDataBound"
        CheckBoxFieldHeaderWidth="3%" SerialText="" RowDblclickUrl="UserShow.aspx?UserID={$Field}"
        RowDblclickBoundField="UserId">
        <Columns>
            <pe:TemplateField HeaderText="会员名" SortExpression="UserName">
                <ItemTemplate>
                    <a href='UserShow.aspx?UserID=<%#DataBinder.Eval(Container.DataItem,"UserID").ToString()%>'>
                        <%#DataBinder.Eval(Container.DataItem,"UserName").ToString()%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="会员类型" SortExpression="UserType">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label runat="server" ID="LblUserType">
                    </asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="所属会员组" SortExpression="GroupName">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("GroupName") %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Balance" HeaderText="资金余额" DataFormatString="{0:N2}" SortExpression="Balance"
                HtmlEncode="False">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:BoundField DataField="UserPoint" HeaderText="可用点券" SortExpression="UserPoint">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="剩余天数" SortExpression="Status">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblValidNum" />
                    天
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </pe:TemplateField>
            <pe:BoundField DataField="UserExp" HeaderText="可用积分" SortExpression="UserExp">
                <HeaderStyle Width="8%" />
                <ItemStyle HorizontalAlign="Right" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="最后登录IP&lt;br/&gt;最后登录时间" SortExpression="Status">
                <HeaderStyle Width="18%" />
                <ItemTemplate>
                    <%#Eval("LastLogOnIP")%>
                    <br />
                    <%#Eval("LastLogOnTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="LogOnTimes" HeaderText="登录次数" SortExpression="LogOnTimes">
                <HeaderStyle Width="4%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态" SortExpression="Status">
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
                <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有用户</td>
            <td>
                <pe:ExtendedButton ID="EBtnBatchDelete" Text="批量删除" IsChecked="true" OperateCode="UserDelete"
                    OnClientClick="return batchconfirm('是否要删除会员？');" OnClick="EBtnBatchDelete_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnBatchLock" Text="批量锁定" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('确定要锁定选中的会员吗？');" OnClick="EBtnBatchLock_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserPointAdd" Text="奖励点券" IsChecked="true" OperateCode="userpointmanage"
                    OnClick="EBtnUserPointAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserMoneyAdd" Text="批量发奖金" IsChecked="true" OperateCode="usermoneymanage"
                    OnClick="EBtnUserMoneyAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserValidDateAdd" Text="添加有效期" IsChecked="true" OperateCode="uservaliddatemanage"
                    OnClick="EBtnUserValidDateAdd_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserAuditing" Text="批量认证" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('确定要将选定的会员认证通过吗？');" OnClick="EBtnUserAuditing_Click"
                    CausesValidation="False" runat="server" />
                <br />
                <pe:ExtendedButton ID="EBtnSendEmail" Text="发送邮件" IsChecked="true" OperateCode="sendinfomanage"
                    OnClick="EBtnSendEmail_Click" CausesValidation="False" runat="server" />
                
                <pe:ExtendedButton ID="EBtnBatchMove" Text="批量移动" IsChecked="true" OperateCode="usermove"
                    OnClick="EBtnBatchMove_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnBatchUnLock" Text="批量解锁" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('确定要将选定的会员解锁吗？');" OnClick="EBtnBatchunLock_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserPointMinus" Text="扣除点券" IsChecked="true" OperateCode="userpointmanage"
                    OnClick="EBtnUserPointMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserMoneyMinus" Text="批量扣奖金" IsChecked="true" OperateCode="usermoneymanage"
                    OnClick="EBtnUserMoneyMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserValidDateMinus" Text="减少有效期" IsChecked="true" OperateCode="uservaliddatemanage"
                    OnClick="EBtnUserValidDateMinus_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnUserNormal" Text="置为正常" IsChecked="true" OperateCode="UserLock"
                    OnClientClick="return batchconfirm('确定要将选定的会员置为正常吗？');" OnClick="EBtnUserNormal_Click"
                    CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnSendTelMessage" Text="发手机短信" IsChecked="true" OperateCode="smsmanage"
                    OnClick="EBtnSendTelMessage_Click" CausesValidation="False" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
