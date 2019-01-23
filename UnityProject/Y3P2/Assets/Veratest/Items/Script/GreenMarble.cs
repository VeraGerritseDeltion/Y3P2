using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMarble : Item
{
    public float speed;
    Rigidbody myR;
    int wallsHit;
    public int maxWalls;

    public override void UseItem()
    {
        myRacer.anim.SetTrigger("Over");
        myRacer.anim.speed = 2;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.parent = null;
        myR = gameObject.GetComponent<Rigidbody>();
        myR.isKinematic = false;
        myR.AddForce(myRacer.gameObject.transform.forward * speed);
        myRacer.anim.speed = 1;
        myRacer.myItem = null;
    }

        private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            collision.gameObject.GetComponent<Racer>().Damage();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Walls")
        {
            if(wallsHit == maxWalls)
            {
                Destroy(gameObject);
            }
            if (myR == null)
            {
                myR = gameObject.GetComponent<Rigidbody>();
            }
            speed = speed * 0.5f;
            Vector3 dir = collision.contacts[0].normal;
            //dir = -dir.normalized;
            myR.AddForce(dir * speed);
            wallsHit++;
        }
    }
}
