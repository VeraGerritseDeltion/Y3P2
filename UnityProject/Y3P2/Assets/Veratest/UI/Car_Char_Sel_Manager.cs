using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car_Char_Sel_Manager : MonoBehaviour
{
    public static Car_Char_Sel_Manager instance;
    public enum CustomazationIndex { noControler, gender, hat, beard, car, wheels, ready }
    public List<CustomazationIndex> current = new List<CustomazationIndex>();

    [Header("customazation")]
    [Header("Gender")]
    public List<Sprite> genderSpr = new List<Sprite>();
    public List<GameObject> genderObj = new List<GameObject>();

    [Header("Hat")]
    public List<Sprite> hatSpr = new List<Sprite>();
    public List<GameObject> hatObj = new List<GameObject>();

    [Header("Beard")]
    public List<Sprite> beardSpr = new List<Sprite>();
    public List<GameObject> beardObj = new List<GameObject>();

    [Header("Car")]
    public List<Sprite> carSpr = new List<Sprite>();
    public List<GameObject> carObj = new List<GameObject>();

    [Header("Wheels")]
    public List<Sprite> wheelSpr = new List<Sprite>();
    public List<GameObject> wheelObj = new List<GameObject>();

    [Header("header")]
    public List<TMP_Text> headers = new List<TMP_Text>();

    [Header("Panels")]
    public List<GameObject> CarSelection = new List<GameObject>();
    public List<GameObject> CharSelection = new List<GameObject>();
    public List<GameObject> readyStamp = new List<GameObject>();
    public List<TMP_Text> playerText = new List<TMP_Text>();

    [Header("Scripts")]
    public List<Car_Char_Selection> carChar = new List<Car_Char_Selection>();

    [Header("Ready")]
    public GameObject ready;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Ready())
        {
            ready.SetActive(true);
            if (Input.GetButtonDown("Start"))
            {

            }
        }
        else
        {
            ready.SetActive(false);
        }
        if (Input.GetButtonDown("C1 A"))
        {
            if (!Here(1))
            {
                AC(1);
            }
        }
        if (Input.GetButtonDown("C2 A"))
        {
            if (!Here(2))
            {
                AC(2);
            }
        }
        if (Input.GetButtonDown("C3 A"))
        {
            if (!Here(3))
            {
                AC(3);
            }
        }
        if (Input.GetButtonDown("C4 A"))
        {
            if (!Here(4))
            {
                AC(4);
            }
        }
        for (int i = 0; i < current.Count; i++)
        {
            switch (current[i])
            {
                case CustomazationIndex.noControler:
                    CarSelection[i].SetActive(false);
                    CharSelection[i].SetActive(false);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Press A to play!";
                    Highlight(0, i);
                    break;
                case CustomazationIndex.gender:
                    CarSelection[i].SetActive(false);
                    CharSelection[i].SetActive(true);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Select your character.";
                    Highlight(1, i);
                    break;
                case CustomazationIndex.hat:
                    CarSelection[i].SetActive(false);
                    CharSelection[i].SetActive(true);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Select your character.";
                    Highlight(2, i);
                    break;
                case CustomazationIndex.beard:
                    CarSelection[i].SetActive(false);
                    CharSelection[i].SetActive(true);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Select your character.";
                    Highlight(3, i);
                    break;
                case CustomazationIndex.car:
                    CarSelection[i].SetActive(true);
                    CharSelection[i].SetActive(false);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Select your car.";
                    Highlight(4, i);
                    break;
                case CustomazationIndex.wheels:
                    CarSelection[i].SetActive(true);
                    CharSelection[i].SetActive(false);
                    readyStamp[i].SetActive(false);
                    playerText[i].text = "Select your car.";
                    Highlight(5, i);
                    break;
                case CustomazationIndex.ready:
                    CarSelection[i].SetActive(false);
                    CharSelection[i].SetActive(false);
                    readyStamp[i].SetActive(true);
                    playerText[i].text = "Ready to play!";
                    Highlight(0, i);
                    break;
            }
        }
    }

    private void Highlight(int index, int panel)
    {
        for (int i = 0; i < carChar[panel].highlights.Count; i++)
        {
            carChar[panel].highlights[i].SetActive(false);
        }
        switch (index)
        {
            case 1:
                carChar[panel].highlights[0].SetActive(true);
                break;
            case 2:
                carChar[panel].highlights[1].SetActive(true);
                break;
            case 3:
                carChar[panel].highlights[2].SetActive(true);
                break;
            case 4:
                carChar[panel].highlights[3].SetActive(true);
                break;
            case 5:
                carChar[panel].highlights[4].SetActive(true);
                break;
        }
            
    }

    private List<int> allControlers = new List<int>();

    private bool Ready()
    {
        bool minPlayer = false;
        bool allReady = true;
        for (int i = 0; i < current.Count; i++)
        {
            if(current[i] == CustomazationIndex.ready)
            {
                minPlayer = true;
            }
            if(current[i] != CustomazationIndex.ready && current[i] != CustomazationIndex.noControler)
            {
                allReady = false;
            }
        }
        print(minPlayer + "           " + allReady);
        if(minPlayer && allReady)
        {
            return true;
        }
        return false;        
    }

    private bool Here(int index)
    {
        bool exist = false;
        for (int i = 0; i < allControlers.Count; i++)
        {
            if (allControlers[i] == index)
            {
                exist = true;
            }
        }
        return exist;
    }

    private void AC(int index)
    {
        bool exist = false;
        for (int i = 0; i < allControlers.Count; i++)
        {
            if (allControlers[i] == index)
            {
                exist = true;
            }
        }
        if (!exist)
        {
            allControlers.Add(index);
            AddPlayer(allControlers.Count - 1, index);
        }
    }

    public void NextCatergory(int panel)
    {
        if (current[panel] != CustomazationIndex.ready)
        {
            current[panel] += 1;
        }
    }

    public void LastCatergory(int panel)
    {
        if (current[panel] != CustomazationIndex.gender)
        {
            current[panel] -= 1;
        }
    }

    public void AddPlayer(int panel, int con)
    {
        //current[panel] = CustomazationIndex.gender;

        carChar[panel].panel = panel;
        carChar[panel].StartMe();
        print(con);
        carChar[panel].controller = con;
    }
}
