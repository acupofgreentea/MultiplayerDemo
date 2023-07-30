using UnityEngine;

public class WreckingBallFollowState : WreckingBallStateBase
{
    public WreckingBallFollowState(WreckingBall wreckingBall, float lerpSpeed, float radius, Transform model) : base(wreckingBall)
    {
        this.lerpSpeed = lerpSpeed;
        this.radius = radius;
        this.model = model;
    }

    public override void EnterState()
    {
        followTarget = wreckingBall.WreckingBallMovement.FollowPos;
    }
    
    private Vector3 lastPosition;
    private Transform followTarget;
    
    private readonly Transform model;
    private readonly float radius;
    private readonly float lerpSpeed;
    public override void UpdateState()
    {
        Vector3 currentPosition = wreckingBall.transform.position;
        
        wreckingBall.transform.position = Vector3.Lerp(currentPosition, followTarget.position, lerpSpeed * wreckingBall.Runner.DeltaTime);
        
        Vector3 direction = lastPosition - currentPosition;
        float distance = direction.magnitude;
        float angle = distance * (180f / Mathf.PI) * radius;
        
        model.rotation = Quaternion.Euler(followTarget.right * angle) * model.rotation;
        
        lastPosition = currentPosition;
    }

    public override void ExitState()
    {
    }
}