namespace TickTask;

public partial struct TaskData
{
    public void AddTag(string tag)
    {
        if (Tags.Contains(tag)) return;

        Tags.Add(tag);
        ModifiedDate = TaskTime.CurrentTime;
    }

    public static TaskData Create(string name, string project = "Inbox", TaskState state = TaskState.Pending)
    {
        var task = new TaskData
        {
            Name = name,
            Project = project,
            State = state,
        };

        return task;
    }
}