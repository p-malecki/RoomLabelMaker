using Newtonsoft.Json;
namespace RoomLabelMakerApp.Models;


public class DoorLabelModel
{
    //public string ProjectName = ""; // TODO add project name
    public TextObjectModel? RoomNumber;
    public TextObjectModel? RoomMembers;
    public ImageObjectModel? Image;

    public DoorLabelModel()
    {
        RoomNumber = null;
        RoomMembers = null;
        Image = null;
    }

    public void SetData(TextObjectModel roomNumberModel, TextObjectModel roomMembersModel, string logo)
    {
        RoomNumber = roomNumberModel;
        RoomMembers = roomMembersModel;
        Image = new ImageObjectModel(logo);
    }

    public string Serialize()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    public void Deserialize(string json)
    {
        var d = JsonConvert.DeserializeObject<DoorLabelModel>(json);
        if (d == null) return;
        RoomNumber = d.RoomNumber;
        RoomMembers = d.RoomMembers;
        Image = d.Image;
    }
}