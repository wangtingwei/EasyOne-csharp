var img=$("bimg").getElementsByTagName("div");
var td=$("simg").getElementsByTagName("td");
var text=$("info").getElementsByTagName("div");
var auto;
var now=1;
var def=0;
var speed=10000;
for(var i=0;i<count;i++){
	td[i].name = i;
	td[i].className = "";
	td[i].onclick = function (){changeBar(this.name)}
	td[i].onmouseover = function(){clearAuto()}
	td[i].onmouseout = function(){setAuto()}
	img[i].style.display = "none";
	img[i].onmouseover = function(){clearAuto()}
	img[i].onmouseout = function(){setAuto()}
	text[i].style.display = "none";
	text[i].onmouseover = function(){clearAuto()}
	text[i].onmouseout = function(){setAuto()}
}
td[def].className = "s";
img[def].style.display = "block";
text[def].style.display = "block";
function setClass(v){ 
	for(var i=0;i<count;i++)
		td[i].className = "";
	td[v].className = "s";
}  
function setImg(v){	
	try{
		with (bimg){
			filters[0].Apply(); 
			for(var i=0;i<count;i++){
				img[i].style.display = 'none';	
				text[i].style.display = 'none';	
			}	
			img[v].style.display = 'block';
			text[v].style.display = 'block';
			filters[0].play(); 	
		}
	}
	catch(e){		
		for(var i=0;i<count;i++){
			img[i].style.display = 'none';	
			text[i].style.display = 'none';
		}			
		img[v].style.display = 'block';
		text[v].style.display = 'block';
	}
}
function changeBar(v){
	setClass(v);
	setImg(v);
	now= v+1;
} 
function setChange(){
	if(now<count){
		changeBar(now);		
		}
	else{
		now = 0;
		changeBar(now);
	}
}
function setAuto(){auto = setInterval('setChange()', speed)}
function clearAuto(){clearInterval(auto)}
setAuto();