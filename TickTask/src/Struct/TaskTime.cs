using System.Globalization;

namespace TickTask;

public struct TaskTime()
{
    private const string TaskTimeFormat = "yyyy-MM-dd-HH-mm-ss";

    public static string CurrentTime => DateTime.UtcNow.ToString(TaskTimeFormat);

    /// <summary>
    /// Convert UTC Time To TaskTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string DateTimeToTaskTime(DateTime dateTime)
    {
        return dateTime.ToString(TaskTimeFormat);
    }

    /// <summary>
    /// Convert Task Time To Local Time
    /// </summary>
    /// <param name="taskTime"></param>
    /// <returns></returns>
    public static string UTCToLocalTime(string taskTime)
    {
        if (DateTime.TryParseExact(taskTime, TaskTimeFormat,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            DateTime newTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
            return newTime.ToString(TaskTimeFormat);
        }
        else
        {
            throw new ArgumentException("Invalid taskTime format.");
        }
    }
}