namespace TickTask;

class CommandParser
{
    public static void Parse(string[] args)
    {
        if (args.Length == 0) TaskController.ListTask();

        switch (args[0])
        {
            case "add":
                TaskController.AddTask(args[1..]);
                break;
            case "ls":
                TaskController.ListTask(args[1..]);
                break;
            case "rm":
            case "remove":
                TaskController.RemoveTask(args[1..]);
                break;
            case "search":
                TaskController.FuzzySearchTask(args[1..]);
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
                    TaskController.ModifiedTask(args[0], args[2..]);
                    break;
                case "done":
                    {
                        int.TryParse(args[0], out var index);
                        TaskController.CompleteTask(index);
                        break;
                    }
                case "wr":
                case "write":
                    {
                        int.TryParse(args[0], out var index);
                        TaskController.WriteNote(index, string.Join(" ", args[2..]));
                        break;
                    }
                // case "app":
                // case "append":
                //     {
                //         int.TryParse(args[0], out var index);
                //         TaskManager.AppendNote(index, string.Join(" ", args[2..]));
                //         break;
                //     }
                // case "appln":
                // case "appendline":
                //     {
                //         int.TryParse(args[0], out var index);
                //         TaskManager.AppendLineNote(index, string.Join(" ", args[2..]));
                //         break;
                //     }
            }
    }
}