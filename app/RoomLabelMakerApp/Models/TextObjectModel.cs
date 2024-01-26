using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Windows;

namespace RoomLabelMakerApp.Models;

public class TextObjectModel : ObjectBase, INotifyPropertyChanged
{
    private string _text;
    private string _fontFamily;
    private int _fontSize;
    private string _fontWeight;
    private string _fontStyle;

    public TextObjectModel(string text, string fontFamily, int fontSize, string fontWeight, string fontStyle) : base(0, 0, 0, 0)
    {
        _text = text;
        _fontFamily = fontFamily;
        _fontSize = fontSize;
        _fontWeight = fontWeight;
        _fontStyle = fontStyle;
    }

    public TextObjectModel(TextObjectModel other) : base(0, 0, 0, 0)
    {
        _text = other.Text;
        _fontFamily = other.FontFamily;
        _fontSize = other.FontSize;
        _fontWeight = other.FontWeight;
        _fontStyle = other.FontStyle;
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged(nameof(Text));
        }
    }
    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            _fontFamily = value;
            OnPropertyChanged(nameof(FontFamily));
        }
    }
    public int FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            OnPropertyChanged(nameof(FontSize));
        }
    }
    public string FontWeight
    {
        get => _fontWeight;
        set
        {
            _fontWeight = value;
            OnPropertyChanged(nameof(FontWeight));
        }
    }
    public string FontStyle
    {
        get => _fontStyle;
        set
        {
            _fontStyle = value;
            OnPropertyChanged(nameof(FontStyle));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}