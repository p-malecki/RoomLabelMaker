using Newtonsoft.Json;

namespace RoomLabelMakerApp.Models;

public abstract class ObjectBase : IContentObject
{
    private int _x;
    private int _y;
    private int _width;
    private int _height;

    protected ObjectBase()
    {
    }

    protected ObjectBase(int x, int y, int width, int height)
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

    public int X
    {
        get => _x;
        set => _x = value;
    }

    public int Y
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

    public string Serialize()   
    {
        return JsonConvert.SerializeObject(this);
    }

    public static T? Deserialize<T>(string json) where T : ObjectBase
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}

