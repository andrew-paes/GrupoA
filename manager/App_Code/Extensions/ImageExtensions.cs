using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using Microsoft.Win32;

public enum ResizeModes
{
    Fix,
    Fit,
    Crop
}

/// <summary>
/// Summary description for ImageExtensions
/// </summary>
public static class ImageExtensions
{

    /*
    -- Exemplo de Uso --
		
    string imageFile = String.Concat("~/Uploads/modelGallery/", mediaGallery.FilePath);
    string path = HttpContext.Current.Request.MapPath(imageFile);

    System.Drawing.Image resizedImage;

    using System.Drawing.Image image = System.Drawing.Image.FromFile(path))
    {
        string thumbPath = Regex.Replace(path, "(\\.[^\\.]+)$", "-thumb$1");

        image.ResizeTo(120, 120, ResizeModes.Crop).StreamSave(thumbPath);

        resizedImage = image.ResizeTo(510, 330, ResizeModes.Crop);
    }

    resizedImage.StreamSave(path);
    resizedImage.Dispose();
    */


    /// <summary>
    /// Get incoder info
    /// </summary>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    /// <see cref="http://msdn.microsoft.com/en-us/library/system.drawing.imaging.encoder.transformation.aspx"/>
    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats.
        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec.
        for (int i = 0; i < encoders.Length; ++i)
        {
            if (encoders[i].MimeType == mimeType)
            {
                return encoders[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Get MIME type from a file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <see cref="http://kseesharp.blogspot.com/2008/04/c-get-mimetype-from-file-name.html"/>
    private static string GetMimeType(string fileName)
    {
        string mimeType = "application/unknown";
        string extension = Path.GetExtension(fileName).ToLower();
        RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(extension);

        if (regKey != null && regKey.GetValue("Content Type") != null)
        {
            mimeType = regKey.GetValue("Content Type").ToString();
        }

        return mimeType;
    }

    public static Image ResizeTo(this Image image, Nullable<int> width)
    {
        return image.ResizeTo(width, null);
    }

    public static Image ResizeTo(this Image image, Nullable<int> width, Nullable<int> height)
    {
        return image.ResizeTo(width, height, ResizeModes.Fix);
    }

    public static Image ResizeTo(this Image image, Nullable<int> width, Nullable<int> height, ResizeModes resizeMode)
    {
        if (!width.HasValue && !height.HasValue)
        {
            throw new Exception("Width or height must have a value");
        }
        else if (!width.HasValue)
        {
            width = image.Width * height.Value / image.Height;
        }
        else if (!height.HasValue)
        {
            height = image.Height * width.Value / image.Width;
        }

        Size size = new Size(width.Value, height.Value);

        return image.ResizeTo(size, resizeMode);
    }

    /// <summary>
    /// Resize the image cropping image properly
    /// </summary>
    /// <param name="image"></param>
    /// <param name="size"></param>
    /// <param name="resizeMode"></param>
    /// <returns></returns>
    /// <see cref="http://michael.sivers.co.uk/post/2007/08/20/Crop-and-resize-an-image-in-ASPNET.aspx"/>
    /// <see cref="http://www.eggheadcafe.com/articles/20030515.asp"/>
    /// <see cref="http://getlara.com/north-america/canada/alberta/edmonton/post/2008/10/13/png-jpg-gif-image-resize-with-net-with-transparency"/>
    public static Image ResizeTo(this Image image, Size size, ResizeModes resizeMode)
    {
        // Graphics objects can not be created from bitmaps
        // with an Indexed Pixel Format, use RGB instead.
        PixelFormat format = image.PixelFormat;

        if (format.ToString().Contains("Indexed"))
        {
            format = PixelFormat.Format24bppRgb;
        }

        float ratio = 1;
        float xRatio = (float)size.Width / (float)image.Width;
        float yRatio = (float)size.Height / (float)image.Height;
        int newX = 0;
        int newY = 0;

        // Create a fit thumbnail, both sides need to be smaller
        // than the given values, save dimensions.
        if (resizeMode == ResizeModes.Fit)
        {
            ratio = Math.Min(xRatio, yRatio);
        }

        // Creates a fixed size thumbnail by first scaling the image
        // up or down and cropping a specified area from the center.
        else if (resizeMode == ResizeModes.Crop)
        {
            if (xRatio > yRatio)
            {
                ratio = xRatio;
                newY = Convert.ToInt32((size.Height - image.Height * ratio) / 2);
            }
            else
            {
                ratio = yRatio;
                newX = Convert.ToInt32((size.Width - image.Width * ratio) / 2);
            }
        }

        int newWidth = Convert.ToInt32(image.Width * ratio);
        int newHeight = Convert.ToInt32(image.Height * ratio);

        if (resizeMode == ResizeModes.Fit)
        {
            size.Width = newWidth;
            size.Height = newHeight;
        }

        Bitmap newImage = new Bitmap(size.Width, size.Height, image.PixelFormat);

        newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics canvas = Graphics.FromImage(newImage))
        {
            canvas.CompositingQuality = CompositingQuality.HighQuality;
            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
            canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
            canvas.SmoothingMode = SmoothingMode.AntiAlias;

            if (resizeMode == ResizeModes.Crop)
            {
                canvas.DrawImage(image,
                    new Rectangle(newX, newY, newWidth, newHeight),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            else
            {
                canvas.DrawImage(image, 0, 0, size.Width, size.Height);
            }
        }

        return newImage;
    }

    public static void StreamSave(this Image image, string path)
    {
        image.StreamSave(path, 100);
    }

    /// <summary>
    /// Save image avoiding GDI+ generic errors
    /// </summary>
    /// <param name="path"></param>
    /// <param name="img"></param>
    /// <param name="quality"></param>
    public static void StreamSave(this Image image, string path, int quality)
    {
        EncoderParameter qualityParameter = new EncoderParameter(Encoder.Quality, quality);
        string mimeType = ImageExtensions.GetMimeType(path);
        ImageCodecInfo codec = ImageExtensions.GetEncoderInfo(mimeType);
        EncoderParameters encoderParams = new EncoderParameters(1);

        encoderParams.Param[0] = qualityParameter;

        MemoryStream memoryStream = new MemoryStream();
        
        image.Save(memoryStream, codec, encoderParams);

        if (File.Exists(path))
        {
            image.Dispose();
        }

        byte[] matrix = memoryStream.ToArray();

        memoryStream.Close();

        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

        fileStream.Write(matrix, 0, matrix.Length);
        fileStream.Close();
    }

}