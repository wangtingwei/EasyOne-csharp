<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.PhotoPreview" Codebehind="PhotoPreview.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" CurrentNode="查看内容"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/JavaScript">
   var tID=0;
   var arrTabTitle = new Array("TabTitle0","TabTitle1");
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
                基本信息
            </td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                属性信息
            </td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)" style="display: none">
                收费信息
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tbody id="TabsBase">
            <tr class="tdbg">
                <td width="15%" class="tdbgleft" align="right">
                    <strong>图片名称</strong>：&nbsp;
                </td>
                <td width="35%">
                    <%=GetValue("Title")%>
                </td>
                <td width="50%" colspan="2" rowspan="5" align="center" valign="middle">
                    <%=GetImgPath("DefaultPicUrl", "Title")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>图片作者</strong>：&nbsp;
                </td>
                <td width="200">
                    <%=GetValue("Author")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>更新时间</strong>：&nbsp;
                </td>
                <td width="200">
                    <%=GetValue("UpdateTime")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>评分等级</strong>：&nbsp;
                </td>
                <td width="200">
                    <%=GetValue("Stars")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>查看次数</strong>：&nbsp;
                </td>
                <td>
                    本日：<%=GetValue("DayHits")%>
                    本周：<%=GetValue("WeekHits")%>
                    本月：<%=GetValue("MonthHits")%>
                    总计：<%=GetValue("Hits")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>图片地址</strong>：&nbsp;
                </td>
                <td colspan="3">
                    <%=GetDownloadurl("PhotoUrl")%>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>图片简介</strong>：&nbsp;
                </td>
                <td colspan="3">
                    <%=GetValue("PhotoIntro")%>
                </td>
            </tr>
        </tbody>
        <asp:Repeater ID="RptContent" runat="server" OnItemDataBound="RptContent_ItemDataBound">
            <ItemTemplate>
                <tr id="Tab" runat="server" class="tdbg">
                    <td class="tdbgleft" style="width: 15%;" align="right">
                        <strong>
                            <asp:Literal ID="LtrFieldAlias" Text='<%# Eval("FieldAlias")%>' runat="server"></asp:Literal>
                            ：&nbsp;</strong>
                    </td>
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
        OperateCode="ContentManage" OnClientClick="return confirm('确定要审核通过该信息吗？')" OnClick="EBtnCheck_Click"
        runat="server" />
    <div id="Div1" style="display: none;">
        <pe:ExtendedNodeButton ID="EBtnMove" Text=" 移动 " IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnMove_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnBack" Text="直接退稿" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnBack_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnCPass" Text="取消审核" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnCPass_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnTop" Text="设为固顶" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnTop_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnEltiy" Text="设为推荐" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnEltiy_Click" runat="server" />
    </div>
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
