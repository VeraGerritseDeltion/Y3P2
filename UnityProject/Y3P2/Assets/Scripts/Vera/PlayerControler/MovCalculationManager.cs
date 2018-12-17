using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCalculationManager : MonoBehaviour
{
    public static MovCalculationManager instance;
    private float maxSpeed = 100;
    private float minSpeed = 75;

    private float maxAcceleration = 450;
    private float minAcceleration = 350;

    private float maxAngle = 55;
    private float minAngle = 35;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public float MaxSpeed (CarStats st)
    {
        float f = 0;
        f = Mathf.Clamp(st.speed / (st.weight / 100f), minSpeed, maxSpeed);
        return f;
    }

    public float Acceleration(CarStats st)
    {
        float f = 0;
        f = Mathf.Clamp((st.acceleration + 500f) / st.weight, minAcceleration, maxAcceleration);
        return f;
    }

    public float Handling (CarStats st)
    {
        float f = 0;
        f = Mathf.Clamp(st.handling / (st.weight), minAngle, maxAngle);
        return f;
    }

    public float Break (CarStats st)
    {
        float f = 0;
        f = Mathf.Clamp((st.acceleration + 500f) * (st.weight), minAcceleration, maxAcceleration)+100;
        return f;
    }

}
