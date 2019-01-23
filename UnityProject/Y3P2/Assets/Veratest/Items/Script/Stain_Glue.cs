using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain_Glue : MonoBehaviour
{
    public Animator anim;
    public float timer;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        anim.SetTrigger("Lessen");
    }

}
