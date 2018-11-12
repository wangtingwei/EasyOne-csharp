<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.PictureType" Codebehind="PictureType.ascx.cs" %>
<tr id="Tab" runat="server" class="tdbg">
    <td class="tdbgleft" align="right" style="width: 20%;">
        <strong>
            <%= FieldAlias %>
            ：&nbsp;</strong><br />
    </td>
    <td>
        <table border='0' cellpadding='0' cellspacing='1' width='100%'>
            <tr class='tdbg'>
                <td class="tdbg" align="left" colspan="2">
                    <asp:TextBox ID="TxtImageText" runat="server"></asp:TextBox>
                    <pe:ExtendedLabel ID="LblIsFromSelected" runat="server" HtmlEncode="false" Text=""></pe:ExtendedLabel>
                    <pe:RequiredFieldValidator ID="ReqTxtImageText" runat="server" ControlToValidate="TxtImageText"
                        SetFocusOnError="true" ErrorMessage="必填项不能为空" Display="Dynamic" Visible="false"></pe:RequiredFieldValidator>
                    <span style="color: Green">
                        <%= Description %>
                    </span>
                </td>
            </tr>
            <tr class='tdbg' runat ="server" id ="ShowUploadFiles" style ="display:none;">
                <td class="tdbg" align="left" colspan="2"  >
                    直接从上传图片中选择：&nbsp;
                    <asp:DropDownList ID="DropUploadFiles" runat="server">
                        <asp:ListItem Value="">不指定首页图片</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="HdnUploadFiles" runat="server" />
                </td>
            </tr>
            <pe:ExtendedLabel ID="LblUpload" HtmlEncode="false" runat="server" Text=""></pe:ExtendedLabel>
        </table>
    </td>
</tr>

<script type="text/javascript">

function <%= m_JSPrefix %>DealwithUploadErrMessage(message)
{
    alert(message);
}

function <%= m_JSPrefix %>DealwithUpload(path,size,id)
{
    document.getElementById("<%= TxtImageText.ClientID %>").value = path;
    <%= m_JSPrefix %>AddItem(path);  
    //系统首页图片处理  
    try
    {
        setTimeout("try{setpic('" + path + "')}catch(e){}", 1000);
    }
    catch(e)
    {
    }
}

function <%= m_JSPrefix %>AddItem(strFileName){
    var arrName=strFileName.split('.');
    var FileExt=arrName[1];
    document.getElementById("<%=this.DropUploadFiles.ClientID %>").options[document.getElementById("<%=this.DropUploadFiles.ClientID %>").length]=new Option(strFileName,strFileName);
    document.getElementById("<%=this.DropUploadFiles.ClientID %>").selectedIndex+=1;
      
    if(document.getElementById("<%=this.HdnUploadFiles.ClientID %>").value==''){
        document.getElementById("<%=this.HdnUploadFiles.ClientID %>").value=strFileName;
    }else{
        document.getElementById("<%=this.HdnUploadFiles.ClientID %>").value=document.getElementById("<%=this.HdnUploadFiles.ClientID %>").value+'|'+strFileName;
    }
}

function <%= m_JSPrefix %>SelectFiles()
{
    var urlstr= '<%= m_ShowUploadFilePath %>/Accessories/ShowUploadFiles.aspx';
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    var arr = "";
    if (isMSIE)
    {
        arr= window.showModalDialog(urlstr,'self,width=200,height=150,resizable=yes,scrollbars=yes');
        if(arr!=null)
        {
            document.getElementById("<%= TxtImageText.ClientID %>").value = arr;
        }
    }
    else
    {
        urlstr = urlstr + "?ThumbClientId=<%= TxtImageText.ClientID %>";
        arr= window.open(urlstr,'newWin','modal=yes,width=400,height=300,resizable=yes,scrollbars=yes');
    }
}

function <%= m_JSPrefix %>OnUploadCompleted(errorNumber,fileName)
{
	switch ( errorNumber )
	{
		case 0 :	// No errors
			alert( '您的' + fileName + '文件已经成功上传！' ) ;
			break ;
		case 1 :
			alert( '上传文件类型不对！' ) ;
			return ;
		case 2 :
			alert( "权限错误：你当前的栏目没有开启上传功能，请检查你的栏目。" ) ;
			return ;
		default :
			alert( '上传文件失败，失败的原因是： ' + errorNumber ) ;
			return ;
	}
}
</script>

