<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Common.GuestWrite"
    StylesheetTheme="" EnableTheming="false"  ValidateRequest="false" Codebehind="GuestWrite.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>签写留言</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
                <tr align="center">
                    <td colspan="5" class="spacingtitle">
                        <b>签写留言</b>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft" style="width: 25%;">
                        <strong>姓名：</strong></td>
                    <td style="width: 35%;">
                        <asp:TextBox ID="TxtGuestName" runat="server" Width="276px"></asp:TextBox>
                        <pe:RequiredFieldValidator runat="server" ID="ValrGuestName" ControlToValidate="TxtGuestName"
                            Display="Dynamic" ErrorMessage="请输入名称" />
                        <asp:Label ID="LblGuestName" runat="server" Text="" Visible ="false" />
                    </td>
                    <td rowspan="4" colspan="2">
                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                            <tr>
                                <td style="width: 100%;" align="left">
                                    <asp:DropDownList ID="DropGuestImages" runat="server">
                                        <asp:ListItem Value="01">01</asp:ListItem>
                                        <asp:ListItem Value="02">02</asp:ListItem>
                                        <asp:ListItem Value="03">03</asp:ListItem>
                                        <asp:ListItem Value="04">04</asp:ListItem>
                                        <asp:ListItem Value="05">05</asp:ListItem>
                                        <asp:ListItem Value="06">06</asp:ListItem>
                                        <asp:ListItem Value="07">07</asp:ListItem>
                                        <asp:ListItem Value="08">08</asp:ListItem>
                                        <asp:ListItem Value="09">09</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                        <asp:ListItem Value="17">17</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                        <asp:ListItem Value="19">19</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="21">21</asp:ListItem>
                                        <asp:ListItem Value="22">22</asp:ListItem>
                                        <asp:ListItem Value="23">23</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <img id="showphoto" src="../Images/Comment/01.gif" width="80" height="90" alt="用户头像" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>性别：</strong></td>
                    <td>
                        <asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                            <asp:ListItem Value="0">女</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>--%>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>E_mail：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtGuestEmail" runat="server" Width="276px"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ValrEmail" ControlToValidate="TxtGuestEmail" runat="server"
                            ErrorMessage="Email不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                        <pe:EmailValidator ID="Vmail" runat="server" ControlToValidate="TxtGuestEmail" Display="Dynamic"
                            ErrorMessage="电子邮件格式不对"></pe:EmailValidator>
                    
                        <asp:Label ID="LblGuestEmail" runat="server" Text="" Visible ="false" />        
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>QQ：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtGuestOicq" runat="server" Width="276px"></asp:TextBox>
                        <asp:Label ID="LblGuestOicq" runat="server" Text="" Visible ="false" />  
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>MSN：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtGuestMsn" runat="server" Width="276px"></asp:TextBox>
                        <asp:Label ID="LblGuestMsn" runat="server" Text="" Visible ="false" />      
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>个人主页：</strong></td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtGuestHomepage" runat="server" Width="276px"></asp:TextBox>
                        <asp:Label ID="LblGuestHomepage" runat="server" Text="" Visible ="false" />     
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>留言主题：</strong></td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtSubject" runat="server" Width="276px"></asp:TextBox>
                        <pe:RequiredFieldValidator runat="server" ID="ValrSubject" ControlToValidate="TxtSubject"
                            Display="Dynamic" ErrorMessage="主题不能为空！" />
                        </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>留言类别：</strong></td>
                    <td colspan="3">
                        <asp:DropDownList ID="DropCategoryId" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0" Text="0" Selected="true">不指定任何类型</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>现在心情：</strong></td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadlGuestFace" runat="server" RepeatDirection="Horizontal"
                            RepeatColumns="10">
                            <asp:ListItem Selected="True" Value="1">&lt;img src=&quot;../Images/Comment/face1.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="2">&lt;img src=&quot;../Images/Comment/face2.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="3">&lt;img src=&quot;../Images/Comment/face3.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="4">&lt;img src=&quot;../Images/Comment/face4.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="5">&lt;img src=&quot;../Images/Comment/face5.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="6">&lt;img src=&quot;../Images/Comment/face6.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="7">&lt;img src=&quot;../Images/Comment/face7.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="8">&lt;img src=&quot;../Images/Comment/face8.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="9">&lt;img src=&quot;../Images/Comment/face9.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="10">&lt;img src=&quot;../Images/Comment/face10.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="11">&lt;img src=&quot;../Images/Comment/face11.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="12">&lt;img src=&quot;../Images/Comment/face12.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="13">&lt;img src=&quot;../Images/Comment/face13.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="14">&lt;img src=&quot;../Images/Comment/face14.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="15">&lt;img src=&quot;../Images/Comment/face15.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="16">&lt;img src=&quot;../Images/Comment/face16.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="17">&lt;img src=&quot;../Images/Comment/face17.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="18">&lt;img src=&quot;../Images/Comment/face18.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="19">&lt;img src=&quot;../Images/Comment/face19.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                            <asp:ListItem Value="20">&lt;img src=&quot;../Images/Comment/face20.gif&quot; width=&quot;19&quot; height=&quot;19&quot;/&gt;</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>留言内容：</strong></td>
                    <td colspan="3">
                        <pe:PEeditor ID="EditorGuestContent" ToolbarSet="Basic" runat="server" Width="580px"
                            Height="300px">
                        </pe:PEeditor>
                        <pe:FckEditorValidator ID="ValrGuestContent" runat="server" ControlToValidate="EditorGuestContent"
                            ErrorMessage="留言内容不能为空" Display="Dynamic"></pe:FckEditorValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft">
                        <strong>是否隐藏：</strong>
                    </td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadlGuestIsPrivate" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Selected="True" Value="0">正常</asp:ListItem>
                            <asp:ListItem Value="1">隐藏</asp:ListItem>
                        </asp:RadioButtonList>
                        选择隐藏后，此留言只有管理员和留言者才可以看到。</td>
                </tr>
                <tr>
                    <td colspan="4" align="center" class="tdbg">
                        <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                        <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="EBtnSubmit_Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
