using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public WheelSet myWheels;

    public List<WheelCollider> wheelCol = new List<WheelCollider>();

    public CarSet myCar;
    public Transform carLoc;

    public Animator anim;

    public int playerNum = 1;

    public CarStats myStats;
    public float speed;

    private float hor;

    private float newSteerAngle;

    void Update ()
    {
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

        for (int i = 0; i < wheelCol.Count; i++)
        {
            myWheels.wheelObj[i].transform.rotation = wheelCol[i].transform.rotation;
        }
    }

    private void CarMovement()
    {
        if(MovCalculationManager.instance != null && myStats != null)
        {
            Motor();
        }
    }

    private void Motor()
    {
        if (Input.GetButton("C" + playerNum.ToString() + " A") && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {
                wheelCol[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats);
            }
            Turn();
        }
        else if(Input.GetButton("C" + playerNum.ToString() + " B") && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {

                wheelCol[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats);
            }
            Turn();
        }
        else if (Speed() > MovCalculationManager.instance.MaxSpeed(myStats) || Speed() < -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {
                wheelCol[i].motorTorque = 0;
            }
            Turn();
        }
        else if (Speed() > 0.1 && Speed() < MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {
                wheelCol[i].motorTorque = -MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
            Turn();
        }
        else if(Speed() < -0.1 && Speed() > -MovCalculationManager.instance.MaxSpeed(myStats))
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {
                wheelCol[i].motorTorque = MovCalculationManager.instance.Acceleration(myStats) - 100;
            }
            Turn();
        }
        else
        {
            for (int i = 0; i < wheelCol.Count; i++)
            {
                wheelCol[i].motorTorque = 0;
                wheelCol[i].steerAngle = 0;
            }
        }
    }

    bool right;
    bool left;
    bool drift;
    float h;

    private void Turn()
    {
        if(Input.GetButton("C" + playerNum.ToString() + " RT"))
        {
            if (Input.GetAxis("C" + playerNum.ToString() + " Hor") > 0 && !right)
            {
                if (!drift)
                {
                    drift = true;
                    anim.SetBool("Drifting", true);
                }
                h = Mathf.Clamp(h + 0.1f, -1f, 1f);
                anim.SetFloat("HorAxis", h);
                wheelCol[0].steerAngle = Mathf.Clamp(wheelCol[0].steerAngle + 0.5f, -MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier, MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier);
                wheelCol[1].steerAngle = Mathf.Clamp(wheelCol[1].steerAngle + 0.5f, -MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier, MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier);
                left = true;
            }
            else if (Input.GetAxis("C" + playerNum.ToString() + " Hor") < 0 && !left)
            {
                if (!drift)
                {
                    drift = true;
                    anim.SetBool("Drifting", true);
                }
                h = Mathf.Clamp(h - 0.1f, -1f, 1f);
                print(h);
                anim.SetFloat("HorAxis", h);
                wheelCol[0].steerAngle = Mathf.Clamp(wheelCol[0].steerAngle - 0.5f, MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier, -MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier);
                wheelCol[1].steerAngle = Mathf.Clamp(wheelCol[1].steerAngle - 0.5f, MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier, -MovCalculationManager.instance.Handling(myStats) * hor * myStats.driftMultiplier);
                right = true;
            }
            return;
        }
        else
        {
            anim.SetFloat("HorAxis", 0);
            drift = false;
            anim.SetBool("Drifting", false);
            left = false;
            right = false;
            h = 0;
            wheelCol[0].steerAngle = 0;
            wheelCol[1].steerAngle = 0;
        }
        hor = Input.GetAxis("C" + playerNum.ToString() + " Hor");
        print(hor);
        if(hor > 0)
        {
            wheelCol[0].steerAngle = MovCalculationManager.instance.Handling(myStats) * hor;
            wheelCol[1].steerAngle = MovCalculationManager.instance.Handling(myStats) * hor;
        }
        else if(hor < 0)
        {
            wheelCol[0].steerAngle = (MovCalculationManager.instance.Handling(myStats) * hor);
            wheelCol[1].steerAngle = (MovCalculationManager.instance.Handling(myStats) * hor);
        }
        else
        {
            wheelCol[0].steerAngle = 0;
            wheelCol[1].steerAngle = 0;
        }
    }

    private float Speed()
    {
        return (2 * Mathf.PI * wheelCol[0].radius * wheelCol[0].rpm * 60f / 1000f);
    }
}
