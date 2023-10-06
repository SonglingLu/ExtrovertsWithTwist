using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    LineRenderer lineRenderer;
    bool positionsSet = false;
    Vector3[] relativePositions;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            // Get the parent object's position.
            Vector3 parentPosition = transform.parent.position;

            // Get the number of points in the line renderer object.
            int numPoints = lineRenderer.positionCount;

            // Get the line renderer object's positions.
            Vector3[] positions = new Vector3[numPoints];

            // Update the positions of all of the line renderer object's points.
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {

                positions[i] = parentPosition + relativePositions[i];
            }

            // Set the line renderer object's positions.
            lineRenderer.SetPositions(positions);
        }
    }

    public void SetRelativePositions()
    {
        relativePositions = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {

            relativePositions[i] = lineRenderer.GetPosition(i) - (lineRenderer.bounds.center);
        }
    }
}
