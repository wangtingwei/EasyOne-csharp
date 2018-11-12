<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Profile.QuickLinksSort" Codebehind="QuickLinksSort.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    导航预览、排序
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <table>
        <tr>
            <td align="center">
                <span style="color: Green;">注：上下拖动链接进行排序</span>
            </td>
        </tr>
    </table>
    <div class="guide">
        <ul id="Links">
            <asp:Repeater ID="RptMenu" runat="server">
                <ItemTemplate>
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LitLink" runat="server"></pe:ExtendedLiteral>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <style type="text/css">
div.AlertStyle
    {
      background-color: #FFC080;
      top:90%;
      left: 1%;
      height: 20px;
      width: 190px;
      position: absolute;
      visibility: hidden;
    }
.guide li
{
cursor:move;
}
</style>
    <asp:ScriptManager ID="SmQuickLinksConfig" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WebServices/QuickLinksConfig.asmx" />
        </Services>
    </asp:ScriptManager>

    <script src="../../js/lib/prototype.js" type="text/javascript"></script>

    <script src="../../js/lib/scriptaculous.js" type="text/javascript"></script>

    <script type="text/javascript">
    Sortable.create('Links',{onUpdate:function(sortable){UpdateSortable(SerializeToString(Sortable.serialize(sortable),"Links"));}});

    function ActivateAlertDiv(visstring, elem, msg)
    {
        var adiv = $get(elem);
        adiv.style.visibility = visstring;
        adiv.innerHTML = msg;                     
    }
    
    function UpdateSortable(sortable)
    {
        ActivateAlertDiv('visible', 'AlertDiv',  '正在加载...');
        EasyOne.WebSite.Admin.Profile.QuickLinksConfig.UpdateLinkSort(sortable,onUpdateSortableCompleted);
    }
    
    function onUpdateSortableCompleted(value)
    {
        ActivateAlertDiv('hidden', 'AlertDiv', '');
    }

    function AddLink(id,lefturl,righturl,text)
    {
        var links = document.getElementById("Links");
        var newli = document.createElement("LI");
        newli.setAttribute("id","Links_"+id);
        var newlink = document.createElement("A");
        url = "javascript:OpenLink('"+lefturl+"','"+righturl+"');";
        newlink.setAttribute("href",url);
        var text = document.createTextNode(text);
        newlink.appendChild(text);
        newli.appendChild(newlink);
        links.appendChild(newli);
        Sortable.create('Links',{onUpdate:function(sortable){UpdateSortable(SerializeToString(Sortable.serialize(sortable),"Links"));}});
    }
    
    function DeleteLink(id)
    {
        var links = document.getElementById("Links");
        var li = document.getElementById("Links_"+id);
        links.removeChild(li);
    }
    
function OpenLink(FileName_Left,FileName_Right)
{
}

function SerializeToString(para,key)
{
  var keys = key + "[[]]=";
  var pattern = new RegExp(keys,"g");
  var paras = para.replace(pattern,"");
  pattern = new RegExp("\&","g");
  paras = paras.replace(pattern,",");
  return paras;
}
    </script>

    <div id="AlertDiv" class="AlertStyle">
    </div>
</asp:Content>
