using Fusion;
using UnityEngine;

public class WreckingBall : NetworkBehaviour
{
    public WreckingBallMovement WreckingBallMovement { get; private set; }
    public WreckingBallStateController WreckingBallStateController { get; private set; }

    public Car Car { get; private set; }

    public Transform CarInterpolationTarget => Car.transform.GetChild(0);
    
    private void Awake()
    {
        WreckingBallMovement = GetComponent<WreckingBallMovement>().Init(this);
        WreckingBallStateController = GetComponent<WreckingBallStateController>().Init(this);
    }

    public void OnSpawn(Car car)
    {
        this.Car = car;
        car.WreckingBall = this;
        WreckingBallMovement.FollowPos = car.CarBallFollowPos;
    }
}