<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.SalePromotion" Codebehind="SalePromotion.ascx.cs" %>
<asp:RadioButton GroupName="SalePromotionType" ID="RadSalePromotionType1" Checked="true" runat="server" />
������<br />
<span style="color: Red;">
    <asp:RadioButton GroupName="SalePromotionType" ID="RadSalePromotionType2" runat="server" />
    ��
    <asp:TextBox Width="30px" ID="TxtMinNumber1" Text="1" MaxLength="4" runat="server"></asp:TextBox>
    ��
    <asp:TextBox ID="TxtPresentNumber1" Width="30px" Text="1" MaxLength="4" runat="server"></asp:TextBox>
    ͬ������Ʒ</span><br />
<span style="color: Blue">
    <asp:RadioButton GroupName="SalePromotionType" ID="RadSalePromotionType3" runat="server" />
    ��
    <asp:TextBox ID="TxtMinNumber2" Width="30px" Text="1" MaxLength="4" runat="server"></asp:TextBox>
    ��
    <asp:TextBox ID="TxtPresentNumber2" Width="30px" Text="1" MaxLength="4" runat="server"></asp:TextBox>
    ������Ʒ&nbsp;&nbsp;��Ʒ���ƣ�<pe:CrmSelectControl ID="SelectPresent1" runat="server" ButtonText="..."
        FileUrl="~/Admin/Shop/PresentList.aspx"></pe:CrmSelectControl>&nbsp; </span>
      <asp:CustomValidator ID="ValxPresent1" ClientValidationFunction="ValxPresent1_ClientValidate" Display="dynamic" ValidateEmptyText="true" SetFocusOnError="true" runat="server" ErrorMessage="��������Ʒ����" ControlToValidate="SelectPresent1"></asp:CustomValidator>
     <br />
<span style="color: Red;">
    <asp:RadioButton GroupName="SalePromotionType" ID="RadSalePromotionType4" runat="server" />
    ��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��
    <asp:TextBox ID="TxtPresentNumber3" Text="1" MaxLength="4" Width="30px" runat="server"></asp:TextBox>
    ͬ������Ʒ</span><br />
<span style="color: Blue">
    <asp:RadioButton GroupName="SalePromotionType" ID="RadSalePromotionType5" runat="server" />
    ��&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;��
    <asp:TextBox ID="TxtPresentNumber4" Width="30px" Text="1" MaxLength="4" runat="server"></asp:TextBox>
    ������Ʒ&nbsp;&nbsp;��Ʒ���ƣ�</span><pe:CrmSelectControl ID="SelectPresent2" runat="server" ButtonText="..." FileUrl="~/Admin/Shop/PresentList.aspx"></pe:CrmSelectControl>
      <asp:CustomValidator ID="ValxPresent2" ClientValidationFunction="ValxPresent2_ClientValidate" Display="dynamic" ValidateEmptyText="true" SetFocusOnError="true" runat="server" ErrorMessage="��������Ʒ����" ControlToValidate="SelectPresent2"></asp:CustomValidator>
    <script language="javascript" type="text/javascript">
        function ValxPresent1_ClientValidate(s,e)
        {
             var present = document.getElementById('<%=SelectPresent1.ClientID %>').value
             var salePromotionType = document.getElementById('<%=RadSalePromotionType3.ClientID %>')
             if(present =="" && salePromotionType.checked==true)
             {
                e.IsValid = false;
             }
             else
             {
                e.IsValid = true;
             }
           
        }
        function ValxPresent2_ClientValidate(s,e)
        {
             var present = document.getElementById('<%=SelectPresent2.ClientID %>').value
             var salePromotionType = document.getElementById('<%=RadSalePromotionType5.ClientID %>')
             if(present =="" && salePromotionType.checked==true)
             {
                e.IsValid = false;
             }
             else
             {
                e.IsValid = true;
             }
           
        }

    </script>
