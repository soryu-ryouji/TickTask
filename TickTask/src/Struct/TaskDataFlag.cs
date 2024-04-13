namespace TickTask;

public enum TaskDataFlag
{
    Name,
    Due,
    CreateDate,
    ModifiedDate,
    Project,
    State,
    UUID,
    Note,
    Unknown,
}

public class TaskDataFlagExtension
{
    public static TaskDataFlag TryParse(string flag)
    {
        return flag?.ToLowerInvariant() switch // 转换为小写并忽略大小写比较
        {
            "name" => TaskDataFlag.Name,
            "due" => TaskDataFlag.Due,
            "ctime" => TaskDataFlag.CreateDate,
            "mtime" => TaskDataFlag.ModifiedDate,
            "project" => TaskDataFlag.Project,
            "state" => TaskDataFlag.State,
            "uuid" => TaskDataFlag.UUID,
            "note" => TaskDataFlag.Note,
            _ => TaskDataFlag.Unknown
        };
    }
}