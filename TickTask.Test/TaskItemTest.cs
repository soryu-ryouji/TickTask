using Xunit.Abstractions;

namespace TickTask.Test;
public class TestTaskItem
{
    private readonly ITestOutputHelper output;

    public TestTaskItem(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void TestInit()
    {
        var task = new TaskItem()
        {
            Name = "Test Task",
            Project = "Test Project"
        };

        output.WriteLine("........................");

        Assert.True(true);
    }
}