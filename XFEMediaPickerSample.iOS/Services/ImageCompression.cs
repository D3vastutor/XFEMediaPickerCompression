using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using XFEMediaPickerSample.Utils;

namespace XFEMediaPickerSample.iOS.Services
{
    public class ImageCompression : IImageCompressionService
    {
        public ImageCompression() { }

        public byte[] CompressImage(byte[] imageData, string destinationPath, int compressionPercentage)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            if (originalImage != null)
            {
                nfloat compressionQuality = (nfloat)(compressionPercentage / 100.0);
                var resizedImage = originalImage.AsJPEG(compressionQuality).ToArray();
                var stream = new FileStream(destinationPath, FileMode.Create);
                stream.Write(resizedImage, 0, resizedImage.Length);
                stream.Flush();
                stream.Close();

                return resizedImage;
            }

            return imageData;
        }

        private UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
                return null;

            UIImage image;
            try
            {
                image = new UIImage(NSData.FromArray(data));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PhotoProcessingService: "+ ex);
                return null;
            }

            return image;
        }
    }
}