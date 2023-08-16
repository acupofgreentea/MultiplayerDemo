using Fusion;

public class SafeZoneManager : NetworkBehaviour
{
    public override void Spawned()
    {
        Managers.Instance.GameManager.OnCloseTimerExpired += HandleTimerExpired;
    }

    private void HandleTimerExpired()
    {
        //close safe zone
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Managers.Instance.GameManager.OnCloseTimerExpired -= HandleTimerExpired;
    }
}
