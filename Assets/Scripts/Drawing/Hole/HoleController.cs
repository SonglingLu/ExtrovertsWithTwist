using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleController : MonoBehaviour
{
    [HideInInspector]
    public Toggle DrawHoleToggle;

    public GameObject holePrefab;
    private bool holeExists = false;
    private float holeLifetime = 5.0f;
    PlayerBasicMovement playerBasics;
    public bool mouseOverHoleable = false;

    private void Start()
    {

        DrawHoleToggle = gameObject.GetComponent<Toggle>();
        playerBasics = FindObjectOfType<PlayerBasicMovement>();
    }



    void Update()
    {
        if (DrawHoleToggle.isOn && !holeExists && Input.GetMouseButtonDown(0) && !MouseOverUILayerObject.IsPointerOverUIObject() && playerBasics.currentFloor != 0 && mouseOverHoleable)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;

            GameObject newHole = Instantiate(holePrefab, spawnPosition, Quaternion.identity);

            holeExists = true;
            DrawHoleToggle.isOn = false;

            StartCoroutine(DestroyHoleAfterTime(newHole));
        }
    }

    IEnumerator DestroyHoleAfterTime(GameObject newHole)
    {
        yield return new WaitForSeconds(holeLifetime);
        Destroy(newHole);
        holeExists = false;

    }
}
