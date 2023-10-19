using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRender : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelection;

    // Start is called before the first frame update
    void Start()
    {
        bool showMainMenu = GlobalVariables.GetShowMainMenu();
        mainMenu.SetActive(showMainMenu);
        levelSelection.SetActive(!showMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
