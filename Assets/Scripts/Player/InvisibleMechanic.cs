using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvisibleMechanic : MonoBehaviour
{
    public bool isCloaked = false;
    private GhostMovement ghostMovement;
    private float cloakDuration = 5f;

    private Collider2D collider2D;
    private GameObject brush;

    private CircleCollider2D brushCollider;

    private int overlapCount = 0;
    private int requiredOverlaps;

    private GameObject currentLine;
    public GameObject lineprefab;

    private float brushWidth = 0.3f;

    private Toggle InvisibleBrushToggle;


    // Start is called before the first frame update
    void Start()
    {
        GameObject childObject = transform.Find("Body").gameObject;
        if(childObject != null){
            collider2D = childObject.GetComponent<Collider2D>();
        }
        

        ghostMovement = FindObjectOfType<GhostMovement>();
        if (ghostMovement == null)
        {
            Debug.LogError("GhostChase script not found in the scene. Please ensure a ghost with the GhostChase script is present.");
        }

        

        InitializeBrush();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("cursor clicked");
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            brush.transform.position = mousepos;

            Collider2D[] results = new Collider2D[10];
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Player"));
            int count = Physics2D.OverlapCollider(brush.GetComponent<Collider2D>(), filter, results);


            if (count > 0)
            {
                overlapCount++;
                Debug.Log(overlapCount / requiredOverlaps);
                if (overlapCount > requiredOverlaps / 2 && !isCloaked)
                {
                    StartCoroutine(ActivateCloaking());
                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            overlapCount = 0;
        }

        
    }

    private IEnumerator ActivateCloaking()
    {
        isCloaked = true;

        yield return new WaitForSeconds(5);

        isCloaked = false;
        Debug.Log("Cloaking Deactivated");

    }

    public bool IsPlayerCloaked()
    {
        return isCloaked;
    }

    void brushLine()
    {
        currentLine = Instantiate(lineprefab, Vector3.zero, Quaternion.identity);

    }

    void InitializeBrush()
    {
        brush = new GameObject("Brush");
        CircleCollider2D brushCollider = brush.AddComponent<CircleCollider2D>();
        brushCollider.isTrigger = true;
        brushCollider.radius = 0.05f;
        requiredOverlaps = Mathf.CeilToInt(collider2D.bounds.size.x * collider2D.bounds.size.y  / (Mathf.PI * Mathf.Pow(brushCollider.radius,2)));

    }
}
