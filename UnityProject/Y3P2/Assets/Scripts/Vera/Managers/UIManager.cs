using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private bool ready;
    public enum MenuType {mainMenu, carSelect, inGame}
    public MenuType menuType;

    [Header("CarsSelect")]
    public List<TMP_Text> allText = new List<TMP_Text>();

    [Header("StartText")]
    public TMP_Text startText;
    public string unreadyText;
    public string readyText;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject carSelect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnMenuChanged(MenuType.mainMenu);
    }

    private void Update()
    {
        if (menuType == MenuType.mainMenu)
        {

        }

        else if (menuType == MenuType.carSelect)
        {
            CarSelect();
        }
    }
    private void CarSelect()
    {
        if (Input.GetButtonDown("Back"))
        {
            OnMenuChanged(MenuType.mainMenu);
        }
        if (Input.GetButtonDown("Start") && ready)
        {
            //GameManager.instance.StartGame();
        }
    }

    public void AddPlayer(int index)
    {
        allText[index].text = "Player: " + (index + 1).ToString() + " is playing.";
    }

    public void Ready(bool allR)
    {
        if (allR)
        {
            ready = true;
            startText.text = readyText;
        }
        else
        {
            ready = false;
            startText.text = unreadyText;
        }
    }



    public void Exit()
    {
        GameManager.instance.ExitGame();
    }

    public void ToCarSelect()
    {
        OnMenuChanged(MenuType.carSelect);
    }

    public void OnMenuChanged(MenuType type)
    {
        menuType = type;
        switch (menuType)
        {
            case MenuType.carSelect:
                mainMenu.SetActive (false);
                carSelect.SetActive(true);
                break;
            case MenuType.inGame:
                mainMenu.SetActive(false);
                carSelect.SetActive(false);
                break;
            case MenuType.mainMenu:
                mainMenu.SetActive(true);
                carSelect.SetActive(false);
                break;
        }
    }
}
