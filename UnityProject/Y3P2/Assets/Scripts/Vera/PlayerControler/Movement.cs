using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public List<WheelCollider> wheels = new List<WheelCollider>();
    public List<GameObject> objWheels = new List<GameObject>();

    public Transform wheelLoc;
    public Transform carLoc;

    public int playerNum = 1;

    public CarStats myStats;
    public float speed;

    private float hor;

    private float newSteerAngle;

    void Update ()
    {
        //print (Speed());
        if(GameManager.instance != null)
        {
            if (GameManager.instance.racing)
            {
                CarMovement();
            }
        }
        else
        {
            CarMovement();
        }
        speed = Speed();

        for (int i = 0; i < wheels.Count; i++)
        {
            objWheels[i].transform.rotation = wheels[i].transform.rotation;
        }

        Turn();
    }


    private void CarMovement()
    {
        if(MovCalculationManager.instance != null && myStats != null)
        {
            Motor();
            //Break();
        }
    }

    private void Break()
    {
        if (Input.GetKey(KeyCode.S))
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                print("break");
                wheels[i].motorTorque = 0;
                wheels[i].brakeTorque = MovCalculationManager.instance.Break(myStats);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].brakeTorque = 0;
            }
        }
    }

    private void Motor()
    {
        if (Input.GetButton("C" + playerNum.ToString() + " A") && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {

                wheels[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats);
            }
            Turn();
        }
        else if(Input.GetButton("C" + playerNum.ToString() + " B") && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {

                wheels[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats);
            }
            Turn();
        }
        else if (Speed() > MovCalculationManager.instance.MaxSpeed(myStats) || Speed() < -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            { 
                wheels[i].motorTorque = 0;
            }
            Turn();
        }
        else if (Speed() > 0.1 && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
            Turn();
        }
        else if(Speed() < -0.1 && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
            Turn();
        }
        else
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                print("still");
                wheels[i].motorTorque = 0;
                wheels[0].steerAngle = 0;
                wheels[1].steerAngle = 0;
            }
        }
    }

    private void Turn()
    {
        hor = Input.GetAxis("C" + playerNum.ToString() + " Hor");
        print(hor);
        if(hor > 0)
        {

            wheels[0].steerAngle = Mathf.Clamp(wheels[0].steerAngle + 0.5f, -MovCalculationManager.instance.Handling(myStats) * hor, MovCalculationManager.instance.Handling(myStats) * hor);
            wheels[1].steerAngle = Mathf.Clamp(wheels[1].steerAngle + 0.5f, -MovCalculationManager.instance.Handling(myStats) * hor, MovCalculationManager.instance.Handling(myStats) * hor);
        }
        else if( hor < 0)
        {
            wheels[0].steerAngle = Mathf.Clamp(wheels[0].steerAngle - 0.5f, MovCalculationManager.instance.Handling(myStats) * hor, -MovCalculationManager.instance.Handling(myStats) * hor);
            wheels[1].steerAngle = Mathf.Clamp(wheels[1].steerAngle - 0.5f, MovCalculationManager.instance.Handling(myStats) * hor, -MovCalculationManager.instance.Handling(myStats) * hor);
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    private float Speed()
    {
        return (2 * Mathf.PI * wheels[0].radius * wheels[0].rpm * 60f / 1000f);
    }
}
