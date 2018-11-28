using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Car : MonoBehaviour
    {
        private float CalculateAngle(Waypoint nextWaypoint)
        {
            return Quaternion.Angle(transform.rotation, nextWaypoint.rotation);
        }
    }
}
