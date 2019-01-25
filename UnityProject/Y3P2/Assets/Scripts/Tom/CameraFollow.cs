using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    public int playerNum;

    [SerializeField] private Vector2 dal = new Vector2(1f, 1f);
    [SerializeField] private Vector2 dbc = new Vector2(5,-3);
    [SerializeField] private Vector2 dac = new Vector2(2, 1);
    [SerializeField] private float smoothingDistance = 15f;
    [SerializeField] private Vector2 smoothingValues = new Vector2(0.1f, 0.5f);

    private Vector3 v;
    private float ds;
    private Vector3 dp;
    
    private bool drifting;
    private bool lookBehind;

    private void FixedUpdate()
    {
        if(car == null)
        {
            Debug.LogError(name + " has no car assigned!");
            return;
        }

        drifting = Input.GetButton("C" + playerNum + " LB");
        lookBehind = Input.GetButton("C" + playerNum + " Y");

        

        if (!lookBehind)
        {
            transform.position = GetPosBehind(car, dbc.x, dac.x);
            transform.LookAt(car.TransformPoint(Vector3.up * dal.x));
        }
        else
        {
            transform.position = GetPosBehind(car, dbc.y, dac.y);
            transform.LookAt(car.TransformPoint(Vector3.up * dal.y));
        }
    }

    private Vector3 GetPosBehind(Transform target, float distanceBehind, float distanceAbove)
    {
        return target.position - (target.forward * distanceBehind) + (Vector3.up * distanceAbove);
    }
}
