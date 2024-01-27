using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RoomLabelMakerApp.Views
{
    /// <summary>
    /// Interaction logic for PrintPreviewView.xaml
    /// </summary>
    public partial class PrintPreviewView : Window
    {
        public PrintPreviewView(FixedDocumentSequence document)
        {
            InitializeComponent();

            PreviewDocument.Document = document;
            PreviewDocument.FitToHeight();
        }
    }
}
