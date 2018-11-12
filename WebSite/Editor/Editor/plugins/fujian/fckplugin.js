/*
 * For FCKeditor 2.3
 * 
 * File Name: fckplugin.js
 * 	Add a toolbar button to insert media.
 * 
 * File Authors:
 * 		Madpolice (madpolice_dong@163.com) 20060726
 */

// Register the related commands.
FCKCommands.RegisterCommand( 'fujian'		, new FCKDialogCommand( FCKLang['DlgMadplusTitle']	, FCKLang['DlgMadplusTitle']		, FCKConfig.BasePath + 'dialog/fck_link.html'	, 500, 500 ) ) ;

// Create the "Media" toolbar button.
var oFindItem		= new FCKToolbarButton( 'fujian', FCKLang['plusTitle'] ) ;
oFindItem.IconPath	= FCKConfig.PluginsPath + 'fujian/fujian.gif' ;

FCKToolbarItems.RegisterItem( 'fujian', oFindItem ) ;			// 'plusTitle' is the name used in the Toolbar config.
