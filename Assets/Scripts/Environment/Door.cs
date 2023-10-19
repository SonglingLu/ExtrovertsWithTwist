using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject finshScreen;
    public GameObject win;
    private bool isUnlocked = false;

    private int level;

    private void Start()
    {
        level = Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last());
    }

    // Method to open or close the door
    public void Open()
    {
        // Debug.Log("open called");
        GetComponent<SpriteRenderer>().color = Color.green;
        isUnlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player") && isUnlocked)
        {
            // Debug.Log("you win");
            GlobalVariables.SetPlayerMovable(false);
            if (GlobalVariables.GetHighestLevel() > level && GlobalVariables.GetHighestReachedLevel() <= level) {
                GlobalVariables.SetHighestReachedLevel(level + 1);
            }
            StartCoroutine(GameObject.FindAnyObjectByType<FirebaseManager>().postLevelAnalytics(true));

            finshScreen.SetActive(true);
            win.SetActive(true);

        }
    }
}