using System.Windows;

namespace RoomLabelMakerApp.Models;

public partial class RoomMembersObjectModel
{
    static int defaultX = 0;
    static int defaultY = 0;
    static int defaultWidth = 0;
    static int defaultHeight = 0;

    static string defaultText = "Michał Mnich";
    static string defaultFontStyle = "Italic";
    static string defaultForegroundColor = "Black";
    static string defaultFontSize = "120";
    
    public string Text { get; set; }
    
    public string FontStyle { get; set; }
    
    public string FontSize { get; set; }
}