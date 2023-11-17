using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{    
    public int level;

    void Start() {
        gameObject.GetComponent<Button>().interactable = GlobalVariables.GetHighestReachedLevel() >= level;
    }

    public void SelectLevel() {
        SceneManager.LoadScene("Level " + level.ToString());
        GlobalVariables.SetPlayerMovable(true);
        GlobalVariables.SetDrawing(false);
    }
}
