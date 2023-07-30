using UnityEngine;
using Fusion;

public class Car : NetworkBehaviour
{
    [field: SerializeField] public Transform CarBallFollowPos { get; private set; }
    
    public WreckingBall WreckingBall { get; set; }
    public CarMovement CarMovement { get; private set; }
    public CarRopeHandler CarRopeHandler { get; private set; }

    private NetworkRigidbody networkRigidbody;

    private void Awake()
    {
        networkRigidbody = GetComponent<NetworkRigidbody>();
        CarMovement = GetComponent<CarMovement>().Init(this);
        CarRopeHandler = GetComponent<CarRopeHandler>().Init(this);
    }

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            Camera.main.GetComponent<CameraController>().OnPlayerSpawned(networkRigidbody.InterpolationTarget);
        }
    }
} 
