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
        var indexs = TaskModel.Search(TaskDataFlag.Name, taskName);
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

            var indexs = TaskModel.Search(TaskDataFlag.Name, taskName);
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

    public static void ListTask()
    {
        int order = 1;
        var table = new ConsoleTable("order", "name", "project", "ctime", "mtime");
        foreach (var item in TaskModel.Tasks)
        {
            table.AddRow(order, item.Name, item.Project, item.CTime, item.MTime);
            order += 1;
        }
        
        table.Write();
    }
}