<%@ Page Language="C#" AutoEventWireup="true" Title="风格管理" ValidateRequest="false"
    MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Template.StyleSheets" Codebehind="StyleSheets.aspx.cs" %>

<asp:Content ID="ContentNavigation" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td colspan="2" class="tdbgleft">
                <pe:AlternateLiteral ID="AlternateLiteral1" Text="填写文件名：" AlternateText="编辑样式：" runat="Server" /><asp:Label
                    ID="LblFileName" runat="server"></asp:Label><asp:TextBox ID="TxtFileName" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                        ID="ValrTxtFileName" ControlToValidate="TxtFileName" Display="Dynamic" runat="server"
                        ErrorMessage="请填写文件名"></pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeTxtFileName" ControlToValidate="TxtFileName"
                    Display="Dynamic" runat="server" ErrorMessage='请使用正确的扩展名.css，不能包含\/:*?"<>|.和空格等字符！'></asp:RegularExpressionValidator><asp:Label
                        ID="savefilename" Style="color: Red" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" valign="top">
                <asp:TextBox ID="EditorContent" runat="server" Height="400" Width="99%" TextMode="MultiLine"
                    Rows="7" Wrap="true"></asp:TextBox>
                <div style="text-align: center; vertical-align: top;">
                    <img alt="增加高度" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50)" />
                    <img alt="减少高度" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50)" />
                </div>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnSubmit" runat="server" Text="保存" OnClick="BtnSubmit_Click" />&nbsp;
        <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('<%= ViewState["UrlReferrer"].ToString() %>')" />
    </center>

    <script type="text/javascript">
    function sizeChange(size){
    var obj=document.getElementById("<% = EditorContent.ClientID %>");
    var height = parseInt(obj.offsetHeight);
    if (height+size>=100){
        obj.style.height=height+size+'px';
    }
}
    </script>

</asp:Content>
