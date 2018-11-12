<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.PresentPic" Codebehind="PresentPic.ascx.cs" %>
<tr class="tdbg">
    <td class="tdbgleft" align="right" <%=TitleWidth%>>
        <strong>商品清晰图：&nbsp; </strong><br />
    </td>
    <td class="tdbg" align="left" <%=ColspanControl%>>
        <pe:FileUploadControl ID="FileUploadPresentPic" Height="30px" ModuleName="Shop" runat="server">
        </pe:FileUploadControl>
        <asp:CheckBox ID="ChkThumb" runat="server"/>自动生成缩略图<br />
        <asp:CheckBox ID="ChkPresentPicWatermark" runat="server" />添加水印
    </td>
</tr>
<tbody runat="server" id="tbThumb">
<tr class="tdbg">
    <td class="tdbgleft" align="right">
        <strong>商品缩略图：&nbsp; </strong><br />
    </td>
    <td class="tdbg" align="left" <%=ColspanControl%>>
        <pe:FileUploadControl ID="FileUploadPresentThumb" Height="30px" ModuleName="Shop" runat="server">
        </pe:FileUploadControl>
        <asp:CheckBox ID="ChkPresentThumbWatermark" runat="server" />添加水印
    </td>
</tr>
</tbody>
<script language="javascript" type="text/javascript">
        function SetThumb()
        {
            var ChkThumb = document.getElementById('<%=ChkThumb.ClientID %>');
            var tbThumb = document.getElementById('<%=tbThumb.ClientID %>');
            if(ChkThumb.checked == true)
            {
                tbThumb.style.display = "none";
            }
            else
            {
                tbThumb.style.display = "";
            }
        }
</script>
