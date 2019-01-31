using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Red_Marble : Item
{
    public float speed;
    Rigidbody myR;
    public GameObject target;
    [SerializeField] private NavMeshAgent movement;
    bool fired;

    public override void UseItem()
    {
        myRacer.anim.SetTrigger("Over");
        myRacer.anim.speed = 2;
        movement.enabled = false;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.transform.parent = null;
        myR = gameObject.GetComponent<Rigidbody>();
        myR.isKinematic = false;
        myRacer.anim.speed = 1;
        myRacer.myItem = null;
        movement.enabled = true;

        if (CheckpointManager.instance.NextTarget(myRacer, null) == null)
        {
            myR.AddForce(myRacer.gameObject.transform.forward * speed);
        }
        else
        {
            fired = true;
        }
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<Collider>().enabled = true;

    }

    private void Update()
    {
        
        if (fired)
        {
            target = CheckpointManager.instance.NextTarget(myRacer, target);
            if (target != null)
            {
                movement.destination = target.transform.position;
            }
            else
            {
                myR.AddForce(myRacer.gameObject.transform.forward * speed);
                fired = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            collision.gameObject.GetComponentInChildren<Racer>().Damage();
            Destroy(gameObject);
        }
    }
}
