namespace TickTask;

public class TaskManager
{
    public static void AddTask(TaskItem task)
    {
        TaskModel.Add(task);
    }

    public static void RemoveTask(TaskDataFlag flag, string taskName)
    {
        var index = TaskModel.Search(flag, taskName);
        if (index.Length != 0) TaskModel.Remove(index[0]);
    }
}