namespace TickTask;

public partial class TaskItem
{
    private void ChangeDataWithoutMTime(TaskDataFlag flag, string data)
    {
        switch (flag)
        {
            case TaskDataFlag.Name: _name = data; break;
            case TaskDataFlag.CreateDate: _ctime = data; break;
            case TaskDataFlag.ModifiedDate: _mtime = data; break;
            case TaskDataFlag.Due: _dueTime = data; break;
            case TaskDataFlag.Project: _project = data; break;
            case TaskDataFlag.State: _state = TaskStateExtensions.Parse(data); break;
            case TaskDataFlag.UUID: _uuid = _uuid = data; break;
            case TaskDataFlag.Note: _note = _note = data; break;
            default: throw new ArgumentException("Task Data Flag Can't to Process");
        }
    }

    public string[] SplitNote()
    {
        var result = Note.Split("\\n");
        return result;
    }
}