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
        if (UIManager.instance.menuType == UIManager.MenuType.carSelect)
        {
            addControllers();
        }
    }

    private void addControllers()
    {
        if (Input.GetButtonDown("C1 A"))
        {
            AC(1);
        }
        if (Input.GetButtonDown("C2 A"))
        {
            AC(2);
        }
        //if (Input.GetButtonDown("C3 A"))
        //{
        //    AC(3);
        //}
        //if (Input.GetButtonDown("C4 A"))
        //{
        //    AC(4);
        //}


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
            //UIManager.instance.AddPlayer(allControlers.Count - 1);
            //Car_Char_Sel_Manager.instance.AddPlayer(index);
            //CarSelectionManager.instance.AddPlayer(index);
        }
    }
}
