<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.FileManage" Title="上传文件管理" Codebehind="FileManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px;
        position: absolute; top: 0px; height: 10px">
    </div>

    <script src="<%=BasePath%>Admin/JS/Popup.js" language="javascript" type="text/javascript"></script>

    <br />
    <asp:Literal ID="LitMessage" runat="server" Visible="false"></asp:Literal>
    <asp:Panel ID="PanContent" runat="server">
        <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
            <tr class="tdbg">
                <td>
                    <a href="fileManage.aspx?Dir=<%=Server.UrlEncode(m_CurrentDir) + (RequestString("ShowType") != "0" ? "&ShowType=0" : "")  %>">
                        <%=RequestString("ShowType") != "0" ? "切换到缩略图方式" : "切换到列表方式"%>
                    </a>
                </td>
                <td style="width: 350px" align="right">
                    &nbsp; 搜索当前目录文件<span style="color: Red">[支持Windows通配符搜索]</span>：
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
                    <%= string.IsNullOrEmpty(m_CurrentDir) ? "<a disabled=\"true\">返回上一级</a>" : "<a href=\"fileManage.aspx?Dir=" + m_ParentDir + "&ShowType=" + RequestString("ShowType") + "\">返回上一级</a>"%>
                </td>
            </tr>
        </table>
        <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
            <HeaderTemplate>
                <%if (RequestString("ShowType") != "0")
                  { %>
                <table width="100%" cellpadding="0" cellspacing="1" border="0" class="border">
                    <tr class="title" align="center">
                        <td style="width: 30px">
                            选择</td>
                        <td>
                            <a href='<%# GetSorturl("name") %>'>名称</a><%# GetSortShow("name") %></td>
                        <td style="width: 60px">
                            <a href='<%# GetSorturl("size") %>'>大小</a><%# GetSortShow("size") %></td>
                        <td style="width: 80px">
                            类型</td>
                        <td style="width: 120px">
                            <a href='<%# GetSorturl("lastwritetime") %>'>最后修改时间</a><%# GetSortShow("lastwritetime") %>
                        </td>
                        <td style="width: 40px">
                            操作</td>
                    </tr>
                    <%}
                      else
                      {%>
                    <table width="100%" cellpadding="0" cellspacing="1" border="0" class="border">
                        <tr class="tdbgleft">
                            <td>
                                排序方式：<a href='<%# GetSorturl("name") %>'>名称</a><%# GetSortShow("name") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href='<%# GetSorturl("size") %>'>大小</a><%# GetSortShow("size") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href='<%# GetSorturl("lastwritetime") %>'>最后修改时间</a><%# GetSortShow("lastwritetime") %></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="1" border="0" class="border">
                        <tr class="tdbg">
                            <%} %>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HiddenField ID="HdnName" runat="server" Value='<%# Eval("Name")%>' />
                <asp:HiddenField ID="HdnType" runat="server" Value='<%# Eval("type")%>' />
                <asp:HiddenField ID="HdnExtension" runat="server" Value='<%# Eval("content_type")%>' />
                <%if (RequestString("ShowType") != "0")
                  { %>
                <tr class="tdbg" align="center" onmouseover="MouseOver(this,'tdbgmouseover')" onmouseout="MouseOut(this)">
                    <td>
                        <asp:CheckBox ID="ChkListFiles" runat="server" onclick="javascript:CheckItem(this,'ChkAll','tdbg','tdbgselected');"
                            Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' />
                    </td>
                    <td align="left">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<img src=\"../../admin/images/Folder/mfolderclosed.gif\">" : "<img src=\"../../admin/images/Folder/" + GetShowExtension(Eval("content_type").ToString()) + ".gif\">"%>
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"fileManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "\">" + Eval("Name").ToString() + "</a>" : "<span onmouseover=\"ShowADPreview('" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()) + "')\" onclick=\"OpenNewWindowsToPrview('" + GetFilePath(Eval("Name").ToString()) + "','"+ Eval("content_type").ToString()+"')\" onmouseout=\"hideTooltip('dHTMLADPreview')\">" + 
            Eval("Name").ToString() + "</span>"%>
                    </td>
                    <td align="right">
                        <%# GetSize(Eval("size").ToString())%>
                    </td>
                    <td>
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
                    </td>
                    <td>
                        <%# Eval("lastWriteTime")%>
                    </td>
                    <td>
                        <asp:LinkButton ID="LbtnDelList" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                            CommandArgument='<%# Eval("Name")%>' OnClientClick="if(!this.disabled) return confirm('确定要删除吗？');"
                            Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' runat="server">删除</asp:LinkButton></td>
                </tr>
                <%}
                  else
                  {%>
                <td align="center" onmouseover="MouseOver(this,'tdbgmouseover2')" onmouseout="MouseOut(this)">
                    <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<img src=\"../../admin/images/Folder/mfolderclosed.gif\">" : GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()).Replace("\\", "")%>
                    <br />
                    <br />
                    <span style="color: Red">
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"fileManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "&ShowType=0\">" + Eval("Name").ToString() + "</a>" : Eval("Name").ToString()%>
                    </span>
                    <br />
                    <br />
                    大小：<%# GetSize(Eval("size").ToString())%><br />
                    类型：<%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %><br />
                    <asp:CheckBox ID="ChkFiles" onclick="javascript:CheckItem(this,'ChkAll','tdbgnolineheight','tdbgselected2');"
                        runat="server" Text="选中" Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' />
                    <asp:LinkButton ID="LbtnDel" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                        CommandArgument='<%# Eval("Name")%>' OnClientClick="if(!this.disabled) return confirm('确定要删除吗？');"
                        Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' runat="server">删除</asp:LinkButton>
                </td>
                <%
                    m_ItemIndex++;
                    if (m_ItemIndex % 5 == 0 && m_ItemIndex > 1)
                    {%>
                </tr><tr class="tdbg">
                    <%}
                  }%>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <table border="0" align="center" cellpadding="2" cellspacing="0">
            <tr>
                <td align="center">
                    <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
                    </pe:AspNetPager>
                </td>
            </tr>
        </table>
        <br />
        <input type="checkbox" onclick="javascript:CheckAll(this);" id="ChkAll" /><label
            for="ChkAll">选中所有</label>
        <asp:Button ID="BtnDelSelected" runat="server" Text="删除选中的目录或文件" OnClick="BtnDelSelected_Click"
            OnClientClick="return batchconfirm('是否要删除选中的目录或文件？');" />
        <asp:Button ID="BtnDelCurrentFiles" runat="server" Text="删除当前目录的所有文件" OnClick="BtnDelCurrentFiles_Click"
            OnClientClick="return confirm('确定要删除当前目录的所有文件吗？');" />
        <asp:Button ID="BtnDelAll" runat="server" Text="删除所有文件与子目录" OnClick="BtnDelAll_Click"
            OnClientClick="return confirm('确定要删除当前目录的所有文件与子目录吗？');" />
        <asp:Button ID="BtnWaterMark" runat="server" Text="批量给选中的图片添加水印" OnClick="BtnWaterMark_Click"
            OnClientClick="return batchconfirm('是否批量给选中的图片添加水印？');" />
        <asp:Button ID="BtnThumb" runat="server" Text="批量给选中的图片生成缩略图" OnClick="BtnThumb_Click"
            OnClientClick="return batchconfirm('是否批量给选中的图片生成缩略图？');" />
        <%= "<input id=\"Button1\" type=\"button\" class=\"inputbutton\" value=\"返回上一级\" " + (string.IsNullOrEmpty(m_CurrentDir) ? "disabled=\"disabled\"" : "") + "onclick=\"javascript:location.href='fileManage.aspx?Dir=" + m_ParentDir + "&ShowType=" + RequestString("ShowType") + "';\" />"%>

        <script type="text/javascript">
        function OpenNewWindowsToPrview(imgSrc,extension)
        {
            switch (extension)
            {
                case "jpeg":
                case "jpe":
                case "bmp":
                case "png":
                case "jpg":
                case "gif":
                    OpenNewImgWindow(imgSrc)
                    break;

                case "wmv":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "ra":
                case "ram":
                case "swf":
                    OpenObjectWindow(imgSrc)
                    break;

                default:
                    break;
            }
             
        }
        
        function OpenObjectWindow(spath)
        {
           var newWin = window.open("",null,"directories=0,height=600,width=500,menubar=0,left=0,top=0,resizable =0,status=1,titlebar=0,toolbar=0,scrollbars=0,location=0");
           newWin.document.write("<html>");
           newWin.document.write("<title>查看文件</title>");
           newWin.document.write("<body><div>");
           newWin.document.write("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'");
           newWin.document.write(" width='600'");
           newWin.document.write(" height='500'>");
           newWin.document.write("<param name='movie' value='" + spath + "'>");
           newWin.document.write("<param name='wmode' value='transparent'>");
           newWin.document.write("<param name='quality' value='autohigh'>");
           newWin.document.write("<embed src='" + spath + "' quality='autohigh'");
           newWin.document.write(" pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash'");
           newWin.document.write(" wmode='transparent'");
           newWin.document.write(" width='600'");
           newWin.document.write(" height='500'>");
           newWin.document.write("</embed></object>");
           newWin.document.write("</div>");
           newWin.document.write("</body></html>");
           newWin.document.close();
        }
        
        function OpenNewImgWindow(imgSrc)
        {
             var image=new Image();
	         image.src=imgSrc;
             var newWin = window.open("",null,"directories=0,height="+image.height+",width ="+image.width+",menubar=0,left=0,top=0,resizable =0,status=1,titlebar=0,toolbar=0,scrollbars=0,location=0");
             newWin.document.write("<html>");
             newWin.document.write("<title>查看图片</title>");
             newWin.document.write("<body><div>");
             newWin.document.write("<img src='"+imgSrc+"' width='"+image.width+"' height='"+image.height+"'/> ");
             newWin.document.write("</div>");
             newWin.document.write("</body></html>");
             newWin.document.close();
        }
        
function CheckAll(me)
{
    var index = me.name.indexOf('_');  
    var prefix = me.name.substr(0, index); 
    var checkstatus = me.checked;
    for(var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    if(!o.disabled)
                    {
                        o.checked = !me.checked;
                        o.click();
                        me.checked = checkstatus;
                    }
                }
            }
        } 
    } 
}

function CheckItem(me, HeaderID, RowClassName, SelectedCssClass)
{
    if (me.checked)
    {
        HighLight(me, SelectedCssClass);
    }
    else
    {
        LowLight(me, RowClassName);
    }
    
    var headerchk = document.getElementById(HeaderID)
    var index = headerchk.name.indexOf('_');  
    var prefix = headerchk.name.substr(0, index); 
    var totalnumber = 0;
    var totalchecked=0;
    if(headerchk.checked)
    {
        headerchk.checked = headerchk.checked & 0;
    }
    for(var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (o.name != headerchk.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    totalnumber++;
                    if (o.checked) totalchecked++;
                }
            }
        } 
    }
    if (totalnumber == totalchecked)
    {
        headerchk.checked = true;
    }
}

function HighLight(Element, SelectedCssClass)
{
    while (Element.tagName != '<%=RequestString("ShowType") != "0" ? "TR" : "TD"%>') { Element = Element.parentNode; }
    Element.className = SelectedCssClass;
    CurrentClassName = Element.className;
}

function LowLight(Element, RowClassName)
{
    while (Element.tagName !=  '<%=RequestString("ShowType") != "0" ? "TR" : "TD"%>') { Element = Element.parentNode; }
    Element.className = RowClassName;
    CurrentClassName = Element.className;
}

function MouseOver(me, MouseOverCssClass)
{
    CurrentClassName = me.className;
    me.className = MouseOverCssClass;
}

function MouseOut(me)
{
    me.className = CurrentClassName;
}


String.prototype.endWith = function(oString)
{   
    var reg = new RegExp(oString + "$");
    return reg.test(this);
}

function batchconfirm(prompt, nocheckprompt)
{
    var prompt = (arguments.length > 0) ? arguments[0] : "确定要进行此批量操作？";
    var nocheckprompt = (arguments.length > 1) ? arguments[1] : "请选择所要操作的项目！";
    var haschecked = false;
    for (var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == "checkbox" && o.name.endWith("Files") && o.checked == true) 
        { 
            haschecked = true;
            break;
        } 
    } 
    if (!haschecked)
    {
        alert(nocheckprompt);
        return false;
    }
    else
    {
        if (!confirm(prompt))
        {
            return false;
        }
    }
}

        </script>

    </asp:Panel>
</asp:Content>
