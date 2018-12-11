using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    /*
    public class Car : MonoBehaviour
    {
        public WheelCollider frontWheelLeft;
        public WheelCollider frontWheelRight;

        public float maxSteerAngle = 45f;
        public float maxSpeed = 130f;
        public float motorTorque = 1000f;

        public AnimationCurve curve;

        private Vector2 relativeVector;
        private float oldSteerAngle;
        private float newSteerAngle;
        private int waypointIndex = 1;
        private Vector3[] path;

        private void Start()
        {
            path = FindObjectOfType<Path>().GetPath();
            oldSteerAngle = GetSteerAngle(path[waypointIndex]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(waypointIndex < path.Length - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                print(waypointIndex);
            }
        }

        private void FixedUpdate()
        {
            SteerWheelsTowards(path[waypointIndex]);
            PowerWheels();
        }

        private float GetSteerAngle(Vector3 position)
        {
            relativeVector = transform.InverseTransformPoint(position);
            return (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        }

        private void SteerWheelsTowards(Vector3 position)
        {
            newSteerAngle = GetSteerAngle(position);

            frontWheelLeft.steerAngle = newSteerAngle;
            frontWheelRight.steerAngle = newSteerAngle;
            oldSteerAngle = newSteerAngle;
        }

        private void PowerWheels()
        {
            if ((2 * Mathf.PI * frontWheelLeft.radius * frontWheelLeft.rpm * 60f / 1000f) < maxSpeed)
            {
                frontWheelLeft.motorTorque = motorTorque;
                frontWheelRight.motorTorque = motorTorque;
            }
            else
            {
                frontWheelLeft.motorTorque = 0f;
                frontWheelRight.motorTorque = 0f;
            }
            //print("Current speed: " + 2 * Mathf.PI * frontWheelLeft.radius * frontWheelLeft.rpm * 60f / 1000f);
        }

        private void OnDrawGizmos()
        {
            if(path != null && UnityEditor.EditorApplication.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, path[waypointIndex]);
            }
        }
    }
    */
}
