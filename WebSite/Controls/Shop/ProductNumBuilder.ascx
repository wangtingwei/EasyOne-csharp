<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.ProductNumBuilder" Codebehind="ProductNumBuilder.ascx.cs" %>
<asp:UpdatePanel ID="UpnlProductNum" runat="server" UpdateMode="conditional">
    <ContentTemplate>
        <asp:TextBox ID="TxtProductNum" runat="server" Enabled="false" Columns="30"></asp:TextBox><input
            id="ChkProductNum" checked="checked" onclick="SetTxtProductNumDisabled(this.checked)"
            type="checkbox" />�Զ����<asp:Button ID="BtnCheckProductNum" runat="server" CausesValidation="false"
                OnClick="BtnCheckProductNum_Click" Text="����Ƿ������ͬ����Ʒ���" />
        <pe:ExtendedLabel HtmlEncode="false" ID="LblNotes" runat="server" Text=""></pe:ExtendedLabel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

      function SetTxtProductNumDisabled(checked)
      {
        document.getElementById('<%=TxtProductNum.ClientID %>').disabled =checked;
      }
      
</script>