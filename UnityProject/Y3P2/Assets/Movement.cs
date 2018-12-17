using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public List<WheelCollider> wheels = new List<WheelCollider>();

    public CarStats myStats;
    public float speed;


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
        if (Input.GetKey(KeyCode.W) && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {

                wheels[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats);
            }
        }
        else if(Input.GetKey(KeyCode.S) && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {

                wheels[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats);
            }
        }
        else if (Speed() > MovCalculationManager.instance.MaxSpeed(myStats) || Speed() < -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            { 
                wheels[i].motorTorque = 0;
            }
        }
        else if (Speed() > 0.1 && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
        }
        else if(Speed() < -0.1 && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
        }
        else
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                print("still");
                wheels[i].motorTorque = 0;
            }
        }
    }

    private float Speed()
    {
        return (2 * Mathf.PI * wheels[0].radius * wheels[0].rpm * 60f / 1000f);
    }
}
