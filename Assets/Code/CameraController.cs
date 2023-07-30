using Cinemachine;
using Fusion;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachine;


    public void OnPlayerSpawned(Transform player)
    {
        cinemachine.Follow = player;
    }
}