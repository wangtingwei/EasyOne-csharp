<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Contents.ContentBatch" Codebehind="ContentBatchSet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/JavaScript">
        var tID=0;
        var arrTabTitle = new Array("TabTitle0","<%=TabTitle1.ClientID%>");
        var arrTabs = new Array("Tabs0","Tabs1");
        function ShowTabs(ID)
        {
            if(ID!=tID)
            {
                document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
                document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
                document.getElementById(arrTabs[ID].toString()).style.display = "";
                tID=ID;
            }
        }
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="5" class="spacingtitle"">
                批量内容信息属性
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 10%" valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="BatchItemType" GroupName="BatchType" runat="server" />
                            指定项目ID:
                            <br />
                            <asp:TextBox ID="TxtBatchItmeID" runat="server" Width="231px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="BatchNodeType" GroupName="BatchType" runat="server" />
                            指定节点ID:
                            <br />
                            <asp:ListBox ID="LstNodes" runat="server" DataTextField="NodeName" DataValueField="NodeId"
                                SelectionMode="Multiple" Height="282px" Width="231px"></asp:ListBox></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="inputbutton"
                                value="  选定所有节点  " />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="inputbutton"
                                value="取消选定所有节点" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">提示：</span>可以按住“Shift”<br />
                            或“Ctrl”键进行多个节点的选择</td>
                    </tr>
                </table>
            </td>
            <td class="tdbgleft" style="width: 90%" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                            基本信息</td>
                        <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" runat="server">
                            收费选项</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" cellpadding="2" cellspacing="1" class="border">
                    <!--基本信息-->
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkEliteLevel" runat="server" /></td>
                            <td style="width: 20%" class="tdbgleft">
                                <strong>推荐级别：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtEliteLevel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkPriority" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>优先级：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtPriority" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkHits" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>点击数：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtHits" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkDayHits" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>日点击数：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtDayHits" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkWeekHits" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>周点击数：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtWeekHits" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkMonthHits" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>月点击数：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtMonthHits" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkUpdateTime" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>更新时间：</strong></td>
                            <td>
                                <pe:DatePicker ID="DpkUpdateTime" runat="server"></pe:DatePicker>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkTemplateFile" runat="server" /></td>
                            <td style="width: 300" class="tdbgleft">
                                <strong>内容页模板：</strong></td>
                            <td>
                                <pe:TemplateSelectControl ID="FileCTemplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                            </td>
                        </tr>
                    </tbody>
                    <!--收费设置-->
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="width: 5%">
                                <asp:CheckBox ID="ChkInfoPurview" runat="server" /></td>
                            <td style="width: 20%" align="right" class="tdbgleft">
                                <strong>阅读权限：&nbsp;</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlInfoPurview" runat="server">
                                    <asp:ListItem Value="0" Selected="True">继承栏目权限（当所属栏目为认证栏目时，建议选择此项）</asp:ListItem>
                                    <asp:ListItem Value="1">所有会员（想单独对某些文章进行阅读权限设置，可以选择此项）</asp:ListItem>
                                    <asp:ListItem Value="2">指定会员组（想单独对某些文章进行阅读权限设置，可以选择此项）</asp:ListItem>
                                </asp:RadioButtonList>
                                <table>
                                    <tr>
                                        <td style="width: 20px;">
                                        </td>
                                        <td>
                                            <pe:ExtendedCheckBoxList ID="EChklUserGroupList" RepeatColumns="5" runat="server">
                                            </pe:ExtendedCheckBoxList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkInfoPoint" runat="server" /></td>
                            <td style="width: 150px;" align="right" class="tdbgleft">
                                <strong>消费点数：&nbsp;</strong></td>
                            <td style="height: 17px">
                                <asp:TextBox ID="TxtInfoPoint" runat="server" Columns="5" MaxLength="4"></asp:TextBox>&nbsp;
                                <span style="color: #0000FF">如果点数大于0，则有权限的会员阅读此文章时将消耗相应点数（设为9999时除外），游客将无法阅读此文章</span></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkChargeType" runat="server" /></td>
                            <td style="width: 150px;" align="right" class="tdbgleft">
                                <strong>重复收费：&nbsp;</strong></td>
                            <td>
                                <pe:ShowChargeType ID="ShowChargeType" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <asp:CheckBox ID="ChkDividePercent" runat="server" /></td>
                            <td style="width: 150px;" align="right" class="tdbgleft">
                                <strong>分成比例：&nbsp;</strong></td>
                            <td>
                                <asp:TextBox ID="TxtDividePercent" runat="server" Columns="5" MaxLength="3"></asp:TextBox>%
                                &nbsp;<span style="color: #0000FF">如果比例大于0，则将按比例把向阅读者收取的点数支付给录入者</span></td>
                        </tr>
                    </tbody>
                    <tbody>
                        <tr>
                            <td colspan="3" class="tdbg">
                                说明：
                                <br />
                                1、若要批量修改某个属性的值，请先选中其左侧的复选框，然后再设定属性值。<br />
                                2、这里显示的属性值都是系统默认值，与所选节点的已有属性无关
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td class="tdbgleft" id="commonfooter" colspan="2">
                <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="返回"
                    onclick="Redirect('ContentManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
