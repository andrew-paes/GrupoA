using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class imoveis_thumb : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // get the file name -- fall800.jpg
        string file = Request.QueryString["file"];
        Int32 w = Convert.ToInt32(Request.QueryString["w"]);
        Int32 h = Convert.ToInt32(Request.QueryString["h"]);
        
        if (!System.IO.File.Exists(Server.MapPath(file).ToString())) 
            return;

        // create an image object, using the filename we just retrieved
        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file));


        var thumbnailBitmap = new Bitmap(w, h);
        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

        var imageRectangle = new Rectangle(0, 0, w, h);
        thumbnailGraph.DrawImage(image, imageRectangle);

        MemoryStream imageStream = new MemoryStream();
        thumbnailBitmap.Save(imageStream, image.RawFormat);

        // make byte array the same size as the image
        byte[] imageContent = new Byte[imageStream.Length];

        // rewind the memory stream
        imageStream.Position = 0;

        // load the byte array with the image
        imageStream.Read(imageContent, 0, (int)imageStream.Length);

        imageStream.Close();
        image.Dispose();
        thumbnailBitmap.Dispose();
        thumbnailGraph.Dispose();

        // return byte array to caller with image type
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(imageContent);



        //// create the actual thumbnail image
        //System.Drawing.Image thumbnailImage = image.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

        //// make a memory stream to work with the image bytes
        //MemoryStream imageStream = new MemoryStream();

        //// put the image into the memory stream
        //thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        //// make byte array the same size as the image
        //byte[] imageContent = new Byte[imageStream.Length];

        //// rewind the memory stream
        //imageStream.Position = 0;

        //// load the byte array with the image
        //imageStream.Read(imageContent, 0, (int)imageStream.Length);

        //// return byte array to caller with image type
        //Response.ContentType = "image/jpeg";
        //Response.BinaryWrite(imageContent);
    }

    public bool ThumbnailCallback()
    {
        return true;
    }
}
