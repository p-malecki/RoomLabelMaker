namespace RoomLabelMakerApp.Models;
using Newtonsoft.Json;
using System.Reflection;


public partial class RoomMembersObjectModel : ObjectBase, IContentObject
{
    public RoomMembersObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
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

        return JsonConvert.SerializeObject(new { RoomMembersObjectModel = propertyValues });
    }

}

