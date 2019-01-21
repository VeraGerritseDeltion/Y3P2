using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Car : MonoBehaviour
{
    #region Fields
    [Header("Maximum speed (in km/h)"), SerializeField] protected float maxSpeed = 130f;
    protected float currentSpeed;

    [Header("Torques (in Newtonmeter)"), SerializeField] private float motorTorque = 1000f;
    [SerializeField] private float brakeMoterTorque = 1100f;
    protected bool shouldBrake;

    [Header("Steering"), SerializeField] protected AnimationCurve steeringCurve; 
    [SerializeField] private float steerSpeed = 10f;
    [SerializeField] protected float maxSteerAngle = 45f;
    private float newAngle;
    private float interpolation;

    [Header("Wheels (order: FL>FR>BL>BR)"), SerializeField] private Wheels[] wheelColliders = new Wheels[4];
    #endregion

    #region Main Methods

    private void FixedUpdate()
    {
        // Get the current speed in KM/H
        currentSpeed = 2 * Mathf.PI * wheelColliders[0].collider.radius * wheelColliders[0].collider.rpm * 60 / 1000;

        Torques(GetSpeedInput());
        Steering(GetSteerAngle());
    }

    private void Torques(bool? speedInput)
    {
        if (speedInput == true && !shouldBrake && currentSpeed < maxSpeed)
        {
            wheelColliders[0].collider.motorTorque = motorTorque;
            wheelColliders[1].collider.motorTorque = motorTorque;
        }
        else if (speedInput == false && !shouldBrake && currentSpeed > -maxSpeed)
        {
            wheelColliders[0].collider.motorTorque = -motorTorque;
            wheelColliders[1].collider.motorTorque = -motorTorque;
        }
        else
        {
            wheelColliders[0].collider.motorTorque = 0f;
            wheelColliders[1].collider.motorTorque = 0f;
        }
        
        wheelColliders[2].collider.brakeTorque = shouldBrake ? brakeMoterTorque : 0f;
        wheelColliders[3].collider.brakeTorque = shouldBrake ? brakeMoterTorque : 0f;
    }

    private void Steering(float steerAngle)
    {
        interpolation = steerSpeed * Time.fixedDeltaTime;

        newAngle = Mathf.SmoothStep(wheelColliders[0].collider.steerAngle, steerAngle, interpolation);

        wheelColliders[0].collider.steerAngle = newAngle;
        wheelColliders[1].collider.steerAngle = newAngle;
    }
    #endregion

    #region Abstract Methods
    public abstract bool? GetSpeedInput();
    public abstract float GetSteerAngle();
    #endregion
}

[Serializable]
public struct Wheels
{
    public WheelCollider collider;
    public Transform model;
}