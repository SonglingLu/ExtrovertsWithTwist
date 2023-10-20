using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawTool : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    Coroutine drawing;
    public GameObject drawPoint, Player;
    GameObject toolParent;
    public SpriteRenderer ToolPlaceHolder;
    public GameObject DrawToolButton;
    private Toggle DrawToolToggle;

    bool canDraw = false;
    bool exitedWhileDrawing = false;

    Vector3 mousePosition,lastPosition;
    Coroutine spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        DrawToolToggle = DrawToolButton.GetComponent<Toggle>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (canDraw)
        {
            if (spawnCoroutine == null)
            {

                lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spawnCoroutine = StartCoroutine(SpawnSprites());
            }

        }
    }

    void StartLine()
    {
        //if (drawing != null)
        //{
        //    StopCoroutine(drawing);
        //}
        //drawing = StartCoroutine(DrawLine());

        toolParent = new GameObject();
        
        toolParent.transform.position = transform.position;
        canDraw = true;
    }

    void FinishLine()
    {
        canDraw = false;
        exitedWhileDrawing = false;


        // Calculate the scaling factor
        float scaleX = ToolPlaceHolder.bounds.size.x / transform.GetComponent<SpriteRenderer>().bounds.size.x;
        float scaleY = ToolPlaceHolder.bounds.size.y / transform.GetComponent<SpriteRenderer>().bounds.size.y;


        toolParent.transform.localScale = new Vector3(scaleX, scaleY, 0);
        toolParent.transform.parent = ToolPlaceHolder.transform;

        toolParent.transform.localPosition = new Vector3(0, 0, 0);
        toolParent.transform.rotation = Player.transform.rotation;

        foreach (Transform child in toolParent.transform)
        {
            child.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }

        toolParent.tag = "Player";
        toolParent.AddComponent<DestroySelf>();

        StartCoroutine(DisableSelf());
    }

    private IEnumerator SpawnSprites()
    {
        while (canDraw)
        {

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z-coordinate to 0 to place the sprite in the 2D plane.

            Debug.Log(Vector3.Distance(mousePosition, lastPosition));
            if (Vector3.Distance(mousePosition,lastPosition) > 0.03)
            {
                // Instantiate the sprite at the mouse position within the parent GameObject.
                Instantiate(drawPoint, mousePosition, Quaternion.identity, toolParent.transform);
                Instantiate(drawPoint, (mousePosition + lastPosition) / 2, Quaternion.identity, toolParent.transform);
            }
            

            yield return null;

            lastPosition = mousePosition;
        }

        // Reset the coroutine when the mouse button is released.
        spawnCoroutine = null;
    }


    IEnumerator DisableSelf()
    {

        yield return new WaitForSeconds(0.1f);

        gameObject.transform.parent.gameObject.SetActive(false);
        DrawToolToggle.isOn = false;

    }



    public void OnPointerDown(PointerEventData eventData)
    {
        GlobalVariables.SetPlayerMovable(false);
        StartLine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FinishLine();
        GlobalVariables.SetPlayerMovable(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(canDraw)
        {
            exitedWhileDrawing = true;
            canDraw = false;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(exitedWhileDrawing)
        {
            exitedWhileDrawing = false;
            canDraw = true;
        }
    }
}
