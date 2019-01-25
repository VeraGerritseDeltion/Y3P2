using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public List<CarPartsSpawn> allPlayers = new List<CarPartsSpawn>();
    public static PlayerManager instance;

    public Image rSG;
    public List<Sprite> rsg = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RSG()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allPlayers[i].GetComponent<KartPhysics>().stop = true;
        }
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        rSG.enabled = true;
        rSG.sprite = rsg[0];
        yield return new WaitForSeconds(1);
        rSG.sprite = rsg[1];
        yield return new WaitForSeconds(1);
        rSG.sprite = rsg[2];
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allPlayers[i].GetComponent<KartPhysics>().stop = false;
        }
        yield return new WaitForSeconds(1);
        rSG.enabled = false;
    }

    public void AddPlayer(CarPartsSpawn newPlayer)
    {
        allPlayers.Add(newPlayer);
    }
}
