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
    private static List<int> s_tasksOrder = [];

    public static int[] Search(TaskDataFlag flag, string searchStr)
    {
        Init();

        var result = flag switch
        {
            TaskDataFlag.Name => SearchWithName(searchStr),
            TaskDataFlag.CreateDate => SearchWithCDate(searchStr),
            TaskDataFlag.ModifiedDate => SearchWithMDate(searchStr),
            _ => throw new NotImplementedException()
        };

        return result;
    }

    private static int[] SearchWithName(string name)
    {
        var query = from task in s_tasks
                    where task.Name.Contains(name)
                    select s_tasks.IndexOf(task);

        return query.ToArray();
    }

    private static int[] SearchWithCDate(string date)
    {
        throw new NotImplementedException();
    }

    private static int[] SearchWithMDate(string date)
    {
        throw new NotImplementedException();
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

    public static void Init()
    {
        var dataTxt = AssetManager.Load("data.tt");
        s_tasks = Parse(dataTxt);
    }

    public static void Export()
    {
        var text = string.Join(Environment.NewLine, s_tasks.Select(task => task.ToString()));
        Console.WriteLine("Export: \n" + text);

        AssetManager.Save("data.tt", text);
    }

    public static List<TaskItem> Parse(string text)
    {
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