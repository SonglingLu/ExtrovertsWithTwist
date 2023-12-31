using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class DrawBarrier : MonoBehaviour
{
    public static DrawBarrier instance;
    private int currentPoint;
    public GameObject lineprefab; // The prefab for the line
    public float blinkDuration = 2f;
    public float blinkInterval = 0.1f;

    private float lineDuration = 5f; // The duration of the line
    private GameObject currentLine; // The currently drawn line object
    private LineRenderer linerenderer; // LineRenderer component of the line object
    private EdgeCollider2D edgeCollider; // EdgeCollider2D component for collision
    private List<Vector2> FingerPositions; // List to store finger positions for the line
    private float lineWidth = 0.4f; // Width of the line

    private Toggle DrawBarrierToggle;
    private bool drawing = false;

    private float inkCoeff = 1f;
    public GameObject InkBar;

    void Start()
    {
        instance = this;
        // Initialize the list to store finger positions
        FingerPositions = new List<Vector2>();

        DrawBarrierToggle = gameObject.GetComponent<Toggle>();
    }

    void Update()
    {
        // check if button is on
        if (DrawBarrierToggle.isOn) {
            // Check for mouse button press to start drawing a line
            if (!MouseOverLayerObject.IsPointerOverUIObject() && InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButtonDown(0)) {
                CreateLine();
                GlobalVariables.SetDrawing(true);
                drawing = true;
            }
            // Check for mouse button hold to continue drawing the line
            if (drawing && InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButton(0)) {
                Vector2 temFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Check if the finger has moved far enough to add a new point to the line
                if (Vector2.Distance(temFingerPos, FingerPositions[FingerPositions.Count - 1]) > 0.1f)
                {
                    float distance = Vector2.Distance(temFingerPos, FingerPositions.Last());
                    InkBar.GetComponent<InkManagement>().UseInk(distance * inkCoeff, 1);
                    UpdateLine(temFingerPos);
                }
            }
            if (drawing && Input.GetMouseButtonUp(0)) {
                edgeCollider.points = new Vector2[]{FingerPositions[0], FingerPositions.Last()};
                edgeCollider.enabled = true;

                linerenderer.positionCount = 2;
                linerenderer.SetPosition(0, new Vector3(FingerPositions[0].x, FingerPositions[0].y, -1));
                linerenderer.SetPosition(1, new Vector3(FingerPositions.Last().x, FingerPositions.Last().y, -1));

                DrawBarrierToggle.isOn = false;
                GlobalVariables.SetDrawing(false);
                GlobalVariables.TriggerFinishDrawing();
                drawing = false;

                // Start a coroutine to destroy the line after a specified duration
                StartCoroutine(DestroyLineDelayed(currentLine, lineDuration, blinkDuration, blinkInterval));            
            }
            
        }
    }

    void CreateLine()
    {
        // Instantiate a new line object using the provided prefab
        currentLine = Instantiate(lineprefab, Vector3.zero, Quaternion.identity);

        // Get LineRenderer and EdgeCollider2D components of the line object
        linerenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        edgeCollider.enabled = false;

        // Set the edgeRadius of the EdgeCollider based on the line width
        edgeCollider.edgeRadius = lineWidth / 2;

        // Clear the list of finger positions and add the initial positions
        FingerPositions.Clear();
        FingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Set the line width and initial positions
        linerenderer.startWidth = lineWidth;
        linerenderer.endWidth = lineWidth;
        linerenderer.positionCount = 2;
        linerenderer.SetPosition(0, new Vector3(FingerPositions[0].x, FingerPositions[0].y, -1));
        linerenderer.SetPosition(1, new Vector3(FingerPositions[1].x, FingerPositions[1].y, -1));

        currentPoint = 2;
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        // Add new finger position to the list and update the LineRenderer
        FingerPositions.Add(newFingerPos);
        linerenderer.positionCount++;
        linerenderer.SetPosition(linerenderer.positionCount - 1, new Vector3(newFingerPos.x, newFingerPos.y, -1));
        currentPoint++;
    }

    // Coroutine to destroy the line object after a specified delay
    // IEnumerator DestroyLineDelayed(GameObject line, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(line);
    // }
    IEnumerator DestroyLineDelayed(GameObject line, float lineDuration, float blinkDuration, float blinkInterval)
{

    // Wait for the initial delay minus the blink duration
    yield return new WaitForSeconds(lineDuration - blinkDuration);
    bool lineVisible = true;
    float blinkTime = 0f;

    while (blinkTime < blinkDuration)
    {
        line.GetComponent<LineRenderer>().enabled = lineVisible;
        lineVisible = !lineVisible;

        yield return new WaitForSeconds(blinkInterval);
        blinkTime += blinkInterval;
    }
    line.GetComponent<LineRenderer>().enabled = true;

    Destroy(line);
}

}