﻿document.write("<!-- 搜索 -->\r\n                    <div class=\"main_search\">\r\n\r\n                         软件搜索：<select name=\'nodeid\' size=\'1\'><option value=3>下载中心</option></select><select name=\'fieldoption\' size=\'1\'><option value=\'title\' selected>软件名称</option><option value=\'content\'>软件简介</option><option value=\'author\'>软件作者</option><option value=\'inputer\'>录 入 者</option><option value=\'keyword\'>关键字</option></select><input id=\"Keyword\" onfocus=\"this.value=\'\';\" maxLength=\"100\" size=\"30\" value=\"\" name=\"Keyword\" />\r\n                         <input id=\"Submit\" style=\"border: 0px;width: 65px;height: 21px;\" type=\"image\" src=\"/Skin/Default/Images/search_b.gif\" name=\"Submit\" onclick=\"OnSearchCheckAndSubmit();\" />\r\n                         <a href=\"/search.aspx?searchtype=2&ModelId=3\">高级搜索</a>                 \r\n\r\n					   <script language=\"javascript\" type=\"text/javascript\">\r\n                       function OnSearchCheckAndSubmit()\r\n                       {\r\n                            var keyword=document.getElementById(\"Keyword\").value;\r\n                            if(keyword==\'\'||keyword==null)\r\n                            {\r\n                                alert(\"请填写您想搜索的关键词\");\r\n                                return ;\r\n                            }else{\r\n                           \r\n                            var nodeSel=document.getElementById(\"NodeId\");\r\n                            var fieldoptionSel=document.getElementById(\"fieldoption\");\r\n                            var nodeId=nodeSel.options[nodeSel.options.selectedIndex].value;\r\n                            var fieldoption=fieldoptionSel.options[fieldoptionSel.options.selectedIndex].value;\r\n                            window.location=\"/search.aspx?searchtype=1&ModelId=3&nodeId=\"+nodeId+\"&Keyword=\"+escape(keyword)+\"&fieldoption=\"+fieldoption;\r\n                            }\r\n                       }\r\n                       </script> \r\n                     </div>")