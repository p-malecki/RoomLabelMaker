using RoomLabelMakerApp.ViewModels;
using RoomLabelMakerApp.Models;
using System.Windows.Media.Imaging;
using System.Windows;

namespace RoomLabelMakerApp
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        
        private string room_number = string.Empty;
        private string room_names = string.Empty;
        private string image_data = string.Empty;
        private DoorLabelModel doorlabelmodel = new DoorLabelModel();
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        
        private void Numbers_TextChanged(object sender, RoutedEventArgs e)
        {
            room_number = txtRoomNumber.Text;
            txtRoomNumber1.Text = room_number;
        }
        
        private void Names_TextChanged(object sender, RoutedEventArgs e)
        {
            room_names = txtNames.Text;
            txtNames1.Text = room_names;
        }
        
        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            string path = Models.LogoObjectModel.get_path_to_logo(sender,e);
            logo.Source = Models.LogoObjectModel.url_to_bitmap(path);
            image_data = path;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            doorlabelmodel.set_Variables(room_number,room_names,image_data);
            var result = doorlabelmodel.Serialize();
            doorlabelmodel.save_json_to_file(result);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string json = doorlabelmodel.load_json_from_file();
            if (json != "")
            {
                doorlabelmodel.Deserialize(json);
                room_number = doorlabelmodel.RoomNumber.Text;
                room_names = doorlabelmodel.RoomMembers.Text;
                image_data = doorlabelmodel.Logo.ImageData;
            }
            refresh_screen();
        }

        private void refresh_screen()
        {
            txtRoomNumber1.Text = room_number;
            txtNames1.Text = room_names;
            txtRoomNumber.Text = room_number;
            txtNames.Text = room_names;
            logo.Source = Models.LogoObjectModel.url_to_bitmap(image_data);
        }
    }

}