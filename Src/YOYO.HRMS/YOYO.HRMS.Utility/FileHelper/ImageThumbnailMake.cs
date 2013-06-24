using System.Drawing;

namespace YOYO.HRMS.Utility
{
/// <summary>
///pic_zip 图片缩略图生成类
/// </summary>
public class ImageThumbnailMake
{
    /// <summary>
    /// 图片缩略图生成算法
    /// </summary>
    /// <param name="intWidth">宽度</param>
    /// <param name="intHeight">高度</param>
    /// <param name="inputImgFile">文件路径</param>
    /// <param name="outImgFile">保存文件路径</param>
    /// <param name="filename">文件名</param>
    /// <returns></returns>
  public static bool MakeThumbnail(int intWidth, int intHeight, string inputImgFile, string outImgFile, string filename)
    {
        Image oldimage = Image.FromFile(inputImgFile + filename);
        float newWidth; // 新的宽度    
        float newHeight; // 新的高度    
        int flat = 0;//标记图片是不是等比    


        int xPoint = 0;//若果要补白边的话，原图像所在的x，y坐标。    
        int yPoint = 0;
        //判断图片    

        float oldWidth = oldimage.Width;
        float oldHeight = oldimage.Height;

        if ((oldWidth / oldHeight) > (intWidth / (float)intHeight)) //当图片太宽的时候    
        {
            newHeight = oldHeight * (intWidth / oldWidth);
            newWidth = intWidth;
            //此时x坐标不用修改    
            yPoint = (int)((intHeight - newHeight) / 2);
            flat = 1;
        }
        else if ((oldimage.Width / oldimage.Height) == (intWidth / (float)intHeight))
        {
            newWidth = intWidth;
            newHeight = intHeight;
        }
        else
        {
            newWidth = oldimage.Width * (intHeight / (float)oldimage.Height);  //太高的时候   
            newHeight = intHeight;
            //此时y坐标不用修改    
            xPoint = (int)((intWidth - newWidth) / 2);
            flat = 1;
        }

        // ＝＝＝缩小图片＝＝＝    
        //调用缩放算法
        Image thumbnailImage = Makesmallimage(oldimage, (int)newWidth,(int) newHeight);
        Bitmap bm = new Bitmap(thumbnailImage);

        if (flat != 0)
        {
            Bitmap bmOutput = new Bitmap(intWidth, intHeight);
            Graphics gc = Graphics.FromImage(bmOutput);
            SolidBrush tbBg = new SolidBrush(Color.White);
            gc.FillRectangle(tbBg, 0, 0, intWidth, intHeight); //填充为白色    


            gc.DrawImage(bm, xPoint, yPoint, (int)newWidth, (int)newHeight);
            bmOutput.Save(outImgFile + filename);
        }
        else
        {
            bm.Save(outImgFile + filename);
        }
        oldimage.Dispose();
        return true;
    }

  /// <summary>
    /// 生成缩略图 (高清缩放)
  /// </summary>
  /// <param name="originalImage">原图片</param>
  /// <param name="width">缩放宽度</param>
  /// <param name="height">缩放高度</param>
  /// <returns></returns>
    public static Image Makesmallimage(Image originalImage, int width, int height)
    {
        
        int towidth = 0;
        int toheight = 0;
        if (originalImage.Width > width && originalImage.Height < height)
        {
            towidth = width;
            toheight = originalImage.Height;
        }

        if (originalImage.Width < width && originalImage.Height > height)
        {
            towidth = originalImage.Width;
            toheight = height;
        }
        if (originalImage.Width > width && originalImage.Height > height)
        {
            towidth = width;
            toheight = height;
        }
        if (originalImage.Width < width && originalImage.Height < height)
        {
            towidth = originalImage.Width;
            toheight = originalImage.Height;
        }
        int x = 0;//左上角的x坐标 
        int y = 0;//左上角的y坐标 


        //新建一个bmp图片 
        Image bitmap = new Bitmap(towidth, toheight);

        //新建一个画板 
        Graphics g = Graphics.FromImage(bitmap);

        //设置高质量插值法 
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, x, y, towidth, toheight);
        originalImage.Dispose();
        //bitmap.Dispose();
        g.Dispose();
         return bitmap;
           
        
    }

    /// <summary> 
    /// 生成缩略图 (没有补白)
    /// </summary> 
    /// <param name="originalImagePath">源图路径（物理路径）</param> 
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
    /// <param name="width">缩略图宽度</param> 
    /// <param name="height">缩略图高度</param>   
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
    {
        Image originalImage = Image.FromFile(originalImagePath);

        int towidth = 0;
        int toheight = 0;
        if (originalImage.Width > width && originalImage.Height < height)
        {
            towidth = width;
            toheight = originalImage.Height;
        }

        if (originalImage.Width < width && originalImage.Height > height)
        {
            towidth = originalImage.Width;
            toheight = height;
        }
        if (originalImage.Width > width && originalImage.Height > height)
        {
            towidth = width;
            toheight = height;
        }
        if (originalImage.Width < width && originalImage.Height < height)
        {
            towidth = originalImage.Width;
            toheight = originalImage.Height;
        }
        int x = 0;//左上角的x坐标 
        int y = 0;//左上角的y坐标 


        //新建一个bmp图片 
        Image bitmap = new Bitmap(towidth, toheight);

        //新建一个画板 
        Graphics g = Graphics.FromImage(bitmap);

        //设置高质量插值法 
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, x, y, towidth, toheight);

        try
        {
            
            bitmap.Save(thumbnailPath);
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }

   

}

}



