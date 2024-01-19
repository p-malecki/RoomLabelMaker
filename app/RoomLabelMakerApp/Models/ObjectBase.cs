namespace RoomLabelMakerApp.Models;

public abstract class ObjectBase : IContentObject
{
    protected int _x;
    protected int _y;
    protected int _width;
    protected int _height;

    public ObjectBase(int x, int y, int width, int height)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    public int x { get; }
    public int y { get; }
    public int Width { get; }
    public int Height { get; }

    public void SetPosition(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public void SetSize(int width, int height)
    {
        this._width = width;
        this._height = height;
    }

    abstract public void Serialize();
}

