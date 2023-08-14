using UnityEngine;

public class WreckingBallFollowState : WreckingBallStateBase
{
    public WreckingBallFollowState(WreckingBall wreckingBall, float lerpSpeed) : base(wreckingBall)
    {
        this.lerpSpeed = lerpSpeed;
    }

    public override void EnterState()
    {
    }
    
    private readonly float lerpSpeed;
    
    public override void UpdateState()
    {
        if (wreckingBall.Car == null)
            return;
        
        wreckingBall.transform.position = Vector3.Lerp(wreckingBall.transform.position, wreckingBall.Car.CarBallFollowPos.position, lerpSpeed * wreckingBall.Runner.DeltaTime);
    }

    public override void ExitState()
    {
    }
}