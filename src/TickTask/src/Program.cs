namespace TickTask;

public class Program
{
    public static void Main(string[] args)
    {
        ParserArgs(args);
    }

    private static void ParserArgs(string[] args)
    {
        Console.WriteLine(string.Join(" ", args));
    }
}