using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCalculationManager : MonoBehaviour
{
    public static MovCalculationManager instance;
    private float maxSpeed = 100;
    //private float minSpeed = 75;

    //private float maxAcceleration = 450;
    //private float minAcceleration = 350;

    //private float maxAngle = 7;
    //private float minAngle = 2;

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
        f = st.speed / (st.weight);
        return f;
    }

    public float Acceleration(CarStats st)
    {
        float f = 0;
        f =(st.acceleration + 500f) / st.weight;
        return f;
    }

    public float Handling (CarStats st)
    {
        float f = 0;
        f = st.handling / (st.weight);
        return f;
    }

    public float Break (CarStats st)
    {
        float f = 0;
        f =(st.acceleration + 500f) * (st.weight)   +100;
        return f;
    }

}
