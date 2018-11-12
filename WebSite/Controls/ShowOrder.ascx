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
            付款信息</td>
        <td id='TabOrderTitle1' class='tabtitle' onclick='ShowOrderTabs(1)'>
            发票记录</td>
        <td id='TabOrderTitle2' class='tabtitle' onclick='ShowOrderTabs(2)'>
            发退货记录</td>
        <td id='TabOrderTitle3' class='tabtitle' <%=IsShow()%> onclick='ShowOrderTabs(3)'>
            过户记录</td>
        <td id='TabOrderTitle4' class='tabtitle' onclick='ShowOrderTabs(4)'>
            服务记录</td>
        <td id='TabOrderTitle5' class='tabtitle' onclick='ShowOrderTabs(5)'>
            投诉记录</td>
        <td id='TabOrderTitle6' class='tabtitle' onclick='ShowOrderTabs(6)'>
            反馈记录</td>
        <td id='TabOrderTitle7' class='tabtitle' onclick='ShowOrderTabs(7)'>在线支付记录</td>        
        <td>&nbsp;</td>
    </tr>
</table>
<div id="Tabs_Order0" class="border" style="width: 99%; display: block;">
    <pe:ExtendedGridView ID="EgvBankroll" runat="server" SerialText="" AutoGenerateColumns="False"
        EmptyDataText="没有相关付款记录" OnDataBound="EgvBankroll_DataBound" OnRowDataBound="EgvBankroll_RowDataBound"
        ShowFooter="True" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataTextField="ClientName" HeaderText="客户名称"
                DataNavigateUrlFormatString="~/Admin/Crm/ClientShow.aspx?ClientId={0}">
                <headerstyle width="10%" />
            </asp:HyperLinkField>
            <pe:TemplateField HeaderText="用户名">
                <headerstyle width="8%" />
                <itemtemplate>
                <asp:HyperLink id="LnkUserName" runat="server" NavigateUrl='<%# Eval("UserName", "~/Admin/User/UserShow.aspx?UserName={0}") %>' Text='<%# Eval("UserName") %>' __designer:wfdid="w9"></asp:HyperLink> 
                </itemtemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="DateAndTime" HeaderText="交易时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False">
                <HeaderStyle width="17%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="交易方式">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="币种">
                <HeaderStyle width="7%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="支出金额">
                <ItemStyle horizontalalign="Right" />
                <HeaderStyle width="8%" />
                <ItemTemplate>
                    <%#System.Math.Abs(Convert.ToDecimal(Eval("Money"))).ToString("N2")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="摘要">
                <HeaderStyle width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark" HeaderText="备注/说明"  />
            <asp:HyperLinkField DataNavigateUrlFields="ItemId" HeaderText="操作" Text="查看"
                DataNavigateUrlFormatString="~/Admin/User/BankrollItemDetail.aspx?BankrollItemID={0}">
                <headerstyle width="6%" />
            </asp:HyperLinkField>
        </Columns>
        <FooterStyle CssClass="tdbg" />
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order1" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvInvoice" runat="server" EmptyDataText="没有相关发票记录" AutoGenerateColumns="False"
        SerialText="" OnRowDataBound="EgvInvoice_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <pe:BoundField DataField="InvoiceDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False">
                <HeaderStyle width="10%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="发票类型">
                <HeaderStyle width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="发票号码" SortExpression="InvoiceNum">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("InvoiceNum") %>' NavigateUrl='<%# Eval("InvoiceID", "~/Admin/Shop/InvoiceItemDetail.aspx?InvoiceID={0}") %>' id="LnkInvoiceID"></asp:HyperLink>
                </itemtemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="InvoiceTitle" HeaderText="发票抬头"  >
                <itemstyle horizontalalign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="TotalMoney" HeaderText="发票金额" DataFormatString="{0:N2}"
                HtmlEncode="False">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Drawer" HeaderText="开票人">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Inputer" HeaderText="录入员">
                <HeaderStyle width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="InputTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="录入时间"
                HtmlEncode="False">
                <HeaderStyle width="17%" />
            </pe:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order2" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvDeliverItem" runat="server" AutoGenerateColumns="False"
        OnRowDataBound="EgvDeliverItem_RowDataBound" SerialText="" EmptyDataText="没有相关发退货记录"
        OnRowCommand="EgvDeliverItem_RowCommand">
        <Columns>
            <pe:BoundField DataField="DeliverDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="发货/客户退货">
                <HeaderStyle Width="12%" />
            </pe:BoundField>
             <pe:TemplateField HeaderText="快递公司名" HeaderStyle-Width="12%">
               <ItemTemplate>
                  <asp:Literal ID="LitExpressCompony" runat="server"></asp:Literal>
               </ItemTemplate>
             </pe:TemplateField>
             <pe:TemplateField HeaderText="快递单号" HeaderStyle-Width="15%">
               <ItemTemplate>
                  <asp:Literal ID="LitExpressNumber" runat="server"></asp:Literal>
               </ItemTemplate>
             </pe:TemplateField>
            <pe:BoundField DataField="HandlerName" HeaderText="经手人">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Inputer" HeaderText="录入员">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField HeaderText="客户已签收">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark"  HeaderText="备注/退货原因" HeaderStyle-Width="10%" />
            <pe:TemplateField HeaderText="操作" Visible="False">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClientClick="return confirm('确定已经收到此订单中的货物了吗？')"
                        CommandName="Received">签收</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
             <pe:TemplateField HeaderText="查看物流情况" HeaderStyle-Width="10%">
               <ItemTemplate>
                  <pe:ExtendedLiteral HtmlEncode="false" ID="LitExpressState" runat="server"></pe:ExtendedLiteral>
               </ItemTemplate>
             </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order3" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvTransferLog" runat="server" AutoGenerateColumns="False"
        EmptyDataText="没有相关过户记录" SerialText="">
        <Columns>
            <pe:BoundField DataField="TransferTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="OwnerUserName" HeaderText="过户人">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="TargetUserName" HeaderText="过户给">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Poundage" HeaderText="过户费" DataFormatString="{0:N2}" HtmlEncode="False">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="PayerUserName" HeaderText="付款人">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Remark" HeaderText="备注" />
            <pe:BoundField DataField="Inputer" HeaderText="经手人">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order4" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvService" runat="server" AutoGenerateColumns="False"
        EmptyDataText="没有相关服务记录" SerialText="" OnRowDataBound="EgvService_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="ServiceTime" HeaderText="服务时间" SortExpression="ServiceTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="客户名称" SortExpression="ShortedForm">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ShortedForm") %>' NavigateUrl='<%# Eval("ClientID", "~/Admin/Crm/ClientShow.aspx?ClientId={0}") %>' id="LnkClientShow"></asp:HyperLink>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="主题" SortExpression="ServiceTitle">
                <ItemTemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ServiceTitle") %>' NavigateUrl='<%# Eval("ItemId", "~/Admin/Crm/ServiceShow.aspx?ItemId={0}") %>' id="LnkServiceTitle"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="服务类型" SortExpression="ServiceType" DataField="ServiceType"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="服务方式" SortExpression="ServiceMode" DataField="ServiceMode"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="服务人员" SortExpression="Processor" DataField="Processor"
                >
            </asp:BoundField>
            <asp:BoundField HeaderText="服务结果" SortExpression="ServiceResult" DataField="ServiceResult"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="回访确认">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#Eval("ConfirmTime") == null ? "" : "<strong>√</strong>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="客户评价" DataField="ConfirmScore" >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order5" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvComplain" runat="server" AutoGenerateColumns="False"
        EmptyDataText="没有相关投诉记录" SerialText="" OnRowDataBound="EgvComplain_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="DateAndTime" HeaderText="投诉时间" SortExpression="DateAndTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="客户名称" SortExpression="ShortedForm">
                <headerstyle width="10%" />
                <itemtemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("ShortedForm") %>' NavigateUrl='<%# Eval("ClientID", "~/Admin/Crm/ClientShow.aspx?ClientId={0}") %>' id="LnkClientShow2"></asp:HyperLink>
                </itemtemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ComplainType" HeaderText="投诉类型" SortExpression="ComplainType"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="主题">
                <ItemTemplate>
                <asp:HyperLink runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("ItemId", "~/Admin/Crm/ComplainShow.aspx?ItemId={0}") %>' id="LnkComplainTitle"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MagnitudeOfExigence" HeaderText="紧急程度" SortExpression="MagnitudeOfExigence"
                >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="记录状态" SortExpression="Status" >
                <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    </pe:ExtendedGridView>
</div>
<div id="Tabs_Order6" class="border" style="width: 99%; display: none;">
    <pe:ExtendedGridView ID="EgvFeedback" runat="server" AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="false" CssClass="TableWrap"
        EmptyDataText="没有反馈记录" SerialText="" OnRowCommand="EgvFeedback_RowCommand">
        <Columns>
            <pe:BoundField DataField="WriteTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="15%" />
            </pe:BoundField>  
            <pe:BoundField DataField="Content" ItemStyle-CssClass="TdWrap" ItemStyle-HorizontalAlign="left"  HeaderText="内容">
            </pe:BoundField>                        
             <pe:BoundField DataField="ReplyName" HeaderText="管理员名">
                <HeaderStyle Width="10%" />
            </pe:BoundField>                             
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="14%" />
                <ItemTemplate>
                        <asp:LinkButton ID="LbtnReply" Visible='<%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyContent")))%>' runat="server" Text="回复" CommandArgument='<%#Eval("Id") %>' CommandName="ReplyContent"></asp:LinkButton>
                        <a href='OrderFeedbackModify.aspx?ID=<%#Eval("Id")%><%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyName")))?"":"&Action=ModifyReply"%>'>修改</a>   
                        <asp:LinkButton ID="LbtnDelFeedback" runat="server" Text="删除" CommandArgument='<%#Eval("Id") %>' CommandName='<%#string.IsNullOrEmpty(Convert.ToString(Eval("ReplyName")))?"Del":"DelReply" %>'></asp:LinkButton>                
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
            <pe:TemplateField HeaderText="支付序号">
                <HeaderStyle Width="18%" />
                <ItemTemplate>
                    <asp:HyperLink ID="LnkPaymentNum" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="支付平台">
                <ItemTemplate>
                    <asp:Label ID="LblPlatform" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="交易时间" SortExpression="PayTime">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="汇款金额" SortExpression="MoneyPay">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyPay", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="实际转账金额" SortExpression="MoneyTrue">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyTrue", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="交易状态">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LblStatus" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>