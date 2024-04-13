using System.Text;

namespace TickTask;

public class AssetController
{
    private static string ConfigFolderPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "TickTask"
        );

    static AssetController()
    {
        CheckAndInit();
    }

    private static void CheckAndInit()
    {
        if (!Directory.Exists(ConfigFolderPath))
        {
            Directory.CreateDirectory(ConfigFolderPath);
        }
    }

    public static List<string> Load(string fileName)
    {
        string filePath = Path.Combine(ConfigFolderPath, fileName);

        if (!File.Exists(filePath)) return [];

        var text = File.ReadAllLines(filePath, Encoding.UTF8).ToList();

        return text;
    }

    public static void Save(string fileName, string text)
    {
        CheckAndInit();
        string filePath = Path.Combine(ConfigFolderPath, fileName);
        File.WriteAllText(filePath, text, Encoding.UTF8);
    }
}