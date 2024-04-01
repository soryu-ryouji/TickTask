namespace TickTask;

public enum TaskState
{
    Pending,
    Waiting,
    Recurring,
    Deleted,
    Completed,
}

public partial struct TaskItem
{
    public string Name = "";
    public TaskTime CreateDate { get; private set; }
    public TaskTime ModifiedDate { get; private set; }

    public string _project;
    public string Project
    {
        get
        {
            return _project;
        }
        set
        {
            _project = value;
            ModifiedDate = TaskTime.CurrentTime;
        }
    }

    public TaskState _state;
    public TaskState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            ModifiedDate = TaskTime.CurrentTime;
        }
    }

    public string UUID { get; private set; }

    public List<string> Tags { get; private set; } = [];

    public TaskItem()
    {
        _project = "Inbox";
        CreateDate = TaskTime.CurrentTime;
        ModifiedDate = TaskTime.CurrentTime;
        UUID = Guid.NewGuid().ToString();
    }

    public override string ToString()
    {
        return $"""[name:"{Name}" time:"{CreateDate},{ModifiedDate}" state:"{State}" uuid:"{UUID}"]""";
    }
}