/*
 * For FCKeditor 2.3
 * 
 * File Name: media.js
 * 	Scripts related to the Media dialog window (see media.html).
 * 
 * File Authors:
 * 		Madpolice (madpolice_dong@163.com) 20060726
 */

var oEditor		= window.parent.InnerDialogLoaded() ;
var FCK			= oEditor.FCK ;
var FCKLang		= oEditor.FCKLang ;
var FCKConfig	= oEditor.FCKConfig ;

var oEmbed = FCK.Selection.GetSelectedElement() ;
window.onload = function()
{
	// Translate the dialog box texts.
	oEditor.FCKLanguageManager.TranslatePage(document) ;
	window.parent.SetAutoSize( true ) ;

	// Activate the "OK" button.
	window.parent.SetOkButton( true ) ;
}

//#### The OK button was hit.
function Ok()
{
	if ( ! oEmbed ) {
		oEmbed		= FCK.CreateElement( '<P>' ) ;
	} else {
		oEditor.FCKUndo.SaveUndoStep() ;
	}
	UpdateEmbed( oEmbed ) ;

	return true ;
}

function UpdateEmbed( e )
{
	e.innerHTML = '[NextPage' + GetE('txtPageTitle').value + ']' ;
}