using Fusion;
using UnityEngine;

public class WreckingBallMovement : NetworkBehaviour
{
    [field: SerializeField] public float LerpSpeed { get; private set; } = 3f;

    [SerializeField] private Transform model;

    [SerializeField, Range(0, 1)] private float radius = 0.3f;
    
    [field: SerializeField] public float RotateSpeed { get; private set; } = 15f;
    
    private WreckingBall wreckingBall;

    public Transform Model => model;

    [Networked(OnChanged = nameof(OnNextPositionChanged))] public Vector3 NextPosition {get; set;}

    public static void OnNextPositionChanged(Changed<WreckingBallMovement> behaviour)
    {
        var followPos = behaviour.Behaviour.wreckingBall.Car.CarBallFollowPos;
        Vector3 direction = behaviour.Behaviour.transform.position - behaviour.Behaviour.NextPosition;
        Vector3 movement = direction * behaviour.Behaviour.Runner.DeltaTime;

		float distance = movement.magnitude;

		float angle = distance * (180f / Mathf.PI) / 0.5f;

		behaviour.Behaviour.Model.rotation = Quaternion.Euler(followPos.right * angle) * behaviour.Behaviour.Model.rotation;
    }
    public WreckingBallMovement Init(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;
        return this;
    }

    public override void FixedUpdateNetwork()
    {
        var followPos = wreckingBall.Car.CarBallFollowPos;
        NextPosition = followPos.position;

        transform.position = Vector3.Lerp(transform.position, NextPosition, LerpSpeed * Runner.DeltaTime);
    }
}