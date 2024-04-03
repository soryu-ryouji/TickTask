using System.CommandLine;

namespace TickTask;

public class Program
{
    public static void Main(string[] args)
    {
        ParserArgs(args);
    }
 
    private static void ParserArgs(string[] args)
    {
        var rootCommand = new RootCommand();

        rootCommand.Invoke(args);
    }
}