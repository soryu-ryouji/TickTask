using System.Text;

namespace TickTask;

public class AssetManager
{
    private static string ConfigFolderPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "TickTask"
        );

    static AssetManager()
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

    public static string Load(string fileName)
    {
        string filePath = Path.Combine(ConfigFolderPath, fileName);

        if (!File.Exists(filePath)) return "";

        string text = File.ReadAllText(filePath, Encoding.UTF8);

        return text;
    }

    public static void Save(string fileName, string text)
    {
        CheckAndInit();
        string filePath = Path.Combine(ConfigFolderPath, fileName);
        File.WriteAllText(filePath, text, Encoding.UTF8);
    }
}