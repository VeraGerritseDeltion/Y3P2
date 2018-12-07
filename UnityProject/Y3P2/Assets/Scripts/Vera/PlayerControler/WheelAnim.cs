using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnim : MonoBehaviour
{
    public List<Animator> frontWheels = new List<Animator>();
    public List<GameObject> wheels = new List<GameObject>();

    public void Turn (float turn)
    {
        for (int i = 0; i < frontWheels.Count; i++)
        {
            frontWheels[i].SetFloat("VelocityX", turn);
        }
    }

    public void Motor(float Spd)
    {
        transform.Rotate(Spd, 0, 0);
    }
}
