<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.ThumbConfig"
    Title="网站缩略图配置" Codebehind="ThumbConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">

    <script language="JavaScript" type="text/javascript">
    <!--
          function ShowTabWaterMark(type){   
                if(type == "0"){
                    document.getElementById('<%= TextWaterMark.ClientID%>').style.display="";
                    document.getElementById('<%= PicWaterMark.ClientID%>').style.display="none";
                }
                else{
                    document.getElementById('<%= TextWaterMark.ClientID%>').style.display="none";
                    document.getElementById('<%= PicWaterMark.ClientID%>').style.display="";
                }
          } 
    //-->
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>缩略图参数配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 40%">
                <strong>缩略图默认宽度：</strong></td>
            <td>
                <asp:TextBox ID="TxtThumbsWidth" TextMode="SingleLine" runat="server" Columns="5"></asp:TextBox>像素&nbsp;&nbsp;设为0时，将以高度为准按比例缩小。
                <pe:NumberValidator ID="Vnum" ControlToValidate="TxtThumbsWidth" runat="server"></pe:NumberValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>缩略图默认高度：</strong></td>
            <td>
                <asp:TextBox ID="TxtThumbsHeight" TextMode="SingleLine" runat="server" Columns="5"></asp:TextBox>像素&nbsp;&nbsp;设为0时，将以宽度为准按比例缩小。
                <pe:NumberValidator ID="NumberValidator1" ControlToValidate="TxtThumbsHeight" runat="server"></pe:NumberValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>缩略图算法：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadThumbsMode" runat="server">
                    <asp:ListItem Value="0">
                                常规算法：宽度和高度都大于0时，直接缩小成指定大小，其中一个为0时，按比例缩小
                    </asp:ListItem>
                    <asp:ListItem Value="1">
                                裁剪法：宽度和高度都大于0时，先按最佳比例缩小再裁剪成指定大小，其中一个为0时，按比例缩小。
                    </asp:ListItem>
                    <asp:ListItem Value="2">
                                补充法：在指定大小的背景图上附加上按最佳比例缩小的图片。
                    </asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>缩略图底色：</strong></td>
            <td>
                <pe:ColorPicker ID="TxtBgColor" Text="#EE1169" runat="server"></pe:ColorPicker>
                使用补充算法时将以此设置的背景色填充。</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>水印类型：</strong></td>
            <td>
                <input id="RadText" name="RadWaterMarkType" value="0" onclick="ShowTabWaterMark(this.value)"
                    type="radio" runat="server" />文字水印
                <input id="RadImage" name="RadWaterMarkType" value="1" onclick="ShowTabWaterMark(this.value)"
                    type="radio" runat="server" />图片水印
            </td>
        </tr>
        <tbody id="TextWaterMark" runat="server" style="display: none;">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>水印文字：</strong><br />
                    水印文字字数不宜超过15个字符，不支持HTML标记。</td>
                <td>
                    <asp:TextBox ID="TxtWaterMarkText" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文字字体：</strong></td>
                <td>
                    <asp:DropDownList ID="DropWaterMarkTextFoneType" runat="server">
                        <asp:ListItem Value="宋体" Selected="True">宋体</asp:ListItem>
                        <asp:ListItem Value="楷体_GB2312">楷体</asp:ListItem>
                        <asp:ListItem Value="仿宋_GB2312">新宋体</asp:ListItem>
                        <asp:ListItem Value="黑体">黑体</asp:ListItem>
                        <asp:ListItem Value="隶书">隶书</asp:ListItem>
                        <asp:ListItem Value="幼圆">幼圆</asp:ListItem>
                        <asp:ListItem Value="Andale Mono">Andale Mono</asp:ListItem>
                        <asp:ListItem Value="Arial">Arial</asp:ListItem>
                        <asp:ListItem Value="Arial Black">Arial Black</asp:ListItem>
                        <asp:ListItem Value="Book Antiqua">Book Antiqua</asp:ListItem>
                        <asp:ListItem Value="Century Gothic">Century Gothic</asp:ListItem>
                        <asp:ListItem Value="Comic Sans MS">Comic Sans MS</asp:ListItem>
                        <asp:ListItem Value="Courier New">Courier New</asp:ListItem>
                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                        <asp:ListItem Value="Impact">Impact</asp:ListItem>
                        <asp:ListItem Value="Tahoma">Tahoma</asp:ListItem>
                        <asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
                        <asp:ListItem Value="Trebuchet MS">Trebuchet MS</asp:ListItem>
                        <asp:ListItem Value="Script MT Bold">Script MT Bold</asp:ListItem>
                        <asp:ListItem Value="Stencil">Stencil</asp:ListItem>
                        <asp:ListItem Value="Verdana">Verdana</asp:ListItem>
                        <asp:ListItem Value="Lucida Console">Lucida Console</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>字体样式：</strong></td>
                <td>
                    <asp:DropDownList ID="DropWaterMarkTextFoneStyle" runat="server">
                        <asp:ListItem Value="Bold">加粗</asp:ListItem>
                        <asp:ListItem Value="Italic">倾斜</asp:ListItem>
                        <asp:ListItem Value="Regular" Selected="True">常规</asp:ListItem>
                        <asp:ListItem Value="Strikeout">中间有直线通过</asp:ListItem>
                        <asp:ListItem Value="Underline">带下划线</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文字大小：</strong></td>
                <td>
                    <asp:TextBox ID="TxtWaterMarkTextFoneSize" Text="16" runat="server" Columns="5" MaxLength="3"></asp:TextBox>
                    <pe:NumberValidator ID="NumberValidator14" ControlToValidate="TxtWaterMarkTextFoneSize"
                        runat="server"></pe:NumberValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文字颜色：</strong></td>
                <td>
                    <pe:ColorPicker ID="TxtWaterMarkTextFoneColor" Text="#FFFFFF" runat="server"></pe:ColorPicker>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文字边框大小：</strong><br />
                </td>
                <td>
                    <asp:TextBox ID="TxtWaterMarkTextFoneBorder" Text="1" runat="server" Columns="5"
                        MaxLength="3"></asp:TextBox>
                    设置为0时没有边框
                    <pe:NumberValidator ID="NumberValidator2" ControlToValidate="TxtWaterMarkTextFoneBorder"
                        runat="server"></pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文字边框颜色：</strong></td>
                <td>
                    <pe:ColorPicker ID="TxtWaterMarkTextFoneBorderColor" Text="#000000" runat="server"></pe:ColorPicker>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>坐标起点位置：</strong><br />
                </td>
                <td>
                    <asp:DropDownList ID="DropWaterMarkTextPosition" runat="server">
                        <asp:ListItem Value="WM_TOP_LEFT" Selected="True">左上</asp:ListItem>
                        <asp:ListItem Value="WM_TOP_RIGHT">右上</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_RIGHT">右下</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_LEFT">左下</asp:ListItem>
                        <asp:ListItem Value="WM_SetByManual">手动设置</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>坐标位置：</strong><br />
                    坐标起点位置设置为手动设置时才有效。
                </td>
                <td>
                    X:<asp:TextBox ID="TxtWaterMarketTextPositionX" Text="0" runat="server" Columns="5"></asp:TextBox>像素
                    <pe:NumberValidator ID="NumberValidator3" ControlToValidate="TxtWaterMarketTextPositionX"
                        runat="server"></pe:NumberValidator>
                    <br />
                    Y:<asp:TextBox ID="TxtWaterMarketTextPositionY" Text="0" runat="server" Columns="5"></asp:TextBox>像素
                    <pe:NumberValidator ID="NumberValidator4" ControlToValidate="TxtWaterMarketTextPositionY"
                        runat="server"></pe:NumberValidator>
                </td>
            </tr>
        </tbody>
        <tbody id="PicWaterMark" runat="server" style="display: none;">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>水印图片文件名：</strong><br />
                    这里请填写图片文件的相对路径。</td>
                <td>
                    <asp:TextBox ID="TxtWaterMarkImagePath" runat="server" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>坐标起点位置：</strong><br />
                </td>
                <td>
                    <asp:DropDownList ID="DropWaterMarkImagePosition" runat="server">
                        <asp:ListItem Value="WM_TOP_LEFT" Selected="True">左上</asp:ListItem>
                        <asp:ListItem Value="WM_TOP_RIGHT">右上</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_RIGHT">右下</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_LEFT">左下</asp:ListItem>
                        <asp:ListItem Value="WM_SetByManual">手动设置</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>坐标位置：</strong><br />
                    坐标起点位置设置为手动设置时才有效。
                </td>
                <td>
                    X：<asp:TextBox ID="TxtWaterMarkImagePositionX" Text="0" runat="server" Columns="5"></asp:TextBox>像素
                    <pe:NumberValidator ID="NumberValidator5" ControlToValidate="TxtWaterMarkImagePositionX"
                        runat="server"></pe:NumberValidator>
                    <br />
                    Y：<asp:TextBox ID="TxtWaterMarkImagePositionY" Text="0" runat="server" Columns="5"></asp:TextBox>像素
                    <pe:NumberValidator ID="NumberValidator6" ControlToValidate="TxtWaterMarkImagePositionY"
                        runat="server"></pe:NumberValidator>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
