using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTool : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Coroutine drawing;
    public GameObject drawPoint, Player;
    GameObject toolParent;
    public SpriteRenderer ToolPlaceHolder;

    bool canDraw = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    Vector3 mousePosition;
    Coroutine spawnCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (canDraw)
        {
            if (spawnCoroutine == null)
            {
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
        toolParent.AddComponent<DestroySelf>();
        toolParent.transform.position = transform.position;
        canDraw = true;
    }

    void FinishLine()
    {
        canDraw = false;

        // Calculate the scaling factor
        float scaleX = ToolPlaceHolder.bounds.size.x / transform.GetComponent<SpriteRenderer>().bounds.size.x;
        float scaleY = ToolPlaceHolder.bounds.size.y / transform.GetComponent<SpriteRenderer>().bounds.size.y;


        toolParent.transform.localScale = new Vector3(scaleX, scaleY, 0);
        toolParent.transform.parent = ToolPlaceHolder.transform;

        toolParent.transform.localPosition = new Vector3(0, 0, 0);
        toolParent.transform.rotation = Player.transform.rotation;

        StartCoroutine(DisableSelf());




    }

    private IEnumerator SpawnSprites()
    {
        while (canDraw)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z-coordinate to 0 to place the sprite in the 2D plane.


            // Instantiate the sprite at the mouse position within the parent GameObject.
            Instantiate(drawPoint, mousePosition, Quaternion.identity, toolParent.transform);

            yield return null;
        }

        // Reset the coroutine when the mouse button is released.
        spawnCoroutine = null;
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
