using Fusion;
using UnityEngine;

public class CarMovement : NetworkBehaviour
{
    [SerializeField] private float MoveSpeed = 50;
    [SerializeField] private float MaxSpeed = 15;
    [SerializeField] private float Drag = 0.98f;
    [SerializeField] private float SteerAngle = 20;
    [SerializeField] private float Traction = 1;    
    [SerializeField] private Vector3 MoveForce;


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerNetworkInput playerNetworkInput))
        {
            if (playerNetworkInput.MovementInput == Vector3.zero)
                return;
            
            MoveForce += transform.forward * MoveSpeed *  Runner.DeltaTime;
            transform.position += MoveForce *  Runner.DeltaTime;

            transform.Rotate(Vector3.up * playerNetworkInput.MovementInput.x * MoveForce.magnitude * SteerAngle * Runner.DeltaTime);

            MoveForce *= Drag;
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Runner.DeltaTime) * MoveForce.magnitude;
            
        }
    }
    
}