public abstract class WreckingBallStateBase
{
    protected WreckingBall wreckingBall;
    
    public WreckingBallStateBase(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}