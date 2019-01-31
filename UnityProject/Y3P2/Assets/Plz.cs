using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plz : MonoBehaviour
{
    public KeyCode kc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("C2 A"))
        {
            print("als dit niet werkt weet ik het niet meer");
        }

        if (Input.GetKeyDown(kc))
        {
            print("ik haat t als dit wel werkt");
        }
    }
}
