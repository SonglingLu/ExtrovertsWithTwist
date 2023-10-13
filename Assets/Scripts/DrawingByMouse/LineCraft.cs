using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCraft : MonoBehaviour
{
    public GameObject lineprefab; // The prefab for the line
    private float lineDuration = 5f; // The duration of the line
    private GameObject currentLine; // The currently drawn line object
    private LineRenderer linerenderer; // LineRenderer component of the line object
    private EdgeCollider2D edgeCollider; // EdgeCollider2D component for collision
    private List<Vector2> FingerPositions; // List to store finger positions for the line
    private float lineWidth = 0.1f; // Width of the line

    void Start()
    {
        // Initialize the list to store finger positions
        FingerPositions = new List<Vector2>();
    }

    void Update()
    {
        // Check for mouse button press to start drawing a line
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        // Check for mouse button hold to continue drawing the line
        if (Input.GetMouseButton(0))
        {
            Vector2 temFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Check if the finger has moved far enough to add a new point to the line
            if (Vector2.Distance(temFingerPos, FingerPositions[FingerPositions.Count - 1]) > 0.1f)
            {
                UpdateLine(temFingerPos);
            }
        }
    }

    void CreateLine()
    {
        // Instantiate a new line object using the provided prefab
        currentLine = Instantiate(lineprefab, Vector3.zero, Quaternion.identity);

        // Start a coroutine to destroy the line after a specified duration
        StartCoroutine(DestroyLineDelayed(currentLine, lineDuration));

        // Get LineRenderer and EdgeCollider2D components of the line object
        linerenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

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
        linerenderer.SetPosition(0, FingerPositions[0]);
        linerenderer.SetPosition(1, FingerPositions[1]);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        // Add new finger position to the list and update the LineRenderer
        FingerPositions.Add(newFingerPos);
        linerenderer.positionCount++;
        linerenderer.SetPosition(linerenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = FingerPositions.ToArray();
    }

    // Coroutine to destroy the line object after a specified delay
    IEnumerator DestroyLineDelayed(GameObject line, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(line);
    }
}