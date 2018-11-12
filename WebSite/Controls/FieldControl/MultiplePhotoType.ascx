<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.MultiplePhotoType" Codebehind="MultiplePhotoType.ascx.cs" %>
<%@ Import Namespace="EasyOne.Common" %>
<tbody id="Tab" runat="server"> 
    <tr class='tdbg'>
        <td class='tdbgleft' align='right' style="width: 20%;">
            <div class="DivWordBreak">
                <strong>
                    <%= FieldAlias %>
                    ��&nbsp;</strong><br />
                <%= Tips %>
            </div>
        </td>
        <td class='tdbg' align='left'>
            <div class="DivWordBreak">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 400px">
                            <asp:HiddenField ID="HdnPhotoUrls" runat="server" />
                            <select id="PhotoUrl" runat="server" style="width: 400px; height: 100px" size="2">
                            </select>
                        </td>
                        <td>
                            <%= IsAdminManage? "<input type=\"button\" class=\"button\" value=\"�����ϴ��ļ���ѡ��\" onclick=\"" + m_JSPrefix + "SelectFiles();\" /><br />":"" %>
                            <input type="button" class="button" value="����ⲿ��ַ" onclick="<%= m_JSPrefix %>AddPhotoUrl('<%= PhotoUrl.ClientID %>');" /><br />
                            <input type="button" class="button" value="�޸ĵ�ǰ��ַ" onclick="return <%= m_JSPrefix %>ModifyPhotoUrl('<%= PhotoUrl.ClientID %>');" /><br />
                            <input type="button" class="button" value="ɾ����ǰ��ַ" onclick="<%= m_JSPrefix %>DelPhotoUrl('<%= PhotoUrl.ClientID %>');" />
                        </td>
                    </tr>
                </table>
                <span style="color: Green">
                    <%= Description %>
                </span>
            </div>
        </td>
    </tr>
    <% if (IsAdminManage || EasyOne.Components.PEContext.Current.User.Identity.IsAuthenticated)
       { %>
    <tr>
        <td class='tdbgleft' align='right' style="width: 20%;">
            <strong>�ϴ��ļ� ��&nbsp;</strong></td>
        <td class='tdbg'>
            <iframe id="Upload" src="<%= m_ShowUploadFilePath %>/Accessories/MultiplePhotoUpload.aspx?ReturnJSFunction=<%= m_JSPrefix %>&ModelId=<%=m_ModelId%>&FieldName=<%=m_FieldName%>"
                marginheight="0" marginwidth="0" frameborder="0" width="100%" height="165" scrolling="no">
            </iframe>
        </td>
    </tr>
    <%} %>
</tbody>

<script type="text/javascript">
<!--

function <%= m_JSPrefix %>ErrMessage(message)
{
    alert(message);
}

function <%= m_JSPrefix %>ChangeThumbField(path,thumbpath)
{
    <%= m_JSPrefix %>DealwithUpload(path,"","",thumbpath)
    try  //��ҳͼƬ
    {
        setTimeout("try{homeImageAssignment('" + thumbpath + "')}catch(e){}", 1000);
    }
    catch(e)
    {
    }
}

function <%= m_JSPrefix %>DealwithUpload(path,size,id,thumbpath)
{
    var obj = document.getElementById("<%= PhotoUrl.ClientID %>");
    var url='ͼƬ��ַ'+(obj.length+1)+'|'+path;
    obj.options[obj.length]=new Option(url,url);
    <%= m_JSPrefix %>ChangeHiddenFieldValue();
}

function <%= m_JSPrefix %>ChangeHiddenFieldValue()
{
    var obj = document.getElementById("<%= HdnPhotoUrls.ClientID %>");
    var photoUrls = document.getElementById("<%= PhotoUrl.ClientID %>");
    var value = "";
    for(i=0;i<photoUrls.length;i++)
    {
        if(value!="")
        {
            value = value+ "$$$";
        }
        value = value + photoUrls.options[i].value;
    }
    obj.value = value;
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
            var obj = document.getElementById("<%= PhotoUrl.ClientID %>");
            var url='ͼƬ��ַ'+(obj.length+1)+'|'+arr;
            obj.options[obj.length]=new Option(url,url);
            <%= m_JSPrefix %>ChangeHiddenFieldValue();
        }
    }
    else
    {
        urlstr = urlstr + "?ClientId=<%= PhotoUrl.ClientID %>&HiddenFieldId=<%= HdnPhotoUrls.ClientID %>";
        window.open(urlstr,'newWin','modal=yes,width=400,height=300,resizable=yes,scrollbars=yes');
    }
}
//-->
</script>

