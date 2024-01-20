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

    /// <summary>
    /// Zrobione żeby deserializacja dziala, jezeli bedziemy chcieli zeby byly one tylko read only trzeba
    /// zmienic w kazdym z objektow spsob zapisu _x, _y 
    /// </summary>

    public int x
    {
        get => _x;
        set => _x = value;
    }

    public int y
    {
        get => _y;
        set => _y = value;
    }

    public int Width
    {
        get => _width;
        set => _width = value;
    }

    public int Height
    {
        get => _height;
        set => _height = value;
    }

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

    abstract public string Serialize();
}

