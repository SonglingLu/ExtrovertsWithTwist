using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButtonControl : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelection;

    public void Play() {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
        GlobalVariables.SetShowMainMenu(false);
    }

    public void Back() {
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
        GlobalVariables.SetShowMainMenu(true);
    }
}
