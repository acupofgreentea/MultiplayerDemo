using Fusion;
using UnityEngine;

public class DropBoxManager : PowerUpManager<DropBoxController>
{
    [SerializeField] private float spawnRate = 5f;

    [Networked] public TickTimer SpawnTimer {get; private set;}

    public override void Spawned()
    {
        DropBoxController.OnDropboxInteracted += HandleDropBoxInteracted;   
        SpawnTimer = TickTimer.CreateFromSeconds(Runner, spawnRate);
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
            return;
        
        if (!CanSpawn)
            return;
        
        if(SpawnTimer.Expired(Runner))
        {
            SpawnPowerUp();
        }       
    }

    private Vector3 GetLandPosition()
    {
        Vector3 center = Vector3.zero;

        Vector3 landPos = center + Helpers.GetRandomPosition(-10f, 10f, 1);

        return landPos;
    }

    private void HandleDropBoxInteracted()
    {
        currentPowerUp = null;
        SpawnTimer = TickTimer.CreateFromSeconds(Runner, spawnRate);
    }
    protected override void SpawnPowerUp()
    {
        Vector3 landPosition = GetLandPosition();
        Vector3 spawnPos = landPosition;
        spawnPos.y += 10f;
        
        currentPowerUp = Runner.Spawn(powerUpPrefab, spawnPos);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        DropBoxController.OnDropboxInteracted -= HandleDropBoxInteracted;   
    }
}