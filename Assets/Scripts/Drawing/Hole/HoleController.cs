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

    private float inkCoeff = 15f;
    public GameObject InkBar;

    private void Start()
    {

        DrawHoleToggle = gameObject.GetComponent<Toggle>();
        playerBasics = FindObjectOfType<PlayerBasicMovement>();
    }



    void Update()
    {
        if (
            DrawHoleToggle.isOn && !holeExists && !MouseOverLayerObject.IsPointerOverUIObject() &&
            playerBasics.currentFloor != 0 && mouseOverHoleable &&
            InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButtonDown(0)
        ) {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;

            GameObject newHole = Instantiate(holePrefab, spawnPosition, Quaternion.identity);

            InkBar.GetComponent<InkManagement>().UseInk(inkCoeff, 4);

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
