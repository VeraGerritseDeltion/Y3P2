using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item
{
    [SerializeField] private int speed;
    [SerializeField] private float lenght;

    public override void UseItem()
    {
        myRacer.GetComponentInParent<KartPhysics>().SpeedBoost(speed);
        StartCoroutine(Lenght());

    }

    IEnumerator Lenght()
    {
        yield return new WaitForSeconds(lenght);
        myRacer.GetComponentInParent<KartPhysics>().StopBoost(speed);
        Destroy(gameObject);
    }
}
