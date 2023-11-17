using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtonControl : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GlobalVariables.SetPlayerMovable(true);
        GlobalVariables.SetDrawing(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
