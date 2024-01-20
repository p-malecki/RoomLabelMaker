namespace RoomLabelMakerApp.Models
{
    public interface IContentObject
    {
        string Serialize();
        void SetPosition(int x, int y);
        void SetSize(int width, int height);
    }
}