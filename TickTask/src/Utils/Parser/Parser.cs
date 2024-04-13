namespace TickTask.Parser;

public class ArgumentParser
{
    public static TaskItem ParseAddCommand(string args)
    {
        return new TaskItem(args);
    }
}

public class Parser
{
    public static List<TaskItem> ParseDB(List<string> text)
    {
        return text.Select(line => ParseDBLine(line)).ToList();
    }

    private static TaskItem ParseDBLine(string text)
    {
        var datas = new List<(TaskDataFlag key, string value)>();

        int startPos = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (startPos + 1 == text.Length) break;
            
            if (PeekTokenType(text[i]) == TokenType.DataStart)
            {
                startPos = i + 1;
            }
            else if (PeekTokenType(text[i]) == TokenType.DataEnd)
            {
                if (startPos != i) datas.Add(SplitData(text[startPos..i]));
            }
        }

        return new TaskItem(datas);
    }

    private static (TaskDataFlag key, string value) SplitData(string data)
    {
        var item = data.Split(":");
        return (key: TaskDataFlagExtension.TryParse(item[0]), value: item[1]);
    }

    public static TokenType PeekTokenType(char ch)
    {
        return ch switch
        {
            '[' => TokenType.DataStart,
            ']' => TokenType.DataEnd,
            ':' => TokenType.Colon,
            '"' => TokenType.Quote,
            ' ' => TokenType.WhiteSpace,
            '\n' => TokenType.NewLine,
            _ => TokenType.Data
        };
    }
}