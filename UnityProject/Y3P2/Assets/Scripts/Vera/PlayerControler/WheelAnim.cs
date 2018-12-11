using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnim : MonoBehaviour
{
    public List<Animator> frontWheels = new List<Animator>();
    public List<GameObject> wheels = new List<GameObject>();
    public float maxSpd = 300;

    public void Turn (float turn)
    {
        for (int i = 0; i < frontWheels.Count; i++)
        {
            //frontWheels[i].SetFloat("VelocityX", turn);
        }
    }

    public void Motor(float Spd)
    {
        //float rotate = Mathf.Clamp(Spd / maxSpd, -1, 1);
        //transform.Rotate(rotate * 100, 0, 0);
    }
}
