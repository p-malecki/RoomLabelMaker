using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace RoomLabelMakerApp.Models;

public partial class ImageObjectModel
{
    
    public static BitmapImage? UrlToBitmap(string path)
    {
        var bitmap = new BitmapImage();
        try
        {
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            return bitmap;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string BitmapToUrl(BitmapImage? bitmapImage)
    {
        if (bitmapImage == null)
            return "";

        try
        {
            var uriString = bitmapImage.UriSource?.AbsoluteUri;
            return uriString ?? "";
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string GetPathToLogo()
    {
        var openFileDialog = new OpenFileDialog();
        var filePath = "";

        if (openFileDialog.ShowDialog() == true)
        {
            filePath = openFileDialog.FileName;
        }
        return filePath;
    }
    
}