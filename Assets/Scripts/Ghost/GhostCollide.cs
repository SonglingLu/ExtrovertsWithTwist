using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollide : MonoBehaviour
{
    public GameObject finishScreen;
    public GameObject loseText;
    private GhostChase ghostChase;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("you lose");
            finishScreen.SetActive(true);
            loseText.SetActive(true);

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
