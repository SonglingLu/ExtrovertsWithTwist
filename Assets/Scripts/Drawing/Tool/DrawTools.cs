using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawTool : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject indicatorDrawPoint, drawPoint, Player;
    GameObject toolParent, indicatorToolParent;
    public SpriteRenderer ToolPlaceHolder;
    public GameObject DrawToolButton;
    private Toggle DrawToolToggle;

    bool canDraw = false;
    bool exitedWhileDrawing = false;

    Vector3 mousePosition,lastPosition;
    Coroutine spawnCoroutine;

    private float inkCoeff = 5f;
    public GameObject InkBar;

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
    // Calculate the scaling factor
    float scaleX;
    float scaleY;
    void StartLine()
    {
        //if (drawing != null)
        //{
        //    StopCoroutine(drawing);
        //}
        //drawing = StartCoroutine(DrawLine());
        scaleX = ToolPlaceHolder.bounds.size.x / transform.GetComponent<SpriteRenderer>().bounds.size.x;
        scaleY = ToolPlaceHolder.bounds.size.y / transform.GetComponent<SpriteRenderer>().bounds.size.y;

        toolParent = new GameObject("DrawnEqipment");
        toolParent.transform.position = transform.position;

        indicatorToolParent = new GameObject("IndicatorEqipment");

        indicatorToolParent.transform.localScale = new Vector3(scaleX, scaleY, 0);
        indicatorToolParent.transform.SetParent( ToolPlaceHolder.transform);
      
        indicatorToolParent.transform.localPosition = Vector3.zero;
        indicatorToolParent.transform.rotation = Player.transform.rotation;

        GlobalVariables.SetDrawing(true);
        canDraw = true;
    }

    void FinishLine()
    {
        GameObject.Destroy(indicatorToolParent);
        GlobalVariables.SetDrawing(false);
        canDraw = false;
        exitedWhileDrawing = false;


        // Calculate the scaling factor
        scaleX = ToolPlaceHolder.bounds.size.x / transform.GetComponent<SpriteRenderer>().bounds.size.x;
        scaleY = ToolPlaceHolder.bounds.size.y / transform.GetComponent<SpriteRenderer>().bounds.size.y;


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
         
        GlobalVariables.TriggerFinishDrawing();
        
        StartCoroutine(DisableSelf());

    }

    GameObject IndicatorPoint1,IndicatorPoint2,DrawPoint1,DrawPoint2;
    private IEnumerator SpawnSprites()
    {
        while (canDraw)
        {

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z-coordinate to 0 to place the sprite in the 2D plane.

            // Debug.Log(Vector3.Distance(mousePosition, lastPosition));
            if (Vector3.Distance(mousePosition,lastPosition) > 0.03)
            {
                // Instantiate the sprite at the mouse position within the parent GameObject.
                DrawPoint1 = Instantiate(drawPoint, mousePosition, Quaternion.identity, toolParent.transform);
                DrawPoint2 = Instantiate(drawPoint, (mousePosition + lastPosition) / 2, Quaternion.identity, toolParent.transform);

                IndicatorPoint1 = Instantiate(indicatorDrawPoint, mousePosition, Quaternion.identity, indicatorToolParent.transform);
                IndicatorPoint2 = Instantiate(indicatorDrawPoint, (mousePosition + lastPosition) / 2, Quaternion.identity, indicatorToolParent.transform);


               // IndicatorPoint1.transform.localScale = IndicatorPoint2.transform.localScale = new Vector3(scaleX, scaleY, 0);

                IndicatorPoint1.transform.localPosition = DrawPoint1.transform.localPosition;
                IndicatorPoint2.transform.localPosition = DrawPoint2.transform.localPosition;

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
        if (InkBar.GetComponent<InkManagement>().GetInk() > 0) {
            GlobalVariables.SetPlayerMovable(false);
            StartLine();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FinishLine();
        GlobalVariables.SetPlayerMovable(true);
        InkBar.GetComponent<InkManagement>().UseInk(inkCoeff, 0);
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
