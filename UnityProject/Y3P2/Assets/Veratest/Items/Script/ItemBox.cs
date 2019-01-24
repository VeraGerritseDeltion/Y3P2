using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    bool cooldown;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car" && !cooldown)
        {
            Racer r = other.GetComponentInChildren<Racer>();
            r.NewItem(Item_Manager.instance.GetItem());
            GetComponent<MeshRenderer>().enabled = false;
            cooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(10);
        GetComponent<MeshRenderer>().enabled = true;
        cooldown = false;
    }
}
