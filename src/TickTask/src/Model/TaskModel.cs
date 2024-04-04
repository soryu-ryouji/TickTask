using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace TickTask;

public enum TaskDataFlag
{
    Name,
    CreateDate,
    ModifiedDate,
    State,
    UUID
}

class TaskModel
{
    private const string TaskItemRegexPattern =
    @"\[name:""(?<Name>.+?)"" time:""(?<CreateTime>.+?),(?<ModifyTime>.+?)"" state:""(?<State>.+?)"" uuid:""(?<UUID>.+?)""\]";

    private static List<TaskItem> s_tasks = [];
    // private static List<int> s_tasksOrder = [];

    public static List<TaskItem> Tasks
    {
        get
        {
            return s_tasks;
        }
    }

    static TaskModel()
    {
        Init();
    }

    public static int[] Search(TaskDataFlag flag, string searchStr)
    {
        var result = flag switch
        {
            TaskDataFlag.Name => SearchWithName(searchStr),
            _ => throw new NotImplementedException()
        };
        return result;
    }

    private static int[] FuzzySearchWithName(string name)
    {
        var result = from item in Tasks
                     where item.Name.Contains(name)
                     select Tasks.IndexOf(item);

        return result.ToArray();
    }

    private static int[] SearchWithName(string name)
    {
        var result = from item in Tasks
                     where item.Name == name
                     select Tasks.IndexOf(item);

        return result.ToArray();
    }

    private static int[] SearchWithCDate(string date)
    {
        throw new NotImplementedException();
    }

    private static int[] SearchWithMDate(string date)
    {
        throw new NotImplementedException();
    }

    public static void ModifiyData(int index, TaskDataFlag flag, string data)
    {
        switch (flag)
        {
            case TaskDataFlag.Name:
                s_tasks[index].Name = data;
                break;
            case TaskDataFlag.State:
                s_tasks[index].State = TaskStateExtensions.Parse(data);
                break;
        }

        Export();
    }

    public static void Add(TaskItem task)
    {
        s_tasks.Add(task);
        Export();
    }

    public static void Remove(int index)
    {
        s_tasks.RemoveAt(index);
        Export();
    }

    public static List<TaskItem> GetTask(string fillter)
    {
        return s_tasks;
    }

    public static void Init()
    {
        // Console.WriteLine("Init Task Model");
        var dataTxt = AssetManager.Load("data.ticktask");
        s_tasks = Parse(dataTxt);
    }

    public static void Export()
    {
        var text = string.Join(Environment.NewLine, s_tasks.Select(task => task.ToString()));
        AssetManager.Save("data.ticktask", text);
    }

    public static List<TaskItem> Parse(string text)
    {
        // Console.WriteLine(text);
        var tasks = new List<TaskItem>();

        var matches = Regex.Matches(text, TaskItemRegexPattern);

        foreach (Match match in matches)
        {
            var task = new TaskItem(
                name: match.Groups["Name"].Value,
                createDate: TaskTime.Create(match.Groups["CreateTime"].Value),
                modifiedDate: TaskTime.Create(match.Groups["ModifyTime"].Value),
                state: TaskStateExtensions.Parse(match.Groups["State"].Value),
                uuid: match.Groups["UUID"].Value
            );


            tasks.Add(task);
        }

        return tasks;
    }
}