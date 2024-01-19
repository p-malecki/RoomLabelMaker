namespace RoomLabelMakerApp.Models;

public partial class RoomNumberObjectModel : ObjectBase, IContentObject
{
    public RoomNumberObjectModel() : base (defaultX, defaultY, defaultWidth, defaultHeight)
    {
        Text = defaultText;
    }

    public override void Serialize()
    {
        throw new NotImplementedException();
    }
}