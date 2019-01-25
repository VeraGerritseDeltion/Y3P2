using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racer : MonoBehaviour
{
    public int playerNum;
    public int number;

    private float score;

    public int racePosition;
    public GameObject fini;

    public bool finished;

    public Transform itemLocation;
    public Transform carLocation;

    public GameObject myItem;

    public GameObject testItem;
    public Animator anim;

    public Camera UiCam;
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

    public void NewItem(GameObject item)
    {
        myItem = item;
        IGP_Manager.instance.ItemImage(playerNum, item.GetComponentInChildren<Item>().mySprite);
    }
    private void Update()
    {
        //fini.SetActive(finished);
        IGP_Manager.instance.UpdatePos(playerNum, racePosition);

        if(Input.GetButtonDown("C2 A"))
        {
            print("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
        }
        if (Input.GetButtonDown("C" + playerNum + " X") || Input.GetButtonDown("C" + playerNum + " Y"))
        {
            if(myItem!= null)
            {
                Item ni = myItem.GetComponent<Item>();
                if (ni == null)
                {
                    ni = myItem.GetComponentInChildren<Item>();
                }
                ni.Use(this);
            }
        }
        if (Input.GetButtonUp("C" + playerNum + " X") || Input.GetButtonUp("C" + playerNum + " Y"))
        {
            if(myItem != null)
            {
                Item ni = myItem.GetComponent<Item>();
                if (ni == null)
                {
                    ni = myItem.GetComponentInChildren<Item>();
                }
                ni.UseItem();
                IGP_Manager.instance.DisImg(playerNum);
                myItem = null;
            }
        }
    }

    public void Damage()
    {
        GetComponentInParent<KartPhysics>().Damaged();
    }
}
