using Newtonsoft.Json;
using RoomLabelMakerApp.Views;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace RoomLabelMakerApp.Models;

public class DoorLabelModel
{
    public class RootObject
    {
        public List<ObjectBase> Objects { get; set; }

        public string SerializeToJson()
        {
            var objectsJson = new List<object>();
            foreach (var obj in Objects)
            {
                objectsJson.Add(JsonConvert.DeserializeObject(obj.Serialize()));
            }
            return JsonConvert.SerializeObject(new { objects = objectsJson });
        }
    }

    public int Id;
    public string Name;
    public RoomNumberObjectModel RoomNumber;
    public RoomMembersObjectModel RoomMembers;
    public LogoObjectModel Logo;

    public string Serialize()
    {
        RootObject rootObject = new RootObject
        {
            Objects = new List<ObjectBase> { RoomNumber, RoomMembers, Logo}
        };

        string resultJson = rootObject.SerializeToJson();
        return resultJson;
    }

    public void Deserialize(string json)
    {
        List<Dictionary<string, object>> rootObjectList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

        foreach (var obj in rootObjectList)
        {
            foreach (var kvp in obj)
            {
                switch (kvp.Key)
                {
                    case "RoomNumberObjectModel":
                        RoomNumber = JsonConvert.DeserializeObject<RoomNumberObjectModel>(kvp.Value.ToString());
                        break;

                    case "RoomMembersObjectModel":
                        RoomMembers = JsonConvert.DeserializeObject<RoomMembersObjectModel>(kvp.Value.ToString());
                        break;
                    case "LogoObjectModel":
                        Logo = JsonConvert.DeserializeObject<LogoObjectModel>(kvp.Value.ToString());
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