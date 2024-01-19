namespace RoomLabelMakerApp.Models;

public partial class LogoObjectModel : ObjectBase, IContentObject
{
    public LogoObjectModel() : base(defaultX, defaultY, defaultWidth, defaultHeight)
    {
        ImageData = defaultpicture;
        throw new NotImplementedException("Logo: dodac jako default obrazek UJ");
        //dodac jako default obrazek UJ
    }

    public override void Serialize()
    {
        throw new NotImplementedException();
    }
}