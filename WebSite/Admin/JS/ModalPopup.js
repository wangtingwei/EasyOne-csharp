function BOX_show(e)
{ //��ʾ
    if (!window.XMLHttpRequest)
	{
		if(document.getElementById("DivLstFromNodes") !=null && document.getElementById("DivLstToNodes") !=null)
		{
		    document.getElementById("DivLstFromNodes").style.visibility = "hidden";
            document.getElementById("DivLstToNodes").style.visibility = "hidden";
        }
	}
    if(document.getElementById(e) == null)
    {
        return;
    }
    BOX_layout(e);
}
function BOX_layout(e)
{ //����λ��
    var a = document.getElementById(e);
    if (document.getElementById('BOX_overlay')==null)
    { //�ж��Ƿ��½����ڲ�
        var overlay = document.createElement("div");
        overlay.setAttribute('id','BOX_overlay');
        a.parentNode.appendChild(overlay);
    }
    //ȡ�ͻ����������꣬����
    var scrollLeft = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
    var clientWidth;
    if (window.innerWidth) 
    {
        clientWidth = window.innerWidth-16;
    } else {
        clientWidth = document.documentElement.clientWidth;
    }
    var clientHeight;
    if (window.innerHeigh) 
    {
        clientHeight = window.innerHeight;
    } 
    else 
    {
        clientHeight = document.documentElement.clientHeight;
    }
    var bo = document.getElementById('BOX_overlay');
    bo.style.left = scrollLeft+'px';
    bo.style.top = scrollTop+'px';
    bo.style.width = clientWidth+'px';
    bo.style.height = clientHeight+'px';
    bo.style.display="";
    //Popup���ڶ�λ
    a.style.position = 'absolute';
    a.style.zIndex=101;
    a.style.display="";
    a.style.left = scrollLeft+((clientWidth-a.offsetWidth)/2)+'px';
    a.style.top = scrollTop+((clientHeight-a.offsetHeight)/2)+'px';
}