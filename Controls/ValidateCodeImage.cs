namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ValidateCodeImage ID=\"VcodeImage\" runat=server></{0}:ValidateCodeImage>"), DefaultProperty("Text")]
    public class ValidateCodeImage : WebControl
    {
        private static string[] Fonts = new string[] { "Arial", "Verdana", "Fixedsys", "宋体", "Haettenschweiler", "Lucida Sans Unicode", "Garamond", "Courier New", "Book Antiqua", "Arial Narrow" };
        private string m_ValidateCodeSessionName = "ValidateCodeSession";
        private const string s_ValidateCodeBound = "a|b|c|d|e|f|g|h|i|j|k|L|m|n|o|p|q|r|s|t|u|v|w|x|y|z|0|1|2|3|4|5|6|7|8|9";

        private static void DoBrush(Graphics graphics, ref RectangleF rectangle, ref SolidBrush brush)
        {
            graphics.FillRectangle(brush, rectangle);
            brush.Dispose();
        }

        private static RectangleF DoFill(string checkCode, Graphics graphics, Random random, RectangleF rectangle)
        {
            Color navajoWhite = Color.NavajoWhite;
            switch (random.Next(checkCode.Length))
            {
                case 1:
                    navajoWhite = Color.AliceBlue;
                    break;

                case 2:
                    navajoWhite = Color.Plum;
                    break;

                case 3:
                    navajoWhite = Color.AntiqueWhite;
                    break;

                case 4:
                    navajoWhite = Color.PeachPuff;
                    break;

                case 5:
                    navajoWhite = Color.Pink;
                    break;

                case 6:
                    navajoWhite = Color.Cornsilk;
                    break;
            }
            SolidBrush brush = new SolidBrush(navajoWhite);
            DoBrush(graphics, ref rectangle, ref brush);
            return rectangle;
        }

        private string GetValidateCode()
        {
            Random random = new Random();
            int length = this.ValidateCodeBound.Length;
            int validateCodeLength = this.GetValidateCodeLength();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < validateCodeLength; i++)
            {
                builder.Append(this.ValidateCodeBound[random.Next(length)]);
            }
            if (HttpContext.Current.Session[this.m_ValidateCodeSessionName] == null)
            {
                HttpContext.Current.Session.Add(this.m_ValidateCodeSessionName, builder.ToString());
            }
            else
            {
                HttpContext.Current.Session[this.m_ValidateCodeSessionName] = builder.ToString();
            }
            return builder.ToString();
        }

        private int GetValidateCodeHeight()
        {
            return this.ValidateCodeFontSize;
        }

        private byte GetValidateCodeLength()
        {
            if (this.ValidateCodeLengthMode == ValidateCodeLengthType.Static)
            {
                return this.ValidateCodeMaxLength;
            }
            Random random = new Random();
            return (byte) random.Next(this.ValidateCodeMinLength, this.ValidateCodeMaxLength + 1);
        }

        protected override void OnInit(EventArgs e)
        {
            SetNotCache();
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PaintValidateCode(this.GetValidateCode(), 1, 0, 7, this.GetValidateCodeHeight());
        }

        private static void PaintValidateCode(string checkCode, int enableShowPixel, int enableSplitcheckCode, int fonts, int fontSize)
        {
            if (!string.IsNullOrEmpty(checkCode.Trim()))
            {
                HttpContext current = HttpContext.Current;
                int length = checkCode.Length;
                Bitmap image = new Bitmap((int) Math.Ceiling((double) (length * 11.5)), 20);
                Graphics graphics = Graphics.FromImage(image);
                try
                {
                    Random random = new Random();
                    graphics.Clear(Color.White);
                    for (int i = 0; i < 0x19; i++)
                    {
                        int num3 = random.Next(image.Width);
                        int num4 = random.Next(image.Width);
                        int num5 = random.Next(image.Height);
                        int num6 = random.Next(image.Height);
                        graphics.DrawLine(new Pen(Color.Silver), num3, num5, num4, num6);
                    }
                    Font font = new Font(Fonts[fonts], (float) fontSize, FontStyle.Regular);
                    RectangleF rectangle = new RectangleF(0f, 0f, (float) image.Width, (float) image.Height);
                    if (enableSplitcheckCode == 0)
                    {
                        SolidBrush brush = new SolidBrush(Color.Black);
                        SolidBrush brush2 = new SolidBrush(Color.Blue);
                        for (int j = 1; j <= length; j++)
                        {
                            rectangle = new RectangleF((float) ((image.Width * (j - 1)) / length), 0f, (float) ((image.Width * j) / length), (float) image.Height);
                            rectangle = DoFill(checkCode, graphics, random, rectangle);
                            char[] chArray = checkCode.ToCharArray();
                            if (((chArray[j - 1].ToString() == "l") || (chArray[j - 1].ToString() == "o")) || (chArray[j - 1].ToString() == "z"))
                            {
                                graphics.DrawString(chArray[j - 1].ToString(), font, brush2, rectangle);
                            }
                            else
                            {
                                graphics.DrawString(chArray[j - 1].ToString(), font, brush, rectangle);
                            }
                        }
                        brush.Dispose();
                        brush2.Dispose();
                    }
                    else if (enableSplitcheckCode == 1)
                    {
                        for (int k = 1; k <= length; k++)
                        {
                            rectangle = new RectangleF(0f, 0f, (float) image.Width, (float) image.Height);
                            rectangle = DoFill(checkCode, graphics, random, rectangle);
                        }
                        LinearGradientBrush brush3 = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.DarkBlue, Color.DarkRed, 1.2f, true);
                        graphics.DrawString(checkCode, font, brush3, rectangle);
                    }
                    else
                    {
                        LinearGradientBrush brush4 = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.DarkBlue, Color.DarkRed, 1.2f, true);
                        graphics.DrawString(checkCode, font, brush4, (float) 2f, (float) 2f);
                    }
                    if (enableShowPixel == 0)
                    {
                        for (int m = 0; m < 100; m++)
                        {
                            int x = random.Next(image.Width);
                            int y = random.Next(image.Height);
                            image.SetPixel(x, y, Color.FromArgb(random.Next()));
                        }
                    }
                    Pen pen = new Pen(Color.Silver);
                    graphics.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Gif);
                    current.Response.Expires = -1;
                    current.Response.AddHeader("Pragma", "no-cache");
                    current.Response.ClearContent();
                    current.Response.ContentType = "image/Gif";
                    current.Response.BinaryWrite(stream.ToArray());
                    current.Response.End();
                    stream.Dispose();
                    pen.Dispose();
                    font.Dispose();
                }
                catch
                {
                }
                finally
                {
                    graphics.Dispose();
                    image.Dispose();
                }
            }
        }

        private static void SetNotCache()
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
        }

        [Browsable(false), Description("只读：根据ValidateCodeBoundString获取验证码生成范围的数组")]
        public string[] ValidateCodeBound
        {
            get
            {
                if (this.ViewState["ValidateCodeBoundCharArray"] == null)
                {
                    this.ViewState["ValidateCodeBoundCharArray"] = this.ValidateCodeBoundString.Split(new char[] { '|' });
                }
                return (string[]) this.ViewState["ValidateCodeBoundCharArray"];
            }
        }

        [Description("读取或设置验证码的生成字符范围。"), Category("行为"), DefaultValue("a|b|c|d|e|f|g|h|i|j|k|L|m|n|o|p|q|r|s|t|u|v|w|x|y|z|0|1|2|3|4|5|6|7|8|9")]
        public string ValidateCodeBoundString
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeBound"];
                if (obj2 != null)
                {
                    return obj2.ToString();
                }
                return "a|b|c|d|e|f|g|h|i|j|k|L|m|n|o|p|q|r|s|t|u|v|w|x|y|z|0|1|2|3|4|5|6|7|8|9";
            }
            set
            {
                this.ViewState["ValidateCodeBound"] = value;
            }
        }

        [Description("读取或设置验证码的文字大小，单位为“像素”"), Category("外观"), DefaultValue(10)]
        public byte ValidateCodeFontSize
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeFontSize"];
                if (obj2 != null)
                {
                    return (byte) obj2;
                }
                return 10;
            }
            set
            {
                this.ViewState["ValidateCodeFontSize"] = value;
            }
        }

        [Description("读取或设置验证码长度是可变长度的还是固定长度的。\r\n        如果是固定长度的则长度为ValidateCodeMaxLength所设置的值"), DefaultValue(0), Category("行为")]
        public ValidateCodeLengthType ValidateCodeLengthMode
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeLengthMode"];
                if (obj2 != null)
                {
                    return (ValidateCodeLengthType) obj2;
                }
                return ValidateCodeLengthType.Static;
            }
            set
            {
                this.ViewState["ValidateCodeLengthMode"] = value;
            }
        }

        [Description("读取或设置验证码的最大长度。"), DefaultValue(4), Category("行为")]
        public byte ValidateCodeMaxLength
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeMaxLength"];
                if (obj2 != null)
                {
                    return (byte) obj2;
                }
                return 4;
            }
            set
            {
                if (value <= this.ValidateCodeMinLength)
                {
                    this.ViewState["ValidateCodeMaxLength"] = this.ValidateCodeMinLength;
                }
                else if (value == 0)
                {
                    this.ViewState["ValidateCodeMaxLength"] = 4;
                }
                else
                {
                    this.ViewState["ValidateCodeMaxLength"] = value;
                }
            }
        }

        [Description("读取或设置验证码的最小长度。"), Category("行为"), DefaultValue(4)]
        public byte ValidateCodeMinLength
        {
            get
            {
                object obj2 = this.ViewState["ValidateCodeMinLength"];
                if (obj2 != null)
                {
                    return (byte) obj2;
                }
                return 4;
            }
            set
            {
                if (value >= this.ValidateCodeMaxLength)
                {
                    this.ViewState["ValidateCodeMinLength"] = this.ValidateCodeMaxLength;
                }
                else if (value == 0)
                {
                    this.ViewState["ValidateCodeMinLength"] = 4;
                }
                else
                {
                    this.ViewState["ValidateCodeMinLength"] = value;
                }
            }
        }

        [DefaultValue("ValidateCodeSession"), Description("读取或设置验证码Session保存的名称，如果不设置则默认为ValidateCodeSession")]
        public string ValidateCodeSessionName
        {
            get
            {
                return this.m_ValidateCodeSessionName;
            }
            set
            {
                this.m_ValidateCodeSessionName = value;
            }
        }

        public enum ValidateCodeLengthType
        {
            Static,
            Random
        }
    }
}

