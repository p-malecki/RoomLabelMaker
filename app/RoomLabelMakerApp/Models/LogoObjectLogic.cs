using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using Microsoft.Win32;

namespace RoomLabelMakerApp.Models;

public partial class LogoObjectModel : ObjectBase, IContentObject
{
    
    public LogoObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        ImageData = defaultpicture;
    }

    public static BitmapImage url_to_bitmap(string path)
    {
        BitmapImage bitmap = new BitmapImage();

        try
        {
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            return bitmap;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public static string get_path_to_logo(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string filePath = " ";
        if (openFileDialog.ShowDialog() == true)
        {
            filePath = openFileDialog.FileName;
        }
        return filePath;
    }
    

    public override string Serialize()
    {
        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        var propertyValues = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(this));

        return JsonConvert.SerializeObject(new { LogoObjectModel = propertyValues });
    }
}