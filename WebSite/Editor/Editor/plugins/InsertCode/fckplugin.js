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
FCKCommands.RegisterCommand( 'InsertCode'		, new FCKDialogCommand( FCKLang['DlgMadInsertCode']	, FCKLang['DlgMadInsertCode']		, FCKConfig.PluginsPath + 'InsertCode/InsertCode.htm'	, 510, 450 ) ) ;

// Create the "Media" toolbar button.
var oFindItem		= new FCKToolbarButton( 'InsertCode', FCKLang['InsertCode'] ) ;
oFindItem.IconPath	= FCKConfig.PluginsPath + 'InsertCode/InsertCode.gif' ;

FCKToolbarItems.RegisterItem( 'InsertCode', oFindItem ) ;			// 'InsertCode' is the name used in the Toolbar config.
