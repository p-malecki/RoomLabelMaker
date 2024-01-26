using System.IO;
using Microsoft.Win32;
namespace RoomLabelMakerApp.Utils;

public static class JsonUtils
{
    public static void SaveJsonToFile(string json)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "JSON Files (*.json)|*.json",
            DefaultExt = "json",
            Title = "Save JSON File"
        };

        if (saveFileDialog.ShowDialog() != true)
            return;

        var filePath = saveFileDialog.FileName;
        File.WriteAllText(filePath, json);
    }

    public static string LoadJsonFromFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "JSON Files (*.json)|*.json",
            Title = "Select JSON File to Load"
        };

        if (openFileDialog.ShowDialog() != true)
            return "";

        var filePath = openFileDialog.FileName;
        var jsonContent = File.ReadAllText(filePath);
        return jsonContent;
    }
}