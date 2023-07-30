using Fusion;
using UnityEngine;

public abstract class PowerUpManager<T> : SimulationBehaviour where T : NetworkBehaviour
{
    [SerializeField] private float spawnRate = 5f;

    [SerializeField] protected T powerUpPrefab = null;
    
    protected float lastSpawnTime;

    protected T currentPowerUp;

    protected bool CanSpawn => Runner.SimulationRenderTime > spawnRate + lastSpawnTime && currentPowerUp == null;

    protected abstract void SpawnPowerUp();
}