using Newtonsoft.Json;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace RoomLabelMakerApp.Models;

public partial class RoomNumberObjectModel : ObjectBase, IContentObject
{
    public RoomNumberObjectModel() : base (defaultX, defaultY, defaultWidth, defaultHeight)
    {
        FontStyle = defaultFontStyle;
        FontSize = defaultFontSize;
        ForegroundColor = defaultForegroundColor;
        Text = defaultText;
    }

    public override string Serialize()
    {
        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        var propertyValues = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(this));

        return JsonConvert.SerializeObject(new { RoomNumberObjectModel = propertyValues });
    }
}