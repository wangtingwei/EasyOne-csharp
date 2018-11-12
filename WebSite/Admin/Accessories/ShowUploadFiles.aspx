<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.ShowUploadFiles" Codebehind="ShowUploadFiles.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择文件</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">

        <script src="<%=BasePath %>Admin/JS/Popup.js" language="javascript" type="text/javascript"></script>

        <table width="100%" border="0" cellpadding="2" cellspacing="1">
            <tr class="tdbg">
                <td>
                    <a href="ShowUploadFiles.aspx?Dir=<%=Server.UrlEncode(Request["Dir"]) + (RequestString("ShowType") != "0"?"&ShowType=0":"")%>&ClientId=<%=RequestString("ClientId")%>&ThumbClientId=<%=RequestString("ThumbClientId")%>&HiddenFieldId=<%=RequestString("HiddenFieldId")%>">
                        <%=RequestString("ShowType") != "0" ? "切换到缩略图方式" : "切换到列表方式"%>
                    </a>
                </td>
                <td style="width: 350px" align="right">
                    &nbsp; 搜索当前目录文件：
                    <asp:TextBox ID="TxtSearchKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="搜索" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td>
                    当前目录：<asp:Label ID="LblCurrentDir" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <a href="ShowUploadFiles.aspx?Dir=<%= Server.UrlEncode(m_ParentDir) + RequestString("ShowType") != "0"?"":"&ShowType=0" %>&ClientId=<%=RequestString("ClientId")%>&ThumbClientId=<%=RequestString("ThumbClientId")%>&HiddenFieldId=<%=RequestString("HiddenFieldId")%>">
                        返回上一级</a></td>
            </tr> 
        </table>
        <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
            <HeaderTemplate>
                <table width="100%" cellpadding="0" cellspacing="1" border="0" class="border">
                    <%if (RequestString("ShowType") != "0")
                      { %>
                    <tr class="title" align="center">
                        <td>
                            名称</td>
                        <td>
                            大小</td>
                        <td>
                            类型</td>
                        <td>
                            创建时间</td>
                        <td>
                            最后修改时间</td>
                    </tr>
                    <%}
                      else
                      {%>
                    <tr>
                        <%} %>
            </HeaderTemplate>
            <ItemTemplate>
                <%if (RequestString("ShowType") != "0")
                  { %>
                <tr class="tdbg" align="center">
                    <td align="left">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<img src=\"../../admin/images/Folder/mfolderclosed.gif\">" : GetShowExtension(Eval("content_type").ToString())%>
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"ShowUploadFiles.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "&ClientId=" + RequestString("ClientId") + "&ThumbClientId=" + RequestString("ThumbClientId") + "&HiddenFieldId=" + RequestString("HiddenFieldId") + "\">" + Eval("Name").ToString() + "</a>" : "<span onmouseover=\"ShowADPreview('" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()) + "')\" onmouseout=\"hideTooltip('dHTMLADPreview')\">" + "<a href='javascript:ReturnUrl(\"" + Request.QueryString["Dir"] + "/" + Eval("Name").ToString() + "\")'>" + Eval("Name").ToString() + "</a></span>"%>
                    </td>
                    <td>
                        <%# GetSize(Eval("size").ToString())%>
                    </td>
                    <td>
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
                    </td>
                    <td>
                        <%# Eval("createTime")%>
                    </td>
                    <td>
                        <%# Eval("lastWriteTime")%>
                    </td>
                </tr>
                <%}
                  else
                  {%>
                <td align="center">
                    名称：<%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"ShowUploadFiles.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "&ShowType=0\">" + Eval("Name").ToString() + "</a>" : Eval("Name").ToString()%><br />
                    <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<img src='../../admin/images/Folder/mfolderclosed.gif'>" : "<a href='javascript:ReturnUrl(\""  + Request.QueryString["Dir"] + "/" + Eval("Name").ToString() + "\")'>" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()).Replace("\\", "") + "</a>"%>
                    <br />
                    大小：<%# GetSize(Eval("size").ToString())%><br />
                    类型：<%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
                </td>
                <%
                    m_ItemIndex++;
                    if (m_ItemIndex % 8 == 0 && m_ItemIndex > 1)
                    {%>
                </tr><tr>
                    <%}
                  }%>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px;
            position: absolute; top: 0px; height: 10px">
        </div>

        <script language="javascript" type="text/javascript">
<!--
function ReturnUrl(url)
{
    if (url.substring(0, 1) == "/")
    {
        url = url.substring(1, url.length);
    }
    
    var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
    if (isMSIE)
    {
        window.returnValue = url;
        window.close();
    }
    else
    {
        var thumbClientId = '<%= Request.QueryString["ThumbClientId"] %>';
        var type = '<%= Request.QueryString["Type"] %>';
        var typeName = "图片";
        if(thumbClientId == '')
        {
            if (type != '')
            {
                typeName = '软件';
            }
            var obj = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
            var newurl= typeName + '地址'+(obj.length+1)+'|'+url;
            obj.options[obj.length]=new Option(newurl,newurl);
            ChangeHiddenFieldValue();
        }
        else
        {
            var obj = opener.document.getElementById(thumbClientId);
            obj.value = url;
        }
        window.close();
    }
}

function ChangeHiddenFieldValue()
{
    var obj = opener.document.getElementById('<%= Request.QueryString["HiddenFieldId"] %>');
    var softUrls = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
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
//-->
        </script>

    </form>
</body>
</html>
