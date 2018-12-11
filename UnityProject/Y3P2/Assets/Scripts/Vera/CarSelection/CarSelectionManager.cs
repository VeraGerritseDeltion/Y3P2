using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    public static CarSelectionManager instance;
    [Header("Objects")]
    public List<GameObject> carObjects = new List<GameObject>();
    public List<GameObject> wheelParts = new List<GameObject>();

    [Header("Selection")]
    public List<Player> players = new List<Player>();
    public List<CarSelection> selections = new List<CarSelection>();
    public List<Transform> locations = new List<Transform>();
    public List<GameObject> allPanels = new List<GameObject>();
    [Header("AllCars")]

    public List<GameObject> allCars = new List<GameObject>();

   [SerializeField]private GameObject playerStandart;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < allPanels.Count; i++)
        {
            allPanels[i].SetActive(false);
        }
    }

    public void AddPlayer(int controler)
    {
        print(playerStandart);
        print(locations[players.Count]);
        GameObject newPL = Instantiate(playerStandart, locations[players.Count]);
        Player nP = newPL.GetComponent<Player>();
        if(nP == null)
        {
            nP = newPL.GetComponentInChildren<Player>();
        }
        nP.myCam.enabled = false;
        int myLayer = 10 + players.Count + 1;

        newPL.layer = myLayer;
        foreach(Transform child in newPL.transform)
        {
            child.gameObject.layer = myLayer;
        }
        allPanels[players.Count].SetActive(true);
        selections[players.Count].CarFirstSetup(nP,myLayer,!GameManager.instance.played, controler);
        players.Add(nP);
        allCars.Add(newPL);
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
        if (!notFini)
        {
            for (int i = 0; i < players.Count; i++)
            {
                selections[i].UpdateStats();
                if (selections[i].myStats != null)
                {
                    selections[i].UpdateStats();
                    players[i].myCar = selections[i].myStats;
                }               
                players[i].myCam.enabled = false;
            }
            for (int i = 0; i < allCars.Count; i++)
            {
                allCars[i].layer = 0;
                foreach (Transform child in allCars[i].transform)
                {
                    child.gameObject.layer = 0;
                }
            }
        }
        UIManager.instance.Ready(!notFini);
    }
    
    public void DestroyGarageCars()
    {
        for (int i = 0; i < allCars.Count; i++)
        {
            Destroy(allCars[i]);
        }
        allCars.Clear();
        players.Clear();
        for (int i = 0; i < selections.Count; i++)
        {
            selections[i].myControler = 0;
            selections[i].myLayer = 0;
            selections[i].myPlayer = null;
        }
    }

    private void StartGame()
    {

        //GameManager.instance.StartGame();
    }


}
