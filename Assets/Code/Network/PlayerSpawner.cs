using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkObject playerPrefab;

    [SerializeField] private List<Transform> spawnPoints;

    private void SpawnPlayer(PlayerRef playerRef)
    {
        NetworkObject playerObject = null;
        if (Runner.IsServer)
        {
            int index = playerRef % spawnPoints.Count;
            Vector3 spawnPoint = spawnPoints[index].position;
            
            playerObject = Runner.Spawn(playerPrefab, spawnPoint, Quaternion.identity, playerRef);
            
            Runner.SetPlayerObject(playerRef, playerObject);
            Managers.Instance.GameManager.AddPlayer(playerObject);
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
                Managers.Instance.GameManager.RemovePlayer(playerObject);
            }
        }
    }
}