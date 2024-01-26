using RoomLabelMakerApp.ViewModels;
using System.Windows;
using System.Windows.Controls;
using RoomLabelMakerApp.Models;

namespace RoomLabelMakerApp;


public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();


        ImgNewProject.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/empty-page.png"));
        ImgOpenProject.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/folder.png"));
        ImgSaveProject1.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/floppy-disk.png"));
        ImgLoadImage.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/media-image-plus.png"));
        ImgSaveProject2.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/floppy-disk.png"));
        ImgPrint.Source = ImageObjectModel.UrlToBitmap(Utils.PathHelper.GetAssetPath(@"/Assets/printing-page.png"));
    }

    private void ToolBar_Loaded(object sender, RoutedEventArgs e)
    {
        var toolBar = sender as ToolBar;
        if (toolBar?.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
        {
            overflowGrid.Visibility = Visibility.Collapsed;
        }

        if (toolBar?.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
        {
            mainPanelBorder.Margin = new Thickness();
        }
    }
}