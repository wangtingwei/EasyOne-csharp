<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="True"
    Inherits="EasyOne.WebSite.Admin.Accessories.SiteConfigGuide"
    Title="网站配置向导" Codebehind="SiteConfigGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    网站配置
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guideexpand" onclick="Switch(this)">
        管理操作</div>
    <div class="guide">
        <pe:ExtendedLiteral HtmlEncode="false" ID="LitSiteConfig" runat="server">
        <ul>
            <li><a href="../Configuration/SiteInfo.aspx" target="main_right">网站信息配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/SiteOption.aspx" target="main_right">网站参数配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/UserConfig.aspx" target="main_right">用户参数配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/MailConfig.aspx" target="main_right">邮件参数配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/ThumbConfig.aspx" target="main_right">缩略图参数配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/IPLockConfig.aspx" target="main_right">IP访问限定配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/SmsConfig.aspx" target="main_right">手机短信配置</a></li>
        </ul>
        <ul>
            <li><a href="../Configuration/RssConfig.aspx" target="main_right">RSS/WAP配置</a></li>
        </ul>
        </pe:ExtendedLiteral>
        <pe:ExtendedLiteral HtmlEncode="false" ID="LitFrontPageTemplateConfig" runat="server">
        <ul>
            <li><a href="../Configuration/FrontPageTemplateConfig.aspx" target="main_right">动态页模板配置</a></li>
        </ul>
        </pe:ExtendedLiteral>
        <pe:ExtendedLiteral HtmlEncode="false" ID="LtrShop" runat="server">
        <ul>
            <li><a href="../Shop/ShopConfig.aspx" target="main_right">商店参数配置</a></li>
        </ul>
        <ul>
            <li><a href="../Shop/TemplateConfig.aspx" target="main_right">商店模板配置</a></li>
        </ul>        
        </pe:ExtendedLiteral>
    </div>
</asp:Content>
