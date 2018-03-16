using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.Linq;
using System.Text;
using System.IO;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    /// <summary>
    /// 图片处理 gif图片（动图）暂不压缩
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 最大图片的宽度
        /// </summary>
        public static int MaxImageWith = 640;
        /// <summary>
        /// 图片的后缀名
        /// </summary>
        public static string[] Exts = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };

        /// <summary>
        /// 图片编辑
        /// </summary>
        /// <param name="img"></param>
        /// <param name="filepath"></param>
        /// <param name="zoom">容量是否压缩</param>
        /// <param name="size">尺寸是否缩放</param>
        /// <returns></returns>
        public static bool RewriteImage(Image img, string filepath, bool zoom = true, bool size = true)
        {
            try
            {

                //原图宽
                int imgW0 = img.Width;
                //原图高
                int imgH0 = img.Height;
                //目标图宽
                int timgW = imgW0;
                //目标图高
                int timgH = imgH0;
                if (filepath.IndexOf(".gif", StringComparison.Ordinal) != -1)
                {
                    return false;
                }
                if (zoom)
                {
                    if (size)
                    {
                        if (img.Width > MaxImageWith)//图片需要改变宽度
                        {
                            timgW = MaxImageWith;
                            timgH = timgW * imgH0 / imgW0;
                        }
                    }
                    using (Bitmap bitmap = new Bitmap(timgW, timgH))
                    {
                        bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                        //新建一个画板
                        using (Graphics g =Graphics.FromImage(bitmap))
                        {
                            g.InterpolationMode = InterpolationMode.High;
                            g.SmoothingMode =SmoothingMode.HighQuality;
                            g.CompositingQuality =CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.High;
                            g.Clear(Color.Transparent);
                            g.DrawImage(img, new Rectangle(0, 0, timgW, timgH), new Rectangle(0, 0, imgW0, imgH0), GraphicsUnit.Pixel);
                            // System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, imgW,imgH);
                            //  g.DrawImage(img, rectDestination, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                        }
                        bitmap.Save(filepath, ImageFormat.Jpeg);
                    }
                }
                else
                {
                    img.Save(filepath);
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 图片编辑
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="filepath"></param>
        /// <param name="zoom">容量是否压缩:true压缩，false不压缩</param>
        /// <param name="size">尺寸是否缩放（最大宽度640）：true缩放，false尺寸不修改</param>
        /// <returns></returns>
        public static bool RewriteImage(Stream sm, string filepath, bool zoom = true, bool size = true)
        {
            try
            {
                if (filepath.IndexOf(".gif", StringComparison.Ordinal) != -1)
                {
                    return false;
                }
                using (Image img = Image.FromStream(sm, true, true))
                {
                    if (zoom)
                    {
                        int imgW = img.Width;
                        int imgH = img.Height;
                        int targetw = imgW;
                        int targeth = imgH;
                        if (size)
                        {
                            if (img.Width > MaxImageWith)//图片需要改变宽度
                            {
                                targetw = MaxImageWith;
                                targeth = targetw * imgH / imgW;
                            }
                        }
                        Bitmap bitmap = new Bitmap(targetw, targeth);
                        bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                        //新建一个画板
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.InterpolationMode = InterpolationMode.High;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.CompositingQuality =CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.High;
                            g.Clear(Color.Transparent);

                            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            //g.Clear(Color.Transparent);
                            g.DrawImage(img, new Rectangle(0, 0, targetw, targeth), new Rectangle(0, 0, imgW, imgH), GraphicsUnit.Pixel);
                        }
                        bitmap.Save(filepath, ImageFormat.Jpeg);
                    }
                    else
                    {
                        img.Save(filepath);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 图片编辑
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="filepath">要保存的图片路径</param>
        /// <param name="zoom">是否压缩(容量)</param>
        /// <param name="size">是否缩放(尺寸)</param>
        /// <returns></returns>
        public static bool RewriteImage(byte[] bytes, string filepath, bool zoom = true, bool size = true)
        {
            try
            {
                if (filepath.IndexOf(".gif", StringComparison.Ordinal) != -1)
                {
                    return false;
                }
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image img = Image.FromStream(ms);
                    if (zoom)
                    {
                        int imgW = img.Width;
                        int imgH = img.Height;
                        int targetW = imgW;
                        int targetH = imgH;
                        if (size)
                        {
                            if (img.Width > MaxImageWith)//图片需要改变宽度
                            {
                                targetW = MaxImageWith;
                                targetH = targetW * imgH / imgW;
                            }
                        }
                        Bitmap bitmap = new Bitmap(imgW, imgH);
                        //  bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                        //新建一个画板
                        using (Graphics g =Graphics.FromImage(bitmap))
                        {

                            g.InterpolationMode = InterpolationMode.High;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.Clear(Color.Transparent);
                            //  g.DrawImage(img, new Rectangle(0, 0, imgW, imgH), new Rectangle(0, 0, imgW, imgH), GraphicsUnit.Pixel);
                            g.DrawImage(img, new Rectangle(0, 0, imgW, imgH));
                        }
                        bitmap.Save(filepath, ImageFormat.Jpeg);
                    }
                    else
                    {
                        img.Save(filepath);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        public Image GetReducedImage(Image ResourceImage, int Width, int Height)
        {
            try
            {
                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(ResourceImage, new Rectangle(0, 0, Width, Height));
                return bitmap;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
    }
}
