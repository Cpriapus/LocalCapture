using System.Drawing.Imaging;

namespace LocalCapture.Core
{
    internal static class PicEffect
    {
        //int r = 0, g = 0, b = 0;
        //底片效果
        //r = 255 - pixel.R;
        //g = 255 - pixel.G;
        //b = 255 - pixel.B;

        //浮雕效果
        //注意像素点越界
        //pixel1 = oldBitmap.GetPixel(x, y);
        //pixel2 = oldBitmap.GetPixel(x + 1, y + 1);
        //r = Math.Abs(pixel1.R - pixel2.R + 128);
        //g = Math.Abs(pixel1.G - pixel2.G + 128);
        //b = Math.Abs(pixel1.B - pixel2.B + 128);
        //if (r > 255)
        //    r = 255;
        //if (r < 0)
        //    r = 0;
        //if (g > 255)
        //    g = 255;
        //if (g < 0)
        //    g = 0;
        //if (b > 255)
        //    b = 255;
        //if (b < 0)
        //    b = 0;
        //黑白效果
        //最大值法: 使每个像素点的 R, G, B 值等于原像素点的 RGB (颜色值) 中最大的一个
        //平均值法: 使用每个像素点的 R,G,B值等于原像素点的RGB值的平均值
        //加权平均值法: 对每个像素点的 R, G, B值进行加权
        //switch (iType)
        //{
        //    case 0://平均值法
        //        Result = ((r + g + b) / 3);
        //        break;
        //    case 1://最大值法
        //        Result = r > g ? r : g;
        //        Result = Result > b ? Result : b;
        //        break;
        //    case 2://加权平均值法
        //        Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
        //        break;
        //}
        //newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
        //柔化效果
        //int[] Gauss ={ 1, 2, 1, 2, 4, 2, 1, 2, 1 };
        //for (int x = 1; x < Width - 1; x++)
        //    for (int y = 1; y < Height - 1; y++)
        //    {
        //        int r = 0, g = 0, b = 0;
        //        int Index = 0;
        //        for (int col = -1; col <= 1; col++)
        //            for (int row = -1; row <= 1; row++)
        //            {
        //                pixel = MyBitmap.GetPixel(x + row, y + col);
        //                r += pixel.R * Gauss[Index];
        //                g += pixel.G * Gauss[Index];
        //                b += pixel.B * Gauss[Index];
        //                Index++;
        //            }
        //        r /= 16;
        //        g /= 16;
        //        b /= 16;
        //        //处理颜色值溢出
        //        r = r > 255 ? 255 : r;
        //        r = r < 0 ? 0 : r;
        //        g = g > 255 ? 255 : g;
        //        g = g < 0 ? 0 : g;
        //        b = b > 255 ? 255 : b;
        //        b = b < 0 ? 0 : b;
        //        bitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
        //    }
        //this.pictureBox1.Image = bitmap;

        static int Index, r, g, b = 0;
        static Bitmap? GaussBitmap = null;
        static Color Pixel = new Color();
        //高斯模糊
        public static Bitmap Gauss(Bitmap CaptrueScreen)
        {
            GaussBitmap = new Bitmap(CaptrueScreen.Width, CaptrueScreen.Height);

            int[] GaussArray = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            for (int x = 1; x < CaptrueScreen.Width - 1; x++)
            {
                for (int y = 1; y < CaptrueScreen.Height - 1; y++)
                {
                    r = g = b = Index = 0;
                    for (int pc = -1; pc < 2; pc++)
                    {
                        for (int pr = -1; pr < 2; pr++)
                        {
                            Pixel = CaptrueScreen.GetPixel(x + pc, y + pr);
                            r += Pixel.R * GaussArray[Index];
                            g += Pixel.G * GaussArray[Index];
                            b += Pixel.B * GaussArray[Index];
                            Index++;
                        }
                    }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                    r = r > 255 ? 255 : r;
                    g = g > 255 ? 255 : g;
                    b = b > 255 ? 255 : b;
                    GaussBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            }
            return GaussBitmap;
        }
        //加白
        public static Bitmap Whiter(Bitmap CaptrueScreen)
        {
            int height = CaptrueScreen.Height;
            int width = CaptrueScreen.Width;
            Bitmap WhiterBitmap = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    r = g = b = Index = 0;
                    Pixel = CaptrueScreen.GetPixel(x, y);
                    r += (int)(Pixel.R * 0.8);
                    g += (int)(Pixel.G * 0.8);
                    b += (int)(Pixel.B * 0.8);
                    r = r > 255 ? 255 : r;
                    g = g > 255 ? 255 : g;
                    b = b > 255 ? 255 : b;
                    WhiterBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return WhiterBitmap;
        }
        //九宫格白点
        public static Bitmap NineWhiter(Bitmap CaptrueScreen)
        {
            int height = CaptrueScreen.Height;
            int width = CaptrueScreen.Width;
            for (int x = 1; x < width; x += 2)
            {
                for (int y = 1; y < height; y += 2)
                {
                    r = g = b = 255;
                    CaptrueScreen.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return CaptrueScreen;
        }
        //低分辨率
        public static Bitmap LowPix(Bitmap CaptrueScreen)
        {
            return new Bitmap(CaptrueScreen, (int)(CaptrueScreen.Width * 0.5), (int)(CaptrueScreen.Height * 0.5));
        }
        //内存法
        public static Bitmap? MemoryLowPix(Bitmap CaptrueScreen)
        {
            Bitmap? newbitmap = CaptrueScreen.Clone() as Bitmap;
#pragma warning disable CS8602 // 解引用可能出现空引用。
            Rectangle rect = new(0, 0, newbitmap.Width, newbitmap.Height);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            BitmapData bmpdata = newbitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = newbitmap.Width * newbitmap.Height * 3;
            byte[] rgbvalues = new byte[bytes];
            //复制从ptr开始到bytes位置的数据到rgbvalues
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes);
            for (int i = 0; i < rgbvalues.Length; i++)
            {
                rgbvalues[i] = (byte)(rgbvalues[i] * 0.8);
            }
            //从0位置复制rgbvalues到ptr位置长度为bytes
            System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes);
            newbitmap.UnlockBits(bmpdata);
            return newbitmap.Clone() as Bitmap;
        }
    }
}
