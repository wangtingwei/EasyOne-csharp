function changeimage()
{
  document.myform.GuestImages.value=document.myform.Image.value;
  document.myform.showimages.src='/GuestBook/Images/Face/'+document.myform.Image.value+'.gif';
}
function guestpreview()
{
  document.preview.content.value=document.myform.GuestContent.value;
  var popupWin = window.open('GuestPreview.asp', 'GuestPreview', 'scrollbars=yes,width=620,height=230');
  document.preview.submit();
}
function CheckForm()
{
    if(document.myform.GuestName.value==''){
      alert('��������Ϊ�գ�');
      document.myform.GuestName.focus();
      return(false) ;
    }
  if(document.myform.GuestTitle.value==''){
    alert('���ⲻ��Ϊ�գ�');
    document.myform.GuestTitle.focus();
    return(false);
  }
  if(document.myform.GuestTitle.value.length>30){
    alert('���ⲻ�ܳ���30�ַ���');
    document.myform.GuestTitle.focus();
    return(false);
  }
   var IframeContent=document.getElementById("editor").contentWindow;
   IframeContent.HtmlEdit.focus();
   IframeContent.HtmlEdit.document.execCommand('selectAll');
   IframeContent.HtmlEdit.document.execCommand('copy');
  var CurrentMode=editor.CurrentMode;
  if (CurrentMode==0){
       document.myform.GuestContent.value=editor.HtmlEdit.document.body.innerHTML; 
  }
  else if(CurrentMode==1){
       document.myform.GuestContent.value=editor.HtmlEdit.document.body.innerText;
  }
  if(document.myform.GuestContent.value==''){
    alert('���ݲ���Ϊ�գ�');
    editor.HtmlEdit.focus();
    return(false);
  }
  if(document.myform.GuestContent.value.length>65536){
    alert('���ݲ��ܳ���64K��');
    editor.HtmlEdit.focus();
    return(false);
  }
}

