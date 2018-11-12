<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.SpecialPermissions"
    Title="专题权限管理" EnableViewState="false" Codebehind="SpecialPermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <base target="_self" />
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr class="tdbg">
            <td align="left" class="tdbg" valign="top" style="width: 100%;">
                <!-- 显示专题类别 -->
                <pe:ExtendedGridView ID="EgvSpecial" DataSourceID="OdsSpecial" GridLines="None" runat="server"
                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="EgvSpecial_RowDataBound"
                    CheckBoxFieldHeaderWidth="3%" SerialText="">
                    <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <pe:BoundField DataField="Id" HeaderText="序号" SortExpression="Id" />
                        <pe:TemplateField HeaderText="专题名" SortExpression="Name">
                            <ItemTemplate>
                                <pe:ExtendedLabel HtmlEncode="false"  runat="server" ID="LabName"></pe:ExtendedLabel>
                                <asp:HiddenField ID="HdnSpecialId" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="添加内容到专题" SortExpression="TreeLineType">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSpecialInput" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="专题内容管理" SortExpression="TreeLineType">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSpecialManage" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </pe:TemplateField>
                    </Columns>
                </pe:ExtendedGridView>
                <asp:ObjectDataSource ID="OdsSpecial" runat="server" SelectMethod="GetSpecialTree"
                    TypeName="EasyOne.Contents.Special"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存返回权限设置" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <input type="button" value="取消返回权限设置" onclick="window.close();" />
    </center>

    <script language="JavaScript" type="text/javascript">
    <!--
      function ChkSpecialAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
            var oSpanArr = document.getElementsByTagName('table');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    if(oSpanArr[i].id == "<%=EgvSpecial.ClientID%>")
                    {
                        //在这里给TD中的控件进行选择处理
                        var inputArr = oSpanArr[i].getElementsByTagName('input');
                        var m = inputArr.length
                        for ( var r=0; r< m; r++ ) 
                        {
                            var t = inputArr[r];
				            if (t.name){
					            if (t.name.indexOf(nodeItem) != -1 && t.disabled==false)
					            {
						            t.checked = false;  
					            }
				            }  
                        }
                    }
                }   
            }
			clientId.checked = true;  
		}
	}
	function ChkWipeOffSpecialAll(clientId){
         clientId.checked = false;
    } 
    
    function ChkSpecialAll2(nodeItem,clientId,nodeManage){
		if (clientId.checked)
		{
            var oSpanArr = document.getElementsByTagName('table');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    var inputArr = oSpanArr[i].getElementsByTagName('input');
                    var m = inputArr.length
                    for ( var r=0; r< m; r++ ) 
                    {
                        var t = inputArr[r];
			            if (t.name){
				            if (t.name.indexOf(nodeItem) != -1 && t.disabled==false)
				            {
					            t.checked = false;  
				            }
			            }  
                    }
                    
                }   
            }
			clientId.checked = true;  
		}
		else
		{
			var s = nodeManage.id.substring(nodeManage.id.lastIndexOf("_")+1,nodeManage.id.length);	
		    if(!nodeManage.checked){
		        var oSpanArr = document.getElementsByTagName('table');
                var j = oSpanArr.length;
                for ( var i=0; i<j; i++ ) 
                {
                    if (oSpanArr[i].id != ""){
                        var inputArr = oSpanArr[i].getElementsByTagName('input');
                        var m = inputArr.length
                        for ( var r=0; r< m; r++ ) 
                        {
                            var t = inputArr[r];
			                if (t.name){
				                if (t.name.indexOf(s) != -1 && t.disabled==false)
				                {
				                    if (t.checked)
						            {
				                       var oCell = document.getElementById(t.id.substring(0,t.id.lastIndexOf("_")+1) + nodeItem);
									   oCell.checked = true;
						            }
						        }
			                }  
                        }
                    }   
                }
            }
			else
			{
				if (nodeManage.checked)
				{
					 clientId.checked = true;
					 alert("专题已经拥有专题内容管理权限，专题内容管理拥有录入权限，若取消此权限请先取消专题内容管理权限！");
					 return;
				}
			}
		}
	}
    function ChkWipeOffSpecialPermissionsAll(fatherclientId,nodeItem,clientId){
	    var oCell = document.getElementById(nodeItem);
		if (!clientId.checked)
	    {
			if (oCell.checked)
			{
				 clientId.checked = true;
				 alert("专题已经拥有专题内容管理权限，专题内容管理拥有录入权限，若取消此权限请先取消专题内容管理权限！");
				 return;
			}
	    }
		var s = oCell.id.substring(oCell.id.lastIndexOf("_")+1,oCell.id.length);	
		var oSpanArr = document.getElementsByTagName('table');
		var j = oSpanArr.length;
		for ( var i=0; i<j; i++ ) 
		{
			if (oSpanArr[i].id != ""){
				var inputArr = oSpanArr[i].getElementsByTagName('input');
				var m = inputArr.length
				for ( var r=0; r< m; r++ ) 
				{
					var t = inputArr[r];
					if (t.name){
						if (t.name.indexOf(s) != -1 && t.disabled==false)
						{
							if (t.checked)
							{
							   var oCell = document.getElementById(t.id.substring(0,t.id.lastIndexOf("_")+1) + clientId.id.substring(oCell.id.lastIndexOf("_")+1,oCell.id.length));
							   oCell.checked = true;
							}
						}
					}  
				}
			}   
		}
        fatherclientId.checked = false;
    } 
    
    function ChkSpecialManageAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
			var currentlyNodeId = clientId.id;
			currentlyNodeId = currentlyNodeId.substring(currentlyNodeId.indexOf("_"), currentlyNodeId.lastIndexOf("_"));
			var oSpanArr = document.getElementsByTagName('table');
			var j = oSpanArr.length;
			for ( var i=0; i<j; i++ ) 
			{
				if (oSpanArr[i].id != ""){
					var inputArr = oSpanArr[i].getElementsByTagName('input');
					var m = inputArr.length
					for ( var r=0; r< m; r++ ) 
					{
						var t = inputArr[r];
						if (t.id){
							if (t.id.indexOf(currentlyNodeId) > 0 && t.disabled==false)
							{
								t.checked = true;  
							}
							else{
								t.checked = false;  
							}
						}
					}
				}   
			}
		}
	}
    function ChkWipeOffSpecialManageAll(clientId,nodeItem,inputSpecialId){
		clientId.checked = false;
		if (nodeItem == "" || nodeItem.indexOf("_") == 0)
		{ 
		   return;
		} 
		var oCell = document.getElementById(nodeItem);
		if (!oCell.checked)
		{
			 return;
		}

		nodeItem = nodeItem.substring(nodeItem.indexOf("_"), nodeItem.lastIndexOf("_"));
		var oSpanArr = document.getElementsByTagName('table');
		var j = oSpanArr.length;
		for ( var i=0; i<j; i++ ) 
		{
			if (oSpanArr[i].id != ""){
				var inputArr = oSpanArr[i].getElementsByTagName('input');
				var m = inputArr.length
				for ( var r=0; r< m; r++ ) 
				{
					var t = inputArr[r];
					if (t.id){
						if (t.id.indexOf(nodeItem) > 0 && t.disabled==false)
						{
						    var n = t.id.substring(t.id.lastIndexOf("_")+1,t.id.length);

						    if ((inputSpecialId.checked && inputSpecialId.id.indexOf(n) > 0))
						    {
				                t.checked = false;  
						    }
						    else
						    {
						        t.checked = true;  
						    }
						}
					}  
				}
			}   
		}
    }
		
  //-->
    </script>

</asp:Content>
