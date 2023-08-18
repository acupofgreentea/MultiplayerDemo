using Fusion;
using UnityEngine;

public class SafeZoneManager : NetworkBehaviour
{
    [SerializeField] private GameSettingsSO gameSettingSO;

    [Networked] private TickTimer CircleCloseTimer {get; set;}
    
    [Networked] private float CurrentZoneRange {get; set;}

    [Networked] private float ElapsedTime {get; set;}

    [Networked] private float LastZoneRange {get; set;}
    
    [Networked] private float TargetZoneRange {get; set;}

    private bool isTimerActive = false;
    public int zoneLevel = 0;

    public override void Spawned()
    {
        CircleCloseTimer = TickTimer.CreateFromSeconds(Runner, gameSettingSO.ZoneActiveTime);
        isTimerActive =  true;
        CurrentZoneRange = gameSettingSO.FirstZoneRange;
    }

    private void OnGUI()
    {
        if(Runner == null)
            return;

        GUI.Label(new Rect(10, 10, 300, 20), $"Current Zone Range: {CurrentZoneRange.ToString()}");
        GUI.Label(new Rect(10, 30, 300, 20), $"Target Zone Range: {TargetZoneRange.ToString()}");
    }

    public override void FixedUpdateNetwork()
    {
        SendMessageObjectsOutsideZone();
        if(!isTimerActive)
        {
            ElapsedTime += Runner.DeltaTime / gameSettingSO.ZoneCloseSequence;
            CurrentZoneRange = Mathf.Lerp(LastZoneRange, TargetZoneRange, ElapsedTime);

            if(ElapsedTime >= 1f)
            {
                if(gameSettingSO.CanZoneClose(CurrentZoneRange))
                {
                    CircleCloseTimer = TickTimer.CreateFromSeconds(Runner, gameSettingSO.ZoneActiveTime);
                    isTimerActive = true;
                }
            }
            return;
        }

        if(CircleCloseTimer.Expired(Runner))
        {
            HandleTimerExpired();
            isTimerActive = false;
        }
    }
    private void HandleTimerExpired()
    {
        ElapsedTime = 0f;
        zoneLevel++;
        LastZoneRange = CurrentZoneRange;
        TargetZoneRange = gameSettingSO.GetNextRangeWithLevel(zoneLevel);
    }

    private void SendMessageObjectsOutsideZone()
    {
        var players = Managers.Instance.GameManager.Players;
        
        foreach(NetworkObject player in players)
        {
            var playerPosition = player.transform.position;

            if(Vector3.Distance(playerPosition, transform.position) > CurrentZoneRange)
            {
                Debug.LogError("player is outside of the zone", player);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        float range = Application.isPlaying && Runner != null ? CurrentZoneRange : gameSettingSO.FirstZoneRange;

        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
