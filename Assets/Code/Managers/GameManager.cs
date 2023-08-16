using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameSettingsSO gameSettingSO;

    [Networked] public TickTimer CircleCloseTimer {get; private set;}

    public UnityAction OnCloseTimerExpired {get; set;}

    public override void Spawned()
    {
        CircleCloseTimer = TickTimer.CreateFromSeconds(Runner, 0f);
    }
    public override void FixedUpdateNetwork()
    {
        if(CircleCloseTimer.Expired(Runner))
        {
            OnCloseTimerExpired?.Invoke();
            CircleCloseTimer = TickTimer.CreateFromSeconds(Runner, gameSettingSO.CircleCloseTimer);
        }
    }
}
