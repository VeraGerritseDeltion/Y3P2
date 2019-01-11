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
    public CarAnim carAnim;    

	void Start () {
		
	}

    void Update()
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
        if(wheelsAnim != null)
        {
            print("Broom");
            wheelsAnim.Motor(currentSpeed);
        }

        transform.Translate(currentSpeed, 0, 0);
    }

    private float driftAmount;

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
        if (wheelsAnim != null)
        {
            wheelsAnim.Turn(hor);
        }
        transform.Rotate(0, hor, 0);
    }

    private bool left;
    private bool right;
    private bool driftja;

    private void Drift()
    {
        
        if(currentSpeed != 0)
        {
            if (Input.GetButton("C" + playerNum.ToString() + " BR"))
            {
                if(Input.GetAxis("C" + playerNum.ToString() + " Hor") > 0 && !right)
                {
                    if (!driftja)
                    {
                        carAnim.carAnimator.SetBool("Drifting", true);
                        driftja = true;
                    }
                    print(h);
                    h = Mathf.Lerp(-1, 1, h + 0.0001f);
                    driftAmount = h;
                    hor = -h * myCar.handling * myCar.driftMultiplier;
                    carAnim.carAnimator.SetFloat("HorAxis", -driftAmount);
                    drifting = true;
                    left = true;
                }
                else if(Input.GetAxis("C" + playerNum.ToString() + " Hor") < 0 && !left)
                {
                    if (!driftja)
                    {
                        carAnim.carAnimator.SetBool("Drifting", true);
                        driftja = true;
                    }
                    h = Mathf.Lerp(-1, 1, h - 0.000001f);
                    carAnim.carAnimator.SetFloat("HorAxis", driftAmount);
                    hor = h * myCar.handling * myCar.driftMultiplier;
                    drifting = true;
                    right = true;
                }
            }
            else
            {
                if(driftAmount > 0)
                {
                    driftAmount = Mathf.Lerp(0, 1, driftAmount - 0.001f);
                    carAnim.carAnimator.SetFloat("HorAxis", driftAmount);
                }
                else if(driftAmount < 0)
                {
                    driftAmount = Mathf.Lerp(-1, 0, driftAmount + 0.001f);
                    carAnim.carAnimator.SetFloat("HorAxis", driftAmount);
                }
                right = false;
                left = false;
                drifting = false;
                carAnim.carAnimator.SetBool("Drifting", false);
                driftja = false;
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
