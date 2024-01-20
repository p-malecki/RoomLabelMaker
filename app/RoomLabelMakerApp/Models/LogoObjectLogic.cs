using Newtonsoft.Json;
using System.Reflection;

namespace RoomLabelMakerApp.Models;

public partial class LogoObjectModel : ObjectBase, IContentObject
{
    public LogoObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        ImageData = defaultpicture;
        throw new NotImplementedException("Logo: dodac jako default obrazek UJ");
        //dodac jako default obrazek UJ
    }

    public override string Serialize()
    {
        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        var propertyValues = properties.ToDictionary(prop => prop.Name, prop => prop.GetValue(this));

        return JsonConvert.SerializeObject(new { LogoObjectModel = propertyValues });
    }
}