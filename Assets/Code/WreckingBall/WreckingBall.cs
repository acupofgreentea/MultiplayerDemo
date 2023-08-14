using System.Collections;
using Fusion;
using UnityEngine;

public class WreckingBall : NetworkBehaviour
{
    public WreckingBallMovement WreckingBallMovement { get; private set; }
    public WreckingBallStateController WreckingBallStateController { get; private set; }

    [field: SerializeField] public Car Car { get; set; }

    public Transform CarInterpolationTarget => Car.transform;

    private NetworkObject networkObject;

    public NetworkObject NetworkObject => networkObject ??= GetComponent<NetworkObject>();

    private void Awake()
    {
        WreckingBallMovement = GetComponent<WreckingBallMovement>().Init(this);
        WreckingBallStateController = GetComponent<WreckingBallStateController>().Init(this);
    }

    public override void Spawned()
    {
        networkObject = GetComponent<NetworkObject>();
        transform.SetParent(null);
    }
}