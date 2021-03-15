using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFEMediaPickerSample.Utils;

namespace XFEMediaPickerSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                string path = result.FullPath;
                byte[] m_Byte;


                if (Device.RuntimePlatform == Device.Android)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        m_Byte = memoryStream.GetBuffer();
                    }

                    DependencyService.Get<IImageCompressionService>()
                        .CompressImage(m_Byte, path, 50);

                    resultImage.Source = ImageSource.FromStream(() => stream);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification Access", "Your device is not Android", "OK");
                }

            }
        }

        async void Button1_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}
