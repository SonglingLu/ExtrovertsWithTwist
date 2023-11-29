using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistractionController : MonoBehaviour
{
    public GameObject distractionPrefab;
    private bool distractionExist = false;
    private float distractionLifetime = 5.0f;
    private float blinkDuration = 1.0f;
    private float blinkInterval = 0.1f;

    private Toggle DrawDistractionToggle;

    [SerializeField] GameObject[] ghosts;

    private float inkCoeff = 15f;
    public GameObject InkBar;

    void Start() {
        DrawDistractionToggle = gameObject.GetComponent<Toggle>();
    }

    void Update() {
        if (
            DrawDistractionToggle.isOn && !distractionExist &&
            !MouseOverLayerObject.IsPointerOverUIObject() && MouseOverLayerObject.IsPointerInRoom() &&
            InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButtonDown(0)
        ) {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;

            GameObject newDistraction = Instantiate(distractionPrefab, spawnPosition, Quaternion.identity);

            InkBar.GetComponent<InkManagement>().UseInk(inkCoeff, 2);

            distractionExist = true;
            DrawDistractionToggle.isOn = false;
            GlobalVariables.TriggerFinishDrawing();
            for (int i = 0; i < ghosts.Length; i++) {
                GhostMovement gm = ghosts[i].GetComponent<GhostMovement>();
                gm.setNewDistraction(newDistraction);
                gm.setDistractionExist(distractionExist);
            }
                
            //FindAnyObjectByType<ghost2>().objectToThrow = newDistraction;

            StartCoroutine(DestroySquareAfterTime(newDistraction,blinkDuration, blinkInterval));
        }
    }

       IEnumerator DestroySquareAfterTime(GameObject newDistraction,float blinkDuration, float blinkInterval)
    {
        yield return new WaitForSeconds(distractionLifetime - blinkDuration);

        float blinkTime = 0f;
        bool squareVisible = true;


            SpriteRenderer distractionRenderer = newDistraction.GetComponent<SpriteRenderer>();
    if (distractionRenderer == null)
    {
        Debug.LogError("SpriteRenderer not found on the distraction square.");
        yield break;
    }

    // Blinking effect
    while (blinkTime < blinkDuration)
    {
        // Toggle visibility
        distractionRenderer.enabled = squareVisible;
        squareVisible = !squareVisible;

        // Wait for the blink interval
        yield return new WaitForSeconds(blinkInterval);
        blinkTime += blinkInterval;
    }

    // Ensure the square is visible before destroying
    distractionRenderer.enabled = true;


        Destroy(newDistraction);
        distractionExist = false;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].GetComponent<GhostMovement>().setDistractionExist(distractionExist);
        }
    }
}