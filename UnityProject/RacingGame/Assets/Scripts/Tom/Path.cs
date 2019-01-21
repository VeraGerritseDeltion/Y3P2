using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

namespace AI
{
    public class Path : MonoBehaviour
    {
        public int tempLapCount = 3; // Has to pick from a manager!

        public Vector3[] waypoints = new Vector3[4]
        {
            new Vector3(0,0,3),
            new Vector3(0,0,63),
            new Vector3(-60,0,63),
            new Vector3(-60,0,3),
        };
        public float waypointRadius = 3f;

        public void MoveWaypoint(int index, Vector3 newPosition)
        {
            if (index < waypoints.Length)
            {
                waypoints[index] = newPosition;
            }
        }

        public Vector3[] GetPath()
        {
            Vector3[] generatedPath = new Vector3[waypoints.Length * tempLapCount];

            for (int lap = 0; lap < tempLapCount; lap++)
            {
                for (int waypoint = 0; waypoint < waypoints.Length; waypoint++)
                {
                    generatedPath[(lap * waypoints.Length) + waypoint] = waypoints[waypoint] + transform.position + (Random.insideUnitSphere * waypointRadius);
                }
            }
            return generatedPath;
        }
    }
}