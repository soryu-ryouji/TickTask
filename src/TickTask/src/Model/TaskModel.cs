using System.Collections;

namespace TickTask;

public partial class TaskModel : IEnumerable<TaskItem>
{
    private static List<TaskItem> s_tasks = [];

    public static List<TaskItem> Tasks
    {
        get
        {
            return s_tasks;
        }
    }

    static TaskModel()
    {
        var dataTxt = AssetManager.Load("data.ticktask");
        s_tasks = Parse(dataTxt);
    }

    public IEnumerator<TaskItem> GetEnumerator()
    {
        return s_tasks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}