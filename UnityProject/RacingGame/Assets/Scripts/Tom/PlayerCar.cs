using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : Car
{
    [Header("Number of player (1 to 4)"),SerializeField] private char playerNum = '1';

    public override bool? GetSpeedInput()
    {
        if (Input.GetButton("C" + playerNum + " A"))
        {
            shouldBrake = false;
            return true;
        }
        else if (Input.GetButton("C" + playerNum + " B"))
        {
            shouldBrake = false;
            return false;
        }
        else
        {
            shouldBrake = true;
            return null;
        }
    }

    public override float GetSteerAngle()
    {
        return Input.GetAxis("C" + playerNum + " Hor") * maxSteerAngle * steeringCurve.Evaluate(currentSpeed / maxSpeed);
    }
}