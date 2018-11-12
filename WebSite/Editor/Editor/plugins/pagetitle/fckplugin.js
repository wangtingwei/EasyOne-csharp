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
FCKCommands.RegisterCommand( 'PageTitle'		, new FCKDialogCommand( FCKLang['DlgMadPageTitle']	, FCKLang['DlgMadPageTitle']		, FCKConfig.PluginsPath + 'PageTitle/pagetitle.html'	, 400, 300 ) ) ;

// Create the "Media" toolbar button.
var oFindItem		= new FCKToolbarButton( 'PageTitle', FCKLang['PageTitle'] ) ;
oFindItem.IconPath	= FCKConfig.PluginsPath + 'PageTitle/pagetitle.gif' ;

FCKToolbarItems.RegisterItem( 'PageTitle', oFindItem ) ;			// 'PageTitle' is the name used in the Toolbar config.
