using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<CarPartsSpawn> allPlayers = new List<CarPartsSpawn>();
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer(CarPartsSpawn newPlayer)
    {
        allPlayers.Add(newPlayer);
    }
}
