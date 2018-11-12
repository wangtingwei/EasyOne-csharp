<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ScoreControl" Codebehind="ScoreControl.ascx.cs" %>

<script language="JavaScript" type="text/javascript">
<!--
	function ChangeStar(index, isfixed){
         var colStars = document.getElementById("divStars").getElementsByTagName("input");
         var i = 0;
         var k = isfixed? parseInt(document.getElementById("<%=hdnScore.ClientID%>").value) : index;

         for(i=0; i<colStars.length; i++){
                 colStars[i].src = (i<k? "<%=m_ShowPath%>Images/fstar.gif" : "<%=m_ShowPath%>Images/estar.gif");
         }
	}

	function StarClick(index)
	{
			 document.getElementById("<%=hdnScore.ClientID%>").value=index;
	}

	function StarMouseOver(index){
			 ChangeStar(index,false);
	}

	function StarMouseOut(){
			 ChangeStar(0,true);
	}
//-->
</script>

<!-- ¶¥²¿ -->
<div id="divStars">
    <input onmouseover="StarMouseOver(1)" onclick="StarClick(1);return false;" onmouseout="StarMouseOut()" type="image" src="../Images/estar.gif" id="imageField1" runat ="server"/>
    <input onmouseover="StarMouseOver(2)" onclick="StarClick(2);return false;" onmouseout="StarMouseOut()" type="image" src="../Images/estar.gif" id="imageField2"  runat ="server" />
    <input onmouseover="StarMouseOver(3)" onclick="StarClick(3);return false;"  onmouseout="StarMouseOut()" type="image" src="../Images/estar.gif" id="imageField3"  runat ="server" />
    <input onmouseover="StarMouseOver(4)" onclick="StarClick(4);return false;" onmouseout="StarMouseOut()"  type="image" src="../Images/estar.gif" id="imageField4"  runat ="server" />
    <input onmouseover="StarMouseOver(5)"  onclick="StarClick(5);return false;" onmouseout="StarMouseOut()" type="image" src="../Images/estar.gif" id="imageField5"  runat ="server" />
</div>
<asp:HiddenField ID="hdnScore" runat="server" />

