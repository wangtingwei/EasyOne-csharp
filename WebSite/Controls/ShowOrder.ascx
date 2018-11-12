<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ShowOrder" Codebehind="ShowOrder.ascx.cs" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<script type="text/javascript">
     var oID=0;
    function ShowOrderTabs(ID)
    {
      if(ID!=oID){
        document.getElementById("TabOrderTitle"+ID).className ='titlemouseover';
        document.getElementById("TabOrderTitle"+oID).className = 'tabtitle';
        document.getElementById("Tabs_Order"+oID).style.display = 'none';
        document.getElementById("Tabs_Order"+ID).style.display = '';
        oID=ID;
      }
    }  
</script>

<table style="width:100%" border='0' cellpadding='0' cellspacing='0'>
    <tr style="height: 22px; text-align: center">
        <td id='TabOrderTitle0' class='titlemouseover' onclick='ShowOrderTabs(0)'>
            ������Ϣ</td>
        <td id='TabOrderTitle1' class='tabtitle' onclick='ShowOrderTabs(1)'>
            ��Ʊ��¼</td>
        <td id='TabOrderTitle2' class='tabtitle' onclick='ShowOrderTabs(2)'>
            ���˻���¼</td>
        <td id='TabOrderTitle3' class='tabtitle' <%=IsShow()%> onclick='ShowOrderTabs(3)'>
            ������¼</td>
        <td id='TabOrderTitle4' class='tabtitle' onclick='ShowOrderTabs(4)'>
            �����¼</td>
        <td id='TabOrderTitle5' class='tabtitle' onclick='ShowOrderTabs(5)'>
            Ͷ�߼�¼</td>
        <td id='TabOrderTitle6' class='tabtitle' onclick='ShowOrderTabs(6)'>
            ������¼</td>
        <td id='TabOrderTitle7' class='tabtitle' onclick='ShowOrderTabs(7)'>����֧����¼</td>        
        <td>&nbsp;</td>
    </tr>
</table>
<div id="Tabs_Order0" class="border" style="width: 99%; display: block;">
    <pe:ExtendedGridView ID="EgvBankroll" runat="server" SerialText="" AutoGenerateColumns="False"
        EmptyDataText="û����ظ����¼" OnDataBound="EgvBankroll_DataBound" OnRowDataBound="EgvBankroll_RowDataBound"
        ShowFooter="True" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataTextField="ClientName" HeaderText="�ͻ�����"
                DataNavigateUrlFormatString="~/Admin/Crm/ClientShow.aspx?ClientId={0}">
                <headerstyle width="10%" />
            </asp:HyperLinkField>
            <pe:TemplateField HeaderText="�û���">
                <headerstyle width="8%" />
                <itemtemplate>
                <asp:HyperLink id="LnkUserName" runat="server" NavigateUrl='<%# Eval("UserName", "~/Admin/User/UserShow.aspx?UserName={0}") %>' Text='<%# Eval("UserName") %>' __designer:wfdid="w9"></asp:HyperLink> 
                </itemtemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="DateAndTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False">
                <HeaderStyle width="17%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="���׷�ʽ">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="����">
                <HeaderStyle width="7%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="֧�����">
                <ItemStyle horizontalalign="Right" />
                <HeaderStyle width="8%" />
                <ItemTemplate>
                    <%#System.Math.Abs(Convert.ToDecimal(Eval("Money"))).ToString("N2")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="ժҪ">
                <HeaderStyle width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark" HeaderText="��ע/˵��"  />
            <asp:HyperLinkField DataNavigateUrlFields="ItemId" HeaderText="����" Text="�鿴"
                DataNavigateUrlFormatString="~/Admin/User/BankrollItemDetail.aspx?BankrollItemID={0}">
                <headerstyle width="6%" />
            </asp:HyperLinkField>
        </Columns>
        <FooterStyle CssClass="tdbg" />
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order1" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvInvoice" runat="server" EmptyDataText="û����ط�Ʊ��¼" AutoGenerateColumns="False"
        SerialText="" OnRowDataBound="EgvInvoice_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <pe:BoundField DataField="InvoiceDate" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False">
                <HeaderStyle width="10%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="��Ʊ����">
                <HeaderStyle width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="��Ʊ����" SortExpression="InvoiceNum">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("InvoiceNum") %>' NavigateUrl='<%# Eval("InvoiceID", "~/Admin/Shop/InvoiceItemDetail.aspx?InvoiceID={0}") %>' id="LnkInvoiceID"></asp:HyperLink>
                </itemtemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="InvoiceTitle" HeaderText="��Ʊ̧ͷ"  >
                <itemstyle horizontalalign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="TotalMoney" HeaderText="��Ʊ���" DataFormatString="{0:N2}"
                HtmlEncode="False">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Drawer" HeaderText="��Ʊ��">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Inputer" HeaderText="¼��Ա">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="InputTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="¼��ʱ��"
                HtmlEncode="False">
                <HeaderStyle width="17%" />
            </pe:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order2" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvDeliverItem" runat="server" AutoGenerateColumns="False"
        OnRowDataBound="EgvDeliverItem_RowDataBound" SerialText="" EmptyDataText="û����ط��˻���¼"
        OnRowCommand="EgvDeliverItem_RowCommand">
        <Columns>
            <pe:BoundField DataField="DeliverDate" HeaderText="����" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="����/�ͻ��˻�">
                <HeaderStyle Width="12%" />
            </pe:BoundField>
             <pe:TemplateField HeaderText="��ݹ�˾��" HeaderStyle-Width="12%">
               <ItemTemplate>
                  <asp:Literal ID="LitExpressCompony" runat="server"></asp:Literal>
               </ItemTemplate>
             </pe:TemplateField>
             <pe:TemplateField HeaderText="��ݵ���" HeaderStyle-Width="15%">
               <ItemTemplate>
                  <asp:Literal ID="LitExpressNumber" runat="server"></asp:Literal>
               </ItemTemplate>
             </pe:TemplateField>
            <pe:BoundField DataField="HandlerName" HeaderText="������">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Inputer" HeaderText="¼��Ա">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="�ͻ���ǩ��">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark"  HeaderText="��ע/�˻�ԭ��" HeaderStyle-Width="10%" />
            <pe:TemplateField HeaderText="����" Visible="False">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClientClick="return confirm('ȷ���Ѿ��յ��˶����еĻ�������')"
                        CommandName="Received">ǩ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
             <pe:TemplateField HeaderText="�鿴�������" HeaderStyle-Width="10%">
               <ItemTemplate>
                  <pe:ExtendedLiteral HtmlEncode="false" ID="LitExpressState" runat="server"></pe:ExtendedLiteral>
               </ItemTemplate>
             </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order3" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvTransferLog" runat="server" AutoGenerateColumns="False"
        EmptyDataText="û����ع�����¼" SerialText="">
        <Columns>
            <pe:BoundField DataField="TransferTime" HeaderText="ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="OwnerUserName" HeaderText="������">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="TargetUserName" HeaderText="������">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Poundage" HeaderText="������" DataFormatString="{0:N2}" HtmlEncode="False">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="PayerUserName" HeaderText="������">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark" HeaderText="��ע" />
            <pe:BoundField DataField="Inputer" HeaderText="������">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order4" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvService" runat="server" AutoGenerateColumns="False"
        EmptyDataText="û����ط����¼" SerialText="" OnRowDataBound="EgvService_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="ServiceTime" HeaderText="����ʱ��" SortExpression="ServiceTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ͻ�����" SortExpression="ShortedForm">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ShortedForm") %>' NavigateUrl='<%# Eval("ClientID", "~/Admin/Crm/ClientShow.aspx?ClientId={0}") %>' id="LnkClientShow"></asp:HyperLink>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����" SortExpression="ServiceTitle">
                <ItemTemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ServiceTitle") %>' NavigateUrl='<%# Eval("ItemId", "~/Admin/Crm/ServiceShow.aspx?ItemId={0}") %>' id="LnkServiceTitle"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="��������" SortExpression="ServiceType" DataField="ServiceType"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="����ʽ" SortExpression="ServiceMode" DataField="ServiceMode"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="������Ա" SortExpression="Processor" DataField="Processor"
                >
            </asp:BoundField>
            <asp:BoundField HeaderText="������" SortExpression="ServiceResult" DataField="ServiceResult"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ط�ȷ��">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("ConfirmTime") == null ? "" : "<strong>��</strong>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="�ͻ�����" DataField="ConfirmScore" >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order5" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvComplain" runat="server" AutoGenerateColumns="False"
        EmptyDataText="û�����Ͷ�߼�¼" SerialText="" OnRowDataBound="EgvComplain_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="DateAndTime" HeaderText="Ͷ��ʱ��" SortExpression="DateAndTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ͻ�����" SortExpression="ShortedForm">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ShortedForm") %>' NavigateUrl='<%# Eval("ClientID", "~/Admin/Crm/ClientShow.aspx?ClientId={0}") %>' id="LnkClientShow2"></asp:HyperLink>
                </itemtemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ComplainType" HeaderText="Ͷ������" SortExpression="ComplainType"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("ItemId", "~/Admin/Crm/ComplainShow.aspx?ItemId={0}") %>' id="LnkComplainTitle"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MagnitudeOfExigence" HeaderText="�����̶�" SortExpression="MagnitudeOfExigence"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="��¼״̬" SortExpression="Status" >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order6" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvFeedback" runat="server" AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="false" CssClass="TableWrap"
        EmptyDataText="û�з�����¼" SerialText="" OnRowCommand="EgvFeedback_RowCommand">
        <Columns>
            <pe:BoundField DataField="WriteTime" HeaderText="ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </pe:BoundField>  
            <pe:BoundField DataField="Content" ItemStyle-CssClass="TdWrap" ItemStyle-HorizontalAlign="left"  HeaderText="����">
            </pe:BoundField>                        
             <pe:BoundField DataField="ReplyName" HeaderText="����Ա��">
                <HeaderStyle Width="10%" />
            </pe:BoundField>                             
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="14%" />
                <ItemTemplate>
                        <asp:LinkButton ID="LbtnReply" Visible='<%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyContent")))%>' runat="server" Text="�ظ�" CommandArgument='<%#Eval("Id") %>' CommandName="ReplyContent"></asp:LinkButton>
                        <a href='OrderFeedbackModify.aspx?ID=<%#Eval("Id")%><%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyName")))?"":"&Action=ModifyReply"%>'>�޸�</a>   
                        <asp:LinkButton ID="LbtnDelFeedback" runat="server" Text="ɾ��" CommandArgument='<%#Eval("Id") %>' CommandName='<%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyName")))?"Del":"DelReply" %>'></asp:LinkButton>                
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order7" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="GdvPaymentLogList" runat="server" AutoGenerateCheckBoxColumn="False"
        AutoGenerateColumns="False" OnRowDataBound="GdvPaymentLogList_RowDataBound"
        HorizontalAlign="Center" >
        <Columns>
            <pe:TemplateField HeaderText="֧�����">
                <HeaderStyle Width="18%" />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkPaymentNum" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="֧��ƽ̨">
                <ItemTemplate>
                    <asp:Label ID="LblPlatform" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����ʱ��" SortExpression="PayTime">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����" SortExpression="MoneyPay">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyPay", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="ʵ��ת�˽��" SortExpression="MoneyTrue">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyTrue", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����״̬">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LblStatus" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>