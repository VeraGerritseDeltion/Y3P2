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
        PlayerManager.instance.RSG();
            racing = true;
            played = true;
    }

    public void StartRace()
    {

    }

    private void Update()
    {
        /*
        if (Input.GetButton("C1 A"))
        {
            Debug.Log("Controller 1 is working!");
        }
        if (Input.GetButton("C2 A"))
        {
            Debug.Log("Controller 2 is working!");
        }
        if (Input.GetButton("C3 A"))
        {
            Debug.Log("Controller 3 is working!");
        }
        if (Input.GetButton("C4 A"))
        {
            Debug.Log("Controller 4 is working!");
        }
        */
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
