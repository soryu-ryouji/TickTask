using System.Text.RegularExpressions;

namespace TickTask;

class TaskModel
{
    private const string TaskItemRegexPattern =
    @"\[name:""(?<Name>.+?)"" time:""(?<CreateTime>.+?),(?<ModifyTime>.+?)"" state:""(?<State>.+?)"" uuid:""(?<UUID>.+?)""\]";

    private static List<TaskItem> s_tasks = [];

    public static TaskItem[] Search()
    {
        throw new NotImplementedException();
    }

    public static void Insert(TaskItem task)
    {
        s_tasks.Add(task);
    }

    public static void Remove()
    {
        throw new NotImplementedException();
    }

    public static void Init()
    {
        var dataTxt = AssetManager.Load("data.tt");
        s_tasks = Parse(dataTxt);
    }

    public static void Export()
    {
        var text = string.Join(Environment.NewLine, s_tasks.Select(task => task.ToString()));

        AssetManager.Save("data.tt",text);
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