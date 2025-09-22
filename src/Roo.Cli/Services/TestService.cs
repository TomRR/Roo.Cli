namespace Roo.Cli.Services;

public interface ITest
{
    public string Get();
}
public class TestService : ITest
{
    public string Get()
    {
        return "hello from test";
    }
}