using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    bool cooldown;
    public MeshRenderer box;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Car" && !cooldown)
        {
            Racer r = other.GetComponentInChildren<Racer>();
            if(r.myItem == null)
            {
                r.NewItem(Item_Manager.instance.GetItem());
                box.enabled = false;
                cooldown = true;
                StartCoroutine(Cooldown());
            }        
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(10);
        box.enabled = true;
        cooldown = false;
    }
}
