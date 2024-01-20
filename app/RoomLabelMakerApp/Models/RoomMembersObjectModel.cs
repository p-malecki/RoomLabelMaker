﻿namespace RoomLabelMakerApp.Models;

public partial class RoomMembersObjectModel
{
    static int defaultX = 0;
    static int defaultY = 0;
    static int defaultWidth = 0;
    static int defaultHeight = 0;

    static string[] defaultText = { "Michał Mnich" };
    static string defaultFontStyle = "Italic";
    static string defaultForegroundColor = "Black";
    static double defaultFontSize = 120;

    public string FontStyle { get; set; }
    public string ForegroundColor { get; set; }
    public double FontSize { get; set; }
    public string[] Text { get; set; }
}