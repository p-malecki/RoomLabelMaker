using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoomLabelMakerApp.Models;

public class TextObjectModel : ObjectBase, INotifyPropertyChanged
{
    private string _text;
    private string _fontFamily;
    private int _fontSize;
    private bool _fontIsBold;
    private string _fontWeight;
    private bool _fontIsItalic;
    private string _fontStyle;

    public TextObjectModel() : base(0, 0, 0, 0)
    {
        _text = "";
        _fontFamily = "";
        _fontSize = 0;
        _fontWeight = "";
        _fontStyle = "";
        FontIsBold = false;
        FontIsItalic = false;
    }

    public TextObjectModel(string text, string fontFamily, int fontSize, bool fontIsBold, bool fontIsItalic) : base(0, 0, 0, 0)
    {
        _text = text;
        _fontFamily = fontFamily;
        _fontSize = fontSize;
        FontIsBold = fontIsBold;
        FontIsItalic = fontIsItalic;
    }

    public TextObjectModel(TextObjectModel other) : base(0, 0, 0, 0)
    {
        _text = other.Text;
        _fontFamily = other.FontFamily;
        _fontSize = other.FontSize;
        FontIsBold = other.FontIsBold;
        FontIsItalic = other.FontIsItalic;
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
    public bool FontIsBold
    {
        get => _fontIsBold;
        set
        {
            _fontIsBold = value;
            FontWeight = value ? "Bold" : "Normal";
            OnPropertyChanged(nameof(FontIsBold));
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
    public bool FontIsItalic
    {
        get => _fontIsItalic;
        set
        {
            _fontIsItalic = value;
            FontStyle = value ? "Italic" : "Normal";
            OnPropertyChanged(nameof(FontIsItalic));
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