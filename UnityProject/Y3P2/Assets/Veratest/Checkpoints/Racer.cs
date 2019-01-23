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
            testItem.GetComponent<Item>().Use(this);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            myItem.GetComponent<Item>().UseItem();
        }
    }

    public void Damage()
    {
        print("damaged");
    }
}
