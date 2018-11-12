<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.SpecialType" Codebehind="SpecialType.ascx.cs" %>
<tr id='Tab' runat="server">
    <td class='tdbgleft' align='right' style="width: 20%;">
        <div class="DivWordBreak">
            <strong>
                <%= FieldAlias %>
                ：&nbsp;</strong><br />
            <%= Tips %>
        </div>
    </td>
    <td class='tdbg' align='left'>
        <asp:HiddenField ID="HdnSpecial" runat="server" />
        <div style="margin: 0; padding: 0; float: left;" id="UlSpecial" runat="server">
        </div>
        <input type="button" class="button" onclick="AddSpecial()" value="添加到专题" />
    </td>
</tr>

<script type="text/javascript">
<!--
function AddSpecial()
{
    var strUrl = "<%= path %>/Contents/SpecialList.aspx";
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = null;
    if (isMSIE)
    {
        arr= window.showModalDialog(strUrl,self,'width=250,height=400,resizable=yes,scrollbars=yes');
    }
    else
    {
        window.open(strUrl,'newWin','modal=yes,width=250,height=400,resizable=yes,scrollbars=yes'); 
    }
    
    if (arr != null) 
    {
        UpdateSpecial(arr);
    }
}

function UpdateSpecial(arr)
{
        var arrNodes=arr.split('$$$');
        var HdnSpecial= document.getElementById("<%= HdnSpecial.ClientID %>");
        var SelectedSpecial = HdnSpecial.value.split(",");
        var isExist = false;
        for(i=0;i<SelectedSpecial.length;i++)
        {
            if(SelectedSpecial[i] == arrNodes[0])
            {isExist = true;}
        }

        if(!isExist)
        {
            if(HdnSpecial.value != '')
            {HdnSpecial.value = HdnSpecial.value + ',';} 
     
            HdnSpecial.value = HdnSpecial.value + arrNodes[0]; 
      
            var newli = document.createElement("SPAN");  
            newli.setAttribute("id","SpecialSpanId" + arrNodes[0]);
            newli.innerHTML = arrNodes[1] + " ";
            var newlink = document.createElement("INPUT");
            newlink.onclick = function() { DelSpecial(arrNodes[0]);};
            newlink.setAttribute("type", "button");
            newlink.setAttribute("class", "button");
            newlink.setAttribute("value", "从此专题中移除");
            newli.appendChild(newlink);
            var newbr = document.createElement("BR");  
            newli.appendChild(newbr);
            var links = document.getElementById("<%= UlSpecial.ClientID %>");
            links.appendChild(newli);
            DelSpecial('0');
        }
}

function DelSpecial(specialId)
{
     var li = document.getElementById("SpecialSpanId" + specialId);
     if(li != null)
     {
     li.parentNode.removeChild(li);
     }
     var hdnSpecial = document.getElementById("<%= HdnSpecial.ClientID %>");
     var SelectedSpecial = hdnSpecial.value.split(",");
     var newselected = '';
     for(i=0;i<SelectedSpecial.length;i++)
     {
      if(SelectedSpecial[i] != specialId){ if(newselected != ''){newselected = newselected + ',';} newselected = newselected+SelectedSpecial[i]; }
     }
     hdnSpecial.value = newselected;
}
//-->
</script>

