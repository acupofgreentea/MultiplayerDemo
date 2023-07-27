using System.Collections;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SpawnerPrototype<PlayerSpawnPointPrototype>
{
    [SerializeField] private WreckingBall wreckingBallPrefab;

    protected override void RegisterPlayerAndObject(PlayerRef player, NetworkObject playerObject)
    {
        base.RegisterPlayerAndObject(player, playerObject);

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