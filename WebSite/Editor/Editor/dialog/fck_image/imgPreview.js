
var eImgPreview ;
var oImageOriginal ;

function UpdateOriginal( resetSize )
{
   if ( !eImgPreview ){
		return ;
	}
	oImageOriginal = document.createElement( 'IMG' ) ;	// new Image() ;
	oImageOriginal.src = eImgPreview.src ;
}

function SetPreviewElements( imageElement, linkElement )
{
	eImgPreview = imageElement ;
	UpdateOriginal() ;
	bPreviewInitialized = true ;
}
