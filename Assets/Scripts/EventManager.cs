using System;

public static class EventManager
{
    public static event Action OnPlayerCharge;
    public static event Action OnPlayerDischarge;

    public static void TriggerPlayerCharge()
    {
        OnPlayerCharge?.Invoke();
    }

    public static void TriggerPlayerDischarge()
    {
        OnPlayerDischarge?.Invoke();
    }

    public static void TriggerRestartLevel()
    {
        GameManager.Instance.RestartStage();
    }
}