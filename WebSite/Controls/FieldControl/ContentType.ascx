<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.ContentType" Codebehind="ContentType.ascx.cs" %>
<tbody id='Tab' runat="server">
    <tr class='tdbg'>
        <td style="width: 20%;" align='right' class='tdbgleft' >
            <strong>�������ݣ�&nbsp;</strong>
            <br />
                <asp:CheckBox ID="ChkSaveRemotePic" runat="server" Checked ="true" /><pe:ExtendedLiteral HtmlEncode="false" ID="LitSaveRemotePic" runat="server">�Զ���������<br />�������ͼƬ<br /> 
            <br />
            <br />
            <span style="color: #006600">&nbsp;&nbsp;&nbsp;&nbsp;���ô˹��ܺ������������վ�ϸ������ݵ��ұߵı༭���У����������а�����ͼƬ����ϵͳ���ڱ�������ʱ�Զ������ͼƬ���Ƶ���վ�������ϡ�<br />
                &nbsp;&nbsp;&nbsp;&nbsp;ϵͳ����������ͼƬ�Ĵ�С��Ӱ���ٶȣ�����ͼƬ�϶�ʱ��Ҫʹ�ô˹��ܡ�</span><br />
            <br />
            </pe:ExtendedLiteral>
            <br /><br />
            <span style="color: Red">�����밴Shift+Enter<br />
                <br />
                ����һ���밴Enter</span>
            <br />
            <script src="../../Editor/Editor/dialog/fck_image/imgPreview.js" type="text/javascript"></script>
            <iframe id='frmPreview' width='120' height='150' scrolling="no"  marginwidth='0' marginheight='0' frameborder='0' src="<%=ResolveClientUrl("~/Editor/Editor/dialog/fck_imgPreview.html") %>"></iframe>
        </td>
        <td>
            <pe:PEeditor ID="EditorContent" runat="server" Width="580px" Height="400px">
            </pe:PEeditor>
            <pe:FckEditorValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent"
                ErrorMessage="���ݲ���Ϊ��" Display="Dynamic" Visible ="false"></pe:FckEditorValidator>
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