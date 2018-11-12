<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.FileType" Codebehind="FileType.ascx.cs" %>
<tbody id="Tab" runat="server">
    <tr class='tdbg'>
        <td class='tdbgleft' align='right' style="width: 20%;">
            <div class="DivWordBreak">
                <strong>
                    <%= FieldAlias %>
                    ：&nbsp;</strong><br />
                <%= Tips %>
            </div>
        </td>
        <td class='tdbg' align='left'>
            <div class="DivWordBreak">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 400px">
                            <asp:HiddenField ID="HdnSoftUrls" runat="server" />
                            <select id="SoftUrl" runat="server" style="width: 400px; height: 100px" size="2">
                            </select>
                        </td>
                        <td>
                            <%= IsAdminManage? "<input type=\"button\" class=\"button\" value=\"从已上传文件中选择\" onclick=\"" + m_JSPrefix + "SelectFiles();\" /><br />":"" %>
                            <input type="button" class="button" value="添加外部地址" onclick="<%= m_JSPrefix %>AddSoftUrl('<%= SoftUrl.ClientID %>');" /><br />
                            <input type="button" class="button" value="修改当前地址" onclick="return <%= m_JSPrefix %>ModifySoftUrl('<%= SoftUrl.ClientID %>');" /><br />
                            <input type="button" class="button" value="删除当前地址" onclick="<%= m_JSPrefix %>DelSoftUrl('<%= SoftUrl.ClientID %>');" />
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
            <strong>上传文件 ：&nbsp;</strong></td>
        <td class='tdbg'>
            <iframe id="Upload" src='<%= m_ShowUploadFilePath %>/Accessories/FileUpload.aspx?ModuleName=Node&ReturnJSFunction=<%= m_JSPrefix %>DealwithUpload&ModelId=<%= RequestString("ModelId") %>&FieldName=<%= FieldName %>&NodeId=<%= RequestString("NodeId") %>'
                marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no">
            </iframe>
        </td>
    </tr>
    <%} %>
    <tr>
        <td class='tdbgleft' align='right' style="width: 20%;">
            <strong>文件大小 ：&nbsp;</strong></td>
        <td class='tdbg'>
            <asp:TextBox ID="TxtFileSize" runat="server"></asp:TextBox>&nbsp;K</td>
    </tr>
</tbody>

<script type="text/javascript">
function <%= m_JSPrefix %>DealwithUpload(path,size,id)
{
    var obj = document.getElementById("<%= SoftUrl.ClientID %>");
    var url='下载地址'+(obj.length+1)+'|'+path;
    obj.options[obj.length]=new Option(url,url);
    <%= m_JSPrefix %>ChangeHiddenFieldValue();
    document.getElementById("<%= TxtFileSize.ClientID %>").value = tofloat((size / 1024),2);
}

function   tofloat(f,dec)   {     
  if(dec<0)   return   "Error:dec<0!";     
    result=parseInt(f)+(dec==0?"":".");     
    f-=parseInt(f);     
  if(f==0)     
    for(i=0;i<dec;i++)   result+='0';     
  else   {     
    for(i=0;i<dec;i++)   f*=10;     
    result+=parseInt(Math.round(f));     
  }     
  return result;     
}     
  

function <%= m_JSPrefix %>ChangeHiddenFieldValue()
{
    var obj = document.getElementById("<%= HdnSoftUrls.ClientID %>");
    var softUrls = document.getElementById("<%= SoftUrl.ClientID %>");
    var value = "";
    for(i=0;i<softUrls.length;i++)
    {
        if(value!="")
        {
            value = value+ "$$$";
        }
        value = value + softUrls.options[i].value;
    }
    obj.value = value;
}

function <%= m_JSPrefix %>DealwithUploadErrMessage(message)
{
    alert(message);
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
            var obj = document.getElementById("<%= SoftUrl.ClientID %>");
            var url='下载地址'+(obj.length+1)+'|'+arr;
            obj.options[obj.length]=new Option(url,url);
            <%= m_JSPrefix %>ChangeHiddenFieldValue();
        }
    }
    else
    {
        urlstr = urlstr + "?ClientId=<%= SoftUrl.ClientID %>&HiddenFieldId=<%= HdnSoftUrls.ClientID %>&Type=Soft";
        window.open(urlstr,'newWin','modal=yes,width=400,height=300,resizable=yes,scrollbars=yes');
    }
}
</script>

