using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool racing;
    public bool played;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame(int con)
    {
            UIManager.instance.OnMenuChanged(UIManager.MenuType.inGame);
            LevelManager.instance.PlacePlayers(con);
            CheckpointManager.instance.Circuit();
            racing = true;
            played = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
