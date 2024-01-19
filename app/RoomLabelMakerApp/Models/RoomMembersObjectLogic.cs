namespace RoomLabelMakerApp.Models;

public partial class RoomMembersObjectModel : ObjectBase, IContentObject
{
    public RoomMembersObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        Text = defaultText;
    }
    public override void Serialize()
    {
        throw new NotImplementedException();
    }
}