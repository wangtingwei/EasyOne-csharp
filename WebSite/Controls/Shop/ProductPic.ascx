<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.ProductPic" Codebehind="ProductPic.ascx.cs" %>
<tr class="tdbg">
    <td class="tdbgleft" align="right" <%=TitleWidth%>>
        <strong>��Ʒ����ͼ��&nbsp; </strong><br />
    </td>
    <td class="tdbg" align="left" <%=ColspanControl%>>
        <pe:FileUploadControl ID="FileUploadProductPic" Height="30px" ModuleName="Shop" runat="server">
        </pe:FileUploadControl>
        <asp:CheckBox ID="ChkThumb" runat="server"/>�Զ���������ͼ<br />
        <asp:CheckBox ID="ChkProductPicWatermark" runat="server" />���ˮӡ
    </td>
</tr>
<tbody runat="server" id="tbThumb">
<tr class="tdbg">
    <td class="tdbgleft" align="right">
        <strong>��Ʒ����ͼ��&nbsp; </strong><br />
    </td>
    <td class="tdbg" align="left" <%=ColspanControl%>>
        <pe:FileUploadControl ID="FileUploadProductThumb" Height="30px" ModuleName="Shop" runat="server">
        </pe:FileUploadControl>
        <asp:CheckBox ID="ChkProductThumbWatermark" runat="server" />���ˮӡ
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

