using System.Text.RegularExpressions;
using TerminalTables;
using TickTask.Parser;

namespace TickTask;

public class TaskController
{
    public static void AddTask(TaskItem task)
    {
        TaskModel.Add(task);
    }

    public static void AddTask(string[] taskArgs)
    {
        var taskItem = ArgumentParser.ParseAddCommand(string.Join(" ", taskArgs));
        TaskModel.Add(taskItem);
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

    public static void WriteNote(int index, string content)
    {
        TaskModel.Tasks[index].Note = content;
        TaskModel.Export();
    }

    public static void AppendNote(int index, string content)
    {
        TaskModel.Tasks[index].Note += content;
        TaskModel.Export();
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
            DiyListTask("|order|description|project|");
        }
        if (listModel == "all")
        {
            DiyListTask("|order|description|state|project|ctime|mtime|", true);
        }
    }

    private static void DiyListTask(string rowMetadata, bool showCompleted = false)
    {
        var metadata = rowMetadata.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var table = new Table(metadata);

        foreach (var item in TaskModel.Tasks)
        {
            if (!showCompleted && item.State == TaskState.Completed) continue;

            var rowData = new List<TableCell>();

            foreach (var field in metadata)
            {
                switch (field.Trim().ToLower())
                {
                    case "order": rowData.Add(new(TaskModel.SearchTask(item).ToString())); break;
                    case "description":
                        var cell = new TableCell(item.Name);
                        if (item.Note.Length != 0)
                        {
                            cell.Content.AddRange(item.SplitNote());
                        }
                        rowData.Add(cell);
                        break;
                    case "state": rowData.Add(new(item.State.ToString())); break;
                    case "project": rowData.Add(new(item.Project)); break;
                    case "ctime": rowData.Add(new(item.CTime)); break;
                    case "mtime": rowData.Add(new(item.MTime)); break;
                    default: break;
                }
            }

            table.AddRow(rowData);
        }

        Console.Write(table.Export(20));
    }
}