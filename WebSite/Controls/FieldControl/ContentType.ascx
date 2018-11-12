<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.ContentType" Codebehind="ContentType.ascx.cs" %>
<tbody id='Tab' runat="server">
    <tr class='tdbg'>
        <td style="width: 20%;" align='right' class='tdbgleft' >
            <strong>文章内容：&nbsp;</strong>
            <br />
                <asp:CheckBox ID="ChkSaveRemotePic" runat="server" Checked ="true" /><pe:ExtendedLiteral HtmlEncode="false" ID="LitSaveRemotePic" runat="server">自动下载文章<br />内容里的图片<br /> 
            <br />
            <br />
            <span style="color: #006600">&nbsp;&nbsp;&nbsp;&nbsp;启用此功能后，如果从其它网站上复制内容到右边的编辑器中，并且内容中包含有图片，本系统会在保存文章时自动把相关图片复制到本站服务器上。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;系统会因所下载图片的大小而影响速度，建议图片较多时不要使用此功能。</span><br />
            <br />
            </pe:ExtendedLiteral>
            <br /><br />
            <span style="color: Red">换行请按Shift+Enter<br />
                <br />
                另起一段请按Enter</span>
            <br />
            <script src="../../Editor/Editor/dialog/fck_image/imgPreview.js" type="text/javascript"></script>
            <iframe id='frmPreview' width='120' height='150' scrolling="no"  marginwidth='0' marginheight='0' frameborder='0' src="<%=ResolveClientUrl("~/Editor/Editor/dialog/fck_imgPreview.html") %>"></iframe>
        </td>
        <td>
            <pe:PEeditor ID="EditorContent" runat="server" Width="580px" Height="400px">
            </pe:PEeditor>
            <pe:FckEditorValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent"
                ErrorMessage="内容不能为空" Display="Dynamic" Visible ="false"></pe:FckEditorValidator>
        </td>
    </tr>
</tbody>

<script language="JavaScript" type="text/javascript">
<!--
function AddItem(strFileName){    
    try
    {
        setTimeout("try{homeImageAssignment('" + strFileName + "')}catch(e){}", 1000);
    }
    catch(e)
    {
    }
}
function setpic(strFileName){
    var applicationPath = "<%=Request.ApplicationPath%>";
    var uploadDir = "<%=m_UploadDir%>"
    var imgpath = "";
    
    if (strFileName.length > 7 && strFileName.substring(0,7).toLowerCase() == "http://") {
        imgpath = strFileName;
    }
    else
    {   
        if (applicationPath != "/"){
            imgpath = applicationPath + "/" +  uploadDir;
        }else{
            imgpath = "/" +  uploadDir;
        }
        imgpath += strFileName;
    }
    setTimeout("imgPreview('" + imgpath + "')", 1000);
}
function SetUrl(url){
   setTimeout("imgPreview('" + url + "')", 1000);
   var strFilter = "<%=Request.ApplicationPath%>/<%=m_UploadDir%>";
   strFilter = strFilter.replace("//","/");
   var iurl = url.replace(strFilter,"");
   AddItem(iurl);
}	
function imgPreview(url) {
    try{eImgPreview.src = url;}catch(e){setTimeout("imgPreview(' "+ url + "')",2000);}
}
//-->
</script>