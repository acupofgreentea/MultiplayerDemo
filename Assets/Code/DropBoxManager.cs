using UnityEngine;

public class DropBoxManager : PowerUpManager<DropBoxController>
{
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
            return;
        
        if (!CanSpawn)
            return;
        
        SpawnPowerUp();
    }

    private Vector3 GetLandPosition()
    {
        Vector3 center = Vector3.zero;

        Vector3 landPos = center + Helpers.GetRandomPosition(-10f, 10f, 1);

        return landPos;
    }

    protected override void SpawnPowerUp()
    {
        Vector3 landPosition = GetLandPosition();
        Vector3 spawnPos = landPosition;
        spawnPos.y += 10f;

        lastSpawnTime = Runner.SimulationTime;
        
        currentPowerUp = Runner.Spawn(powerUpPrefab, spawnPos);
    }
}