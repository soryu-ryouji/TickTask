namespace TickTask;

public partial struct TaskData
{
    public void AddTag(string tag)
    {
        if (Tags.Contains(tag)) return;

        Tags.Add(tag);
        ModifiedDate = TaskTime.CurrentTime;
    }

    public static TaskData Create(string name)
    {
        var task = new TaskData
        {
            Name = name,
        };

        return task;
    }
}