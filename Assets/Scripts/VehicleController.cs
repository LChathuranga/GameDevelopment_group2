using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheel Coliders")]
    public WheelCollider frontLeftColider;
    public WheelCollider frontRightColider;
    public WheelCollider backLeftColider;
    public WheelCollider backRightColider;

    [Header("Wheel Transform")]
    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform backLeftTransform;
    public Transform backRightTransform;

    [Header("Engine")]
    public float accelerationFourse=100f;
    public float presentAcceleration=0f;


    private void Update()
    {
        MoveVehicle();
    }

    void MoveVehicle()
    {
        frontLeftColider.motorTorque = presentAcceleration;
        frontRightColider.motorTorque = presentAcceleration;
        backLeftColider.motorTorque = presentAcceleration;
        backLeftColider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationFourse * -Input.GetAxis("Vertical");
    }
}

