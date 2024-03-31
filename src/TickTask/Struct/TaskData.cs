namespace TickTask;

public partial struct TaskData
{
    public string Name = "";
    public TaskTime CreateDate { get; set; }
    public TaskTime ModifiedDate { get; private set; }

    public string Project
    {
        get
        {
            return Project;
        }
        private set
        {
            ModifiedDate = TaskTime.CurrentTime;
            Project = value;
        }
    }

    public List<string> Tags { get; private set; } = [];

    public TaskData()
    {
        CreateDate = TaskTime.CurrentTime;
        ModifiedDate = TaskTime.CurrentTime;
    }
}