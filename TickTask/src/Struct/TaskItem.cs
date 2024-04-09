using System.Text;

namespace TickTask;

public partial class TaskItem
{
    private string _name = "";
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            MTime = TaskTime.CurrentTime;
        }
    }

    public TaskTime CTime { get; private set; }
    public TaskTime MTime { get; private set; }
    private TaskTime _dueTime;
    public TaskTime DueTime
    {
        get
        {
            return _dueTime;
        }
        set
        {
            _dueTime = value;
            MTime = TaskTime.CurrentTime;
        }
    }

    private string _project = "";
    public string Project
    {
        get
        {
            return _project;
        }
        set
        {
            _project = value;
            MTime = TaskTime.CurrentTime;
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
            MTime = TaskTime.CurrentTime;
        }
    }

    private string _uuid = "";
    public string UUID
    {
        get
        {
            return _uuid;
        }
        private set
        {
            _uuid = value;
            MTime = TaskTime.CurrentTime;
        }
    }

    public TaskItem()
    {
        _name = "Default Task";
        _project = "Inbox";
        State = TaskState.Pending;
        CTime = TaskTime.CurrentTime;
        MTime = TaskTime.CurrentTime;
        UUID = Guid.NewGuid().ToString();
    }

    public TaskItem(TaskTime ctime, TaskTime mtime) : this()
    {
        CTime = ctime;
        MTime = mtime;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("[ ");
        sb.Append($"name:\"{Name}\" ");
        sb.Append($"ctime:\"{CTime}\" ");
        sb.Append($"mtime:\"{MTime}\" ");
        sb.Append($"due:\"{DueTime}\" ");
        sb.Append($"project:\"{Project}\" ");
        sb.Append($"state:\"{State}\" ");
        sb.Append($"uuid:\"{UUID}\" ");
        sb.Append(']');

        return sb.ToString();
    }
}