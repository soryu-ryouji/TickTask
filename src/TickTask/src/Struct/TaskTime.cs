using System.Globalization;

namespace TickTask;

public struct TaskTime()
{
    public string Time = "";

    private const string TaskTimeFormat = "UTC yyyy-MM-dd HH:mm:ss:FFF";
    private const string LocalTimeFormat = "yyyy-MM-dd HH:mm:ss:FFF";

    public static TaskTime CurrentTime => new() { Time = DateTimeToTaskTime(DateTime.UtcNow) };

    /// <summary>
    /// Create TaskTime with task time format text
    /// </summary>
    /// <param name="taskTimeStr"></param>
    /// <returns></returns>
    public static TaskTime Create(string taskTimeStr)
    {
        return new TaskTime() { Time = taskTimeStr };
    }

    /// <summary>
    /// Convert UTC Time To TaskTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string DateTimeToTaskTime(DateTime dateTime)
    {
        return dateTime.ToString(TaskTimeFormat);
    }

    public readonly string LocalTime => TaskTimeToLocalTime(Time);

    /// <summary>
    /// Convert Task Time To Local Time
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public static string TaskTimeToLocalTime(string taskTime)
    {
        var dateTime = ToDateTime(taskTime);
        DateTime newTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);

        return newTime.ToString(LocalTimeFormat);
    }

    /// <summary>
    /// Convert Task Time To Local Time
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public static string TaskTimeToLocalTime(TaskTime taskTime)
    {
        return TaskTimeToLocalTime(taskTime.Time);
    }

    /// <summary>
    /// Convert Task Time To DateTime
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(string taskTime)
    {
        if (DateTime.TryParseExact(
            taskTime, TaskTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            return dateTime;
        }
        else
        {
            return new DateTime();
        }
    }

    /// <summary>
    /// Convert Task Time To DateTime
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(TaskTime taskTime)
    {
        return ToDateTime(taskTime.Time);
    }

    /// <summary>
    /// Convert Task Time To DateTime
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public readonly DateTime ToDateTime()
    {
        return ToDateTime(Time);
    }

    public override string ToString()
    {
        return Time;
    }
}