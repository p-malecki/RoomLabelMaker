using Newtonsoft.Json;
using RoomLabelMakerApp.Views;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
namespace RoomLabelMakerApp.Models;


public class DoorLabelModel
{
    //public string ProjectName = ""; // TODO add project name
    public TextObjectModel? RoomNumber;
    public TextObjectModel? RoomMembers;
    public ImageObjectModel? Image;


    public void SetData(TextObjectModel roomNumberModel, TextObjectModel roomMembersModel, string logo)
    {
        RoomNumber = roomNumberModel;
        RoomMembers = roomMembersModel;
        Image = new ImageObjectModel(logo);
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    public void Deserialize(string json)
    {
        var d = JsonConvert.DeserializeObject<DoorLabelModel>(json);
        if (d == null) return;
        RoomNumber = d.RoomNumber;
        RoomMembers = d.RoomMembers;
        Image = d.Image;
    }

    public static void Print(FlowDocument flowDocument)
    {
        var pd = new PrintDialog();
        var stringCopy = XamlWriter.Save(flowDocument); // copy to prevent changes to the original Flow Document
        if (XamlReader.Parse(stringCopy) is not FlowDocument flowDocumentCopy)
            return;

        flowDocumentCopy.PageHeight = pd.PrintableAreaHeight;
        flowDocumentCopy.PageWidth = pd.PrintableAreaWidth;
        flowDocumentCopy.PagePadding = new Thickness(30);
        flowDocumentCopy.ColumnGap = 0;
        flowDocumentCopy.ColumnWidth = pd.PrintableAreaWidth;

        // create window for print view
        if (File.Exists("PreviewPrinting.xps"))
        {
            File.Delete("PreviewPrinting.xps");
        }

        FixedDocumentSequence? document;
        using (var xpsDocument = new XpsDocument("PreviewPrinting.xps", FileAccess.ReadWrite))
        {
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)flowDocumentCopy).DocumentPaginator);
            document = xpsDocument.GetFixedDocumentSequence();
        }

        if (document == null) return;
        var windows = new PrintPreviewView(document);
        windows.ShowDialog();
    }
}