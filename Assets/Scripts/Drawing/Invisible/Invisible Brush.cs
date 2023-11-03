using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class InvisibleBrush : MonoBehaviour
{

    public static InvisibleBrush instance;
    private float _lineLength;
    private float _maxLineLength;
    private int currentPoint;
    public GameObject lineprefab;
    private LineRenderer linerenderer;
    private Toggle InvisibleBrushToggle;
    private bool drawing = false;


    private GameObject brush;
    private CircleCollider2D brushCollider;
    private int overlapCount = 0;
    private int requiredOverlaps;

    public bool isCloaked = false;
    private GhostMovement ghostMovement;
    private float cloakDuration = 5f;
    private Collider2D collider2D;

    private List<Vector2> FingerPositions;

    private float lineWidth = 0.3f;

    private GameObject currentLine;

    private Transform bodyTransform;

    private float someSmallValue  =0.005f;

    private float inkCoeff = 0.5f;
    public GameObject InkBar;

    // Start is called before the first frame update
    void Start()
    {

        FingerPositions = new List<Vector2>();

        GameObject playerObject = GameObject.Find("PlayerOne_v3_Prefab");

        Transform bodyTransform = playerObject.transform.Find("Body");
        if (bodyTransform != null) 
        {
            GameObject bodyObject = bodyTransform.gameObject;
            collider2D = bodyObject.GetComponent<Collider2D>();
        }


        ghostMovement = FindObjectOfType<GhostMovement>();

        InvisibleBrushToggle = gameObject.GetComponent<Toggle>();

        _lineLength = 0f;


        GhostMovement[] objectsOfType = FindObjectsOfType<GhostMovement>();

        // Iterate over the found objects.
        foreach (GhostMovement obj in objectsOfType)
        {
            obj.invisibleBrush = this;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (InvisibleBrushToggle.isOn)
        {
            if (!MouseOverLayerObject.IsPointerOverUIObject() && InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButtonDown(0))
            {
                GlobalVariables.SetPlayerMovable(false);

                InitializeBrush();

                // Clear the list of finger positions and add the initial positions
                FingerPositions.Clear();
                FingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                FingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                linerenderer.startWidth = lineWidth;
                linerenderer.endWidth = lineWidth;
                linerenderer.positionCount = 2;
                linerenderer.SetPosition(0, new Vector3(FingerPositions[0].x, FingerPositions[0].y, -2));
                linerenderer.SetPosition(1, new Vector3(FingerPositions[1].x, FingerPositions[1].y, -2));

                currentPoint = 2;

                ////So Far, Creating Line
                drawing = true;
            }

            if (drawing && InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButton(0) )
            {
                Vector2 temFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(temFingerPos, FingerPositions[FingerPositions.Count - 1]) > 0.1f)
                {
                    float distance = Vector2.Distance(temFingerPos, FingerPositions.Last());
                    InkBar.GetComponent<InkManagement>().UseInk(distance * inkCoeff, 5);
                    UpdateBrush(temFingerPos);
                }

                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if(Vector2.Distance(brush.transform.position, mousepos) > someSmallValue)
                {
                    brush.transform.position = mousepos;

                    Collider2D[] results = new Collider2D[10];
                    ContactFilter2D filter = new ContactFilter2D();
                    filter.SetLayerMask(LayerMask.GetMask("Player"));
                    int count = Physics2D.OverlapCollider(brush.GetComponent<Collider2D>(), filter, results);


                    if (count > 0)
                    {
                        overlapCount++;
                    }
                }

            }
            

            if (drawing && Input.GetMouseButtonUp(0))
            {
                if (overlapCount > requiredOverlaps / 2 && !isCloaked) {
                    StartCoroutine(ActivateCloaking());
                }

                GlobalVariables.SetPlayerMovable(true);
                overlapCount = 0;
                InvisibleBrushToggle.isOn = false;
                drawing = false;
                _lineLength = 0f;
                Destroy(currentLine);
            }
        }
    }


    private IEnumerator ActivateCloaking()
    {
        isCloaked = true;
        // Find the player's body object and get its SpriteRenderer component
        GameObject playerObject = GameObject.Find("PlayerOne_v3_Prefab");
        Transform bodyTransform = playerObject.transform.Find("Body");
        if (bodyTransform != null)
        {
            SpriteRenderer bodySpriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
            if (bodySpriteRenderer != null)
            {
                // Save the original color
                Color originalColor = bodySpriteRenderer.color;
                
                // Change the color to indicate cloaking (e.g., make it transparent)
                bodySpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f); // Adjust alpha as needed
            }
        }

        yield return new WaitForSeconds(cloakDuration);

        // Reset the color back to the original
        if (bodyTransform != null)
        {
            SpriteRenderer bodySpriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
            if (bodySpriteRenderer != null)
            {
                bodySpriteRenderer.color = new Color(bodySpriteRenderer.color.r, bodySpriteRenderer.color.g, bodySpriteRenderer.color.b, 1f); // Reset alpha to 1
            }
        }

        isCloaked = false;

    }

    public bool IsPlayerCloaked()
    {
        return isCloaked;
    }

    void InitializeBrush()
    {
        currentLine = Instantiate(lineprefab, Vector3.zero, Quaternion.identity);
        linerenderer = currentLine.GetComponent<LineRenderer>();


        brush = new GameObject("Brush");
        CircleCollider2D brushCollider = brush.AddComponent<CircleCollider2D>();
        brushCollider.isTrigger = true;
        brushCollider.radius = 0.05f;
        requiredOverlaps = Mathf.CeilToInt( collider2D.bounds.size.x * collider2D.bounds.size.y  / (Mathf.PI * Mathf.Pow(brushCollider.radius,2)));
 
    }

    void UpdateBrush(Vector2 newFingerPos)
    {
        FingerPositions.Add(newFingerPos);
        linerenderer.positionCount++;
        linerenderer.SetPosition(linerenderer.positionCount - 1, new Vector3(newFingerPos.x, newFingerPos.y, -2));
        currentPoint++;

        _lineLength += Vector2.Distance(FingerPositions[currentPoint - 1], FingerPositions[currentPoint - 2]);


    }

}
