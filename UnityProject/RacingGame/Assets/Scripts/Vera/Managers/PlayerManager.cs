using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<Player> allPlayers = new List<Player>();
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer(Player newPlayer)
    {
        allPlayers.Add(newPlayer);
    }
}
