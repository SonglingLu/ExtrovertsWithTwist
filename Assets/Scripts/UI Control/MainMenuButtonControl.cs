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
        GlobalVariables.setShowMainMenu(false);
    }

    public void Back() {
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
        GlobalVariables.setShowMainMenu(true);
    }
}
