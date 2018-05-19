using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace mvc.Helpers
{
    public static class Thumbnail
    {
        public static void GenerateThumbnail(FileInfo file, Size size, DirectoryInfo dest)
        {
            //Image photo = new Bitmap(file.ToString());

            //Bitmap bmp = new Bitmap(size.Width, size.Height);

            //var graphic = Graphics.FromImage(bmp);
            //graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //graphic.SmoothingMode = SmoothingMode.HighQuality;
            //graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //graphic.CompositingQuality = CompositingQuality.HighQuality;
            //graphic.DrawImage(photo, 0, 0, size.Width, size.Height);

            //var destFilePath = Path.Combine(dest.ToString(), file.Name);

            //if (!dest.Exists)
            //{
            //    dest.Create();
            //}

            //bmp.Save(destFilePath);
        }
    }
}