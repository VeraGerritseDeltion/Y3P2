using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    private Button currentButton;
    private int currentB;
    private bool cD;
    private void Start()
    {
        currentButton = buttons[currentB];
        currentButton.Select();
    }

    private void Update()
    {
        if(Input.GetAxis("C1 Hor") > 0 || Input.GetAxis("C1 Vert") < 0)
        {
            print("reeeeeeeeeeeeee");
            if (!cD)
            {
                currentB++;
                if (currentB == buttons.Count)
                {
                    currentB = 0;
                }
                currentButton = buttons[currentB];
                currentButton.Select();
                cD = true;
                StartCoroutine(CD());
            }
        }
        if (Input.GetAxis("C1 Hor") < 0 || Input.GetAxis("C1 Vert") > 0)
        {
            if (!cD)
            {
                currentB--;
                if (currentB < 0)
                {
                    currentB = buttons.Count - 1;
                }
                currentButton = buttons[currentB];
                currentButton.Select();
                cD = true;
                StartCoroutine(CD());
            }
        }
        if(Input.GetButtonDown("C1 A"))
        {
            currentButton.onClick.Invoke();
        }
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(0.2f);
        cD = false;
    }

    public void PlayButton()
    {
        UIManager.instance.OnMenuChanged(UIManager.MenuType.carSelect);

    }

    public void Options()
    {
        print("options");
    }

    public void Quit()
    {
        print("Quit");
        Application.Quit();
    }
}
