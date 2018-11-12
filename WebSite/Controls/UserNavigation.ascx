<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.UserNavigation" Codebehind="UserNavigation.ascx.cs" %>
<% if (m_IsAgent)
   {%>
<style type="text/css">
#UTab1 li
{
	float: left;
	display: block;
	width: 80px;
	text-align: center;
}
</style>
<%} %>
<!-- 我的控制菜单开始 -->
<div id="main_left">
    <dl>
        <dt></dt>
        <dd>
            <div id="mg_user_left">
                <ul>
                    <asp:Repeater ID="RptTopMenu" runat="server">
                        <ItemTemplate>
                            <pe:ExtendedLiteral HtmlEncode="false" ID="LitMenu" runat="server"></pe:ExtendedLiteral>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!-- 我的控制菜单开结束 -->
            <div id="mg_user_right">
                <asp:Repeater ID="RptMenu" runat="server">
                    <ItemTemplate>
                        <pe:ExtendedLiteral HtmlEncode="false" ID="LitDivBegin" runat="server"></pe:ExtendedLiteral>
                        <asp:Repeater ID="RptSubMenu" runat="server">
                            <ItemTemplate>
                                <pe:ExtendedLiteral HtmlEncode="false" ID="LitMenu" runat="server"></pe:ExtendedLiteral>
                            </ItemTemplate>
                        </asp:Repeater>
                        <pe:ExtendedLiteral HtmlEncode="false" ID="LitDivEnd" runat="server"></pe:ExtendedLiteral>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="clearbox">
            </div>
            <!-- 用户快捷导航结束 -->
        </dd>
    </dl>
    <div class="clearbox">
    </div>
    <input type="hidden" id="infoleft" />
<input type="hidden" id="inforight" />
</div>
<%=jumptomainrightjs %>

<script language="JavaScript" type="text/javascript">
function Show_Menu(obj)
{
    var ele=$("#mg_user_right>*");
	var menu=$("#mg_user_left>ul>li");
	for(var i=0;i<ele.length;i++)
    {
    
        if(obj.id==menu[i].id)
        {
			if(i==0){
             $("#"+menu[i].id).addClass("Side_title_top");
			}else{
             $("#"+menu[i].id).addClass("Side_title");
			}
             if(i<ele.length-1)			
             {
                $("#"+menu[i+1].id).addClass("AfterOn");
                $("#mg_user_left").removeClass("onBottom");
             }else{
                $("#mg_user_left").addClass("onBottom");
             }
             $("#"+ele[i].id).css({display:""});
        }else{
            $("#"+menu[i].id).removeClass("Side_title_top");
            $("#"+menu[i].id).removeClass("Side_title");
            $("#"+ele[i].id).css({display:"none"});
            if(i<ele.length-1)			
             {
                $("#"+menu[i+1].id).removeClass("AfterOn");
                $("#mg_user_left").removeClass("onBottom");
            }
        }
    }
}
$(document).ready(function(){
		var ele=$("#mg_user_right>*");
		var menu=$("#mg_user_left>ul>li");
	     $("#"+menu[0].id).addClass("Side_title_top");
	     $("#"+menu[1].id).addClass("AfterOn");
		for(var i=0;i<ele.length;i++)
		{
			 var href=$("#"+ele[i].id+">ul>li>a");
			 for(var m=0;m<href.length;m++)
			 {
			    if(location.href.toLowerCase().indexOf($(href[m]).attr("href").toLowerCase())>0)
			    { 
			        Show_Menu(menu[i]);
			        break;
			   }
			 }
		}
	}	
)
</script>


