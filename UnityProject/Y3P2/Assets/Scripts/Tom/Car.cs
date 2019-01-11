using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Car : MonoBehaviour
    {
        public float waypointRange = 3f;

        [Header("Acceleration")]
        public float maxSpeed = 130f; // In km/h
        public float motorTorque = 500f; // In Newtonmeter

        [Header("Braking")]
        public float brakeTorque = 900f; // In Newtonmeter
        public float wheelFriction = 0.4f;

        [Header("Steering")]
        public float maxSteerAngle = 45f;

        [Header("Wheels (order: FL, FR, BL, BR)")]
        public WheelCollider[] wheelColliders = new WheelCollider[4];
        public Transform[] wheelModels = new Transform[4];

        private bool isBraking;
        private Vector3 relativeVector;
        private float angleNormalized;
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
            Braking();
        }

        private float Speed()
        {
            return (2 * Mathf.PI * wheelColliders[0].radius * wheelColliders[0].rpm * 60f / 1000f);
        }

        private void PowerWheels()
        {
            if (!isBraking && Speed() < maxSpeed)
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

        private float GetSteerAngle(Vector3 targetedPosition)
        {
            relativeVector = transform.InverseTransformPoint(targetedPosition);
            angleNormalized = (relativeVector.x / relativeVector.magnitude);
            return angleNormalized * maxSteerAngle;
        }

        private void Braking()
        {
            Vector3 pointO = path[currentWaypoint];
            Vector3 pointT = path[(currentWaypoint < path.Length - 1) ? currentWaypoint + 1 : 0];

            float angleO = GetSteerAngle(pointO);
            float angleT = GetSteerAngle(pointT);
            float result = angleT - angleO;
            if (result > 30f || result < -30f || angleO > 30f || angleO < -30f)
            {
                if (Vector3.Distance(pointO, transform.position) <= Speed() * Speed() / (250 * wheelFriction))
                {
                    isBraking = true;
                }
                else
                {
                    isBraking = false;
                }
            }
            else
            {
                isBraking = false;
            }
        }

        private void SteerWheelsTowards(Vector3 targetedPosition)
        {
            newSteerAngle = GetSteerAngle(targetedPosition);

            wheelColliders[0].steerAngle = newSteerAngle;
            wheelColliders[1].steerAngle = newSteerAngle;
        }

        private void CheckCurrentWaypoint()
        {
            heading = path[currentWaypoint] - transform.position;
            if (heading.sqrMagnitude < waypointRange * waypointRange)
            {
                if (currentWaypoint < path.Length - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            print(name + " collided with " + collision.transform.name);
        }

        private void OnDrawGizmos()
        {
            if (path != null && path.Length > 0)
            {
                if (currentWaypoint < path.Length)
                {
                    Vector3 pos = path[currentWaypoint];
                    pos.y = 1;
                    Gizmos.DrawLine(transform.position, pos);
                }
            }
        }
    }
}
