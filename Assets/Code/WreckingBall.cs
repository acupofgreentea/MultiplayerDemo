using Fusion;

public class WreckingBall : NetworkBehaviour
{
    public WreckingBallMovement WreckingBallMovement { get; private set; }

    private void Awake()
    {
        WreckingBallMovement = GetComponent<WreckingBallMovement>().Init(this);
    }

    private Car car;
    
    public void OnSpawn(Car car)
    {
        this.car = car;
        WreckingBallMovement.FollowPos = car.CarBallFollowPos;
    }
}