using Aida64Display.Droid.Services;

using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SaveService))]
namespace Aida64Display.Droid.Services
{
    public class SaveService : ISaveService
    {
        void ISaveService.SaveFile(string fileName, byte[] data)
        {
            string picPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryPictures);
            string filePath = Path.Combine(picPath, fileName);
            File.WriteAllBytes(filePath, data);
        }
    }
}