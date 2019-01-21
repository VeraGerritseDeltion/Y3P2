using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTest : MonoBehaviour
{
    public Vector3[] points;

    private void OnDrawGizmos()
    {
        if(points.Length > 1)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i], 0.5f);
            }
        }
    }
}
