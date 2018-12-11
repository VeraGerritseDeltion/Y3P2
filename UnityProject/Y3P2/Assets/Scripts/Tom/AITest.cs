using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI
{
    public class AITest : MonoBehaviour
    {
        public float waypointRange = 3f;

        [Header("Acceleration")]
        public float maxSpeed = 130f; // In km/h
        public float motorTorque = 500f; // In Newtonmeter
        public float brakeTorque = 900f; // In Newtonmeter

        [Header("Steering")]
        public float maxSteerAngle = 45f;
        public float smoothing = 5f;

        [Header("Wheels (order: FL, FR, BL, BR)")]
        public WheelCollider[] wheelColliders = new WheelCollider[4];
        public Transform[] wheelModels = new Transform[4];
        
        public bool isBraking; // Change to private
        private Vector3 relativeVector;
        private float newSteerAngle;
        private Transform currentWheel;
        private Vector3 wheelPosition;
        private Quaternion wheelRotation;

        private Vector3[] path;
        private int currentWaypoint;
        private Vector3 heading;

        private void Start()
        {
            path = FindObjectOfType<Path>().GetPath();
            currentWaypoint = 0;
        }

        private void Update()
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);

                currentWheel = wheelModels[i].transform;
                currentWheel.position = wheelPosition;
                currentWheel.rotation = wheelRotation;
            }
        }

        private void FixedUpdate()
        {
            SteerWheelsTowards(path[currentWaypoint]);
            PowerWheels();
            CheckCurrentWaypoint();
        }

        private void PowerWheels()
        {
            if (!isBraking && (2 * Mathf.PI * wheelColliders[0].radius * wheelColliders[0].rpm * 60f / 1000f) < maxSpeed)
            {
                wheelColliders[0].motorTorque = motorTorque;
                wheelColliders[1].motorTorque = motorTorque;
                wheelColliders[2].brakeTorque = 0f;
                wheelColliders[3].brakeTorque = 0f;
            }
            else
            {
                wheelColliders[0].motorTorque = 0f;
                wheelColliders[1].motorTorque = 0f;
                if (isBraking)
                {
                    wheelColliders[2].brakeTorque = brakeTorque;
                    wheelColliders[3].brakeTorque = brakeTorque;
                }
            }
        }

        private void SteerWheelsTowards(Vector3 targetedPosition)
        {
            relativeVector = transform.InverseTransformPoint(targetedPosition);
            newSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

            wheelColliders[0].steerAngle = newSteerAngle;
            wheelColliders[1].steerAngle = newSteerAngle;
        }

        private void CheckCurrentWaypoint()
        {
            heading = path[currentWaypoint] - transform.position;
            if(heading.sqrMagnitude < waypointRange * waypointRange)
            {
                if(currentWaypoint < path.Length - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0;
                }
            }
        }
    }
}

