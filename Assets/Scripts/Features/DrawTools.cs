using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTool : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Coroutine drawing;
    public GameObject Line;

    public SpriteRenderer ToolPlaceHolder;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    void StartLine()
    {
        if (drawing != null)
        {
            StopCoroutine(drawing);
        }
        drawing = StartCoroutine(DrawLine());
    }

    void FinishLine()
    {
            if (drawing != null)
            {
                StopCoroutine(drawing);

            // Calculate the scaling factor
            float scaleX = ToolPlaceHolder.bounds.size.x / transform.GetComponent<SpriteRenderer>().bounds.size.x;
            float scaleY = ToolPlaceHolder.bounds.size.y / transform.GetComponent<SpriteRenderer>().bounds.size.y;
            
            // Use the minimum of scaleX and scaleY to maintain the aspect ratio
            float minScale = Mathf.Min(scaleX, scaleY);

            Vector3[] linePositions = new Vector3[lineObject.GetComponent<LineRenderer>().positionCount];
            lineObject.GetComponent<LineRenderer>().GetPositions(linePositions);

            for (int i = 0; i < linePositions.Length; i++)
            {
                linePositions[i] = new Vector3(linePositions[i].x * scaleX, linePositions[i].y * scaleY, linePositions[i].z);
            }

            lineObject.GetComponent<LineRenderer>().SetPositions(linePositions);
            lineObject.GetComponent<ChangePosition>().SetRelativePositions();

            lineObject.transform.SetParent(ToolPlaceHolder.transform);
                lineObject.transform.localPosition = new Vector3(0, 0, 0);

               // Add a PolygonCollider2D component
               PolygonCollider2D collider = lineObject.AddComponent<PolygonCollider2D>();


                // Get points from the LineRenderer
                Vector2[] points = new Vector2[lineRendererAdded.positionCount];
                for (int i = 0; i < lineRendererAdded.positionCount; i++)
                {
                    points[i] = lineRendererAdded.GetPosition(i);
                }
                // Set the points for the PolygonCollider2D
                collider.points = points;

                Material material = lineRendererAdded.material;

                // Ensure the material is set to a "Sprites/Default" shader
                // This is a common shader used for LineRenderer materials
                material.shader = Shader.Find("Sprites/Default");

                
                StartCoroutine(DisableSelf());

                
        }

    }

    GameObject lineObject;
    LineRenderer lineRendererAdded;
    GameObject toolObject;
    IEnumerator DrawLine()
    {
       // toolObject = new GameObject("ParentObject");

        lineObject = Instantiate(Line as GameObject, new Vector3(0, 0, 0), Quaternion.identity);

       // lineObject.transform.SetParent(toolObject.transform);
        lineRendererAdded = lineObject.GetComponent<LineRenderer>();
        lineRendererAdded.positionCount = 0;

        while (true)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            lineRendererAdded.positionCount++;
            lineRendererAdded.SetPosition(lineRendererAdded.positionCount - 1, position);
            yield return null;
        }
    }

    IEnumerator DisableSelf()
    {
       

        yield return new WaitForSeconds(0.1f);
        gameObject.transform.parent.gameObject.SetActive(false);

       
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        StartLine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FinishLine();
    }
}
