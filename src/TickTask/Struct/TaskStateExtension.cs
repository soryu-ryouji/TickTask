namespace TickTask;

public static class TaskStateExtensions
{
    public static TaskState Parse(string text)
    {
        Enum.TryParse(text, out TaskState state);

        return state;
    }
}