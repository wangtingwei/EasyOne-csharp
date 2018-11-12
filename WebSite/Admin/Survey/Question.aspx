<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.QuestionUI" Title="问卷管理" Codebehind="Question.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                当前问卷：<asp:Label ID="LblSurveyName" runat="server"></asp:Label>
                <asp:HyperLink ID="LnkSurveyName" runat="server"></asp:HyperLink>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="title" colspan="2" align="center">
                <asp:Label ID="LblTitle" runat="server" Text="添加问题" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>问题内容：&nbsp;</strong></td>
            <td class="tdbg" align="left" style="width: 818px">
                <asp:TextBox ID="TxtQuestionContent" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrQuestionContent" ControlToValidate="TxtQuestionContent"
                    runat="server" ErrorMessage="问题内容不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>是否必填：&nbsp;</strong></td>
            <td class="tdbg" align="left" style="width: 818px">
                <asp:RadioButtonList ID="RadlEnableNull" runat="server" Height="3px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>问题类型：&nbsp;</strong></td>
            <td class="tdbg" align="left" style="width: 818px">
                <asp:RadioButtonList ID="RadlQuestionType" CausesValidation="false" AutoPostBack="true"
                    runat="server" OnSelectedIndexChanged="RadlQuestionType_SelectedIndexChanged"
                    RepeatColumns="3">
                    <asp:ListItem Value="0" Selected="True">单行文本</asp:ListItem>
                    <asp:ListItem Value="1">多行文本</asp:ListItem>
                    <asp:ListItem Value="2">单选</asp:ListItem>
                    <asp:ListItem Value="3">多选</asp:ListItem>
                    <asp:ListItem Value="4">下拉</asp:ListItem>
                    <asp:ListItem Value="5">多选列表</asp:ListItem>
                    <asp:ListItem Value="6">日期和时间</asp:ListItem>
                    <asp:ListItem Value="7">是/否（单选框）</asp:ListItem>
                    <asp:ListItem Value="8">数字</asp:ListItem>
                    <asp:ListItem Value="9">Email</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tbody id="PnlChoice" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px">
                    <strong>问题选项</strong></td>
                <td class="tdbg" align="left" style="width: 818px">
                    <asp:TextBox ID="TxtSettings" runat="server" Height="100px" TextMode="MultiLine"
                        Width="300px" Wrap="false"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrChoiceSelectItem" runat="server" ControlToValidate="TxtSettings"
                        Display="Dynamic" ErrorMessage="分行选项不能为空"></pe:RequiredFieldValidator>
                    <span style="color: blue">注：每一个选项为一行</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlInputType" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px">
                    <strong>允许“填充”选项：&nbsp;</strong></td>
                <td class="tdbg" align="left" style="width: 818px">
                    <asp:RadioButtonList ID="RadlInputType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="RadlInputType_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="1">单行</asp:ListItem>
                        <asp:ListItem Value="2">多行</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlText" visible="true" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px">
                    <strong>回答的最多字符数：&nbsp;</strong></td>
                <td class="tdbg" align="left" style="width: 818px">
                    <asp:TextBox ID="TxtContentLength" Text="255" Width="30" MaxLength="3" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrContentLength" ControlToValidate="TxtContentLength"
                        runat="server" ErrorMessage="最多字符数不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ControlToValidate="TxtContentLength" Display="Dynamic" SetFocusOnError="true"
                        ID="VnumContentLength" runat="server"></pe:NumberValidator>
                    <asp:RangeValidator ID="ValgContentLength" ControlToValidate="TxtContentLength" MinimumValue="1"
                        SetFocusOnError="true" MaximumValue="255" Display="Dynamic" runat="server" ErrorMessage="字符数不能小于1大于255"
                        Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
        </tbody>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:HiddenField ID="HdnQuestionId" runat="server" />
                <asp:HiddenField ID="HdnIsOpen" runat="server" />
                <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnSubmit" runat="server"
                    Text="保存" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    function CheckDefaultValue()
    {
            
    }
    </script>

</asp:Content>
