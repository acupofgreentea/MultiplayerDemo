using System.Collections;
using UnityEngine;
using Fusion;

public class Car : NetworkBehaviour
{
    [field: SerializeField] public Transform CarBallFollowPos { get; private set; }
    [field: SerializeField] public WreckingBall WreckingBall { get; set; }
    public CarMovement CarMovement { get; private set; }
    public CarRopeHandler CarRopeHandler { get; private set; }

    public override void Spawned()
    {
        CarMovement = GetComponent<CarMovement>().Init(this);
        CarRopeHandler = GetComponent<CarRopeHandler>().Init(this);

        if (HasInputAuthority)
        {
            Camera.main.GetComponent<CameraController>().OnPlayerSpawned(transform);
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if(WreckingBall != null)
            runner.Despawn(WreckingBall.NetworkObject);
    }
}