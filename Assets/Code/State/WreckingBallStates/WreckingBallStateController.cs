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
            ChangeState(WreckingBallStates.Follow);
        }
    }

    protected override void CreateDictionary()
    {
        WreckingBallMovement wreckingBallWreckingBallMovement = wreckingBall.WreckingBallMovement;
        
        stateDictionary = new Dictionary<WreckingBallStates, WreckingBallStateBase>
        {
            {
                WreckingBallStates.Follow, new WreckingBallFollowState(wreckingBall, wreckingBallWreckingBallMovement.LerpSpeed) },
            
            { WreckingBallStates.TurnAround, new WreckingBallTurnAroundState(wreckingBall, wreckingBallWreckingBallMovement.RotateSpeed) },
        };
    }

    public override void FixedUpdateNetwork()
    {
        if(!HasInputAuthority)
            return;

        //CurrentState?.UpdateState();
    }

    public override void ChangeState(WreckingBallStates type)
    {
        var nextState = stateDictionary.GetValueOrDefault(type);
        
        if (CurrentState == nextState)
            return;
        
        CurrentState?.ExitState();
        CurrentState = nextState;
        CurrentState?.EnterState();
    }
}