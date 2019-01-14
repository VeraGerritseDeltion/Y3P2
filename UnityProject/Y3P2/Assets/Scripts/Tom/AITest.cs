using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI
{
    public class AITest : MonoBehaviour
    {
        public float waypointRange = 3f;

        [Header("Acceleration"), SerializeField]
        private float maxSpeed = 130f; // In km/h
        [SerializeField]
        public float motorTorque = 500f; // In Newtonmeter

        [Header("Braking"), SerializeField]
        private float brakeTorque = 900f; // In Newtonmeter
        [SerializeField]
        private float brakingDistance = 0.3f, wheelFriction = 0.4f;

        [Header("Steering"), SerializeField]
        private LayerMask mask;
        [SerializeField]
        private float maxSteerAngle = 45f;
        [SerializeField]
        private Transform raycasterLeft, raycasterHalfLeft, raycasterMidLeft, raycasterMid, raycasterMidRight, raycasterHalfRight, raycasterRight;
        [SerializeField]
        private float raycastLenght = 20f, impactForce = 50f;

        [Header("Wheels (order: FL, FR, BL, BR)"), SerializeField]
        private WheelCollider[] wheelColliders = new WheelCollider[4];
        [SerializeField]
        private Transform[] wheelModels = new Transform[4];

        private float speed;
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
        private float stoppingDistance;
        private Vector3? extraWaypoint = null;
        private bool left, halfLeft, midLeft, mid, midRight, halfRight, right, objectToTheRight;
        private float distanceToWaypoint = 1f;
        private Rigidbody rb;

        private void Start()
        {
            path = FindObjectOfType<Path>().GetPath();
            currentWaypoint = 0;
            rb = GetComponent<Rigidbody>();
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

            Raycasting();
        }
        
        private bool Raycast(Vector3 position, Vector3 direction)
        {
            if (Physics.Raycast(position, direction, raycastLenght, mask))
            {
                Debug.DrawRay(position, direction * raycastLenght, Color.red);
                return true;
            }
            else
            {
                Debug.DrawRay(position, direction * raycastLenght, Color.white);
                return false;
            }
        }

        private void Raycasting()
        {
            left = Raycast(raycasterLeft.position, raycasterLeft.forward);
            halfLeft = Raycast(raycasterHalfLeft.position, raycasterHalfLeft.forward);
            midLeft = Raycast(raycasterMidLeft.position, raycasterMidLeft.forward);
            mid = Raycast(raycasterMid.position, raycasterMid.forward);
            midRight = Raycast(raycasterMidRight.position, raycasterMidRight.forward);
            halfRight = Raycast(raycasterHalfRight.position, raycasterHalfRight.forward);
            right = Raycast(raycasterRight.position, raycasterRight.forward);

            /*
            if (mid && !left && !midLeft && !midRight && !right)
            {
                extraWaypoint = raycasterMidRight.position + (raycasterMidRight.forward * raycastLenght);
                print(1);
            }
            */

            if ((halfLeft || halfRight))
            {
                extraWaypoint = (halfRight) ? extraWaypoint = raycasterMidLeft.position + (raycasterMidLeft.forward * raycastLenght) : extraWaypoint = raycasterMidRight.position + (raycasterMidRight.forward * raycastLenght);
            }
            else if ((midLeft || midRight))
            {
                extraWaypoint = (midRight) ? extraWaypoint = raycasterLeft.position + (raycasterLeft.forward * raycastLenght) : extraWaypoint = raycasterRight.position + (raycasterRight.forward * raycastLenght);
            }
            else if (mid)
            {
                extraWaypoint = raycasterRight.position + (raycasterRight.forward * raycastLenght);
            }
            else
            {
                extraWaypoint = null;
            }
        }

        private void FixedUpdate()
        {
            speed = 2 * Mathf.PI * wheelColliders[0].radius * wheelColliders[0].rpm * 60f / 1000f;
            stoppingDistance = speed * speed / (250 * wheelFriction);

            SteerWheelsTowards(extraWaypoint ?? path[currentWaypoint]);
            PowerWheels();
            CheckCurrentWaypoint();
            Braking();
        }

        private void PowerWheels()
        {
            if (!isBraking && speed < (maxSpeed * distanceToWaypoint))
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
                distanceToWaypoint =  Vector3.Distance(pointO, transform.position) / Vector3.Distance(pointT, pointO);

                //if ((pointO - transform.position).magnitude <= (speed * speed / (250 * wheelFriction)) / 2)
                if(distanceToWaypoint <= brakingDistance)
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

        private void OnCollisionEnter(Collision collision)
        {
            print(name + " collided with " + collision.transform.name);
            Vector3 heading = collision.transform.position - transform.position;

            if (collision.transform.tag == "Car")
            {
                collision.transform.GetComponent<Rigidbody>().AddForceAtPosition((heading / heading.magnitude) * motorTorque * impactForce, collision.contacts[0].point);
            }
            else if (collision.transform.tag == "Obstacle")
            {
                rb.AddForceAtPosition((heading / heading.magnitude) * motorTorque * impactForce, collision.contacts[0].point);
            }
        }
    }
}

