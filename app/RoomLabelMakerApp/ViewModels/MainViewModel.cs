using RoomLabelMakerApp.Commands;
using RoomLabelMakerApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RoomLabelMakerApp.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
namespace RoomLabelMakerApp.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<int> FontSizes { get; } = [];
    public ObservableCollection<string> FontFamilies { get; } =
    [
        "Arial",
        "Calibri",
        "Cambria",
        "Comic Sans MS",
        "Consolas",
        "Courier New",
        "Georgia",
        "Impact",
        "Times New Roman",
        "Tahoma",
        "Trebuchet MS",
        "Verdana"
    ];

    private TextObjectModel _roomNumber;
    private TextObjectModel _roomMembers;
    private BitmapImage? _imageData;
    private DoorLabelModel _doorLabelModel;
    private FlowDocument _flowDocumentLabelPreview;
    private readonly TextObjectModel _defaultRoomNumber = new ("[0000]", "Verdana", 60, "Bold", "Normal");
    private readonly TextObjectModel _defaultRoomMembers = new("[name surname]\r\n[name surname]", "Verdana", 40, "Normal", "Normal");

    public MainViewModel()
    {
        _roomNumber = new TextObjectModel(_defaultRoomNumber);
        _roomMembers = new TextObjectModel(_defaultRoomMembers);
        _imageData = ImageObjectModel.UrlToBitmap(PathHelper.GetAssetPath(@"\UJ-logos\logo_basic.png"));
        _doorLabelModel = new DoorLabelModel();
        _flowDocumentLabelPreview = CreateFlowDocument();

        AddFontSizes();

        NewFileCommand = new RelayCommand(NewFile);
        OpenFileCommand = new RelayCommand(OpenFile);
        SaveFileCommand = new RelayCommand(SaveFile);
        LoadImageCommand = new RelayCommand(LoadImage);
        PrintCommand = new RelayCommand(Print);
    }

    #region Properties

    public TextObjectModel RoomNumber
    {
        get => _roomNumber;
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    public TextObjectModel RoomMembers
    {
        get => _roomMembers;
        set
        {
            _roomMembers = value;
            OnPropertyChanged(nameof(RoomMembers));
        }
    }

    public BitmapImage? ImageData
    {
        get => _imageData;
        set
        {
            _imageData = value;
            OnPropertyChanged(nameof(ImageData));
        }
    }

    public FlowDocument FlowDocumentLabelPreview
    {
        get => _flowDocumentLabelPreview;
        set
        {
            if (_flowDocumentLabelPreview == value) return;
            _flowDocumentLabelPreview = value;
            OnPropertyChanged(nameof(FlowDocumentLabelPreview));
        }
    }

    #endregion // Properties


    #region Commands

    public ICommand NewFileCommand { get; set; }
    public ICommand OpenFileCommand { get; set; }
    public ICommand SaveFileCommand { get; set; }
    public ICommand LoadImageCommand { get; set; }
    public ICommand PrintCommand { get; set; }

    #endregion // Commands


    #region Methods

    private void NewFile(object obj)
    {
        _doorLabelModel = new DoorLabelModel();
        CleanView();
    }

    private void OpenFile(object obj)
    {
        var jsonFromFile = JsonUtils.LoadJsonFromFile();
        if (jsonFromFile == "")
        {
            MessageBox.Show("Failed to load selected project.", "Info", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;
        }

        _doorLabelModel.Deserialize(jsonFromFile);
        RoomNumber = _doorLabelModel.RoomNumber!;
        RoomMembers = _doorLabelModel.RoomMembers!;
        ImageData = ImageObjectModel.UrlToBitmap(_doorLabelModel.Image!.ImageData);
    }

    private void SaveFile(object obj)
    {
        _doorLabelModel.SetData(RoomNumber, RoomMembers, ImageObjectModel.BitmapToUrl(ImageData));
        var modelSerialized = _doorLabelModel.Serialize();
        JsonUtils.SaveJsonToFile(modelSerialized);
    }

    private void LoadImage(object obj)
    {
        var path = ImageObjectModel.GetPathToLogo();
        ImageData = ImageObjectModel.UrlToBitmap(path);
    }

    private void Print(object obj)
    {
        DoorLabelModel.Print(_flowDocumentLabelPreview);
    }

    private void CleanView()
    {
        RoomNumber = new TextObjectModel(_defaultRoomNumber);
        RoomMembers = new TextObjectModel(_defaultRoomMembers);
        ImageData = new BitmapImage();
    }


    private static FlowDocument CreateFlowDocument()
    {
        var flowDocument = new FlowDocument();
        var table = new Table();
        var tableRowGroup = new TableRowGroup();
        var tableRow = new TableRow();
        var tableCell = new TableCell();
        var blockUiContainer = new BlockUIContainer();

        var grid = new Grid();
        for (var i = 0; i < 6; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }

        var imgLoadedLogo = new Image
        {
            Height = 150,
            Width = 150,
            Margin = new Thickness(10, 25, 60, 20)
        };
        Grid.SetRow(imgLoadedLogo, 0);
        Grid.SetColumn(imgLoadedLogo, 0);
        Grid.SetRowSpan(imgLoadedLogo, 2);
        Grid.SetColumnSpan(imgLoadedLogo, 2);
        imgLoadedLogo.SetBinding(Image.SourceProperty, nameof(ImageData));

        var txtRoomNumberPreview = new TextBlock
        {
            TextAlignment = TextAlignment.Left,
            Margin = new Thickness(90, 0, 10, 0)
        };
        Grid.SetRow(txtRoomNumberPreview, 0);
        Grid.SetColumn(txtRoomNumberPreview, 2);
        Grid.SetRowSpan(txtRoomNumberPreview, 6);
        Grid.SetColumnSpan(txtRoomNumberPreview, 4);
        txtRoomNumberPreview.SetBinding(TextBlock.TextProperty, "RoomNumber.Text");
        txtRoomNumberPreview.SetBinding(TextBlock.FontFamilyProperty, "RoomNumber.FontFamily");
        txtRoomNumberPreview.SetBinding(TextBlock.FontSizeProperty, "RoomNumber.FontSize");
        txtRoomNumberPreview.SetBinding(TextBlock.FontWeightProperty, "RoomNumber.FontWeight");
        txtRoomNumberPreview.SetBinding(TextBlock.FontStyleProperty, "RoomNumber.FontStyle");

        var txtMembersPreview = new TextBlock
        {
            TextAlignment = TextAlignment.Left,
            Margin = new Thickness(45, 10, 10, 10)
        };
        Grid.SetRow(txtMembersPreview, 2);
        Grid.SetColumn(txtMembersPreview, 1);
        Grid.SetRowSpan(txtMembersPreview, 4);
        Grid.SetColumnSpan(txtMembersPreview, 5);
        txtMembersPreview.SetBinding(TextBlock.TextProperty, "RoomMembers.Text");
        txtMembersPreview.SetBinding(TextBlock.FontFamilyProperty, "RoomMembers.FontFamily");
        txtMembersPreview.SetBinding(TextBlock.FontSizeProperty, "RoomMembers.FontSize");
        txtMembersPreview.SetBinding(TextBlock.FontWeightProperty, "RoomMembers.FontWeight");
        txtMembersPreview.SetBinding(TextBlock.FontStyleProperty, "RoomMembers.FontStyle");

        grid.Children.Add(imgLoadedLogo);
        grid.Children.Add(txtRoomNumberPreview);
        grid.Children.Add(txtMembersPreview);

        blockUiContainer.Child = grid;
        tableCell.Blocks.Add(blockUiContainer);
        tableRow.Cells.Add(tableCell);
        tableRowGroup.Rows.Add(tableRow);
        table.RowGroups.Add(tableRowGroup);
        flowDocument.Blocks.Add(table);

        return flowDocument;
    }

    private void AddFontSizes()
    {
        for (var fontSize = 10; fontSize <= 32; fontSize += 2)
        {
            FontSizes.Add(fontSize);
        }
        for (var fontSize = 40; fontSize <= 95; fontSize += 5)
        {
            FontSizes.Add(fontSize);
        }
    }

    #endregion // Methods


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}