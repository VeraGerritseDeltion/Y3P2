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

    public void PlacePlayers(List<GameObject> aP)
    {
        for (int i = 0; i < currentLevel.startLocations.Count; i++)
        {
            if (aP.Count > i)
            {
                GameObject nP = Instantiate(aP[i], currentLevel.startLocations[i]);
                Player p = nP.GetComponent<Player>();
                p.myCam.enabled = true;
                PlayerManager.instance.AddPlayer(p);
                
            }
            else
            {
                Instantiate(AI, currentLevel.startLocations[i]);
            }
        }
        print(aP.Count + " aaaaaaaaaaaaaa");
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
        CarSelectionManager.instance.DestroyGarageCars();
    }

    void OnePlayer()
    {
        print("1 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 1, 1);
    }

    void TwoPlayers()
    {
        print("2 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 0.5f, 1);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0, 0.5f, 1);
    }

    void ThreePlayers()
    {
        print("3 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);
    }

    void FourPlayers()
    {
        print("4 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[3].myCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
    }
}
