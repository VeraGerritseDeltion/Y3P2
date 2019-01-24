using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : Item
{
    public Animator anim;
    public GameObject gluestain;

    public override void UseItem()
    {
        anim.SetTrigger("Drop");
    }

    public void Stain()
    {
        GameObject stain = Instantiate(gluestain, new Vector3( myRacer.itemLocation.position.x , myRacer.itemLocation.position.y - 0.8f, myRacer.itemLocation.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }
    
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
