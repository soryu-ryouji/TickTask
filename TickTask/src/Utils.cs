using System.Reflection;

namespace TickTask;

class Utils
{
    public static string GetProgramInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string? name = assembly.GetName().Name;
        var version = assembly.GetName().Version;

        return name + ": " + version;
    }
}