using System.Text.RegularExpressions;

namespace TickTask;

public partial class TaskItem
{
    private const string TaskItemRegexPattern =
        @"\[" +
            @"(?<Name>name:"".+"")\s?" +
            @"(?<CTime>ctime:"".+"")?\s?" +
            @"(?<MTime>mtime:"".+"")?\s?" +
            @"(?<due>due:"".+"")?\s?" +
            @"(?<project>project:"".+"")?\s?" +
            @"(?<state>state:"".+"")?" +
            @"(?<uuid>uuid:"".+"")" +
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

    public void ChangeDataWithoutUpdateMTime(TaskDataFlag flag, string data)
    {
        switch (flag)
        {
            case TaskDataFlag.Name:
                _name = data;
                break;
            case TaskDataFlag.CreateDate:
                break;
            case TaskDataFlag.Due:
                _dueTime = TaskTime.Create(data);
                break;
            case TaskDataFlag.Project:
                _project = data;
                break;
            case TaskDataFlag.State:
                _state = TaskStateExtensions.Parse(data);
                break;
            case TaskDataFlag.UUID:
                _uuid = data;
                break;
        }
    }

    public static TaskItem Parser(string item)
    {
        var regex = new Regex(TaskItemRegexPattern);
        var match = regex.Match(item);
        if (match.Success)
        {
            string name = match.Groups["name"].Success ? match.Groups["name"].Value[5..] : "";
            string ctime = match.Groups["ctime"].Success ? match.Groups["ctime"].Value[5..] : "";
            string mtime = match.Groups["mtime"].Success ? match.Groups["mtime"].Value[5..] : "";
            string due = match.Groups["due"].Success ? match.Groups["due"].Value[4..] : "";
            string project = match.Groups["project"].Success ? match.Groups["project"].Value[8..] : "";
            string state = match.Groups["state"].Success ? match.Groups["state"].Value[6..] : "";
            string uuid = match.Groups["uuid"].Success ? match.Groups["uuid"].Value[5..] : "";

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
        }

        throw new NotImplementedException();
    }
}