<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Analytics.CodeReference"
    Title="无标题页" Codebehind="CodeReference.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider">
    </pe:ExtendedSiteMapPath>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">

    <script type="text/javascript">
    function setFileFileds(num){    
     var str="";
     if (num==1){
         str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.ApplicationPath/}Analytics/CounterLink.aspx?Style=simple'></sc" + "ri" +"pt>";
     }
     else if(num==2){
        str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.ApplicationPath/}Analytics/CounterLink.aspx?Style=common'></sc" + "ri" +"pt>";
     }
     else if(num==3){
        str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.ApplicationPath/}Analytics/CounterLink.aspx?Style=all'></sc" + "ri" +"pt>";
     }
     else if(num==4){
        str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.ApplicationPath/}Analytics/CounterLink.aspx?Style=none'></sc" + "ri" +"pt>";
     }
     document.getElementById("selectKey").value = str;
    
    }
    function setValue()
    {
        setFileFileds(1);
        document.getElementById("LinkContent").value = "<a href='{PE.SiteConfig.ApplicationPath/}Analytics/ShowOnline.aspx' target='_blank'>网站在线情况详细列表</a>";
    }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="spacingtitle">
            <td align="center" colspan="2">
                <strong>统计代码调用</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 30%;" class="tdbgleft">
                <strong>统计计数代码类型：</strong><br />
                [请先选择您想要的输出信息类型]</td>
            <td>
                <select name="select" onchange="setFileFileds(this.value)">
                    <option value="1" selected="selected">显示简单样式信息</option>
                    <option value="2">显示普通样式信息</option>
                    <option value="3">显示复杂样式信息</option>
                    <option value="4">统计但不显示信息</option>
                </select>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>显示数据代码：</strong><br />
                请将此代码拷贝到您需要做统计的页面，此代码不仅用于向放置了此代码的页面输出统计数据，而且还对该页面计数。<br />
            </td>
            <td>
                <textarea name="selectKey" cols="50" rows="5" id="selectKey" onfocus="select()"></textarea>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>前台显示在线链接代码：</strong><br />
                请将此代码拷贝到您需要显示在线列表链接的模板中，此代码仅用于向放置了此代码的页面显示在线列表链接，而不对该页面计数。<br />
            </td>
            <td>
                <textarea name="LinkContent" cols="50" rows="5" id="LinkContent" onfocus="select()"></textarea>

                <script type="text/javascript">setValue();</script>

            </td>
        </tr>
    </table>
</asp:Content>
