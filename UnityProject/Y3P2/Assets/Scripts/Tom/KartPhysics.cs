using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class KartPhysics : MonoBehaviour
{
    public int playerNum = 1;

    [Header("Suspension"),SerializeField] private float raycastLenght = 0.6f;
    [SerializeField] private Transform[] corners = new Transform[4];
    [SerializeField] private float suspensionStrenght = 5f;

    [Header("Accelerating"), SerializeField] private float enginePower = 20;
    [SerializeField] private Transform centerOfMass;

    [Header("Turning"), SerializeField] private float rotateSpeed = 20f;

    [Header("Grip"),SerializeField] private float velocityCheck = 0.5f;
    [Range(0, 1), SerializeField] private float velocityDecrease = 0.95f;
    [SerializeField] private float grip = 0.3f;

    private PlayerInput playerInput;
    private RaycastHit rh;
    private Rigidbody rb;
    private int touchingGround;

    [MenuItem("Tools/Create Empty Objects %q")]
    public static void CreateCorners()
    {
        Transform obj = Selection.activeTransform;
        BoxCollider box = obj.GetComponent<BoxCollider>();

        Transform corners = CreateEmptyGameObject("Corners", Vector3.zero, obj);
        corners.localPosition = Vector3.zero;
        corners.localScale = Vector3.one;

        CreateEmptyGameObject("FL Corner", corners.TransformPoint(box.center + new Vector3(-box.size.x, -box.size.y, box.size.z) * 0.5f), corners);
        CreateEmptyGameObject("FR Corner", corners.TransformPoint(box.center + new Vector3(box.size.x, -box.size.y, box.size.z) * 0.5f), corners);
        CreateEmptyGameObject("BL Corner", corners.TransformPoint(box.center + new Vector3(-box.size.x, -box.size.y, -box.size.z) * 0.5f), corners);
        CreateEmptyGameObject("BR Corner", corners.TransformPoint(box.center + new Vector3(box.size.x, -box.size.y, -box.size.z) * 0.5f), corners);
    }

    [MenuItem("Tools/Create Empty Objects %q", true)]
    public static bool CanCreateCorners()
    {
        if (Selection.activeTransform != null && Selection.activeTransform.GetComponent<BoxCollider>() != null)
        {
            return true;
        }
        else
        {
            Debug.Log("Selected object doesn't have a BoxCollider!");
            return false;
        }
    }

    public static Transform CreateEmptyGameObject(string name, Vector3 position, Transform parent)
    {
        Transform obj = new GameObject(name).transform;
        obj.parent = parent;
        obj.position = position;
        return obj;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass -= Vector3.up; // Tweak?
    }

    private void FixedUpdate()
    {
        playerInput = GetPlayerInput();
        Suspension();

        if(touchingGround > 1)
        {
            AcceleratingBraking();
        }
        if (Vector3.Distance(rb.velocity, Vector3.zero) > velocityCheck)
        {
            rb.AddTorque(transform.up * rotateSpeed * playerInput.horizontal, ForceMode.Acceleration);
        }
        
        if(!playerInput.forward && !playerInput.backward)
        {
            rb.velocity = new Vector3(rb.velocity.x * velocityDecrease, rb.velocity.y, rb.velocity.z * velocityDecrease);
        }

        if (playerInput.horizontal == 0)
        {
            rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z) * velocityDecrease;
        }
        
    }

    private PlayerInput GetPlayerInput()
    {
        return new PlayerInput()
        {
            forward = Input.GetButton("C" + playerNum + " A"),
            backward = Input.GetButton("C" + playerNum + " B"),
            horizontal = Input.GetAxis("C" + playerNum + " Hor")
        };
    }
    
    private void Suspension()
    {
        touchingGround = 0;

        for (int i = 0; i < corners.Length; i++)
        {
            Vector3 cF = Vector3.Project(rb.GetPointVelocity(corners[i].position), transform.up); // Calculate the current suspension force.
            Vector3 nF = transform.up * GetCompressionRatio(corners[i].position, -corners[i].up) * suspensionStrenght; // Calculate the new suspension force.

            rb.AddForceAtPosition(nF - cF, corners[i].position, ForceMode.Acceleration);
            Debug.Log(Vector3.ProjectOnPlane(-rb.GetPointVelocity(corners[i].position), rh.normal));
            rb.AddForceAtPosition(Vector3.ProjectOnPlane(-rb.GetPointVelocity(corners[i].position), rh.normal) * grip, rh.point, ForceMode.Acceleration);
        }
    }

    private float GetCompressionRatio(Vector3 origin, Vector3 down)
    {
        if (Physics.Raycast(origin, down, out rh, raycastLenght))
        {
            Debug.DrawLine(origin, rh.point);
            touchingGround++;
            return 1 - (rh.distance / raycastLenght);
        }
        else return 0;
    }
    
    private void AcceleratingBraking()
    {
        if (playerInput.forward)
        {
            if(centerOfMass != null)
            {
                rb.AddForceAtPosition(Vector3.ProjectOnPlane(transform.forward, rh.normal) * enginePower, centerOfMass.position, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.ProjectOnPlane(transform.forward, rh.normal) * enginePower, ForceMode.Acceleration);
            }
            
        }
        else if (playerInput.backward)
        {
            if (centerOfMass != null)
            {
                rb.AddForceAtPosition(Vector3.ProjectOnPlane(-transform.forward, rh.normal) * enginePower, centerOfMass.position, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.ProjectOnPlane(-transform.forward, rh.normal) * enginePower, ForceMode.Acceleration);
            }
        }
    }
}

public struct PlayerInput
{
    public bool forward;
    public bool backward;
    public float horizontal;
}