using System.Text.RegularExpressions;

namespace TickTask;

public partial class TaskItem
{
    private const string TaskItemRegexPattern =
        @"\[" +
            @"(name:""(?<name>.+?)"")?\s?" +
            @"(ctime:""(?<ctime>.+?)"")?\s?" +
            @"(mtime:""(?<mtime>.+?)"")?\s?" +
            @"(due:""(?<due>.+?)"")?\s?" +
            @"(project:""(?<project>.+?)"")?\s?" +
            @"(state:""(?<state>.+?)"")?" +
            @"(uuid:""(?<uuid>.+?)"")?" +
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

    private void ChangeDataWithoutUpdateMTime(TaskDataFlag flag, string data)
    {
        switch (flag)
        {
            case TaskDataFlag.Name: _name = data; break;
            case TaskDataFlag.CreateDate: CTime = TaskTime.Create(data); break;
            case TaskDataFlag.ModifiedDate: MTime = TaskTime.Create(data); break;
            case TaskDataFlag.Due: _dueTime = TaskTime.Create(data); break;
            case TaskDataFlag.Project: _project = data; break;
            case TaskDataFlag.State: _state = TaskStateExtensions.Parse(data); break;
            case TaskDataFlag.UUID: _uuid = _uuid = data; break;
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

            var task = new TaskItem
            {
                CTime = TaskTime.Create(ctime),
                MTime = TaskTime.Create(mtime)
            };
            task.ChangeDataWithoutUpdateMTime(TaskDataFlag.Name, name);
            task.ChangeDataWithoutUpdateMTime(TaskDataFlag.Due, due);
            task.ChangeDataWithoutUpdateMTime(TaskDataFlag.Project, project);
            task.ChangeDataWithoutUpdateMTime(TaskDataFlag.State, state);
            task.ChangeDataWithoutUpdateMTime(TaskDataFlag.UUID, uuid);

            return task;
        }

        throw new NotImplementedException();
    }
}