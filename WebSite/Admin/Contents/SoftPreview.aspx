<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.SoftPreview" Codebehind="SoftPreview.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" CurrentNode="查看内容"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/JavaScript">
   var tID=0;
   var arrTabTitle = new Array("TabTitle0","TabTitle1","TabTitle2");
   var arrTrs0 = new Array("TabsBase");
   var arrTrs1 = new Array(<%= arrTrs0 %>);
   var arrTrs2 = new Array("TabsCharge");
   var arrTab = new Array(arrTrs0,arrTrs1,arrTrs2);
   function ShowTabs(ID)
   {
       if(ID!=tID)
       {
           document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
           document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
           
           for (var i = 0; i < arrTab.length; i++) 
           {
                var tab = arrTab[i];
                if(i==ID)
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "";
                    }
                }
                else
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "none";
                    }
                }
           }
           
           tID=ID;
        }
    }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="spacingtitle" colspan="7" align="center">
                查看内容
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                属性信息</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                收费信息</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tbody id="TabsBase">
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件名称：</td>
                <td colspan="3">
                    <strong>
                        <asp:Literal ID="LitTitle" runat="server"></asp:Literal>
                    </strong>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    文件大小：</td>
                <td width="300">
                     <asp:Literal ID="LitSoftSize" runat="server"></asp:Literal>K</td>
                <td colspan="2" rowspan="5" align="center" valign="middle">
                    <img src='<%=GeturlValue("DefaultPicUrl")%>' width="150">
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    开 发 商：</td>
                <td width="300">
                    <%=GetValue("Author")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件来源：</td>
                <td width="300">
                    <%=GetValue("CopyFrom")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件平台：</td>
                <td width="300">
                    <%=GetValue("OperatingSystem")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件类别：</td>
                <td width="300">
                    <%=GetValue("SoftType")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件语言：</td>
                <td width="300">
                    <%=GetValue("SoftLanguage")%>
                </td>
                <td width="100" align="right" class="tdbgleft">
                    授权形式：</td>
                <td width="300">
                    <%=GetValue("CopyrightType")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    演示地址：</td>
                <td width="300">
                    <a href='<%=GetValue("CopyrightType")%>' target="_blank">
                        <%=GetValue("DemoUrl")%>
                    </a>
                </td>
                <td width="100" align="right" class="tdbgleft">
                    注册地址：</td>
                <td width="300">
                    <a href='<%=GetValue("RegUrl")%>' target="_blank">
                        <%=GetValue("RegUrl")%>
                    </a>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    解压密码：</td>
                <td width="300">
                    <%=GetValue("DecompressPassword")%>
                </td>
                <td width="100" align="right" class="tdbgleft">
                    评分等级：</td>
                <td>
                    <%=GetValue("Stars")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    添加时间：</td>
                <td width="300">
                    <%=GetValue("UpdateTime")%>
                </td>
                <td width="100" align="right" class="tdbgleft">
                    下载次数：</td>
                <td >
                    本日：<%=GetValue("DayHits")%>
                    本周：<%=GetValue("WeekHits")%>
                    本月：<%=GetValue("MonthHits")%>
                    总计：<%=GetValue("Hits")%></td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    下载地址：</td>
                <td colspan="3">
                    <%=GetDownloadurl("DownloadUrl")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td width="100" align="right" class="tdbgleft">
                    软件简介：</td>
                <td height="100" colspan="3">                  
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LitSoftIntro" runat="server"></pe:ExtendedLiteral>
                </td>
            </tr>
        </tbody>
        <asp:Repeater ID="RptContent" runat="server" OnItemDataBound="RptContent_ItemDataBound">
            <ItemTemplate>
                <tr id="Tab" runat="server" class="tdbg">
                    <td class="tdbgleft" style="width: 15%;" align="right">
                        <strong>
                            <asp:Literal ID="LtrFieldAlias" Text='<%# Eval("FieldAlias")%>' runat="server"></asp:Literal>
                            ：&nbsp;</strong></td>
                    <td class="tdbg" align="left">
                        <pe:ExtendedLiteral ID="LitContentText" runat="server"></pe:ExtendedLiteral><asp:Panel ID="PnlContent"
                            runat="server">
                        </asp:Panel>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tbody id="TabsCharge" style="display: none">
            <pe:ContentCharge ID="ContentCharge" runat="server" />
        </tbody>
        <tbody id="TabsSign" style="display: none">
            <pe:ContentSigin ID="ContentSigin" runat="server" />
        </tbody>
    </table>
    <br />
    <pe:ExtendedNodeButton ID="EBtnModify" Text="修改/审核" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClick="EBtnModify_Click" runat="server" />
    <pe:ExtendedNodeButton ID="EBtnDelete" Text="删除" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClientClick="return confirm('确定要删除该信息吗？')" OnClick="EBtnDelete_Click"
        runat="server" />
    <pe:ExtendedNodeButton ID="EBtnCheck" Text="审核通过" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClientClick="return confirm('确定要进行审核操作吗？')" OnClick="EBtnCheck_Click"
        runat="server" />
    <br />
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <li>上一篇：<asp:HyperLink ID="LnkGetPrevInfo" runat="server"></asp:HyperLink></li>
                <li>下一篇：<asp:HyperLink ID="LnkGetNextInfo" runat="server"></asp:HyperLink></li>
            </td>
        </tr>
    </table>
    <br />
    <pe:CommentManage ID="CommentManage" runat="server" />
</asp:Content>
