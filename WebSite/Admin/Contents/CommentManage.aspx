<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Contents.CommentManage" Codebehind="CommentManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="javascript" type="text/javascript">
  function unselectall(){
    if(document.aspnetForm.chkAll.checked){
        document.aspnetForm.chkAll.checked = document.aspnetForm.chkAll.checked&0;
    }
  }
  function CheckAll(form){
  for (var i=0;i <form.elements.length;i++){
      var e = form.elements[i];
      if (e.Name != 'chkAll'&&e.disabled==false)
      e.checked = form.chkAll.checked;
      }
  }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td>
                <asp:LinkButton ID="LbtnAllComment" runat="server" OnClick="BtnAllComment_Click">所有已评论的内容</asp:LinkButton>
                |
                <asp:LinkButton ID="LbtnUNAuditedComment" runat="server" OnClick="BtnUNAuditedComment_Click">有待审核评论的内容</asp:LinkButton>
                |
                <asp:LinkButton ID="LbtnuditedComment" runat="server" OnClick="BtnAuditedComment_Click">评论已全部审核的内容</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <asp:HiddenField ID="HdnSearchType" runat="server" />
    <div style="text-align: left">
        <asp:Repeater ID="RptCommentList" runat="server" OnItemDataBound="RptCommentManage_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <pe:ExtendedLabel HtmlEncode="false" ID="LblRestoreBottom" runat="server" Text="" Visible="false" />
                <pe:ExtendedLiteral ID="LblCommentHead" runat="server" Text="" Visible="false" />
                <tr class="tdbg" onmouseout="this.className='tdbg'" onmouseover="this.className='tdbgmouseover'">
                    <td style="width: 30px;" align="center">
                        <input name="CommentID" type="checkbox" onclick="unselectall()" id="CommentID" value='<%#Eval("CommentID")%>'>
                    </td>
                    <td><pe:ExtendedLiteral ID="LblContentTitle" runat="server" Text="" />
                        <pe:ExtendedLiteral ID="LblContent" runat="server" Text="" /></td>
                    <td style="width: 60px;" align="center">
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblStatus" runat="server" Text="" />
                        <td style="width: 150px;" align="center">
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblManage" runat="server" Text="" />
                        </td>
                </tr>
                <pe:ExtendedLiteral ID="LblRestoreTitle" runat="server" Text="" Visible="false" />
                <pe:ExtendedLiteral ID="LblRestore" runat="server" Text="" Visible="false" />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <pe:ExtendedLabel HtmlEncode="false" ID="LblCommontBottom2" runat="server" Text="</table></td></tr></table>" />
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px; height: 30px;">
                    <input name="chkAll" type="checkbox" id="chkAll" onclick="CheckAll(this.form)" value="checkbox" />
                    选中本页显示的所有评论
                </td>
                <td>
                    <asp:Button ID="BtnSubmit1" runat="server" Text="删除选定的评论" OnClick="BtnSubmit1_Click"
                        UseSubmitBehavior="False" OnClientClick="if(!confirm('确定要批量删除评论吗？')){return false;}" />
                    <asp:Button ID="BtnSubmit2" runat="server" Text="审核通过选定的评论" OnClick="BtnSubmit2_Click"
                        UseSubmitBehavior="False" />
                    <asp:Button ID="BtnSubmit3" runat="server" Text="取消审核选定的评论" OnClick="BtnSubmit3_Click"
                        UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>
        <!-- 显示评论树结束 -->
        <center>
            <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
            </pe:AspNetPager>
        </center>
    </div>
</asp:Content>
