namespace TickTask;

public partial struct TaskItem
{
    public void AddTag(string tag)
    {
        if (Tags.Contains(tag)) return;

        Tags.Add(tag);
        ModifiedDate = TaskTime.CurrentTime;
    }

    public static TaskItem Create(string name, string project = "Inbox", TaskState state = TaskState.Pending)
    {
        var task = new TaskItem
        {
            Name = name,
            Project = project,
            State = state,
        };

        return task;
    }
}