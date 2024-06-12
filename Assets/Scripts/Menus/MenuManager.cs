using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup[] allMenus = new CanvasGroup[8];

    CanvasGroup currentMenu;

    public void Start()
    {
        currentMenu = allMenus[0];

        for(int i = 0; i < allMenus.Length; i++)
        {
            allMenus[i].alpha = 0;
            allMenus[i].interactable = false;
        }

        currentMenu.alpha = 1;
        currentMenu.interactable = true;
    }

    public void OpenMenu(int index)
    {
        currentMenu.alpha = 0;
        currentMenu.interactable = false;

        currentMenu = allMenus[index];

        currentMenu.alpha = 1;
        currentMenu.interactable = true;
    }
}
