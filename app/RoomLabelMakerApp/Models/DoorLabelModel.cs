using Newtonsoft.Json;

namespace RoomLabelMakerApp.Models;

public class DoorLabelModel
{
    public class RootObject
    {
        public List<ObjectBase> Objects { get; set; }

        public string SerializeToJson()
        {
            var objectsJson = new List<object>();
            foreach (var obj in Objects)
            {
                objectsJson.Add(JsonConvert.DeserializeObject(obj.Serialize()));
            }
            return JsonConvert.SerializeObject(new { objects = objectsJson });
        }
    }

    public int Id;
    public string Name;
    public RoomNumberObjectModel RoomNumber;
    public RoomMembersObjectModel RoomMembers;
    public LogoObjectModel Logo;

    public string Serialize()
    {
        RootObject rootObject = new RootObject
        {
            Objects = new List<ObjectBase> { RoomNumber, RoomMembers, Logo}
        };

        string resultJson = rootObject.SerializeToJson();
        return resultJson;
    }

    public void Deserialize(string json)
    {
        List<Dictionary<string, object>> rootObjectList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

        foreach (var obj in rootObjectList)
        {
            foreach (var kvp in obj)
            {
                switch (kvp.Key)
                {
                    case "RoomNumberObjectModel":
                        RoomNumber = JsonConvert.DeserializeObject<RoomNumberObjectModel>(kvp.Value.ToString());
                        break;

                    case "RoomMembersObjectModel":
                        RoomMembers = JsonConvert.DeserializeObject<RoomMembersObjectModel>(kvp.Value.ToString());
                        break;
                    case "LogoObjectModel":
                        Logo = JsonConvert.DeserializeObject<LogoObjectModel>(kvp.Value.ToString());
                        break;
                    default:
                        Console.WriteLine($"Unknown class type: {kvp.Key}");
                        break;
                }
            }
        }
    }
}