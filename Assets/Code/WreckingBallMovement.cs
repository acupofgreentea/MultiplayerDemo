using Fusion;
using UnityEngine;

public class WreckingBallMovement : NetworkBehaviour
{
    public Transform FollowPos { get; set; }
    
    [SerializeField] private float lerpSpeed = 3f;

    [SerializeField] private Transform model;

    [SerializeField, Range(0, 1)] private float radius = 0.3f;

    private Vector3 lastPosition;    
    
    private WreckingBall wreckingBall;
    public WreckingBallMovement Init(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;
        

        return this;
    }

    public override void FixedUpdateNetwork()
    {
        if (FollowPos == null)
            return;
        
        Vector3 currentPosition = transform.position;
        
        transform.position = Vector3.Lerp(currentPosition, FollowPos.position, lerpSpeed * Runner.DeltaTime);
        
        Vector3 direction = lastPosition - currentPosition;
        float distance = direction.magnitude;
        float angle = distance * (180f / Mathf.PI) * radius;
        
        model.rotation = Quaternion.Euler(FollowPos.right * angle) * model.rotation;
        
        lastPosition = currentPosition;
    }
}