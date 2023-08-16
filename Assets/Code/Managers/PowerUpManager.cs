using Fusion;
using UnityEngine;

public abstract class PowerUpManager<T> : NetworkBehaviour where T : NetworkBehaviour
{

    [SerializeField] protected T powerUpPrefab = null;
    
    protected T currentPowerUp;

    protected bool CanSpawn => currentPowerUp == null;

    protected abstract void SpawnPowerUp();
}