using Fusion;

public class WreckingBall : NetworkBehaviour
{
    public WreckingBallMovement WreckingBallMovement { get; private set; }

    private Car car;
    
    private void Awake()
    {
        WreckingBallMovement = GetComponent<WreckingBallMovement>().Init(this);
    }

    public void OnSpawn(Car car)
    {
        this.car = car;
        car.WreckingBall = this;
        WreckingBallMovement.FollowPos = car.CarBallFollowPos;
    }
}