namespace EasyOne.Components
{
    using EasyOne.Common;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    public abstract class WaterMark
    {
        protected WaterMark()
        {
        }

        public static void AddWaterMark(string originalImagePath)
        {
            string filename = "";
            string extension = Path.GetExtension(originalImagePath);
            filename = originalImagePath.Replace(extension, "WaterMark" + extension);
            WaterMarkText waterMarkTextInfo = SiteConfig.WaterMarkConfig.WaterMarkTextInfo;
            WaterMarkImage waterMarkImageInfo = SiteConfig.WaterMarkConfig.WaterMarkImageInfo;
            int waterMarkType = SiteConfig.WaterMarkConfig.WaterMarkType;
            string str4 = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir);
            string str5 = VirtualPathUtility.AppendTrailingSlash(HttpContext.Current.Server.MapPath("~/"));
            originalImagePath = str5 + str4 + originalImagePath;
            filename = str5 + str4 + filename;
            Image image = null;
            try
            {
                image = Image.FromFile(originalImagePath);
            }
            catch (FileNotFoundException)
            {
                CustomException.ThrowBllException("添加水印的源图片未找到。");
            }
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics picture = Graphics.FromImage(bitmap);
            picture.Clear(Color.Green);
            picture.SmoothingMode = SmoothingMode.HighQuality;
            picture.InterpolationMode = InterpolationMode.High;
            picture.DrawImage(image, 0, 0, image.Width, image.Height);
            switch (waterMarkType)
            {
                case 0:
                    AddWatermarkText(picture, waterMarkTextInfo, image.Width, image.Height);
                    break;

                case 1:
                    AddWatermarkImage(picture, waterMarkImageInfo, image.Width, image.Height);
                    break;
            }
            try
            {
                bitmap.Save(filename, ImageFormat.Jpeg);
            }
            catch (ArgumentNullException)
            {
                CustomException.ThrowBllException("该图像的保存路径未找到。");
            }
            finally
            {
                bitmap.Dispose();
                image.Dispose();
                picture.Dispose();
            }
            FileSystemObject.Delete(originalImagePath, FsoMethod.File);
            string newFile = filename.Replace("WaterMark", "");
            FileSystemObject.Move(filename, newFile, FsoMethod.File);
        }

        private static void AddWatermarkImage(Graphics picture, WaterMarkImage waterMarkImageInfo, int width, int height)
        {
            string filename = HttpContext.Current.Server.MapPath("~/" + waterMarkImageInfo.ImagePath);
            string waterMarkPosition = waterMarkImageInfo.WaterMarkPosition;
            int waterMarkPositionX = waterMarkImageInfo.WaterMarkPositionX;
            int waterMarkPositionY = waterMarkImageInfo.WaterMarkPositionY;
            Image image = null;
            try
            {
                image = new Bitmap(filename);
            }
            catch (ArgumentException)
            {
                CustomException.ThrowBllException("图片水印的路径未找到。");
            }
            ImageAttributes imageAttr = new ImageAttributes();
            ColorMap map = new ColorMap();
            map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
            map.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] mapArray = new ColorMap[] { map };
            imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = 0.3f;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int x = 0;
            int y = 0;
            int num5 = 0;
            int num6 = 0;
            double num7 = 1.0;
            double num8 = Convert.ToDouble((int) (width / 4));
            double num9 = Convert.ToDouble((int) (height / 4));
            double num10 = Convert.ToDouble(image.Width);
            double num11 = Convert.ToDouble(image.Height);
            if ((width > (image.Width * 4)) && (height > (image.Height * 4)))
            {
                num7 = 1.0;
            }
            else if ((width > (image.Width * 4)) && (height < (image.Height * 4)))
            {
                num7 = num9 / num11;
            }
            else if ((width < (image.Width * 4)) && (height > (image.Height * 4)))
            {
                num7 = num8 / num10;
            }
            else if ((width * image.Height) > (height * image.Width))
            {
                num7 = num9 / num11;
            }
            else
            {
                num7 = num8 / num10;
            }
            num5 = Convert.ToInt32((double) (image.Width * num7));
            num6 = Convert.ToInt32((double) (image.Height * num7));
            string str3 = waterMarkPosition;
            if (str3 != null)
            {
                if (!(str3 == "WM_TOP_LEFT"))
                {
                    if (str3 == "WM_TOP_RIGHT")
                    {
                        x = (width - num5) - 10;
                        y = 10;
                    }
                    else if (str3 == "WM_BOTTOM_RIGHT")
                    {
                        x = (width - num5) - 10;
                        y = (height - num6) - 10;
                    }
                    else if (str3 == "WM_BOTTOM_LEFT")
                    {
                        x = 10;
                        y = (height - num6) - 10;
                    }
                    else if (str3 == "WM_SetByManual")
                    {
                        x = waterMarkPositionX;
                        y = waterMarkPositionY;
                    }
                }
                else
                {
                    x = 10;
                    y = 10;
                }
            }
            try
            {
                picture.DrawImage(image, new Rectangle(x, y, num5, num6), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
            catch
            {
                CustomException.ThrowBllException("添加图片水印出现异常。");
            }
            finally
            {
                image.Dispose();
                imageAttr.Dispose();
            }
        }

        private static void AddWatermarkText(Graphics picture, WaterMarkText waterMarkTextInfo, int width, int height)
        {
            SizeF ef;
            string text = waterMarkTextInfo.Text;
            string waterMarkPosition = waterMarkTextInfo.WaterMarkPosition;
            int waterMarkPositionX = waterMarkTextInfo.WaterMarkPositionX;
            int waterMarkPositionY = waterMarkTextInfo.WaterMarkPositionY;
            int foneSize = waterMarkTextInfo.FoneSize;
            string foneType = waterMarkTextInfo.FoneType;
            string foneStyle = waterMarkTextInfo.FoneStyle;
            string foneColor = waterMarkTextInfo.FoneColor;
            int foneBorder = waterMarkTextInfo.FoneBorder;
            string foneBorderColor = waterMarkTextInfo.FoneBorderColor;
            Font font = null;
            string str7 = foneStyle;
            if (str7 != null)
            {
                if (!(str7 == "Bold"))
                {
                    if (str7 == "Italic")
                    {
                        font = new Font(foneType, (float) foneSize, FontStyle.Italic);
                        goto Label_00F9;
                    }
                    if (str7 == "Regular")
                    {
                        font = new Font(foneType, (float) foneSize, FontStyle.Regular);
                        goto Label_00F9;
                    }
                    if (str7 == "Strikeout")
                    {
                        font = new Font(foneType, (float) foneSize, FontStyle.Strikeout);
                        goto Label_00F9;
                    }
                    if (str7 == "Underline")
                    {
                        font = new Font(foneType, (float) foneSize, FontStyle.Underline);
                        goto Label_00F9;
                    }
                }
                else
                {
                    font = new Font(foneType, (float) foneSize, FontStyle.Bold);
                    goto Label_00F9;
                }
            }
            font = new Font(foneType, (float) foneSize, FontStyle.Regular);
        Label_00F9:
            ef = picture.MeasureString(text, font);
            float x = 0f;
            float y = 0f;
            string str8 = waterMarkPosition;
            if (str8 != null)
            {
                if (!(str8 == "WM_TOP_LEFT"))
                {
                    if (str8 == "WM_TOP_RIGHT")
                    {
                        x = (width * 0.99f) - (ef.Width / 2f);
                        y = height * 0.01f;
                    }
                    else if (str8 == "WM_BOTTOM_RIGHT")
                    {
                        x = (width * 0.99f) - (ef.Width / 2f);
                        y = (height * 0.99f) - ef.Height;
                    }
                    else if (str8 == "WM_BOTTOM_LEFT")
                    {
                        x = (width * 0.01f) + (ef.Width / 2f);
                        y = (height * 0.99f) - ef.Height;
                    }
                    else if (str8 == "WM_SetByManual")
                    {
                        x = waterMarkPositionX;
                        y = waterMarkPositionY;
                    }
                }
                else
                {
                    x = (width * 0.01f) + (ef.Width / 2f);
                    y = height * 0.01f;
                }
            }
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            ColorConverter converter = new ColorConverter();
            Color color = (Color) converter.ConvertFromString(foneBorderColor);
            SolidBrush brush = new SolidBrush(color);
            picture.DrawString(text, font, brush, x + foneBorder, y + foneBorder, format);
            ColorConverter converter2 = new ColorConverter();
            Color color2 = (Color) converter2.ConvertFromString(foneColor);
            SolidBrush brush2 = new SolidBrush(color2);
            try
            {
                picture.DrawString(text, font, brush2, x, y, format);
            }
            catch (ArgumentNullException)
            {
                CustomException.ThrowBllException("添加文字水印出现异常。");
            }
            finally
            {
                brush.Dispose();
                brush2.Dispose();
                format.Dispose();
                font.Dispose();
            }
        }
    }
}

