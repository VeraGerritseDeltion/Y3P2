using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed;
    public float handling;
    float vert = 1;
    float hor = 1;

    public bool player;
    void Update()
    {
        if (player)
        {
            vert = Input.GetAxis("Vertical") * speed;
            hor = Input.GetAxis("Horizontal") * handling;

            transform.Translate(0, 0, vert);
            transform.Rotate(0, hor, 0);
        }
        else
        {
            vert = Input.GetAxis("Vertical1") * speed;
            hor = Input.GetAxis("Horizontal1") * handling;

            transform.Translate(-vert, 0, 0);
            transform.Rotate(0, hor, 0);
        }
    }
}
