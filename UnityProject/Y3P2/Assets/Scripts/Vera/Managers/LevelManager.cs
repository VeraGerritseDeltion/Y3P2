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

    public void PlacePlayers(int controlers)
    {
        for (int i = 0; i < controlers; i++)
        {
            GameObject nP = Instantiate(player, currentLevel.startLocations[i].position, Quaternion.Euler( currentLevel.startLocations[i].rotation.x, currentLevel.startLocations[i].rotation.y+ 90, currentLevel.startLocations[i].rotation.x));
            KartPhysics r = nP.GetComponentInChildren<KartPhysics>();
            Racer t = nP.GetComponentInChildren<Racer>();
            t.playerNum = i + 1;
            r.playerNum = i +1;            
            CarPartsSpawn p = nP.GetComponentInChildren<CarPartsSpawn>();   
            PlayerManager.instance.AddPlayer(p);
        }
        if (controlers == 1)
        {
            OnePlayer();
        }
        if (controlers == 2)
        {
            TwoPlayers();
        }
        if (controlers == 3)
        {
            ThreePlayers();
        }
        if (controlers == 4)
        {
            FourPlayers();
        }
    }

    void OnePlayer()
    {
        print("1 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 1, 1);
        PlayerManager.instance.allPlayers[0].camUI.rect = new Rect(0, 0, 1, 1);
    }

    void TwoPlayers()
    {
        print("2 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0, 0.5f, 1);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0, 0.5f, 1);

        PlayerManager.instance.allPlayers[0].camUI.rect = new Rect(0, 0, 0.5f, 1);
        PlayerManager.instance.allPlayers[1].camUI.rect = new Rect(0.5f, 0, 0.5f, 1);
    }

    void ThreePlayers()
    {
        print("3 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);

        PlayerManager.instance.allPlayers[0].camUI.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].camUI.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].camUI.rect = new Rect(0, 0, 0.5f, 0.5f);
    }

    void FourPlayers()
    {
        print("4 players");
        PlayerManager.instance.allPlayers[0].myCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].myCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].myCam.rect = new Rect(0, 0, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[3].myCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);

        PlayerManager.instance.allPlayers[0].camUI.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[1].camUI.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[2].camUI.rect = new Rect(0, 0, 0.5f, 0.5f);
        PlayerManager.instance.allPlayers[3].camUI.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
    }
}
