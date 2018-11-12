<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.UserIndividuation" Codebehind="UserIndividuation.ascx.cs" %>
<tr class='tdbg' id="PnlPublicInfo" runat="server">
    <td class='tdbgleft'>
        <strong>发布权限：</strong></td>
    <td>
        <asp:CheckBox ID="ChkPublicInfoNoNeedCheck" runat="server" />发布信息需要审核的栏目，会员发表信息不需要审核
        <br />
        <asp:CheckBox ID="ChkManageSelfPublicInfo" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblManageSelfPublicInfo"
            runat="server" Text="可以修改和删除已审核的（自己的）信息<br />" />
        <span id="SpanSetToNotCheck" runat="server"><asp:CheckBox ID="ChkSetToNotCheck" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblSetToNotCheck" runat="server"
            Text="审核通过的内容在会员修改后自动转为待审状态<br />" /></span>
        <asp:CheckBox ID="ChkSetEditor" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblSetEditor" runat="server"
            Text="发表信息时HTML编辑器为高级模式（默认为简洁模式）<br />" />
        <asp:Label ID="LblMaxPublicInfoOneDay" runat="server" Text="每天最多发布" /><asp:TextBox
            ID="TxtMaxPublicInfoOneDay" MaxLength="5" Columns="5" runat="server" Text ="0"/><pe:ExtendedLabel HtmlEncode="false"
                ID="LblMaxPublicInfoOneDay2" runat="server" Text="条信息（不想限制请设置为<b>0</b>）。<br /> " />
        <asp:Label ID="LblGetExp" runat="server" Text="发布信息时获取积分为栏目设置的" /><asp:TextBox ID="TxtGetExp"
            MaxLength="5" Columns="5" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblGetExp2" runat="server"
                Text="倍<br />" />
        <asp:RangeValidator ID="ValgGetExp" ControlToValidate="TxtGetExp" runat="server"
                        ErrorMessage="范围在0—100之间" Type="Double" Display="Dynamic" MaximumValue="100"
                        MinimumValue="0"></asp:RangeValidator>
        <asp:CheckBox ID="ChkIsXssFilter" runat="server" Checked ="true" />会员发表信息时是否启用XSS（跨站攻击）<br />（如果启用，可能导致会员添加的图片/Flash/视频等不能正常显示，如果禁用，则有跨站攻击漏洞。<br />如果您信任此用户组的用户，可以禁用此功能，否则建议您启用此功能）
    </td>
</tr>
<tr class='tdbg' id="PnlComment" runat="server">
    <td class='tdbgleft'>
        <strong>评论权限：</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableComment" runat="server" />在禁止发表评论的栏目里仍然可发表评论<br />
        <asp:CheckBox ID="ChkCommentNeedCheck" runat="server" />在评论需要审核的栏目里发表评论不需要审核
    </td>
</tr>
<tr class='tdbg' id="PnlMessage" runat="server">
    <td class='tdbgleft'>
        <strong>短消息权限：</strong></td>
    <td>
        每次最多可同时向<asp:TextBox ID="TxtMaxSendToUsers" runat="server" MaxLength="6" Columns="6"
            Text="2000" />人发送短消息（如果为0，则不允许发送短消息）</td>
</tr>
<tr class='tdbg' id="PnlFavorite" runat="server">
    <td class='tdbgleft'>
        <strong>收藏夹权限：</strong></td>
    <td>
        会员收藏夹内最多可收录<asp:TextBox ID="TxtMaxSaveInfos" MaxLength="6" Columns="6" runat="server"
            Text="5000" />
        条信息（如果为0，则没有收藏权限）</td>
</tr>
<tr class='tdbg' id="PnlUpload" runat="server">
    <td class='tdbgleft'>
        <strong>上传权限：</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableUpload" runat="server" />允许在开放上传的模型中上传文件<br />
        最大允许上传<asp:TextBox ID="TxtFileUploadSize" MaxLength="6" Columns="6" runat="server"
            Text="5000" />
        K的文件（当所设置值大于字段的设置时，以字段设置为准。）</td>
</tr>
<tr class='tdbg' id="PnlShop" runat="server">
    <td class='tdbgleft'>
        <strong>商店权限：</strong></td>
    <td>
        <asp:CheckBox ID="ChkSetEnableSale" runat="server" />
        会员中心添加商品时候，可以指定为立即销售<br />  
        购物时可以享受的折扣率：<asp:TextBox ID="TxtDiscount" runat="server" MaxLength="5" Columns="5"
            Text="80" />%<br />
        允许透支的最大额度：<asp:TextBox ID="TxtOverdraft" runat="server" MaxLength="5" Columns="5"
            Text="0" />元人民币
        <br />
        <asp:CheckBox ID="ChkEnablePm" runat="server" />是否可以批发商品<br />
    </td>
</tr>
<tr class='tdbg' id="PnlCharge" runat="server">
    <td class='tdbgleft'>
        <strong>计费方式：</strong></td>
    <td>
        <asp:RadioButton ID="RadChargeByPoint" GroupName="RadCharge" runat="server" Checked ="true" />只判断<pe:ShowPointName ID="ShowPointName" runat="server"></pe:ShowPointName>：有<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>时，即使有效期已经到期，仍可以查看收费内容；<pe:ShowPointName ID="ShowPointName4" runat="server"></pe:ShowPointName>用完后，即使有效期没有到期，也不能查看收费内容。<br />
        <asp:RadioButton ID="RadChargeByValidDate" GroupName="RadCharge" runat="server" />只判断有效期：只要在有效期内，<pe:ShowPointName ID="ShowPointName2" runat="server"></pe:ShowPointName>用完后仍可以查看收费内容；过期后，即使会员有<pe:ShowPointName ID="ShowPointName5" runat="server"></pe:ShowPointName>也不能查看收费内容。<br />
        <asp:RadioButton ID="RadChargeByPointOrValidDate" GroupName="RadCharge" runat="server" />同时判断<pe:ShowPointName ID="ShowPointName3" runat="server"></pe:ShowPointName>和有效期：<pe:ShowPointName ID="ShowPointName6" runat="server"></pe:ShowPointName>用完或有效期到期后，就不可查看收费内容。<br />
        <asp:RadioButton ID="RadChargeByPointAndValidDate" GroupName="RadCharge" runat="server" />同时判断<pe:ShowPointName ID="ShowPointName7" runat="server"></pe:ShowPointName>和有效期：<pe:ShowPointName ID="ShowPointName8" runat="server"></pe:ShowPointName>用完并且有效期到期后，才不能查看收费内容。
    </td>
</tr>
<tr class='tdbg' id="PnlMinusPoint" runat="server">
    <td class='tdbgleft'>
        <strong>扣<pe:ShowPointName ID="ShowPointName9" runat="server"></pe:ShowPointName>方式：</strong></td>
    <td>
        <asp:RadioButton ID="RadNotMinusPointNotWriteToLog" GroupName="RadPoint" runat="server" />有效期内，查看收费内容不扣<pe:ShowPointName ID="ShowPointName10" runat="server"></pe:ShowPointName>，也不做记录。<br />
        <asp:RadioButton ID="RadWriteToLog" GroupName="RadPoint" runat="server" />有效期内，查看收费内容不扣<pe:ShowPointName ID="ShowPointName11" runat="server"></pe:ShowPointName>，但做记录。<br />
        <asp:RadioButton ID="RadMinusPoint" GroupName="RadPoint" runat="server" Checked ="true" />有效期内，查看收费内容也扣<pe:ShowPointName ID="ShowPointName12" runat="server"></pe:ShowPointName>。<br />
        有效期内，总共可以看<asp:TextBox ID="TxtTotalViewInfoNumber" MaxLength="5" Columns="5" runat="server"
            Text="50" />条收费信息（如果为0，则不限制）<br />
        有效期内，每天最多可以看<asp:TextBox ID="TxtViewInfoNumberOneDay" MaxLength="5" Columns="5" runat="server"
            Text="1" />条收费信息（如果为0，则不限制）
    </td>
</tr>
<tr class='tdbg' id="PnlEnableExchange" runat="server">
    <td class='tdbgleft'>
        <strong>自助充值：</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableExchangePoint" runat="server" Checked ="true" />允许自助兑换<pe:ShowPointName ID="ShowPointName13" runat="server"></pe:ShowPointName>
        <asp:CheckBox ID="ChkEnableExchangeValidDate" runat="server" Checked ="true" />允许自助兑换有效期
        <asp:CheckBox ID="ChkEnableGivePointToOthers" runat="server" Checked ="true"  />允许将<pe:ShowPointName ID="ShowPointName14" runat="server"></pe:ShowPointName>赠送给他人
        <asp:CheckBox ID="ChkEnableBuyPoint" runat="server" Checked ="true"  />允许购买<pe:ShowPointName ID="ShowPointName15" runat="server"></pe:ShowPointName>
    </td>
</tr>