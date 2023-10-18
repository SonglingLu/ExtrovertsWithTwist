using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{    
    public int level;

    public void SelectLevel() {
        SceneManager.LoadScene("Level " + level.ToString());
        GlobalVariables.setPlayerMovable(true);
    }
}
