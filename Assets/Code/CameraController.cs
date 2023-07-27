using Cinemachine;
using Fusion;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        PlayerSpawner.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(NetworkObject player)
    {
        cinemachine.Follow = player.transform;
    }

    private void OnDestroy()
    {
        PlayerSpawner.OnPlayerSpawned -= OnPlayerSpawned;
    }
}