using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public List<int> allControlers = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (!GameManager.instance.racing)
        {
            addControllers();
        }
    }

    private void addControllers()
    {
        if (Input.GetButtonDown("C1 A"))
        {
            print("1");
            bool exist = false;
            for (int i = 0; i < allControlers.Count; i++)
            {
                if (allControlers[i] == 1)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                allControlers.Add(1);
            }
        }
        if (Input.GetButtonDown("C2 A"))
        {
            print("2");
            bool exist = false;
            for (int i = 0; i < allControlers.Count; i++)
            {
                if (allControlers[i] == 2)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                allControlers.Add(2);
            }
        }
        if (Input.GetButtonDown("C3 A"))
        {
            print("3");
            bool exist = false;
            for (int i = 0; i < allControlers.Count; i++)
            {
                if (allControlers[i] == 3)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                allControlers.Add(3);
            }
        }
        if (Input.GetButtonDown("C4 A"))
        {
            print("4");
            bool exist = false;
            for (int i = 0; i < allControlers.Count; i++)
            {
                if (allControlers[i] == 4)
                {
                    exist = true;
                }
            }
            if (!exist)
            {
                allControlers.Add(4);
            }
        }

        if (Input.GetButtonDown("Start"))
        {
            GameManager.instance.StartGame();
        }
    }
}
