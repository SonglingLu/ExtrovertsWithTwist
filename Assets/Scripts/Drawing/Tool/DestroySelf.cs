using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    public float blinkDuration = 2f;
    public float blinkInterval = 0.1f;

    GameObject toolparent;

    // Start is called before the first frame update
    void Start()
    {
       
       StartCoroutine(StartDestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSeconds(8f - blinkDuration);

        float endTime = Time.time + blinkDuration;

        while (Time.time < endTime)
        {
            foreach (Transform child in transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    Color color = childRenderer.material.color;
                    color.a = (color.a == 1.0f) ? 0.0f : 1.0f; // Toggle between transparent and opaque
                    childRenderer.material.color = color;
                }
            }

            yield return new WaitForSeconds(blinkInterval);
        }

        // Destroy the gameObject after blinking
        Destroy(gameObject);
    }
   }
