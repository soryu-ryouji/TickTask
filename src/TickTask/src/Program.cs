using System.Reflection;

namespace TickTask;

public class Program
{
    public static void Main(string[] args)
    {
        ParserArgs(args);
    }

    private static void ParserArgs(string[] args)
    {
        if (args.Length == 0) TaskManager.ListTask();

        switch (args[0])
        {
            case "add":
                TaskManager.AddTask(args[1..]);
                break;
            case "ls":
                TaskManager.ListTask();
                break;
            case "rm":
            case "remove":
                TaskManager.RemoveTask(args[1..]);
                break;
            case "search":
                TaskManager.FuzzySearchTask(args[1..]);
                break;
            case "--version":
            case "-v":
                Console.WriteLine(GetProgramInfo());
                break;
            case "--help":
            case "-h":
                break;
        };

        if (args.Length > 1)
        switch (args[1])
        {
            case "mod":
            case "modified":
                TaskManager.ModifiedTask(args[0], args[2..]);
                break;
        }
    }

    private static string GetProgramInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string? name = assembly.GetName().Name;
        var version = assembly.GetName().Version;

        return name + ": " + version;
    }
}