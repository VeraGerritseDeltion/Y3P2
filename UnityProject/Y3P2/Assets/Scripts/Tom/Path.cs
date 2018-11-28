using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Path : MonoBehaviour
    {
        public Waypoint[] waypoints = new Waypoint[4]
        {
            new Waypoint(new Vector3(0,0,3)),
            new Waypoint(new Vector3(0,0,63)),
            new Waypoint(new Vector3(-60,0,63)),
            new Waypoint(new Vector3(-60,0,3)),
        };

        [Header("Trigger size")]
        public Vector3 colliderSize = new Vector3(5f, 5f, 5f);

        private void Start()
        {
            Vector3 heading = Vector3.zero;
            float distance = 0f;

            for (int i = 0; i < waypoints.Length; i++)
            {
                heading = waypoints[i].position - ((i < waypoints.Length - 1) ? waypoints[i + 1].position : waypoints[0].position);
                distance = heading.magnitude;
                waypoints[i].rotation = Quaternion.LookRotation(-(heading / distance));
                BoxCollider trigger = gameObject.AddComponent<BoxCollider>();
                trigger.isTrigger = true;
                trigger.size = colliderSize;
                trigger.center = waypoints[i].position + (Vector3.up * colliderSize.y / 2);
            }
        }

        public void MoveWaypoint(int index, Vector3 newPosition)
        {
            if (index < waypoints.Length)
            {
                waypoints[index].position = newPosition;
            }
        }
    }

    [System.Serializable]
    public class Waypoint
    {
        public Vector3 position;
        [HideInInspector]
        public Quaternion rotation;

        public Waypoint(Vector3 position = default(Vector3))
        {
            this.position = position;
        }
    }
}