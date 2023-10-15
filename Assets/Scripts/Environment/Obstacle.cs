using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject finshScreen;
    public GameObject win;
    private bool isUnlocked = false;
    SpriteRenderer sprite;

    private void Start()
    {

    }

    // Method to open or close the door
    public void Open()
    {
        Debug.Log("obstacle remove called");
        Destroy(gameObject);
    }

}