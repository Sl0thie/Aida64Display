// https://docs.microsoft.com/en-us/answers/questions/536343/xamarin-community-toolkit-cameraview-save-file.html

namespace Aida64Display.Views
{
    using System;
    using System.IO;

    using Xamarin.Forms;

    using Xamarin;
    using Xamarin.Essentials;
    using Xamarin.Forms.Xaml;
    using Xamarin.Forms.PlatformConfiguration;

    using System.Runtime.InteropServices.ComTypes;
    using Android.Graphics;

    using Aida64Common;
    using Aida64Common.Models;


    /// <summary>
    /// StartPage class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartPage"/> class.
        /// </summary>
        public StartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnAppearing method starts a timer the navigates to the CPU page when it expires.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();


            


            //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //{
            //    CPUPage newpage = new CPUPage();
            //    NavigationPage.SetHasNavigationBar(newpage, false);
            //    _ = Navigation.PushAsync(newpage, false);
            //    return false;
            //});
        }

        private void Cv_OnAvailable(object sender, bool e)
        {
            System.Diagnostics.Debug.WriteLine("cv_OnAvailable");

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                cv.Shutter();

                System.Diagnostics.Debug.WriteLine("Shutter");

                return true;
            });
        }

        private void Cv_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Cv_MediaCaptured {e.ImageData.Length} {Environment.CurrentDirectory}");

            try
            {
                ImageData id = new ImageData();
                id.Data = e.ImageData;
                id.FileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".jpg";
                MessagingCenter.Send(id, "SaveImage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR {ex.Message}");
            }
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap 
            BitmapFactory.Options options = new BitmapFactory.Options();// Create object of bitmapfactory's option method for further option use
            options.InPurgeable = true; // inPurgeable is used to free up memory while required
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

            float newHeight = 0;
            float newWidth = 0;

            var originalHeight = originalImage.Height;
            var originalWidth = originalImage.Width;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);

            originalImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);

                resizedImage.Recycle();

                return ms.ToArray();
            }
        }
    }
}