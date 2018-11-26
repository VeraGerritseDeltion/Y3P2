using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Level currentLevel;
    public GameObject player;
    public GameObject AI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlacePlayers(List<int> aP)
    {
        for (int i = 0; i < currentLevel.startLocations.Count; i++)
        {
            if (aP.Count > i)
            {
                GameObject nP = Instantiate(player, currentLevel.startLocations[i].position, Quaternion.identity);
                Player p = nP.GetComponent<Player>();
                p.playerNum = aP[i];
                PlayerManager.instance.AddPlayer(p);
            }
            else
            {
                Instantiate(AI, currentLevel.startLocations[i].position, Quaternion.identity);
            }
        }
        if (aP.Count == 1)
        {
            OnePlayer();
        }
        if (aP.Count == 2)
        {
            TwoPlayers();
        }
        if (aP.Count == 3)
        {
            ThreePlayers();
        }
        if (aP.Count == 4)
        {
            FourPlayers();
        }
    }

    void OnePlayer()
    {
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 1, 1);
    }

    void TwoPlayers()
    {
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 0.5f, 1);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0, 0.5f, 1);
    }

    void ThreePlayers()
    {
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);
    }

    void FourPlayers()
    {
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[3].myCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
    }
}
