using System.Collections;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawner : SpawnerPrototype<PlayerSpawnPointPrototype>
{
    [SerializeField] private WreckingBall wreckingBallPrefab;
    
    public static UnityAction<NetworkObject> OnPlayerSpawned { get; set; }

    protected override void RegisterPlayerAndObject(PlayerRef player, NetworkObject playerObject)
    {
        base.RegisterPlayerAndObject(player, playerObject);
        
        OnPlayerSpawned?.Invoke(playerObject);

        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return null;       
            Car car = playerObject.GetComponent<Car>();

            var wreckingBall = Runner.Spawn(wreckingBallPrefab, car.CarBallFollowPos.position);
            wreckingBall.OnSpawn(car);
        }
    }
}