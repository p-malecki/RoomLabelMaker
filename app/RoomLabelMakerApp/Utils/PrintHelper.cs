using RoomLabelMakerApp.Views;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows;
using System.Windows.Markup;
namespace RoomLabelMakerApp.Utils;

public class PrintHelper
{
    public static void Print(FlowDocument flowDocument, int numberOfCopies)
    {
        var pd = new PrintDialog();
        var stringCopy = XamlWriter.Save(flowDocument); // copy to prevent changes to the original Flow Document
        if (XamlReader.Parse(stringCopy) is not FlowDocument flowDocumentCopy)
            return;

        CloneBlockInFlowDocument(flowDocumentCopy, numberOfCopies - 1);

        flowDocumentCopy.PageHeight = pd.PrintableAreaHeight;
        flowDocumentCopy.PageWidth = pd.PrintableAreaWidth;
        flowDocumentCopy.PagePadding = new Thickness(30);
        flowDocumentCopy.ColumnGap = 0;
        flowDocumentCopy.ColumnWidth = pd.PrintableAreaWidth;


        try
        {
            // create window for print view
            if (File.Exists("PreviewPrinting.xps"))
                File.Delete("PreviewPrinting.xps");
        }
        catch
        {
            // ignored
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

    private static void CloneBlockInFlowDocument(FlowDocument flowDocumentCopy, int numberOfCopies)
    {
        var block = flowDocumentCopy.Blocks.FirstBlock;
        if (block == null) return;

        for (var i = 0; i < numberOfCopies; i++)
        {
            var xamlString = XamlWriter.Save(block);
            if (XamlReader.Parse(xamlString) is not Block clonedBlock) return;
            flowDocumentCopy.Blocks.Add(clonedBlock);
        }
    }
}