namespace Test.Acceptance.Tools.Core;

public interface IStartableHost : IHost
{
    void Start();

    void Stop();
}
