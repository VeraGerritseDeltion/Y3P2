using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour
{
    [Header("Max speed, between 75 and 100")]
    public float speed = 70f;

    [Header("Weight, between 0.90 and 1.10")]
    public float weight = 1f;

    [Header("accelatation, between 0 and 100")]
    public float acceleration = 50f;

    [Header("handling, 35 between and 55")]
    public float handling = 45f;

    [Header("drifting?")]
    public float driftMultiplier = 1.5f;
}
