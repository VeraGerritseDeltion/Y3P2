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

    public Camera myCam;

    public Transform wheelLoc;
    public Transform carLoc;

    [Header("Animators")]
    public WheelAnim wheelsAnim;
    public Animator carAnim;

	void Start () {
		
	}
	
	void Update ()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.racing)
            {
                Movement();
            }
        }
        else
        {
            Movement();
        }

    }
    public void Startup()
    {
        wheelsAnim = GetComponentInChildren<WheelAnim>();
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
        print(currentSpeed + " currentspeed");
        wheelsAnim.Motor(currentSpeed);
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
            print(hor + " hor");
            wheelsAnim.Turn(hor);
        }
        transform.Rotate(0, hor, 0);
    }

    private bool left;
    private bool right;

    private void Drift()
    {
        if(currentSpeed != 0)
        {
            if (Input.GetButton("C" + playerNum.ToString() + " BR"))
            {
                if(Input.GetAxis("C" + playerNum.ToString() + " Hor") > 0 && !right)
                {
                    h = Mathf.Lerp(-1, 1, h + 0.1f);
                    hor = -h * myCar.handling * myCar.driftMultiplier;
                    drifting = true;
                    left = true;
                }
                else if(Input.GetAxis("C" + playerNum.ToString() + " Hor") < 0 && !left)
                {
                    h = Mathf.Lerp(-1, 1, h - 0.1f);
                    hor = h * myCar.handling * myCar.driftMultiplier;
                    drifting = true;
                    right = true;
                }
            }
            else
            {
                right = false;
                left = false;
                drifting = false;
            }
        }


        if (drifting)
        {
            print("Test3");
            currentMaxSpeed = currentMaxSpeed / myCar.driftMultiplier * 0.8f;
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
