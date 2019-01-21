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
    }
}
