<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.Model"
    MasterPageFile="~/Admin/MasterPage.master" Title="模型管理" ValidateRequest="false" Codebehind="Model.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table class="border" width="100%" cellpadding="2" cellspacing="1">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <pe:AlternateLiteral ID="AltrTitle" Text="添加内容模型" AlternateText="修改内容模型" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="trModelTemmpalteId">
            <td class="tdbgleft">
                <strong>载入内容模型模板：</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropModelTemplate" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>内容模型名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" runat="server" Width="156" MaxLength="200" />
                <pe:RequiredFieldValidator ID="ValrModelName" ControlToValidate="TxtModelName" runat="server"
                    ErrorMessage="内容模型名称不能为空！" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>创建的数据表名：</strong>
                <pe:AlternateLiteral ID="TableNameText" Text="<br /><span style='color: red'>注意：</span>创建表后将无法再更改表名称"
                    runat="Server" />
            </td>
            <td>
                <asp:Label ID="LblTablePrefix" runat="server" Text="PE_U_" />
                <asp:TextBox ID="TxtTableName" runat="server" Width="120" MaxLength="50" />
                <pe:RequiredFieldValidator ID="ValrTableName" ControlToValidate="TxtTableName" runat="server"
                    ErrorMessage="数据表名不能为空" SetFocusOnError="true" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName"
                    ErrorMessage="只允许输入字母、数字或下划线" ValidationExpression="^[\w_]+$" SetFocusOnError="true"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目名称：</strong>
                <br />
                例如：文章、软件、图片、商品
            </td>
            <td>
                <asp:TextBox ID="TxtItemName" runat="server" Width="156" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrItemName" ControlToValidate="TxtItemName" runat="server"
                    ErrorMessage="项目名称不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目单位：</strong>
                <br />
                例如：篇、个、件
            </td>
            <td>
                <asp:TextBox ID="TxtItemUnit" runat="server" Width="156" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrItemUnit" ControlToValidate="TxtItemUnit" runat="server"
                    ErrorMessage="项目单位不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>项目图标：</strong>
                <br />
                图标存放在~/Images/ModelIcon/目录下
            </td>
            <td>
                <asp:TextBox ID="TxtItemIcon" Text="Default.gif" runat="server" Width="156" MaxLength="20" />
                <asp:Image ID="ImgItemIcon" runat="server" ImageUrl="~/Images/ModelIcon/Default.gif" />
                <=<asp:DropDownList ID="DrpItemIcon" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>模型描述：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Width="365px"
                    Height="43px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>默认内容页模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="FileCTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>打印页模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscPrintTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>栏目搜索页模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscSearchTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>高级搜索表单页模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscAdvanceSearchFormTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>高级搜索页模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscAdvanceSearchTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>查看评论模板：</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscCommentManageTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>信息发布文件：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtAddInfoFilePath" runat="server" Width="156" MaxLength="200" Text="Content.aspx" />
                <pe:RequiredFieldValidator ID="ValrAddInfoFilePath" ControlToValidate="TxtAddInfoFilePath"
                    runat="server" ErrorMessage="信息发布文件不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>信息管理文件：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtManageInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentManage.aspx" />
                <pe:RequiredFieldValidator ID="ValrManageInfoFilePath" ControlToValidate="TxtManageInfoFilePath"
                    runat="server" ErrorMessage="信息管理文件不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>信息预览文件：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPreviewInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentView.aspx" />
                <pe:RequiredFieldValidator ID="ValrPreviewInfoFilePath" ControlToValidate="TxtPreviewInfoFilePath"
                    runat="server" ErrorMessage="信息预览文件不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>信息批量设置文件：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBatchInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentBatch.aspx" />
                <pe:RequiredFieldValidator ID="ValrBatchInfoFilePath" ControlToValidate="TxtBatchInfoFilePath"
                    runat="server" ErrorMessage="信息量设置文件不能为空" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>自定义内容发布和管理页注意事项：</strong>
            </td>
            <td style="width: 80%">
                如果以上四项使用了自定义的程序文件，请将文件存放在“~/Admin/Contents/”目录下。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否统计点击数：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioIsCountHits" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否禁用：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioDisabled" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="EnableCharge">
            <td class="tdbgleft">
                <strong>是否启用收费：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioEnableCharge" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
         <tr class="tdbg" runat="server" id="EnableChargeTips">
            <td class="tdbgleft">
                <strong>生成静态页时的收费提示：</strong><br />
                支持HTML代码，特别标签有：<br />
                {$ModelName} 项目名称<br />
                {$FileName} 文件夹名<br />
                {$Id}     信息的GeneralId<br />
                
            </td>
            <td>
                <asp:TextBox ID="TxtModelChargeTips" TextMode="MultiLine" MaxLength="255" Width="365px" Height="43px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用签收：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioEnableSignin" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用投票：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadVote" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>用户在此模型下发表内容的限制数：</strong>
                <br />
                如果为0表示没有限制
            </td>
            <td>
                <asp:TextBox ID="TxtMaxPerUser" runat="server" MaxLength="6" Text="0" Width="36" />个内容
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('ModelManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnModelId" runat="server" />
    <asp:HiddenField ID="HdnModelName" runat="server" />
    <asp:HiddenField ID="HdnTableName" runat="server" />

    <script type="text/javascript">
    function ChangeImgItemIcon(icon)
    {
        document.getElementById("<%= ImgItemIcon.ClientID %>").src = "../../Images/ModelIcon/"+icon;
    }
    function ChangeTxtItemIcon(icon)
    {
        document.getElementById("<%= TxtItemIcon.ClientID %>").value = icon;
    }
    </script>

</asp:Content>
