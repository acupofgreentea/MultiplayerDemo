using UnityEngine;
using Fusion;

public class WreckingBallTurnAroundState : WreckingBallStateBase
{
    public WreckingBallTurnAroundState(WreckingBall wreckingBall, float rotateSpeed) :
        base(wreckingBall)
    {
        this.rotateSpeed = rotateSpeed;
    }


    Transform _pivot;
    Vector3 _offsetDirection;
    float _distance;

    private float rotateSpeed;
    private Transform model;

    private float turnAroundTotalTime = 3f;
    private float elapsedTime = 0f;
    public override void EnterState()
    {
        SetPivot(wreckingBall.CarInterpolationTarget);
        elapsedTime = 0f;
    }

    private void SetPivot(Transform pivot)
    {
        if (pivot != null)
        {
            _pivot = pivot;
            _offsetDirection = wreckingBall.transform.position - pivot.position;
            _distance = _offsetDirection.magnitude;
        }
        else
        {
            _pivot = null;
        }
    }
    [Networked] public Vector3 CurrentPosition {get; set;}

    public override void UpdateState()
    {
        if (_pivot == null) 
            return;

        if (elapsedTime >= turnAroundTotalTime)
        {
            wreckingBall.WreckingBallStateController.ChangeState(WreckingBallStates.Follow);
            return;
        }
        
        elapsedTime += wreckingBall.Runner.DeltaTime;
        

        Quaternion rotate = Quaternion.Euler(0, rotateSpeed * wreckingBall.Runner.DeltaTime, 0);

        _offsetDirection = (rotate * _offsetDirection).normalized;

        CurrentPosition = _pivot.position + _offsetDirection * _distance;
        wreckingBall.transform.position = CurrentPosition;
    }

    public override void ExitState()
    {
    }
}