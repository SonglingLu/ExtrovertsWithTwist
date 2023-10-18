using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject finshScreen;
    public GameObject win;
    private bool isUnlocked = false;

    private string level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().name;
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
            finshScreen.SetActive(true);
            win.SetActive(true);
            GlobalVariables.unlockLevel(level);
        }
    }
}