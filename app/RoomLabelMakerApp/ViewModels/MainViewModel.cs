using RoomLabelMakerApp.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RoomLabelMakerApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _exampleProperty;


    public MainViewModel()
    {
        ExampleProperty = "";
        ExampleCommand = new RelayCommand(ExampleMethod);
    }


    #region Properties

    public string ExampleProperty
    {
        get => _exampleProperty;
        set
        {
            _exampleProperty = value;
            OnPropertyChanged();
        }
    }

    #endregion // Properties


    #region Commands

    public ICommand ExampleCommand { get; set; }

    #endregion // Properties


    #region Methods

    private void ExampleMethod(object obj) // obj is passed by CommandParameter
    {
        var passedData = obj as string;
        MessageBox.Show("Clicked!\n" + passedData);

        ExampleProperty = passedData;
    }

    #endregion // Methods


    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}