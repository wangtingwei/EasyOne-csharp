namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;

    public abstract class Thumbs
    {
        protected Thumbs()
        {
        }

        public static string GetThumbsPath(string originalImagePath, string thumbnailPath)
        {
            string str = thumbnailPath;
            string uploadDir = SiteConfig.SiteOption.UploadDir;
            int thumbsWidth = SiteConfig.ThumbsConfig.ThumbsWidth;
            int thumbsHeight = SiteConfig.ThumbsConfig.ThumbsHeight;
            int thumbsMode = SiteConfig.ThumbsConfig.ThumbsMode;
            string addBackColor = SiteConfig.ThumbsConfig.AddBackColor;
            ThumbsMode byHeightAndWidth = ThumbsMode.ByHeightAndWidth;
            if (thumbsWidth <= 0)
            {
                if (thumbsHeight > 0)
                {
                    byHeightAndWidth = ThumbsMode.ByHeight;
                }
            }
            else if (thumbsHeight <= 0)
            {
                byHeightAndWidth = ThumbsMode.ByWidth;
            }
            else
            {
                switch (thumbsMode)
                {
                    case 0:
                        byHeightAndWidth = ThumbsMode.ByHeightAndWidth;
                        goto Label_007A;

                    case 1:
                        byHeightAndWidth = ThumbsMode.CutByHeightOrWidth;
                        goto Label_007A;

                    case 2:
                        byHeightAndWidth = ThumbsMode.AddBackColor;
                        goto Label_007A;
                }
            }
        Label_007A:
            if (!uploadDir.EndsWith("/", StringComparison.Ordinal))
            {
                uploadDir = uploadDir + @"\";
            }
            string str4 = HttpContext.Current.Server.MapPath("~/");
            originalImagePath = str4 + uploadDir + originalImagePath;
            thumbnailPath = str4 + uploadDir + thumbnailPath;
            MakeThumbnail(originalImagePath, thumbnailPath, thumbsWidth, thumbsHeight, byHeightAndWidth, addBackColor);
            return str;
        }

        public static string GetThumbsPath(string originalImagePath, string thumbnailPath, string path)
        {
            string str = thumbnailPath;
            string uploadDir = SiteConfig.SiteOption.UploadDir;
            int thumbsWidth = SiteConfig.ThumbsConfig.ThumbsWidth;
            int thumbsHeight = SiteConfig.ThumbsConfig.ThumbsHeight;
            int thumbsMode = SiteConfig.ThumbsConfig.ThumbsMode;
            string addBackColor = SiteConfig.ThumbsConfig.AddBackColor;
            ThumbsMode byHeightAndWidth = ThumbsMode.ByHeightAndWidth;
            if (thumbsWidth <= 0)
            {
                if (thumbsHeight > 0)
                {
                    byHeightAndWidth = ThumbsMode.ByHeight;
                }
            }
            else if (thumbsHeight <= 0)
            {
                byHeightAndWidth = ThumbsMode.ByWidth;
            }
            else
            {
                switch (thumbsMode)
                {
                    case 0:
                        byHeightAndWidth = ThumbsMode.ByHeightAndWidth;
                        goto Label_007A;

                    case 1:
                        byHeightAndWidth = ThumbsMode.CutByHeightOrWidth;
                        goto Label_007A;

                    case 2:
                        byHeightAndWidth = ThumbsMode.AddBackColor;
                        goto Label_007A;
                }
            }
        Label_007A:
            if (!uploadDir.EndsWith("/", StringComparison.Ordinal))
            {
                uploadDir = uploadDir + @"\";
            }
            originalImagePath = path + uploadDir + originalImagePath;
            thumbnailPath = path + uploadDir + thumbnailPath;
            MakeThumbnail(originalImagePath, thumbnailPath, thumbsWidth, thumbsHeight, byHeightAndWidth, addBackColor);
            return str;
        }

        private static double GetThumbsPercent(int originalImageWidth, int originalImageHeight, int width, int height)
        {
            double num = 1.0;
            if (width == 0)
            {
                width = 1;
            }
            if (height == 0)
            {
                height = 1;
            }
            double num2 = Convert.ToDouble(originalImageWidth);
            double num3 = Convert.ToDouble(originalImageHeight);
            double num4 = Convert.ToDouble(width);
            double num5 = Convert.ToDouble(height);
            if ((originalImageWidth <= originalImageHeight) && (width >= height))
            {
                num = num3 / num5;
            }
            else if ((originalImageWidth > originalImageHeight) && (width < height))
            {
                num = num2 / num4;
            }
            else if ((originalImageWidth <= originalImageHeight) && (width <= height))
            {
                if ((originalImageHeight / height) >= (originalImageWidth / width))
                {
                    num = num2 / num4;
                }
                else
                {
                    num = num3 / num5;
                }
            }
            else if ((originalImageHeight / height) >= (originalImageWidth / width))
            {
                num = num3 / num5;
            }
            else
            {
                num = num2 / num4;
            }
            if (num <= 1.0)
            {
                num = 1.0;
            }
            return num;
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbsMode thumbsMode, string bgColor)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(originalImagePath);
            }
            catch (FileNotFoundException)
            {
                CustomException.ThrowBllException("生成缩略图的源图片未找到");
            }
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            switch (thumbsMode)
            {
                case ThumbsMode.ByWidth:
                    num2 = (image.Height * width) / image.Width;
                    break;

                case ThumbsMode.ByHeight:
                    num = (image.Width * height) / image.Height;
                    break;

                case ThumbsMode.CutByHeightOrWidth:
                    if (num == 0)
                    {
                        num = 1;
                    }
                    if (num2 == 0)
                    {
                        num2 = 1;
                    }
                    if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                    {
                        num6 = image.Height;
                        num5 = (image.Height * num) / num2;
                        y = 0;
                        x = (image.Width - num5) / 2;
                    }
                    else
                    {
                        num5 = image.Width;
                        num6 = (image.Width * height) / num;
                        x = 0;
                        y = (image.Height - num6) / 2;
                    }
                    break;

                case ThumbsMode.AddBackColor:
                {
                    double num7 = GetThumbsPercent(image.Width, image.Height, width, height);
                    if (width == 0)
                    {
                        width = 1;
                    }
                    if (height == 0)
                    {
                        height = 1;
                    }
                    num = Convert.ToInt32((double) (((double) image.Width) / num7));
                    num2 = Convert.ToInt32((double) (((double) image.Height) / num7));
                    x = (width - num) / 2;
                    y = (height - num2) / 2;
                    num5 = x + num;
                    num6 = y + num2;
                    break;
                }
            }
            if (num == 0)
            {
                num = 1;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            Image image2 = new Bitmap(num, num2);
            if (thumbsMode == ThumbsMode.AddBackColor)
            {
                image2 = new Bitmap(width, height);
            }
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            if ((thumbsMode == ThumbsMode.AddBackColor) && !string.IsNullOrEmpty(bgColor))
            {
                ColorConverter converter = new ColorConverter();
                Color color = (Color) converter.ConvertFromString(bgColor);
                graphics.Clear(color);
            }
            else
            {
                graphics.Clear(Color.Transparent);
            }
            if (thumbsMode == ThumbsMode.AddBackColor)
            {
                graphics.DrawImage(image, new Rectangle(x, y, num, num2), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            }
            try
            {
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch
            {
                throw new ExternalException("该图像以错误的图像格式保存。- 或 - 该图像被保存到创建该图像的文件");
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }
    }
}

