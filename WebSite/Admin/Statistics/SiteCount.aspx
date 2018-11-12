<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Statistics.SiteCount"
    Title="无标题页" Codebehind="SiteCount.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:PlaceHolder ID="PlcControl" runat="server"></asp:PlaceHolder>

    <script language="JavaScript" type="text/javascript">
<!--
	function StaticRows()
	{
		var table=document.getElementById("statistics");
		if(table!=null){
		for(var j=1;j<table.rows[0].cells.length;j++)
		{
		    var b=0;
			for(var i=1;i<table.rows.length;i++)   
			{
			   b=b+Number(table.rows[i].cells[j].innerText);
			}
			table.rows[table.rows.length-1].cells[j].innerText=b;
		}
		}
	}
	StaticRows();
//-->
    </script>

</asp:Content>
