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
        gameObject.transform.parent = null;
        gameObject.transform.position = myRacer.useLocation.position;
        gameObject.transform.rotation = myRacer.gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x + angle, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        myR = gameObject.GetComponent<Rigidbody>();
        myR.isKinematic = false;
        myR.AddForce(myRacer.gameObject.transform.forward * speed);
        StartCoroutine(Explode());
        myRacer.myItem = null;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        part.Play();
        yield return new WaitForSeconds(0.5f);
        Collider[] playersHit = Physics.OverlapSphere(gameObject.transform.position, size);
        for (int i = 0; i < playersHit.Length; i++)
        {
            if(playersHit[i].gameObject.tag == "Car")
            {
                playersHit[i].GetComponent<Racer>().Damage();
            }
        }
    }
}
