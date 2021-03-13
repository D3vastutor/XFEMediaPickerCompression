using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XFEMediaPickerSample.Droid.Services;
using XFEMediaPickerSample.Utils;

[assembly: Dependency(typeof(ImageCompression))]
namespace XFEMediaPickerSample.Droid.Services
{
    public class ImageCompression : IImageCompressionService
    {
        public ImageCompression() { }

        public byte[] CompressImage(byte[] imageData, string destinationPath, int compressionPercentage)
        {
            var resizedImage = GetResizedImage(imageData, compressionPercentage);
            var stream = new FileStream(destinationPath, FileMode.Create);
            stream.Write(resizedImage, 0, resizedImage.Length);
            stream.Flush();
            stream.Close();
            return resizedImage;
        }

        private object GetResizedImage(byte[] imageData, int compressionPercentage)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            using (MemoryStream ms = new MemoryStream())
            {
                originalImage.Compress(Bitmap.CompressFormat.Jpeg, compressionPercentage, ms);
                return ms.ToArray();
            }
        }
    }
}