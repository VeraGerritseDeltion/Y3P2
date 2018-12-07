using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class Car : MonoBehaviour
    {
        public WheelCollider frontWheelLeft;
        public WheelCollider frontWheelRight;

        public float maxSteerAngle = 45f;
        public float maxSpeed = 130f;
        public float motorTorque = 1000f;

        public Transform test;

        private float timer;

        private void Update()
        {
            if ((2 * Mathf.PI * frontWheelLeft.radius * frontWheelLeft.rpm * 60f / 1000f) >= maxSpeed)
            {
                print(timer);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            SteerWheelsTowards(test.position);
            PowerWheels();
        }

        private void SteerWheelsTowards(Vector3 position)
        {
            Vector3 relativeVector = transform.InverseTransformPoint(position);

            float newSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

            frontWheelLeft.steerAngle = newSteerAngle;
            frontWheelRight.steerAngle = newSteerAngle;
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
            print("Current speed: " + 2 * Mathf.PI * frontWheelLeft.radius * frontWheelLeft.rpm * 60f / 1000f);
        }
    }
}
