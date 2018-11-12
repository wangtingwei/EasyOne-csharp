<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Contents.ContentManageUI" ValidateRequest="false" Codebehind="ContentManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="5" cellspacing="0">
            <tr id='Tab' runat="server">
                <td align='left'>
                    栏目导航：
                    <asp:Label ID="LblNavigation" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a id="poplink">选择栏目<span id="newbox"><iframe marginwidth="0" marginheight="0" frameborder="0" width="250" height="400"
                src="NodeTree.aspx"></iframe></span></a>
              </td>
            </tr>
        </table>
        <script language="javascript" type="text/javascript">
        function popup() {
	        document.getElementById("poplink").onmouseover=function(){document.getElementById("newbox").style.display="block";};
	        document.getElementById("poplink").onmouseout=function(){document.getElementById("newbox").style.display="none";};
        }

        window.onload = popup;
        </script>

        <pe:ExtendedGridView ID="EgvContent" runat="server" DataSourceID="OdsContents" SerialText=""
            AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
            OnRowDataBound="EgvContent_RowDataBound" OnRowCommand="EgvContent_RowCommand"
            DataKeyNames="GeneralId">
            <Columns>
                <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                    <HeaderStyle Width="6%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="标题" SortExpression="Title">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <pe:LinkImage ID="LinkImageModel" runat="server">
                            <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                            <asp:HyperLink ID="LnkItem" runat="server" />
                        </pe:LinkImage>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
                <pe:BoundField DataField="EliteLevel" HeaderText="推荐级别" SortExpression="EliteLevel">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="状态" SortExpression="Status">
                    <HeaderStyle Width="12%" />
                    <ItemTemplate>
                        <%# GetStatusShow(Eval("Status").ToString())%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="操作" SortExpression="Disabled">
                    <HeaderStyle Width="13%" />
                    <ItemTemplate>
                        <asp:HyperLink ID="ContentModify" Text="修改" runat="server" />
                        <asp:LinkButton ID="LtnDelete" Text="删除" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息吗？')"
                            runat="server" CommandArgument='<%# Eval("GeneralId")%>' CommandName="DeleteContent" />
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <div style="padding-top: 5px;">
            <table width="100%" cellpadding="5" cellspacing="0" class="border">
                <tr class="tdbg">
                    <td>
                        <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
                            for="ChkAll">选中本页显示的所有项目</label>
                        <asp:Button ID="EBtnBatchDelete" Text="批量删除" OnClientClick="return confirm('确定要删除选中的项目吗？');"
                            OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;搜索标题：<asp:TextBox ID="TxtSearchTitle" runat="server"></asp:TextBox><asp:Button
                            ID="BtnSearch" runat="server" Text="搜索" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
        <asp:HiddenField ID="HdnStatus" runat="server" Value="100" />
        <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetCommonModelInfoListByUserName"
            TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
            StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCommonModelInfoByUserName">
            <SelectParameters>
                <asp:QueryStringParameter Name="nodeId" QueryStringField="NodeID" Type="Int32" />
                <asp:Parameter Name="userName" Type="string" />
                <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="sortType" PropertyName="Value" />
                <asp:ControlParameter ControlID="HdnStatus" Type="Int32" Name="status" PropertyName="Value" />
                <asp:ControlParameter ControlID="TxtSearchTitle" Type="string" Name="title" PropertyName="Text" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
