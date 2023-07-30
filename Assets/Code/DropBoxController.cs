using Fusion;
using UnityEngine;

public class DropBoxController : NetworkBehaviour
{
    private NetworkObject networkObject;

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Car car))
            return;

        Debug.LogError("destroyed dropbox");
        Runner.Despawn(networkObject);
    }
}