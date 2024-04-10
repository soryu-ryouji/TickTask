using System.Text.RegularExpressions;
using ConsoleTables;

namespace TickTask;

public class TaskManager
{
    public static void AddTask(TaskItem task)
    {
        TaskModel.Add(task);
    }

    public static void AddTask(string[] taskArgs)
    {
        TaskModel.Add(TaskItem.Create(string.Join(" ", taskArgs)));
    }

    public static void CompleteTask(int index)
    {
        TaskModel.ModifiyData(index - 1, TaskDataFlag.State, "Completed");
    }

    public static void RemoveTask(string[] taskArgs)
    {
        // if order == 1
        // then it in the TaskModel.Task[0]
        if (int.TryParse(taskArgs[0], out var index)) TaskModel.Remove(index - 1);
    }

    public static void FuzzySearchTask(string[] taskArgs)
    {
        FuzzySearchTask(string.Join(" ", taskArgs));
    }

    public static void FuzzySearchTask(string taskName)
    {
        var indexs = TaskModel.SearchTasks(TaskDataFlag.Name, taskName);
        if (indexs.Length == 0) return;

        foreach (var i in indexs)
        {
            Console.WriteLine(TaskModel.Tasks[i].Name);
        }
    }

    public static void ModifiedTask(string taskName, string[] taskArgs)
    {
        var taskStr = string.Join(" ", taskArgs);
        if (taskArgs.Length == 0) return;

        var regex = new Regex(@"(?<name>name:.+)?\s?(?<due>due:.+)?\s?(?<state>state:.+)?");
        var match = regex.Match(taskStr);

        if (match.Success)
        {
            string name = match.Groups["name"].Success ? match.Groups["name"].Value[5..] : "";
            string due = match.Groups["due"].Success ? match.Groups["due"].Value : "";
            string state = match.Groups["state"].Success ? match.Groups["state"].Value : "";

            var indexs = TaskModel.SearchTasks(TaskDataFlag.Name, taskName);
            if (indexs.Length == 0) return;

            foreach (var i in indexs)
            {
                if (TaskModel.Tasks[i].Name == taskName)
                {
                    if (name != "") TaskModel.ModifiyData(indexs[0], TaskDataFlag.Name, name);
                    if (state != "") TaskModel.ModifiyData(indexs[0], TaskDataFlag.State, state);
                }
            }
        }
    }


    public static void ListTask(string[] args)
    {
        if (args.Length == 0)
        {
            ListTask("simple");
            return;
        }

        switch (args[0])
        {
            case "-a":
            case "--all":
                ListTask("all");
                break;
            default:
                ListTask("simple");
                break;
        }
    }

    public static void ListTask(string listModel = "all")
    {
        if (listModel == "simple")
        {
            DiyListTask("|order|name|state|");
        }
        if (listModel == "all")
        {
            DiyListTask("|order|name|state|project|ctime|mtime|uuid|");
        }
    }

    private static void DiyListTask(string rowMetadata)
    {
        var metadata = rowMetadata.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var table = new ConsoleTable(metadata);

        foreach (var item in TaskModel.Tasks)
        {
            var rowValues = new List<object>();

            foreach (var field in metadata)
            {
                switch (field.Trim().ToLower())
                {
                    case "order":
                        rowValues.Add(TaskModel.SearchTask(item));
                        break;
                    case "name":
                        rowValues.Add(item.Name);
                        break;
                    case "state":
                        rowValues.Add(item.State);
                        break;
                    case "project":
                        rowValues.Add(item.Project);
                        break;
                    case "ctime":
                        rowValues.Add(item.CTime);
                        break;
                    case "mtime":
                        rowValues.Add(item.MTime);
                        break;
                    case "uuid":
                        rowValues.Add(item.UUID);
                        break;
                    default:
                        // Handle unknown fields or custom fields if needed
                        rowValues.Add("");
                        break;
                }
            }

            table.AddRow(rowValues.ToArray());
        }

        table.Write();
    }
}