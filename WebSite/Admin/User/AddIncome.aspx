<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AddIncome"
    MasterPageFile="~/Admin/MasterPage.master" Title="添加会员银行汇款信息" Codebehind="AddIncome.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text="添加会员银行汇款信息" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="ShowUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    到款日期：</td>
                <td align="left">
                    <pe:DatePicker ID="DpkReceipt" runat="server">
                    </pe:DatePicker>
                    <span style="color: Blue;">不填为当天日期</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    汇款金额：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" Columns="8" MaxLength="8"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="汇款金额不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td style="text-align: left">
                    <asp:CheckBox ID="ChkPoint" runat="server" />给用户赠送<asp:TextBox ID="TxtPoint" runat="server"
                        Width="50px" MaxLength="9"></asp:TextBox><pe:ShowPointName ID="ShowPointName2" PointType="PointUnit"
                            runat="server" /><pe:ShowPointName ID="ShowPointName1" runat="server" />
                    <asp:RegularExpressionValidator ID="ValrPoint" ControlToValidate="TxtPoint" SetFocusOnError="true"
                        Display="dynamic" ValidationExpression="\d*" runat="server" ErrorMessage="赠送数应输入整数"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    汇入银行：</td>
                <td align="left">
                    <asp:DropDownList ID="DropBankShortName" runat="server" Width="97px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    备注：</td>
                <td align="left">
                    <asp:TextBox ID="TxtRemark" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtRemark" runat="server"
                        ErrorMessage="备注不能为空"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    内部记录：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                        Rows="4" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />同时发送手机短信通知会员
                </td>
            </tr>
            <tr class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <span style="color: #FF0000;">注意：汇款/收入信息一旦录入，就不能再修改或删除！所以在保存之前确认输入无误！</span></td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="保存汇款信息" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
