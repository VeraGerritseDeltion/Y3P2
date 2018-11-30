using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

namespace AI
{
    public class Path : MonoBehaviour
    {
        public int tempLapCount = 3;

        public Vector3[] waypoints = new Vector3[4]
        {
            new Vector3(0,0,3),
            new Vector3(0,0,63),
            new Vector3(-60,0,63),
            new Vector3(-60,0,3),
        };
        public float waypointRadius = 3f;

        private Vector3[] generatedPath;

        private void Start()
        {
            generatedPath = new Vector3[waypoints.Length * tempLapCount];
        }

        public void MoveWaypoint(int index, Vector3 newPosition)
        {
            if (index < waypoints.Length)
            {
                waypoints[index] = newPosition;
            }
        }

        public Vector3[] GetPath()
        {
            for (int lap = 0; lap < tempLapCount; lap++)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    generatedPath[(lap * (waypoints.Length - 1)) + i] = waypoints[i];
                }
            }
            return generatedPath;
        }
    }
}