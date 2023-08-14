using Fusion;
using UnityEngine;

public class WreckingBallMovement : NetworkBehaviour
{
    [field: SerializeField] public float LerpSpeed { get; private set; } = 3f;

    [SerializeField] private Transform model;

    [SerializeField, Range(0, 1)] private float radius = 0.3f;
    
    [field: SerializeField] public float RotateSpeed { get; private set; } = 15f;
    
    private WreckingBall wreckingBall;

    [Networked] public Vector3 NextPosition {get; set;}

    public WreckingBallMovement Init(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;
        return this;
    }

    private Vector3 lastContactNormal;

    public override void FixedUpdateNetwork()
    {
        if (wreckingBall.Car == null)
            return;

        var followPos = wreckingBall.Car.CarBallFollowPos;
        NextPosition = followPos.position;

        Vector3 direction = transform.position - NextPosition;
        transform.position = Vector3.Lerp(transform.position, NextPosition, LerpSpeed * Runner.DeltaTime);

        Vector3 movement = direction * Runner.DeltaTime;
		float distance = movement.magnitude;

		if (distance < 0.001f) 
			return;

		float angle = distance * (180f / Mathf.PI) / radius;

        Vector3 rotationAxis = Vector3.Cross(lastContactNormal, movement).normalized;
		model.rotation = Quaternion.Euler(rotationAxis * angle) * model.rotation;
    }
}