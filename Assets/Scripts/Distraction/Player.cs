using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
   
   //player's speed
    private float speed = 3.0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //use wasd to control the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        //movement
        transform.Translate(movement * speed * Time.deltaTime);
    }

}
