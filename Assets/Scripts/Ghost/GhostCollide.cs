using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollide : MonoBehaviour
{
    public GameObject finishScreen;
    public GameObject lose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player") && !finishScreen.activeSelf)
        {
            finishScreen.SetActive(true);
            lose.SetActive(true);
            GlobalVariables.SetPlayerMovable(false);
        }
    }
}
