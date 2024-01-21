
using Newtonsoft.Json;
using RoomLabelMakerApp.Views;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
namespace RoomLabelMakerApp.Models;

public class DoorLabelModel
{
    public int Id;
    public string Name;
    public RoomNumberObjectModel RoomNumber;
    public RoomMembersObjectModel RoomMembers;
    public LogoObjectModel Logo;

    public DoorLabelModel()
    {
        Id = 0;
        Name = "";
        RoomNumber = new();
        RoomMembers = new();
        Logo = new();
    }

    public class RootObjectSerialize
    {
        public List<ObjectBase> Objects { get; set; }

        public string SerializeToJson()
        {
            var objectsJson = new List<object>();
            foreach (var obj in Objects)
            {
                objectsJson.Add(JsonConvert.DeserializeObject(obj.Serialize())!);
            }
            return JsonConvert.SerializeObject(new { objects = objectsJson });
        }
    }
    

    public class RootObjectDeserialize
    {
        public List<Dictionary<string, object>> Objects { get; set; }

    }

    public void set_Variables(RoomNumberObjectModel room_number, RoomMembersObjectModel room_members, string logo)
    {
        RoomNumber = room_number;
        RoomMembers = room_members;
        Logo.ImageData = logo;
    }

    public void save_json_to_file(string json)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "JSON Files (*.json)|*.json";
        saveFileDialog.DefaultExt = "json";
        saveFileDialog.Title = "Save JSON File";
        
        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, json);
        }
    }
    
    public string load_json_from_file()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON Files (*.json)|*.json";
        openFileDialog.Title = "Select JSON File to Load";
        
        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            string jsonContent = File.ReadAllText(filePath);
            return jsonContent;
        }
        else
        {
            return "";
        }
    }

    public string Serialize()
    {
        RootObjectSerialize rootObject = new RootObjectSerialize
        {
            Objects = new List<ObjectBase> { RoomNumber, RoomMembers, Logo }
        };

        string resultJson = rootObject.SerializeToJson();
        return resultJson;
    }

    public void Deserialize(string json)
    {
        RootObjectDeserialize rootObjects = JsonConvert.DeserializeObject<RootObjectDeserialize>(json)!;

        foreach (var obj in rootObjects.Objects)
        {
            foreach (var kvp in obj)
            {
                switch (kvp.Key)
                {
                    case "RoomNumberObjectModel":
                        RoomNumber = JsonConvert.DeserializeObject<RoomNumberObjectModel>(kvp.Value.ToString())!;
                        break;

                    case "RoomMembersObjectModel":
                        RoomMembers = JsonConvert.DeserializeObject<RoomMembersObjectModel>(kvp.Value.ToString())!;
                        break;
                    case "LogoObjectModel":
                        Logo = JsonConvert.DeserializeObject<LogoObjectModel>(kvp.Value.ToString())!;
                        break;
                    default:
                        Console.WriteLine($"Unknown class type: {kvp.Key}");
                        break;
                }
            }
        }
    }

    public static void Print(FlowDocument flowDocument)
    {
        PrintDialog pd = new PrintDialog();
        String copyString = XamlWriter.Save(flowDocument);
        FlowDocument copy = XamlReader.Parse(copyString) as FlowDocument;

        //copy of FlowDocument to not change state of DoorLabel original Flow Document
        copy.PageHeight = pd.PrintableAreaHeight;
        copy.PageWidth = pd.PrintableAreaWidth;
        copy.PagePadding = new Thickness(30);
        copy.ColumnGap = 0;
        copy.ColumnWidth = pd.PrintableAreaWidth;

        //Create Window for Print View
        if (File.Exists("PreviewPrinting.xps"))
        {
            File.Delete("PreviewPrinting.xps");
        }

        var xpsDocument = new XpsDocument("PreviewPrinting.xps", FileAccess.ReadWrite);
        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
        writer.Write(((IDocumentPaginatorSource)copy).DocumentPaginator);
        var Document = xpsDocument.GetFixedDocumentSequence();
        xpsDocument.Close();
        var windows = new PrintPreviewView(Document);
        windows.ShowDialog();
    }
}