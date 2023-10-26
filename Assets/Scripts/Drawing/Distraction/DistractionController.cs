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

    private Toggle DrawDistractionToggle;

    [SerializeField] GameObject[] ghosts;

    void Start() {
        DrawDistractionToggle = gameObject.GetComponent<Toggle>();
    }

    void Update() {
        if (!MouseOverUILayerObject.IsPointerOverUIObject() && DrawDistractionToggle.isOn && !distractionExist && Input.GetMouseButtonDown(0)) {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;

            GameObject newDistraction = Instantiate(distractionPrefab, spawnPosition, Quaternion.identity);

            distractionExist = true;
            DrawDistractionToggle.isOn = false;

            for (int i = 0; i < ghosts.Length; i++) {
                ghosts[i].GetComponent<GhostMovement>().setNewDistraction(newDistraction);
                ghosts[i].GetComponent<GhostMovement>().setDistractionExist(distractionExist);
            }
                
            //FindAnyObjectByType<ghost2>().objectToThrow = newDistraction;

            StartCoroutine(DestroySquareAfterTime(newDistraction));
        }
    }

    IEnumerator DestroySquareAfterTime(GameObject newDistraction)
    {
        yield return new WaitForSeconds(distractionLifetime);
        Destroy(newDistraction);
        distractionExist = false;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].GetComponent<GhostMovement>().setDistractionExist(distractionExist);
        }
    }
}