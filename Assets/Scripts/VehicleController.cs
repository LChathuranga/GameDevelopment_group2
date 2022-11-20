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
    public float breackFource = 200f;
    public float presentBreackFource = 0f;

    [Header("Vehicle Stearing")]
    public float wheelsTorque = 20f;
    public float presentTurnAngle = 0f;

    [Header("Vehicle Sequrity")]
    public PlayerScript player;
    public Transform vehicleDoor;
    public float radius = 15f;
    public bool isOpend=false;


    [Header("Disable Things")]
    public GameObject Aimcam;
    public GameObject AimCanvas;
    public GameObject thirdPersonCam;
    public GameObject thirdPersonCanvas;
    public GameObject PlayerCharacter;

    

    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpend = true;
                radius = 5000f;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                radius = 15f;
                isOpend = false;
            }
        }

        if (isOpend==true)
        {
           
            Aimcam.SetActive(false);
            AimCanvas.SetActive(false);
            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            PlayerCharacter.SetActive(false);

            MoveVehicle();
            VehicleStearing();
            ApplyBreack();

        }
        else if(isOpend==false)
        {
            Aimcam.SetActive(true);
            AimCanvas.SetActive(true);
            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }
        
          
            

        

        
    }

    void MoveVehicle()
    {
        frontLeftColider.motorTorque = presentAcceleration;
        frontRightColider.motorTorque = presentAcceleration;
        backLeftColider.motorTorque = presentAcceleration;
        backLeftColider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationFourse * -Input.GetAxis("Vertical");
    }

    void VehicleStearing()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");

        frontLeftColider.steerAngle = presentTurnAngle;
        frontRightColider.steerAngle = presentTurnAngle;

        //Animation
        StearingWheels(frontLeftColider, frontLeftTransform);
        StearingWheels(frontRightColider, frontRightTransform);
        StearingWheels(backRightColider, backRightTransform);
        StearingWheels(backLeftColider, backLeftTransform);

    }
    void StearingWheels(WheelCollider wc,Transform wt)
    {
        Vector3 position;
        Quaternion rotation;

        wc.GetWorldPose(out position, out rotation);
        wt.position = position;
        wt.rotation = rotation;
    }
    void ApplyBreack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreackFource = breackFource;
        }
        else
        {
            presentBreackFource = 0;
        }
        frontRightColider.brakeTorque = presentBreackFource;
        frontLeftColider.brakeTorque = presentBreackFource;
        backLeftColider.brakeTorque = presentBreackFource;
        backRightColider.brakeTorque = presentBreackFource;
    }
}

