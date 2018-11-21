using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public CarStats myCar;
    public float hor;
    public float vert;
    public int playerNum = 1;

    float currentSpeed;

	void Start () {
		
	}
	
	void Update () {
        hor = Input.GetAxis("C" + playerNum.ToString() + " Hor") * myCar.speed;
        transform.Rotate(0, 0, hor);
    }
}
