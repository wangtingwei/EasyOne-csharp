<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.IncludeFileManage" Codebehind="IncludeFileManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvIncludeFileList" runat="server" AutoGenerateColumns="false"
        AllowPaging="true" DataSourceID="OdsIncludeFileList" DataKeyNames="Id" ItemName="内嵌代码"
        OnRowDataBound="EgvIncludeFileList_RowDataBound" OnRowCommand="EgvIncludeFileList_RowCommand"
        AutoGenerateCheckBoxColumn="true" ItemUnit="个"
        RowDblclickBoundField="Id" RowDblclickUrl="IncludeFile.aspx?action=modify&amp;id={$Field}">
        <Columns>
            <pe:TemplateField HeaderText="名称">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <span title="<%# Eval("Description") %>"><a href="IncludeFile.aspx?action=modify&id=<%# Eval("Id") %>">
                        <%# Eval("Name") %>
                    </a></span>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="IncludeType" HeaderText="类型" SortExpression="IncludeType"
                >
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="文件名">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <pe:ExtendedLiteral ID="LitFileName" runat="server"></pe:ExtendedLiteral>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="调用代码">
                <ItemTemplate>
                    <asp:TextBox ID="TxtIncludeCode" onclick="selectItem(this);" runat="server" TextMode="MultiLine"
                        Height="40" Width="98%" Wrap="true"></asp:TextBox>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <a href="IncludeFile.aspx?action=modify&id=<%# Eval("Id") %>">修改</a>
                    <asp:LinkButton ID="LnkCreateIncludeFile" runat="server" CommandArgument='<%# Eval("Id") %>'
                        CommandName="CreateIncludeFile" Text="刷新"></asp:LinkButton><br />
                    <asp:HyperLink runat="server" NavigateUrl='<%# "IncludeFilePreview.aspx?id=" + Eval("Id") %>' ID="HlnkPreview">预览</asp:HyperLink>
                    <asp:LinkButton ID="LnkDelete" OnClientClick="return confirm('确实要删除该内嵌代码吗？');" runat="server"
                        CommandArgument='<%# Eval("Id") %>' CommandName="DeleteIncludeFile" Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">选中本页所有项</label>
    <asp:Button ID="BtnBatchDelete" runat="server" OnClick="BtnBatchDelete_Click" OnClientClick="return confirm('确实要删除选中的内嵌代码吗？');"
        Text="批量删除" />
    <asp:Button ID="BtnBatchCreateIncludeFile" runat="server" OnClick="BtnBatchCreateIncludeFile_Click"
        Text="批量刷新" />
    <asp:Button ID="BtnAllCreateIncludeFile" runat="server" Text="刷新所有内嵌代码" OnClick="BtnAllCreateIncludeFile_Click" />
    <asp:ObjectDataSource ID="OdsIncludeFileList" runat="server" SelectCountMethod="GetTotalOfIncludeFileInfo"
        SelectMethod="GetIncludeFileInfoList" TypeName="EasyOne.Templates.IncludeFile"
        EnablePaging="true" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
    </asp:ObjectDataSource>
    <br />
    <br />
    <b>说明：</b>
    <ul>
        <li style="margin-left:20px;">这些内嵌代码是为了加快访问速度特别生成的。</li>
        <li style="margin-left:20px;"><span style="color: red">若文件名为红色，表示此内嵌代码文件还没有生成。 </span></li>
    </ul>
    <b>使用方法：</b>
    <ul>
        <li style="margin-left:20px;">将相关调用代码复制到页面或模板中的相关位置即可。可参见系统提供的各页面及模板。</li>
    </ul>

    <script type="text/javascript">
    function selectItem(obj)
    {
        var range = obj.createTextRange()      
        range.moveStart("character",0);      
        range.select();          
    }
    
    </script>

</asp:Content>
