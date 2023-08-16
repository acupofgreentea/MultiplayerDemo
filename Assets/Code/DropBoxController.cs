using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class DropBoxController : NetworkBehaviour
{
    private NetworkObject networkObject;

    public static UnityAction OnDropboxInteracted {get; set;}

    public override void Spawned()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Car car))
            return;
        
        OnDropboxInteracted?.Invoke();
        
        car.WreckingBall.WreckingBallStateController.ChangeState(WreckingBallStates.TurnAround);
        Runner.Despawn(networkObject);
    }
}