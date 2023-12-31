﻿using Fusion;
using UnityEngine;

public class CarMovement : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 50;
    [SerializeField] private float maxSpeed = 15;
    [SerializeField] private float drag = 0.98f;
    [SerializeField] private float steerAngle = 20;
    [SerializeField] private float traction = 1;    
    
    [Networked] public Vector3 moveForce {get; set;}
    
    private Car car;    
    
    public CarMovement Init(Car car)
    {
        this.car = car;

        return this;
    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out PlayerNetworkInput input))
            return;

        if (!input.HasInput)
            return;
        
        moveForce += transform.forward * moveSpeed *  Runner.DeltaTime;
        transform.position += moveForce *  Runner.DeltaTime;

        transform.Rotate(Vector3.up * input.MovementInput.x * moveForce.magnitude * steerAngle * Runner.DeltaTime);

        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Runner.DeltaTime) * moveForce.magnitude;
    }
    
}