<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.QuickLinks"
    Title="无标题页" Codebehind="QuickLinks.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    快捷导航
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guide">
        <ul id="Links">
            <asp:Repeater ID="RptMenu" runat="server">
                <ItemTemplate>
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LitLink" runat="server"></pe:ExtendedLiteral>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

    <script type="text/javascript">
var currentLeftSrc = "";
var currentRightSrc = "";

function OpenLink(FileName_Left,FileName_Right)
{
    if(currentLeftSrc != FileName_Left && FileName_Left != "")
    {
        currentLeftSrc = FileName_Left;
        try
        {
            parent.currentLeftSrc = FileName_Left;
            parent.document.getElementById("left").src = FileName_Left + GetUrlParm(FileName_Left);
        }
        catch(err)
        {}
    }
    if(FileName_Right != "")
    {
        parent.document.getElementById("main_right").src = FileName_Right + GetUrlParm(FileName_Right);
    }
}

function GetUrlParm(url)
{
    var urlparm = "?";
    if(url.indexOf('?')>=0)
    {
        urlparm = "&";
    }
    urlparm = urlparm + "t=" + GetRandomNum();
    return urlparm;
}


function GetRandomNum()
{
        var Range = 1000;
        var Rand = Math.random();
        return (Math.round(Rand * Range));
}
    </script>

</asp:Content>
