using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    [SerializeField] private float delayInSeconds = 5f; // The delay before changing gravity to 1
    private Rigidbody2D obstacleRigidbody;
    private bool gravityChanged = false;

    private void Start()
    {
        obstacleRigidbody = GetComponent<Rigidbody2D>();

        // Start a coroutine to change gravity after the specified delay
        StartCoroutine(ChangeGravityAfterDelay());
    }

    private IEnumerator ChangeGravityAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (obstacleRigidbody != null && !gravityChanged)
        {
            // Change the gravity scale to 1
            obstacleRigidbody.gravityScale = 1f;
            gravityChanged = true;
        }
    }
}
