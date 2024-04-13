using Xunit.Abstractions;

namespace TickTask.Parser.Test;

public class TokenizerTest
{
    private readonly ITestOutputHelper output;
    public TokenizerTest(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void TokenizeTest()
    {
    }
}