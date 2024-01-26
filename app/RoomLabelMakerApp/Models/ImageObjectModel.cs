using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoomLabelMakerApp.Models;

public partial class ImageObjectModel(string imageData) : ObjectBase(0, 0, 0, 0), INotifyPropertyChanged
{
    public string ImageData
    {
        get => imageData;
        set
        {
            imageData = value;
            OnPropertyChanged(nameof(ImageData));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}