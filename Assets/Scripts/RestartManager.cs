using System.Collections.Generic;

public class RestartManager : IRestartable
{
    private readonly List<IRestartable> _restartables = new List<IRestartable>();

    public void Register(IRestartable restartable)
    {
        _restartables.Add(restartable);
    }

    public void UnRegister(IRestartable restartable)
    {
        _restartables.Remove(restartable);
    }

    public void Restart()
    {
        foreach (var restartable in _restartables)
        {
            restartable.Restart();
        }
    }
}
