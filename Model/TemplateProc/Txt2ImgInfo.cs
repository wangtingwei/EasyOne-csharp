namespace EasyOne.Model.TemplateProc
{
    using System;
    using System.Drawing;

    public class Txt2ImgInfo
    {
        private bool _adaptable = true;
        private int _alpha = 0xff;
        private int _b_blue;
        private int _b_green;
        private int _b_red;
        private string _backgroundImage = string.Empty;
        private int _blue = 0xe5;
        private string _fontFamily = "黑体";
        private int _fontSize = 100;
        private System.Drawing.FontStyle _fontStyle = System.Drawing.FontStyle.Regular;
        private int _green = 0xe5;
        private int _height = 50;
        private int _left;
        private long _quality = 100L;
        private int _red = 0xe5;
        private string _resultImage;
        private bool _shadow;
        private string _text;
        private int _top;
        private int _width = 460;

        public bool Adaptable
        {
            get
            {
                return this._adaptable;
            }
            set
            {
                this._adaptable = value;
            }
        }

        public int Alpha
        {
            get
            {
                return this._alpha;
            }
            set
            {
                this._alpha = value;
            }
        }

        public int BackgroundBlue
        {
            get
            {
                return this._b_blue;
            }
            set
            {
                this._b_blue = value;
            }
        }

        public int BackgroundGreen
        {
            get
            {
                return this._b_green;
            }
            set
            {
                this._b_green = value;
            }
        }

        public string BackgroundImage
        {
            get
            {
                return this._backgroundImage;
            }
            set
            {
                this._backgroundImage = value;
            }
        }

        public int BackgroundRed
        {
            get
            {
                return this._b_red;
            }
            set
            {
                this._b_red = value;
            }
        }

        public int Blue
        {
            get
            {
                return this._blue;
            }
            set
            {
                this._blue = value;
            }
        }

        public string FontFamily
        {
            get
            {
                return this._fontFamily;
            }
            set
            {
                this._fontFamily = value;
            }
        }

        public int FontSize
        {
            get
            {
                return this._fontSize;
            }
            set
            {
                this._fontSize = value;
            }
        }

        public System.Drawing.FontStyle FontStyle
        {
            get
            {
                return this._fontStyle;
            }
            set
            {
                this._fontStyle = value;
            }
        }

        public int Green
        {
            get
            {
                return this._green;
            }
            set
            {
                this._green = value;
            }
        }

        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public int Left
        {
            get
            {
                return this._left;
            }
            set
            {
                this._left = value;
            }
        }

        public long Quality
        {
            get
            {
                return this._quality;
            }
            set
            {
                this._quality = value;
            }
        }

        public int Red
        {
            get
            {
                return this._red;
            }
            set
            {
                this._red = value;
            }
        }

        public string ResultImage
        {
            get
            {
                return this._resultImage;
            }
            set
            {
                this._resultImage = value;
            }
        }

        public bool Shadow
        {
            get
            {
                return this._shadow;
            }
            set
            {
                this._shadow = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
    }
}

