using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public Player myPlayer;
    private GameObject wheels;
    private GameObject car;

    private int currentWheelIndex;
    private int currentCarIndex;

    public int myLayer;
    public int myControler;
    public CarStats myStats;

    private int curP;
    public enum CurrentPart { car,wheels, finished}
    public CurrentPart currentPart;

    public bool finished;
    public List<Animator> myButtons = new List<Animator>();
    private int currentButton;

    public GameObject buttons;

    public void Update()
    {
        if(UIManager.instance.menuType == UIManager.MenuType.carSelect && myControler != 0)
        {
            if(Input.GetButtonDown("C" + myControler.ToString() + " BR"))
            {
                Part(false);
            }
            else if (Input.GetButtonDown("C" + myControler.ToString() + " BL"))
            {
                Part(true);
            }
            if(Input.GetButtonDown("C" + myControler.ToString() + " A") && currentPart == CurrentPart.finished)
            {
                Finished(true);
            }
            if(Input.GetButtonDown("C" + myControler.ToString() + " A") && currentPart != CurrentPart.finished)
            {
                curP++;
                currentButton = Mathf.Clamp(currentButton + 1 ,0,myButtons.Count-1);
                
                
                currentPart = (CurrentPart)curP;
                UpdateButtons();
            }
            if(Input.GetButtonDown("C" + myControler.ToString() + " B") && curP != 0)
            {
                curP--;
                currentButton = Mathf.Clamp(currentButton -1, -1, myButtons.Count - 1);
                
                currentPart = (CurrentPart)curP;
                Finished(false);
                UpdateButtons();
            }
        }
    }


    private void UpdateButtons()
    {
        print(currentButton);
        for (int i = 0; i < myButtons.Count; i++)
        {
            myButtons[i].SetBool("Highlight", false);
        }
        myButtons[currentButton].SetBool("Highlight", true);
    }

    public void CarFirstSetup(Player myP, int layer, bool firstSetup, int controler)
    {
        myPlayer = myP;
        myLayer = layer;
        print(controler.ToString() + "controler");
        myControler = controler;
        myPlayer.playerNum = controler;
        if (firstSetup)
        {
            currentWheelIndex = Random.Range(0, CarSelectionManager.instance.wheelParts.Count);
            currentCarIndex = Random.Range(0, CarSelectionManager.instance.carObjects.Count);
        }
        StartCoroutine(Delay());
        UpdateButtons();
        Wheel();
        Car();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        currentPart = CurrentPart.car;
        currentButton = 0;
        UpdateButtons();
        curP = 0;
    }

    private void Part(bool left)
    {
        switch (currentPart)
        {
            case CurrentPart.car:
                if (left)
                {
                    CarLeft();
                }
                else
                {
                    CarRight();
                }
                break;
            case CurrentPart.wheels:
                if (left)
                {
                    WheelLeft();
                }
                else
                {
                    WheelRight();
                }
                break;
        }
        
    }

    public void CarRight()
    {
        currentCarIndex++;
        if(currentCarIndex == CarSelectionManager.instance.wheelParts.Count)
        {
            currentCarIndex = 0;
        }
        Car();
    }

    public void CarLeft()
    {
        currentCarIndex--;
        if (currentCarIndex <= 0)
        {
            currentCarIndex = CarSelectionManager.instance.wheelParts.Count -1;
        }
        Car();
    }

    private void Car()
    {
        GameObject temp = car;
        print(CarSelectionManager.instance.carObjects.Count);
        print(myPlayer.carLoc);
        car = Instantiate(CarSelectionManager.instance.carObjects[currentCarIndex], myPlayer.carLoc);
        car.transform.parent = myPlayer.carLoc;
        car.layer = myLayer;
        foreach (Transform child in car.transform)
        {
            child.gameObject.layer = myLayer;
        }
        if (temp != null)
        {
            Destroy(temp);
        }
        UpdateStats();
    }

    public void WheelRight()
    {
        currentWheelIndex++;
        if(currentWheelIndex == CarSelectionManager.instance.wheelParts.Count)
        {
            currentWheelIndex = 0;
        }
        Wheel();
    }

    public void WheelLeft()
    {
        currentWheelIndex--;
        if (currentWheelIndex <= 0)
        {
            currentWheelIndex = CarSelectionManager.instance.wheelParts.Count - 1;
        }
        Wheel();
    }

    private void Wheel()
    {
        GameObject temp = wheels;
        wheels = Instantiate(CarSelectionManager.instance.wheelParts[currentWheelIndex], myPlayer.wheelLoc);
        wheels.transform.parent = myPlayer.wheelLoc;
        wheels.layer = myLayer;
        foreach (Transform child in wheels.transform)
        {
            child.gameObject.layer = myLayer;
        }
        if(temp != null)
        {
            Destroy(temp);
        }
        UpdateStats();
    }

    public void UpdateStats()
    {
        PartStats wS = new PartStats();
        PartStats cS = new PartStats();
        if (car != null)
        {
            cS = car.GetComponent<PartStats>();
        }
        if(wheels != null)
        {
            wS = wheels.GetComponent<PartStats>();
        }


        if(wS == null)
        {
            wS = new PartStats();
        }
        if(cS == null)
        {
            cS = new PartStats();
        }
        if(myStats == null)
        {
            myStats = new CarStats();
        }
        myStats.acceleration = cS.acceleration + wS.acceleration;
        myStats.speed = cS.speed + wS.speed;
        myStats.handling = cS.handling + wS.handling;
        myStats.driftMultiplier = cS.driftMultiplier + wS.driftMultiplier;
        myStats.weight = cS.weight + wS.weight;
    }

    public void Finished(bool fini)
    {
        finished = fini;
 
        if (finished)
        {
            buttons.SetActive(false);
        }
        else
        {
            buttons.SetActive(true);
        }
        CarSelectionManager.instance.AllFinished();
    }
}
