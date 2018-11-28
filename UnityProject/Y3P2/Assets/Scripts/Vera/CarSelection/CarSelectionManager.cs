using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    public static CarSelectionManager instance;
    [Header("Objects")]
    public List<GameObject> carObjects = new List<GameObject>();
    public List<GameObject> wheelParts = new List<GameObject>();

    public List<Player> players = new List<Player>();
    public List<CarSelection> selections = new List<CarSelection>();
    public List<Transform> locations = new List<Transform>();

   [SerializeField]private GameObject playerStandart;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer(int controler)
    {
        GameObject newPL = Instantiate(playerStandart, locations[players.Count]);
        Player nP = newPL.GetComponent<Player>();
        int myLayer = 10 + controler;
        newPL.layer = myLayer;
        foreach(Transform child in newPL.transform)
        {
            child.gameObject.layer = myLayer;
        }
        selections[players.Count].CarFirstSetup(nP,myLayer,!GameManager.instance.played);


        
        players.Add(nP);
    }

    public void AllFinished()
    {
        bool notFini = false;
        for (int i = 0; i < selections.Count; i++)
        {
            if(i < players.Count)
            {
                if (!selections[i].finished)
                {
                    notFini = true;
                }
            }
        }
        UIManager.instance.Ready(!notFini);
        if (!notFini)
        {
            //StartGame();
        }
    }


    private void StartGame()
    {

        //GameManager.instance.StartGame();
    }


}
