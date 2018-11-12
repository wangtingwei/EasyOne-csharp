<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableViewState="false" Inherits="EasyOne.WebSite.Admin.User.RolePermissionsManage"
    Title="角色权限设置" Codebehind="RolePermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>角色权限设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px;">
                <strong>角色名：</strong>
            </td>
            <td>
                <asp:Label ID="LblRoleName" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="height: 79px">
                <strong>角色描述：</strong>
            </td>
            <td style="height: 79px">
                <asp:Label ID="LblDescription" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>常规模块权限设置 </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="left" class="tdbg" colspan="2" valign="top" style="padding-left: 2px;">
                <fieldset id="ModelPurview" style="border: 1px solid #FFFFFF;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="1">
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblModelPurview" Text="" runat="server" />
                    </pe:ExtendedLabel>
                </fieldset>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存角色权限设置" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="BtnCancle" runat="server" Text="返回角色管理" ValidationGroup="BtnCancleValidationGroup"
            OnClick="BtnCancle_Click" />
    </center>

    <script language="javascript" id="check" type="text/javascript"> 
    
    function ShowWindow(type){
        var strUrl = "";
        
        switch (type)
        {
            case 1:
                strUrl = "NodePermissions.aspx?PermissionsType=Role&Type=Content&RoleId=<%=Request["RoleId"]%>";
                break ;   
            case 2:
                strUrl = "SpecialPermissions.aspx?PermissionsType=Role&RoleId=<%=Request["RoleId"]%>";
                break ;    
            case 3:
                strUrl = "NodePermissions.aspx?PermissionsType=Role&Type=Comment&RoleId=<%=Request["RoleId"]%>";
                break ;
            case 4:
                strUrl = "NodePermissions.aspx?PermissionsType=Role&Type=Node&RoleId=<%=Request["RoleId"]%>";
                break ;
            default:
                break ;
        }
        
        var arr= window.open(strUrl,'newWin','modal=yes,width=700px,height=400px,resizable=yes,scrollbars=yes'); 

        if (arr != null) {

        }
    }
        
    function CheckModel(obj){
    
	    var oSpanArr = document.getElementsByTagName('fieldset');
	    var j = oSpanArr.length;
		
	    //更改所有所有子节点的状态
	    for ( var i=0; i<j; i++ ) 
	    {    
		    if (oSpanArr[i].id != ""){       
			    var inputArr = oSpanArr[i].getElementsByTagName('input');
			    var m = inputArr.length
			    for ( var r=0; r< m; r++ ) 
			    {
				    var t = inputArr[r];
				    if (t.id){
				       if (t.id.substr(0,obj.id.length+1) == obj.id + '_')
				       t.checked = obj.checked;
				    }  
			    }
		    }
	    }
		    
		//开始更改父节点的状态
		if(obj.checked==true){
			CheckParentModel(obj.id);
		}
		else{
			ChangeParentModel(obj.id);
		}
	}
	
	function CheckParentModel(objID){
		if(objID.indexOf("_") > 0)
		{
		    var parentid=objID.substr(0,objID.lastIndexOf("_"));
		    document.getElementById(parentid).checked=true;
		    CheckParentModel(parentid);
		}
	}
	function ChangeParentModel(objID){
		if(objID.indexOf("_") > 0)
		{
            var oSpanArr = document.getElementsByTagName('fieldset');
		    var j = oSpanArr.length;
		    var parentid=objID.substr(0,objID.lastIndexOf("_"));
		    document.getElementById(parentid).checked = false;

		    for ( var i=0; i<j; i++ ) 
		    {    
			    if (oSpanArr[i].id != ""){       
				    var inputArr = oSpanArr[i].getElementsByTagName('input');
				    var m = inputArr.length
				    
				    for ( var r=0; r< m; r++ ) 
				    {
					    var t = inputArr[r];
					    if (t.id){
					       if (t.id.substr(0,parentid.length+1) == parentid + '_'){
						       if(t.checked == true){
							       document.getElementById(parentid).checked = true;
							       break;
						       }
						    }
					    }  
				    }
			    }
		    }
		    ChangeParentModel(parentid);
		}
	}
    </script>

</asp:Content>
