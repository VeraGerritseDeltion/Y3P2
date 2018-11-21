using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public CarStats myCar;
    public float hor;
    public float vert;
    public int playerNum = 1;

    private float currentMaxSpeed;
    private float currentSpeed;
    private bool drifting;
    private float h;
    [SerializeField] private float driftMultiplier;

	void Start () {
		
	}
	
	void Update ()
    {
        Movement();
    }

    private void Movement()
    {
        currentMaxSpeed = myCar.speed;
        Turn();
        Motor();
    }

    private void Motor()
    {
        float spd = currentMaxSpeed * Time.deltaTime;
        if (Input.GetButton("C" + playerNum.ToString() + " A"))
        {
            currentSpeed = Mathf.Clamp(currentSpeed + Acceleration(), -spd, spd);
        }
        else if (Input.GetButton("C" + playerNum.ToString() + " B"))
        {
            currentSpeed = Mathf.Clamp(currentSpeed - Acceleration() * 2, -spd / 2, spd);
        }
        else if (currentSpeed < 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + Acceleration() / 1.5f, -spd, 0);
        }
        else if (currentSpeed > 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed - Acceleration() / 1.5f, 0, spd);
        }

        transform.Translate(currentSpeed, 0, 0);
    }

    private void Turn()
    {
        Drift();
        if (!drifting)
        {
            if (currentSpeed != 0)
            {
                hor = Input.GetAxis("C" + playerNum.ToString() + " Hor") * myCar.handling;
            }
            else if (hor > 0)
            {
                hor = Mathf.Clamp(hor - 0.1f, 0, myCar.handling);
            }
            else if (hor < 0)
            {
                hor = Mathf.Clamp(hor + 0.1f, -myCar.handling, 0);
            }
        }
        transform.Rotate(0, hor, 0);
    }

    private void Drift()
    {
        drifting = false;
        if(currentSpeed != 0)
        {
            if (Input.GetButton("C" + playerNum.ToString() + " BL"))
            {
                print("Test");
                h = Mathf.Lerp(-1, 1, h - 0.1f);
                hor = h * myCar.handling * driftMultiplier;
                drifting = true;
            }
            else if (Input.GetButton("C" + playerNum.ToString() + " BR"))
            {
                h = Mathf.Lerp(-1, 1, h + 0.1f);
                hor = -h * myCar.handling * driftMultiplier;
                drifting = true;
            }
        }


        if (drifting)
        {
            print("Test3");
            currentMaxSpeed = currentMaxSpeed / driftMultiplier;
            //transform.Rotate(0, hor, 0);
        }
    }

    float Acceleration()
    {
        return myCar.acceleration / 1000;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Car")
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce(Force(other.contacts[0].normal));
        }
    }

    private Vector3 Force(Vector3 dir)
    {
        //float force = currentSpeed * myCar.weight;
        Vector3 force = dir * currentSpeed * myCar.weight* 10;
        return force;
    }
}
