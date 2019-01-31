using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class KartPhysics : MonoBehaviour
{
    public int playerNum = 1;
    public bool stop;

    public Transform hitEffect;

    [Header("Suspension"),SerializeField] private float raycastLenght = 0.6f;
    [SerializeField] private Transform[] corners = new Transform[4];
    [SerializeField] private float suspensionStrenght = 10f;

    [Header("Accelerating"), SerializeField] private float enginePower = 20;
    [SerializeField] private Transform centerOfMass;

    [Header("Turning"), SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private Vector2 wheelHeight;
    [SerializeField] private float wheelSmooth = 0.15f;
    [SerializeField] private float turn = 40f;
    [SerializeField] private Transform character;
    [SerializeField] private float characterAngle = 7.5f;
    [SerializeField] private float characterRotSpeed = 0.05f;

    [Header("Grip"),SerializeField] private float velocityCheck = 0.5f;
    [Range(0, 1), SerializeField] private float velocityDecrease = 0.95f;
    [Range(0, 1), SerializeField] private float angularVelocityDecrease = 0.95f;
    [SerializeField] private float grip = 0.5f;
    [SerializeField] private float gripDrift = 0.3f;
    [SerializeField] private float slowValue = 0.7f;

    [Header("Damaged"),SerializeField] private float damagedDuration = 3f;
    [SerializeField] private int spins = 3;

    [Header("Stabilizing"),SerializeField] private float stabilizer = 0.3f;
    [SerializeField] private float stabilzingSpeed = 2f;

    private PlayerInput playerInput = new PlayerInput();
    private RaycastHit rh;
    private Rigidbody rb;
    private BoxCollider bc;
    private int touchingGround;

    // Orderd section!
    private Vector3 groundNormal;
    private float refVelocity;
    private Vector3 refCharVelocity;
    private bool damaged;
    private Vector3 rot;
    private bool slow;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        //rb.centerOfMass -= Vector3.up; // Tweak?
    }

    private void FixedUpdate()
    {
        GetPlayerInput();
        Suspension();

        float x = 0f, z = 0f;

        if (!damaged && !stop)
        {
            if (touchingGround > 1)
            {
                AcceleratingBraking();
            }

            if (Vector3.Distance(rb.velocity, Vector3.zero) > velocityCheck && ((playerInput.forward > 0 || playerInput.forward < 0) || touchingGround < 1))
            {
                rb.AddTorque(transform.up * (playerInput.drifting ? rotateSpeed * 2 : rotateSpeed) * playerInput.horizontal, ForceMode.Acceleration);
                //rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotateSpeed * playerInput.horizontal));
                
                if (playerInput.forward > 0)
                {
                    z = -playerInput.horizontal * characterAngle;
                    x = -characterAngle;
                }
                else
                {
                    z = playerInput.horizontal * characterAngle;
                    x = characterAngle;
                }

                if (playerInput.drifting)
                {
                    z *= 3;
                }
            }

            if (playerInput.forward == 0  && touchingGround > 0)
            {
                rb.velocity *= velocityDecrease;
            }

            if (playerInput.horizontal == 0 && touchingGround > 0)
            {
                //Debug.Log(new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z) * velocityDecrease);
                //rb.angularVelocity = Vector3.Slerp(rb.angularVelocity, new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z) * velocityDecrease, characterRotSpeed); //change!
            }
            else
            {
                float rotation = playerInput.horizontal * 40f * (playerInput.forward < 0 ? -1 : 1);
                
                wheels[0].localEulerAngles = new Vector3(0, Mathf.SmoothDampAngle(wheels[0].localEulerAngles.y, rotation, ref refVelocity, wheelSmooth));
                wheels[1].localEulerAngles = new Vector3(0, Mathf.SmoothDampAngle(wheels[1].localEulerAngles.y, rotation, ref refVelocity, wheelSmooth));
            }
            //Stabilizer();
        }
        character.localRotation = Quaternion.Slerp(character.localRotation, Quaternion.Euler(new Vector3(x, 0, z)), characterRotSpeed);

        rb.angularVelocity *= angularVelocityDecrease;

        if(touchingGround < 1)
        {
            rb.velocity -= new Vector3(0, 20, 0) * Time.deltaTime;
        }
    }

    private void GetPlayerInput()
    {
        playerInput.forward = Input.GetAxis("C" + playerNum + " Vert");
        //playerInput.backward = Input.GetAxis("C" + playerNum + " Vert");
        playerInput.horizontal = Input.GetAxis("C" + playerNum + " Hor");
        playerInput.drifting = Input.GetButton("C" + playerNum + " LB");
    }

    private void Stabilizer()
    {
        Vector3 predictedUp = Quaternion.AngleAxis(rb.angularVelocity.magnitude * Mathf.Rad2Deg * stabilizer / stabilzingSpeed, rb.angularVelocity) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        rb.AddTorque(torqueVector * stabilzingSpeed * stabilzingSpeed);
    }
    
    private void Suspension()
    {
        touchingGround = 0;
        groundNormal = Vector3.up;

        slow = false;

        for (int i = 0; i < corners.Length; i++)
        {
            float compressionRatio = GetCompressionRatio(corners[i].position, -corners[i].up);
            
            Vector3 cF = Vector3.Project(rb.GetPointVelocity(corners[i].position), transform.up); // Calculate the current suspension force.
            Vector3 nF = transform.up * compressionRatio * suspensionStrenght; // Calculate the new suspension force.

            rb.AddForceAtPosition(nF - cF, corners[i].position, ForceMode.Acceleration);

            wheels[i].localPosition = new Vector3(wheels[i].localPosition.x, Mathf.Lerp(wheelHeight.x, wheelHeight.y, compressionRatio), wheels[i].localPosition.z);

            //Debug.DrawLine(rh.point, rh.point + Vector3.ProjectOnPlane(-rb.GetPointVelocity(corners[i].position), rh.normal) * (playerInput.drifting ? gripDrift : grip), Color.red);
            //rb.AddForceAtPosition(Vector3.ProjectOnPlane(-rb.GetPointVelocity(corners[i].position), rh.normal) * (playerInput.drifting ? gripDrift : grip), rh.point, ForceMode.Acceleration);
        }
        if (touchingGround > 0)
        {
            Debug.DrawLine(centerOfMass.position + Vector3.up, centerOfMass.position + Vector3.up + Vector3.ProjectOnPlane(-rb.velocity, rh.normal) * grip, Color.blue);
            //rb.AddForceAtPosition(Vector3.ProjectOnPlane(-rb.velocity, rh.normal) * grip, centerOfMass.position, ForceMode.Acceleration);
            rb.AddForceAtPosition(-rb.velocity * grip, centerOfMass.position, ForceMode.Acceleration);
            groundNormal = new Vector3(groundNormal.x / touchingGround, groundNormal.y / touchingGround, groundNormal.z / touchingGround);
        }
        else
        {
            groundNormal = Vector3.up;
        }
    }

    private float GetCompressionRatio(Vector3 origin, Vector3 down)
    {
        if (Physics.Raycast(origin, down, out rh, raycastLenght))
        {
            Debug.DrawLine(origin, rh.point);
            touchingGround++;
            groundNormal += rh.normal;
            if (rh.transform.CompareTag("Glue"))
            {
                slow = true;
            }
            return 1 - (rh.distance / raycastLenght);
        }
        else return 0;
    }
    
    private void AcceleratingBraking()
    {
        if (playerInput.forward > 0)
        {
            //rb.AddForceAtPosition(Vector3.ProjectOnPlane(transform.forward, groundNormal) * (slow ? enginePower * slowValue : enginePower), centerOfMass.position, ForceMode.Acceleration);
            rb.AddForceAtPosition(transform.forward * (slow ? enginePower * slowValue : enginePower), centerOfMass.position, ForceMode.Acceleration);

        }
        else if (playerInput.forward < 0)
        {
            //rb.AddForceAtPosition(Vector3.ProjectOnPlane(-transform.forward, groundNormal) * (slow ? enginePower * slowValue : enginePower), centerOfMass.position, ForceMode.Acceleration);
            rb.AddForceAtPosition(-transform.forward * (slow ? enginePower * slowValue : enginePower), centerOfMass.position, ForceMode.Acceleration);
        }

        //Debug.DrawLine(centerOfMass.position + Vector3.up, centerOfMass.position + Vector3.up + Vector3.ProjectOnPlane(rb.velocity, rh.normal), Color.red);
    }

    public void Damaged()
    {
        StopCoroutine(Damage());
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        damaged = true;
        rb.isKinematic = true;
        bc.enabled = false;

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        float startRotation = transform.localEulerAngles.y;
        float endRotation = startRotation + (360f * spins);
        float time = 0;

        while(time < damagedDuration)
        {
            time += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, time / damagedDuration) % 360f;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.z, yRotation, transform.localEulerAngles.z);
            yield return null;
        }
        rb.isKinematic = false;
        bc.enabled = true;
        damaged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var heading = collision.transform.position - transform.position;

        if (collision.transform.CompareTag("Car"))
        {
            collision.transform.GetComponent<Rigidbody>().AddForceAtPosition(heading / heading.magnitude * enginePower * 5, collision.contacts[0].point);
        }
        else if(collision.transform.CompareTag("Bounds"))
        {
            stop = true;
            rb.isKinematic = true;
            bc.enabled = false;

            transform.position = CheckpointManager.instance.LastCheckpoint(GetComponentInChildren<Racer>()).transform.position;
            transform.rotation = CheckpointManager.instance.LastCheckpoint(GetComponentInChildren<Racer>()).transform.rotation;

            bc.enabled = true;
            rb.isKinematic = false;
            stop = false;
        }
    }
}

public class PlayerInput
{
    public float forward;
    public float backward;
    public float horizontal;
    public bool drifting;
}