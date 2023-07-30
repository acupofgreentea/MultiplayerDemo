using UnityEngine;

public class WreckingBallTurnAroundState : WreckingBallStateBase
{
    public WreckingBallTurnAroundState(WreckingBall wreckingBall, float rotateSpeed, Transform model) :
        base(wreckingBall)
    {
        this.rotateSpeed = rotateSpeed;
    }


    Transform _pivot;
    Vector3 _offsetDirection;
    float _distance;

    private float rotateSpeed;
    private Transform model;

    public override void EnterState()
    {
        SetPivot(wreckingBall.CarInterpolationTarget);
    }

    public void SetPivot(Transform pivot)
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

    public override void UpdateState()
    {
        if (_pivot == null) return;

        Quaternion rotate = Quaternion.Euler(0, rotateSpeed * wreckingBall.Runner.DeltaTime, 0);

        _offsetDirection = (rotate * _offsetDirection).normalized;

        wreckingBall.transform.position = _pivot.position + _offsetDirection * _distance;
    }

    public override void ExitState()
    {
    }
}