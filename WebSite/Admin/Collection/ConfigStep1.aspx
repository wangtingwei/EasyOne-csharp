<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.ConfigStep1"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集配置第一步" ValidateRequest="false" Codebehind="ConfigStep1.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="添加采集项目设置" AlternateText="修改采集项目设置" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>项目名称 ：&nbsp;</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtItemName" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValeItemName" ControlToValidate="TxtItemName" ErrorMessage="项目名称不能为空！"
                    runat="server"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>对应内容模型 ：&nbsp;</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropModelId" runat="server" DataValueField="ModelId" DataTextField="ModelName"
                    OnSelectedIndexChanged="DropNode_SelectedIndexChanged">
                </asp:DropDownList>
                <pe:RequiredFieldValidator ID="ValeModelId" ControlToValidate="DropModelId" ErrorMessage="模型不能为空！"
                    runat="server"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <pe:NodeType ID="NodeType1" FieldAlias="所属节点" runat="server" />
        <pe:SpecialType ID="SpecialId" runat="server" FieldAlias="所属专题" />
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>采集网站 ：&nbsp;</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtWebSite" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>采集URL ：&nbsp;</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUrl" runat="server" Width="400px" Text="http://"></asp:TextBox>
                <span style="color: Green">注：以 http:// 开头</span>
                <pe:RequiredFieldValidator ID="ValeUrl" ControlToValidate="TxtUrl" ErrorMessage="采集URL不能为空！"
                    runat="server"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>编码选择 ：&nbsp;</strong>
            </td>
            <td style="height: 30px">
                <asp:RadioButtonList runat="server" ID="RadlCodeType" RepeatDirection="Horizontal">
                    <asp:ListItem Value="GB2312" Selected="True">GB2312</asp:ListItem>
                    <asp:ListItem Value="UTF-8">UTF-8</asp:ListItem>
                    <asp:ListItem Value="Big5">Big5</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>指定采集数量 ：&nbsp;</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtMaxNum" runat="server" Width="50px"></asp:TextBox>
                <span style="color: Green">注：不指定为全部</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>采集后信息是否生成html ：&nbsp;</strong>
            </td>
            <td style="height: 30px">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="RadlAutoCreateHtml" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True">生成</asp:ListItem>
                                <asp:ListItem Value="False" Selected="True">不生成</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <span style="color: Green;">注：只有终审通过的信息才会生成</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>采集顺序 ：&nbsp;</strong>
            </td>
            <td style="height: 30px">
                <asp:RadioButtonList runat="server" ID="RadlOrder" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">正序</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">倒序</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%; height: 27px;" align="right">
                <strong>采集简介 ：&nbsp;</strong>
            </td>
            <td style="height: 27px">
                <asp:TextBox ID="TxtIntro" runat="server" Height="120px" TextMode="MultiLine" Width="300px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnSubmit" Text="下一步" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;<input
            name="Cancel" type="button" class="inputbutton" id="Cancel2" value="返回采集管理" onclick="Redirect('ItemManage.aspx')" />
        <asp:HiddenField ID="HdnAction" runat="server" />
        <asp:HiddenField ID="HdnItemName" runat="server" />
        <asp:HiddenField ID="HiddenModel" runat="server" />
        <asp:HiddenField ID="HdnDetection" runat="server" />
    </center>
</asp:Content>
