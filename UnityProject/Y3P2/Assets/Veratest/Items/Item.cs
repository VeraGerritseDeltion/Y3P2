using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite mySprite;
    public GameObject myObject;
    public float chance;
    public Racer myRacer;
    public void Use(Racer myR)
    {
        myRacer = myR;
        GameObject item = Instantiate(myObject, myRacer.itemLocation.position, Quaternion.identity);
        item.transform.parent = myRacer.itemLocation;
        item.GetComponent<Rigidbody>().isKinematic = true;
        myRacer.myItem = item;
    }

    public virtual void UseItem()
    {

    }
}
