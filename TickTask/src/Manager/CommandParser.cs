namespace TickTask;

class CommandParser
{
    public static void Parse(string[] args)
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
                Console.WriteLine(Utils.GetProgramInfo());
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
}