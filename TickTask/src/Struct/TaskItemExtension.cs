using System.Text.RegularExpressions;

namespace TickTask;

public partial class TaskItem
{
    private const string TaskItemRegexPattern =
        @"\[" +
            @"(\[name:""(?<name>.+?)""\])?" +
            @"(\[ctime:""(?<ctime>.+?)""\])?" +
            @"(\[mtime:""(?<mtime>.+?)""\])?" +
            @"(\[due:""(?<due>.+?)""\])?" +
            @"(\[project:""(?<project>.+?)""\])?" +
            @"(\[state:""(?<state>.+?)""\])?" +
            @"(\[uuid:""(?<uuid>.+?)""\])?" +
        @"\]";

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
            default:
                throw new ArgumentException("Task Data Flag Can't to Process");
        }
    }

    public static TaskItem Parse(string item)
    {
        var regex = new Regex(TaskItemRegexPattern);
        var match = regex.Match(item);
        if (match.Success)
        {
            string name = match.Groups["name"].Success ? match.Groups["name"].Value : "";
            string ctime = match.Groups["ctime"].Success ? match.Groups["ctime"].Value : "";
            string mtime = match.Groups["mtime"].Success ? match.Groups["mtime"].Value : "";
            string due = match.Groups["due"].Success ? match.Groups["due"].Value : "";
            string project = match.Groups["project"].Success ? match.Groups["project"].Value : "";
            string state = match.Groups["state"].Success ? match.Groups["state"].Value : "";
            string uuid = match.Groups["uuid"].Success ? match.Groups["uuid"].Value : "";

            var task = new TaskItem();

            task.ChangeDataWithoutMTime(TaskDataFlag.Name, name);
            task.ChangeDataWithoutMTime(TaskDataFlag.CreateDate, ctime);
            task.ChangeDataWithoutMTime(TaskDataFlag.ModifiedDate, mtime);
            task.ChangeDataWithoutMTime(TaskDataFlag.Due, due);
            task.ChangeDataWithoutMTime(TaskDataFlag.Project, project);
            task.ChangeDataWithoutMTime(TaskDataFlag.State, state);
            task.ChangeDataWithoutMTime(TaskDataFlag.UUID, uuid);

            return task;
        }

        throw new NotImplementedException();
    }
}