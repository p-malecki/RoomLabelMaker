using System.Windows;
using System.Windows.Documents;

namespace RoomLabelMakerApp.Views
{
    /// <summary>
    /// Interaction logic for PrintPreviewView.xaml
    /// </summary>
    public partial class PrintPreviewView : Window
    {
        private FixedDocumentSequence _documnentPreview;

        public PrintPreviewView(FixedDocumentSequence document)
        {
            _documnentPreview = document;
            InitializeComponent();
            PreviewDocument.Document = document;
        }
    }
}
