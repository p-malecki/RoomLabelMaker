using System.IO;

namespace RoomLabelMakerApp.Utils;

public static class PathHelper
{
    public static string GetAssetPath(string relativePath)
    {
#if DEBUG
        // Debug mode path
        var debugPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        return Path.Join(Path.GetFullPath(debugPath), relativePath);
#else
        // Release mode path
        return Path.Join(AppDomain.CurrentDomain.BaseDirectory, relativePath);
#endif
    }
}