using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace mvc.Helpers
{
    /// <summary>
    /// This class summarizes all possible operations applicable to an image.
    /// </summary>
    public class ImageUtil
    {
        /// <summary>
        /// This method convert an image's bytes array to a based 64 String.
        /// </summary>
        /// <param name="bytesArray">Image's bytes in an array.</param>
        /// <returns>Based 64 String.</returns>
        public static String ConvertArrayOfBytesToStringBase64(byte[] bytesArray)
        {
            return System.Convert.ToBase64String(bytesArray, 0, bytesArray.Length);
        }

        /// <summary>
        /// This method loads an image to an array of bytes.
        /// </summary>
        /// <param name="fileFullPath">Full path (path + file name) of the image.</param>
        /// <returns>Image's bytes in an array.</returns>
        public static byte[] ConvertImageToArrayOfBytes(String fileFullPath)
        {
            FileStream fS = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);

            byte[] imgBytes = new byte[fS.Length];
            fS.Read(imgBytes, 0, (int)fS.Length);

            fS.Close();

            return imgBytes;
        }

        /// <summary>
        /// This method returns the extension of the image's file.
        /// </summary>
        /// <param name="fileFullPath">Full path (path + file name) of the image.</param>
        /// <returns>The extension of the image's file.</returns>
        public static String GetImageExtension(String fileFullPath)
        {
            return Path.GetExtension(fileFullPath);
        }

        /// <summary>
        /// This method generates an HTML &lt;img /&gt; tag with the embedded image data.
        /// </summary>
        /// <param name="fileExtension">The extension of the image's file.</param>
        /// <param name="imgInBase64">The image's bytes in a based 64 String.</param>
        /// <returns>An HTML &lt;img /&gt; tag with embedded image data.</returns>
        public static String GenerateHTMLImgTag(String fileExtension, String imgInBase64)
        {
            return "<img alt=\"Embedded ImageUtil\" src=\"data:image/" + fileExtension + ";base64," + imgInBase64 + "\" />";
        }

        /// <summary>
        /// This method resizes the image to a specified size.
        /// </summary>
        /// <param name="inFileFullPath">Full path (path + file name) of the original image.</param>
        /// <param name="outFileFullPath">Full path (path + file name) of the resized image.</param>
        /// <param name="newImagSize">Wanted size.</param>
        /// <returns></returns>
        public static Boolean ResizeImageTo(String inFileFullPath, String outFileFullPath, Size newImagSize)
        {
            System.Drawing.Image originalImage = new Bitmap(inFileFullPath);

            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;

            if (originalWidth > newImagSize.Width || originalHeight > newImagSize.Height)
            {
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)newImagSize.Width / (float)originalWidth);
                nPercentH = ((float)newImagSize.Height / (float)originalHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(originalWidth * nPercent);
                int destHeight = (int)(originalHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(originalImage, 0, 0, destWidth, destHeight);
                g.Dispose();
                b.Save(outFileFullPath, ImageFormat.Jpeg);

                b.Dispose();

                return true;
            }

            return false;
        }
    }
}