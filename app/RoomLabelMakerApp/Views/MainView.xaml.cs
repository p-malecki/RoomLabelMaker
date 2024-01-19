using RoomLabelMakerApp.ViewModels;
using System.Windows;

namespace RoomLabelMakerApp
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }

}