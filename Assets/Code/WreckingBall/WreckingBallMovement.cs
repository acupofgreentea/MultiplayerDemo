using Fusion;
using UnityEngine;

public class WreckingBallMovement : NetworkBehaviour
{
    public Transform FollowPos { get; set; }
    
    [field: SerializeField] public float LerpSpeed { get; private set; } = 3f;

    [field: SerializeField] public Transform Model { get; private set; }

    [field: SerializeField, Range(0, 1)] public float Radius { get; private set; } = 0.3f;
    
    [field: SerializeField] public float RotateSpeed { get; private set; } = 15f;

    
    private WreckingBall wreckingBall;
    public WreckingBallMovement Init(WreckingBall wreckingBall)
    {
        this.wreckingBall = wreckingBall;

        return this;
    }
}