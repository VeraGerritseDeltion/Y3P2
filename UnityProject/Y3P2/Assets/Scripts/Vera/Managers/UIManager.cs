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
    [Header("MainMenu")]
    public List<Button> allButtons = new List<Button>();
    [SerializeField] private int currentButton;
    private bool delay;

    [Header("CarsSelect")]
    public List<TMP_Text> allText = new List<TMP_Text>();

    [Header("StartText")]
    public TMP_Text startText;
    public string unreadyText;
    public string readyText;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject carSelect;
    public GameObject inGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnMenuChanged(MenuType.mainMenu);
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].animator.SetTrigger("Normal");
        }
    }

    private void Update()
    {
        if (menuType == MenuType.mainMenu)
        {
            MainMenu();
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
            GameManager.instance.StartGame();
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

    private void MainMenu()
    {
        //allButtons[currentButton].animator.SetTrigger("Highlighted");
        if (Input.GetAxis("C1 Vert") > 0 && !delay)
        {
            print(currentButton);
            allButtons[currentButton].animator.SetBool("Highlight", false);
            currentButton++;
            if (currentButton == allButtons.Count)
            {
                currentButton = 0;
            }
            delay = true;
            StartCoroutine(StartDelay());
        }
        else if (Input.GetAxis("C1 Vert") < 0 && !delay)
        {
            allButtons[currentButton].animator.SetBool("Highlight", false);
            currentButton--;
            if (currentButton < 0)
            {
                currentButton = allButtons.Count - 1;
            }

            delay = true;
            StartCoroutine(StartDelay());
        }
        allButtons[currentButton].animator.SetBool("Highlight", true);
        if (Input.GetButtonDown("C1 A"))
        {
            allButtons[currentButton].onClick.Invoke();
        }
    }
    IEnumerator StartDelay()
    {

        yield return new WaitForSeconds(0.3f);
        delay = false;
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
                inGame.SetActive(false);
                carSelect.SetActive(true);
                break;
            case MenuType.inGame:
                mainMenu.SetActive(false);
                inGame.SetActive(true);
                carSelect.SetActive(false);
                break;
            case MenuType.mainMenu:
                mainMenu.SetActive(true);
                inGame.SetActive(false);
                carSelect.SetActive(false);
                break;
        }
    }
}
