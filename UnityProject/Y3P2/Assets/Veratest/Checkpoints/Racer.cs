using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racer : MonoBehaviour
{
    public int number;

    private float score;

    public int racePosition;

    public Text rP;

    public GameObject fini;

    public bool finished;

    public Transform itemLocation;
    public Transform useLocation;

    public GameObject myItem;

    public GameObject testItem;
    public Animator anim;
    private void Start()
    {
        number = CheckpointManager.instance.GetNumber(this);
    }

    public float GetScore()
    {
        return score;
    }

    public void SetScore(float ns)
    {
        score = ns;
    }

    private void Update()
    {
        fini.SetActive(finished);
        rP.text = racePosition.ToString();

        if (Input.GetKeyDown(KeyCode.I))
        {
            Item ni = testItem.GetComponent<Item>();
            if(ni == null)
            {
               ni = testItem.GetComponentInChildren<Item>();
            }
            ni.Use(this);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            Item ni = myItem.GetComponent<Item>();
            if(ni == null)
            {
                ni = myItem.GetComponentInChildren<Item>();
            }
            print(ni);
            ni.UseItem();
        }
    }

    public void Damage()
    {
        print("damaged");
    }
}
