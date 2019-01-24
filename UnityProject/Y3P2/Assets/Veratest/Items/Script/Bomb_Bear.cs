using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Bear : Item
{
    public float speed;
    public float angle;

    Rigidbody myR;
    public float timer;
    [SerializeField] private ParticleSystem part;
    [SerializeField] private float size;
    public override void UseItem()
    {
        myRacer.anim.SetTrigger("Over");
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.parent = null;
        myR = gameObject.GetComponent<Rigidbody>();
        myR.isKinematic = false;
        myR.AddForce(myRacer.gameObject.transform.forward * speed + myRacer.gameObject.transform.up * angle);
        StartCoroutine(Explode());
        myRacer.myItem = null;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        part.Play();
        Collider[] playersHit = Physics.OverlapSphere(gameObject.transform.position, size);
        for (int i = 0; i < playersHit.Length; i++)
        {
            if (playersHit[i].gameObject.tag == "Car")
            {
                playersHit[i].GetComponentInChildren<Racer>().Damage();
            }
        }
        yield return new WaitForSeconds(0.5f);
        Collider[] playersHits = Physics.OverlapSphere(gameObject.transform.position, size);
        for (int i = 0; i < playersHits.Length; i++)
        {
            if(playersHits[i].gameObject.tag == "Car")
            {
                playersHits[i].GetComponent<Racer>().Damage();
            }
        }
        Destroy(gameObject);
    }
}
