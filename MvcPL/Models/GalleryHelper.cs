﻿using System;
using System.Drawing;
using System.IO;

namespace MvcPL.Models
{
    public static class GalleryHelper
    {
        public static Image CutImage(Image target, int width, int height)
        {
            Bitmap bmpImage = new Bitmap(target);
            int size = Math.Min(target.Width, target.Height);
            var rect = new Rectangle((target.Width - size) / 2, (target.Height - size) / 2, size, size);
            var img = bmpImage.Clone(rect, bmpImage.PixelFormat);

            var newSize = new Size(width, height);
            var imgSize = NewImageSize(img.Size, newSize);
            return new Bitmap(img, imgSize.Width, imgSize.Height);
        }

        public static Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}