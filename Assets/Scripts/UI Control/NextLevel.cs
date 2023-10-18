using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private string nextLevel;
    private bool hasNext;

    // Start is called before the first frame update
    void Start()
    {
        nextLevel = "Level " + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last()) + 1).ToString();
        hasNext = GlobalVariables.existScene(nextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(hasNext && GlobalVariables.getLevelStatus(nextLevel));
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        GlobalVariables.setPlayerMovable(true);
    }
}
