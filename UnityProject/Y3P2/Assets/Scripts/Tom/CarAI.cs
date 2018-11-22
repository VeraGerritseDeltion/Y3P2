using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Transform target;
    private CarStats stats;
    private Rigidbody rb;

    private void Start()
    {
        stats = GetComponent<CarStats>();
        rb = GetComponent<Rigidbody>();
    }
}
