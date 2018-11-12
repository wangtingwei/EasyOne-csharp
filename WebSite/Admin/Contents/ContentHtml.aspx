<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ContentHtml" Codebehind="ContentHtml.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server">
    </asp:ScriptManager>
    <pe:ContentManageNavigation ID="Smn" runat="server" />
    <table style="width: 100%; margin: 0 auto;" cellpadding="1" cellspacing="1" class="border">
        <tr>
            <td style="width: 80px" align="left" class="tdbg">
                <b>内容选项：</b>
            </td>
            <td class="tdbg">
                <asp:RadioButtonList ID="RadlCreated" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RadlCreated_SelectedIndexChanged">
                    <asp:ListItem Value="0" Selected="True">所有内容</asp:ListItem>
                    <asp:ListItem Value="1">已生成的内容</asp:ListItem>
                    <asp:ListItem Value="2">未生成的内容</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                排序方式：
                <asp:DropDownList ID="DropRescentQuery" AutoPostBack="true" OnSelectedIndexChanged="DropSelectedIndex_Changed"
                    runat="server">
                    <asp:ListItem Value="-1">按ID降序</asp:ListItem>
                    <asp:ListItem Value="-2">按ID升序</asp:ListItem>
                    <asp:ListItem Value="1">按推荐级别降序</asp:ListItem>
                    <asp:ListItem Value="2">按推荐级别升序</asp:ListItem>
                    <asp:ListItem Value="3">按优先级别降序</asp:ListItem>
                    <asp:ListItem Value="4">按优先级别升序</asp:ListItem>
                    <asp:ListItem Value="5">按日点击数降序</asp:ListItem>
                    <asp:ListItem Value="6">按日点击数升序</asp:ListItem>
                    <asp:ListItem Value="7">按周点击数降序</asp:ListItem>
                    <asp:ListItem Value="8">按周点击数升序</asp:ListItem>
                    <asp:ListItem Value="9">按月点击数降序</asp:ListItem>
                    <asp:ListItem Value="10">按月点击数升序</asp:ListItem>
                    <asp:ListItem Value="11">按总点击数降序</asp:ListItem>
                    <asp:ListItem Value="12">按总点击数升序</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvContents" runat="server" DataSourceID="OdsContents" SerialText=""
        AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        DataKeyNames="GeneralId" OnRowCommand="EgvContents_RowCommand" OnRowDataBound="EgvContents_RowDataBound"
        CheckBoxFieldHeaderWidth="3%">
        <Columns>
            <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                <HeaderStyle Width="40px" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="Title">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                        <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                        <asp:HyperLink ID="HypTitle" runat="server" />
                    </pe:LinkImage>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                <HeaderStyle Width="60px" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态" SortExpression="Status">
                <HeaderStyle Width="50px" />
                <ItemTemplate>
                    <%# GetStatusShow(Eval("Status").ToString())%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已生成" SortExpression="Status">
                <HeaderStyle Width="40px" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblIsCreateHtml" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="生成HTML操作" SortExpression="Disabled">
                <HeaderStyle Width="180px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LnkCreateHtml" CommandName="CreateHtml" CommandArgument='<%# Eval("GeneralId") %>'
                        runat="server">生成文件</asp:LinkButton>
                    <asp:HyperLink ID="LnkHtmlView" runat="server" Target="_blank">查看文件</asp:HyperLink>
                    <asp:LinkButton ID="LnkDeleteHtml" CommandName="DeleteHtml" OnClientClick="if(!this.disabled) return confirm('确实要删除此HTML文件吗？删除后你将不可以还原！')"
                        CommandArgument='<%# Eval("GeneralId") %>' runat="server">删除文件</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnCreated" runat="server" Value="0" />
    <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
    <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetCreateHtmlCommonModelInfoList"
        TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCreateHtmlCommonModelInfo">
        <SelectParameters>
            <asp:QueryStringParameter Name="nodeId" QueryStringField="NodeID" Type="Int32" />
            <asp:ControlParameter ControlID="HdnCreated" Type="Int32" Name="created" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="sortType" PropertyName="Value" />
            <asp:Parameter DefaultValue="false" Name="isEshop" Type="boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">选中本页显示的所有项目</label>
    &nbsp;&nbsp;
    <asp:Button ID="BtnCreateAll" runat="server" CausesValidation="False" Text="生成所有内容" OnClick="BtnCreateAll_Click" />
    <asp:Button ID="BtnCreate" runat="server" CausesValidation="False" Text="生成选定内容" OnClick="BtnCreate_Click" />
    <asp:Button ID="BtnDelete" runat="server" OnClientClick="return batchconfirm('确定要删除选择的HTML文件？');"
        Text="删除选定内容的HTML文件" CausesValidation="False" OnClick="BtnDelete_Click" />
</asp:Content>
