namespace TickTask;

public partial class TaskModel
{
    public static int[] SearchTasks(TaskDataFlag flag, string searchStr)
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

    public static int SearchTask(TaskItem item)
    {
        return s_tasks.IndexOf(item);
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

    public static int[] GetTaskOrder(TaskDataFlag flag, string data)
    {
        var order = flag switch
        {
            TaskDataFlag.Name => SearchWithName(data),
            _ => throw new NotImplementedException()
        };

        return order;
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

    public static void Export()
    {
        var text = string.Join(Environment.NewLine, s_tasks.Select(task => task.ToString()));
        AssetController.Save("data.ticktask", text);
    }
}