namespace RoomLabelMakerApp.Models;

public class DoorLabelModel
{
    public int Id;
    public string Name;
    public ICollection<IContentObject> ContentObjects;
}