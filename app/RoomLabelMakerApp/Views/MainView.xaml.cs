using RoomLabelMakerApp.ViewModels;
using RoomLabelMakerApp.Models;
using System.Windows.Media;
using System.Windows;
using RoomLabelMakerApp.Models;
using System.Windows.Controls;

namespace RoomLabelMakerApp
{
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
            string path = Models.LogoObjectModel.get_path_to_logo(sender, e);
            logo.Source = Models.LogoObjectModel.url_to_bitmap(path);
            image_data = path;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            doorlabelmodel.set_Variables(new RoomNumberObjectModel(room_number,txtRoomNumber1.FontFamily.ToString(),txtRoomNumber1.FontSize.ToString())
                ,new RoomMembersObjectModel(room_names, txtNames1.FontFamily.ToString(), txtNames1.FontSize.ToString()), image_data);
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
                txtRoomNumber1.FontFamily = new FontFamily(doorlabelmodel.RoomNumber.FontStyle);
                txtRoomNumber1.FontSize = Convert.ToDouble(doorlabelmodel.RoomNumber.FontSize);
                room_names = doorlabelmodel.RoomMembers.Text;
                txtNames1.FontFamily = new FontFamily(doorlabelmodel.RoomMembers.FontStyle);
                txtNames1.FontSize = Convert.ToDouble(doorlabelmodel.RoomMembers.FontSize);
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

        private void cmbFontSizeRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cmbFontSizeRoomNumber.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                if (int.TryParse(selectedValue, out int fontSize))
                {
                    txtRoomNumber1.FontSize = fontSize;
                }
            }
        }

        private void cmbFontFamilyRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cmbFontFamilyRoomNumber.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                txtRoomNumber1.FontFamily = new FontFamily(selectedValue);
            }
        }
        
        private void cmbFontSizeNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cmbFontSizeNames.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                if (int.TryParse(selectedValue, out int fontSize))
                {
                    txtNames1.FontSize = fontSize;
                }
            }
        }

        private void cmbFontFamilyNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cmbFontFamilyNames.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                txtNames1.FontFamily = new FontFamily(selectedValue);
            }
        }
        
    }
}