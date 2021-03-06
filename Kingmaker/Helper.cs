﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingmaker
{
    static class Helper
    {
        public static string GetStringFromImage(Image image)
        {
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(
                    buffer,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            return null;

        }

        public static Image GetImageFromString(string base64String)
        {
            byte[] buffer = Convert.FromBase64String(base64String);

            if (buffer != null)
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as Image;
            }
            return null;

        }
    }
}
