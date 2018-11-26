using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool racing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        if(InputManager.instance.allControlers.Count != 0)
        {
            LevelManager.instance.PlacePlayers(InputManager.instance.allControlers);
            racing = true;
        }
    }
}
