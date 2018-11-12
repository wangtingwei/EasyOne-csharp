<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.FileManage" Title="�ϴ��ļ�����" Codebehind="FileManage.aspx.cs" %>

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
                        <%=RequestString("ShowType") != "0" ? "�л�������ͼ��ʽ" : "�л����б�ʽ"%>
                    </a>
                </td>
                <td style="width: 350px" align="right">
                    &nbsp; ������ǰĿ¼�ļ�<span style="color: Red">[֧��Windowsͨ�������]</span>��
                    <asp:TextBox ID="TxtSearchKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="����" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td>
                    ��ǰĿ¼��<asp:Label ID="LblCurrentDir" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <%= string.IsNullOrEmpty(m_CurrentDir) ? "<a disabled=\"true\">������һ��</a>" : "<a href=\"fileManage.aspx?Dir=" + m_ParentDir + "&ShowType=" + RequestString("ShowType") + "\">������һ��</a>"%>
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
                            ѡ��</td>
                        <td>
                            <a href='<%# GetSorturl("name") %>'>����</a><%# GetSortShow("name") %></td>
                        <td style="width: 60px">
                            <a href='<%# GetSorturl("size") %>'>��С</a><%# GetSortShow("size") %></td>
                        <td style="width: 80px">
                            ����</td>
                        <td style="width: 120px">
                            <a href='<%# GetSorturl("lastwritetime") %>'>����޸�ʱ��</a><%# GetSortShow("lastwritetime") %>
                        </td>
                        <td style="width: 40px">
                            ����</td>
                    </tr>
                    <%}
                      else
                      {%>
                    <table width="100%" cellpadding="0" cellspacing="1" border="0" class="border">
                        <tr class="tdbgleft">
                            <td>
                                ����ʽ��<a href='<%# GetSorturl("name") %>'>����</a><%# GetSortShow("name") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href='<%# GetSorturl("size") %>'>��С</a><%# GetSortShow("size") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href='<%# GetSorturl("lastwritetime") %>'>����޸�ʱ��</a><%# GetSortShow("lastwritetime") %></td>
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
                        <%# System.Convert.ToInt32(Eval("type")) == 1 ? "�ļ���" : Eval("content_type").ToString() + "�ļ�" %>
                    </td>
                    <td>
                        <%# Eval("lastWriteTime")%>
                    </td>
                    <td>
                        <asp:LinkButton ID="LbtnDelList" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                            CommandArgument='<%# Eval("Name")%>' OnClientClick="if(!this.disabled) return confirm('ȷ��Ҫɾ����');"
                            Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' runat="server">ɾ��</asp:LinkButton></td>
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
                    ��С��<%# GetSize(Eval("size").ToString())%><br />
                    ���ͣ�<%# System.Convert.ToInt32(Eval("type")) == 1 ? "�ļ���" : Eval("content_type").ToString() + "�ļ�" %><br />
                    <asp:CheckBox ID="ChkFiles" onclick="javascript:CheckItem(this,'ChkAll','tdbgnolineheight','tdbgselected2');"
                        runat="server" Text="ѡ��" Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' />
                    <asp:LinkButton ID="LbtnDel" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                        CommandArgument='<%# Eval("Name")%>' OnClientClick="if(!this.disabled) return confirm('ȷ��Ҫɾ����');"
                        Enabled='<%# GetDeleteEnabled(Eval("Name").ToString()) %>' runat="server">ɾ��</asp:LinkButton>
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
            for="ChkAll">ѡ������</label>
        <asp:Button ID="BtnDelSelected" runat="server" Text="ɾ��ѡ�е�Ŀ¼���ļ�" OnClick="BtnDelSelected_Click"
            OnClientClick="return batchconfirm('�Ƿ�Ҫɾ��ѡ�е�Ŀ¼���ļ���');" />
        <asp:Button ID="BtnDelCurrentFiles" runat="server" Text="ɾ����ǰĿ¼�������ļ�" OnClick="BtnDelCurrentFiles_Click"
            OnClientClick="return confirm('ȷ��Ҫɾ����ǰĿ¼�������ļ���');" />
        <asp:Button ID="BtnDelAll" runat="server" Text="ɾ�������ļ�����Ŀ¼" OnClick="BtnDelAll_Click"
            OnClientClick="return confirm('ȷ��Ҫɾ����ǰĿ¼�������ļ�����Ŀ¼��');" />
        <asp:Button ID="BtnWaterMark" runat="server" Text="������ѡ�е�ͼƬ���ˮӡ" OnClick="BtnWaterMark_Click"
            OnClientClick="return batchconfirm('�Ƿ�������ѡ�е�ͼƬ���ˮӡ��');" />
        <asp:Button ID="BtnThumb" runat="server" Text="������ѡ�е�ͼƬ��������ͼ" OnClick="BtnThumb_Click"
            OnClientClick="return batchconfirm('�Ƿ�������ѡ�е�ͼƬ��������ͼ��');" />
        <%= "<input id=\"Button1\" type=\"button\" class=\"inputbutton\" value=\"������һ��\" " + (string.IsNullOrEmpty(m_CurrentDir) ? "disabled=\"disabled\"" : "") + "onclick=\"javascript:location.href='fileManage.aspx?Dir=" + m_ParentDir + "&ShowType=" + RequestString("ShowType") + "';\" />"%>

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
           newWin.document.write("<title>�鿴�ļ�</title>");
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
             newWin.document.write("<title>�鿴ͼƬ</title>");
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
    var prompt = (arguments.length > 0) ? arguments[0] : "ȷ��Ҫ���д�����������";
    var nocheckprompt = (arguments.length > 1) ? arguments[1] : "��ѡ����Ҫ��������Ŀ��";
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
