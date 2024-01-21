using Newtonsoft.Json;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace RoomLabelMakerApp.Models;

public partial class RoomNumberObjectModel : ObjectBase, IContentObject
{
    public RoomNumberObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        FontStyle = defaultFontStyle;
        FontSize = defaultFontSize;
        Text = defaultText;
    }
    public RoomNumberObjectModel(string text, string font_style, string font_size) : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        FontStyle = font_style;
        FontSize = font_size;
        Text = text;
    }

    public override string Serialize()
    {
        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        var propertyValues = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(this));

        return JsonConvert.SerializeObject(new { RoomNumberObjectModel = propertyValues });
    }
}