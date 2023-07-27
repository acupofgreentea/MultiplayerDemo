using UnityEngine;

public class Car : MonoBehaviour
{
    [field: SerializeField] public Transform CarBallFollowPos { get; private set; }
    
    public WreckingBall WreckingBall { get; set; }
    public CarMovement CarMovement { get; private set; }
    public CarRopeHandler CarRopeHandler { get; private set; }

    private void Awake()
    {        
        CarMovement = GetComponent<CarMovement>().Init(this);
        CarRopeHandler = GetComponent<CarRopeHandler>().Init(this);
    }
} 
