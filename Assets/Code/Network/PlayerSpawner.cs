using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private WreckingBall wreckingBallPrefab;
    [SerializeField] private NetworkObject playerPrefab;

    [SerializeField] private List<Transform> spawnPoints;
    public static UnityAction<NetworkObject> OnPlayerSpawned { get; set; }

    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            int index = playerRef % spawnPoints.Count;
            Vector3 spawnPoint = spawnPoints[index].position;
            
            var playerObject = Runner.Spawn(playerPrefab, spawnPoint, Quaternion.identity, playerRef);
            
            Runner.SetPlayerObject(playerRef, playerObject);
            
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


    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }

    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerObject))
            {
                Runner.Despawn(playerObject);
                
                Runner.SetPlayerObject(playerRef, null);
            }
        }
    }
}