using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Imagem
/// </summary>

namespace ImagemThumb
{
    public class ImagemThumb
    {
        public ImagemThumb()
        {
        }

        public void GerarProporcao(string pathOrigem, int width, int height, System.Drawing.Color color)
        {
            if (System.IO.File.Exists(pathOrigem))
            {
                int posX = 0;
                int posY = 0;
                int vWidth = 0;
                int vHeight = 0;
                double dblDiffWidth = 0;
                double dblDiffHeight = 0;

                System.Drawing.Image imgPhotoVert = System.Drawing.Image.FromFile(pathOrigem);

                //calcula a altura baseado na largura
                if (height == 0)
                {
                    if (imgPhotoVert.Width > width)
                    {
                        height = imgPhotoVert.Height / (imgPhotoVert.Width / width);
                    }
                    else
                    {
                        height = imgPhotoVert.Height * (width / imgPhotoVert.Width);
                    }
                }

                System.Drawing.Bitmap bit = new System.Drawing.Bitmap(width, height);

                //calcula a proporção
                dblDiffWidth = (double)width / (double)imgPhotoVert.Width;
                dblDiffHeight = (double)height / (double)imgPhotoVert.Height;

                if (dblDiffHeight < dblDiffWidth)
                {
                    vHeight = height;
                    vWidth = Convert.ToInt32(imgPhotoVert.Width * dblDiffHeight);
                    posX = ((width - vWidth) / 2);
                    posY = 0;
                }
                else
                {
                    vWidth = width;
                    vHeight = Convert.ToInt32(imgPhotoVert.Height * dblDiffWidth);
                    posX = 0;
                    posY = ((height - vHeight) / 2);
                }

                System.Drawing.Rectangle oRectangle = new System.Drawing.Rectangle(posX, posY, vWidth, vHeight);
                System.Drawing.Graphics gra = System.Drawing.Graphics.FromImage(bit);
                gra.Clear(color);
                gra.DrawImage(imgPhotoVert, oRectangle);
                gra.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gra.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                imgPhotoVert.Dispose();
                bit.Save(pathOrigem, System.Drawing.Imaging.ImageFormat.Jpeg);
                bit.Dispose();
            }
        }
    }
}