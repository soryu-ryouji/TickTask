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

    private string _ctime = "";
    public string CTime
    {
        get
        {
            return _ctime;
        }
        set
        {
            _ctime = value;
        }
    }

    private string _mtime = "";
    public string MTime
    {
        get
        {
            return _mtime;
        }
        set
        {
            _mtime = value;
        }
    }

    private string _dueTime = " ";
    public string DueTime
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

    private string _project = " ";
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

    public TaskState _state = TaskState.Pending;
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

    private string _note = "";
    public string Note
    {
        get
        {
            return _note;
        }
        set
        {
            _note = value;
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

    public TaskItem(string ctime, string mtime) : this()
    {
        CTime = ctime;
        MTime = mtime;
    }

    public TaskItem(List<(TaskDataFlag flag, string value)> datas)
    {
        foreach (var (flag, value) in datas)
        {
            ChangeDataWithoutMTime(flag, value);
        }
    }

    public TaskItem(string name) : this()
    {
        _name = name;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"[name:{Name}]");
        sb.Append($"[ctime:{CTime}]");
        sb.Append($"[mtime:{MTime}]");
        sb.Append($"[project:{Project}]");
        sb.Append($"[due:{DueTime}]");
        sb.Append($"[state:{State}]");
        sb.Append($"[uuid:{UUID}]");
        sb.Append($"[note:{Note}]");

        return sb.ToString();
    }
}