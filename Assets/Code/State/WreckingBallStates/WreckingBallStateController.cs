using System.Collections;
using System.Collections.Generic;

public class WreckingBallStateController : StateControllerBase<WreckingBallStates, WreckingBallStateBase>
{
    private WreckingBall wreckingBall;

    public WreckingBallStateController Init(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;
        return this;
    }

    public override void Spawned()
    {
        CreateDictionary();

        StartCoroutine(Delay());
        
        IEnumerator Delay()
        {
            yield return null;
            ChangeState(WreckingBallStates.TurnAround);
        }
    }

    protected override void CreateDictionary()
    {
        WreckingBallMovement wreckingBallWreckingBallMovement = wreckingBall.WreckingBallMovement;
        
        stateDictionary = new Dictionary<WreckingBallStates, WreckingBallStateBase>
        {
            {
                WreckingBallStates.Follow, new WreckingBallFollowState(wreckingBall, wreckingBallWreckingBallMovement.LerpSpeed,
                    wreckingBallWreckingBallMovement.Radius, wreckingBallWreckingBallMovement.Model) },
            
            { WreckingBallStates.TurnAround, new WreckingBallTurnAroundState(wreckingBall, wreckingBallWreckingBallMovement.RotateSpeed, wreckingBallWreckingBallMovement.Model) },
        };
    }

    public override void FixedUpdateNetwork()
    {
        CurrentState?.UpdateState();
    }

    public override void ChangeState(WreckingBallStates type)
    {
        CurrentState?.ExitState();
        CurrentState = stateDictionary.GetValueOrDefault(type);
        CurrentState?.EnterState();
    }
}