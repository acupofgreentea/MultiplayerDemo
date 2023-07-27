using Fusion;
using UnityEngine;

public class CarRopeHandler : NetworkBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    
    [SerializeField] private Transform carRopeStartPosition;

    private Car car;

    public CarRopeHandler Init(Car car)
    {
        this.car = car;

        return this;
    }
    
    public override void FixedUpdateNetwork()
    {
        if (car == null)
            return;

        if (car.WreckingBall == null)
            return;
        
        lineRenderer.SetPosition(0, carRopeStartPosition.position);
        lineRenderer.SetPosition(1, car.WreckingBall.transform.position);
    }
}