<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ChargeTypeSetting" Codebehind="ChargeTypeSetting.ascx.cs" %>

 <asp:RadioButton ID="RadChargeType0" GroupName="ChargeType" runat="server" Checked="true" Text="不重复收费" /><br/>
 <asp:RadioButton ID="RadChargeType1" GroupName="ChargeType" runat="server" Checked="false" Text="距离上次收费时间" />
 <asp:TextBox ID="TxtPitchTime" runat="server"  Width="60px"></asp:TextBox>小时后重新收费<br/>
 <asp:RadioButton ID="RadChargeType2" GroupName="ChargeType" runat="server" Checked="false" Text="会员重复阅读此文章" />  
 <asp:TextBox ID="TxtReadTimes" runat="server"  Width="60px"></asp:TextBox>次后重新收费<br/> 
 <asp:RadioButton ID="RadChargeType3" GroupName="ChargeType" runat="server" Checked="false" Text="上述两者都满足时重新收费" /><br/>
 <asp:RadioButton ID="RadChargeType4" GroupName="ChargeType" runat="server" Checked="false" Text="上述两者任一个满足时就重新收费" /><br/>
 <asp:RadioButton ID="RadChargeType5" GroupName="ChargeType" runat="server" Checked="false" Text="每阅读一次就重复收费一次（建议不要使用）" /><br/>   
