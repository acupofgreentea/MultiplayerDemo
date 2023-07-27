using UnityEngine;

public class Car : MonoBehaviour
{
    [field: SerializeField] public Transform CarBallFollowPos { get; private set; }
    public CarMovement CarMovement { get; private set; }

    private void Awake()
    {        
        CarMovement = GetComponent<CarMovement>().Init(this);
    }
} 
