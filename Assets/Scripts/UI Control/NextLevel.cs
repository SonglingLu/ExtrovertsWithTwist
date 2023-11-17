using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private int level;
    private bool hasNext;

    // Start is called before the first frame update
    void Start()
    {
        level = Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last());
        hasNext = level < GlobalVariables.GetHighestLevel();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(hasNext && GlobalVariables.GetHighestReachedLevel() > level);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (level + 1).ToString());
        GlobalVariables.SetPlayerMovable(true);
        GlobalVariables.SetDrawing(false);
    }
}
