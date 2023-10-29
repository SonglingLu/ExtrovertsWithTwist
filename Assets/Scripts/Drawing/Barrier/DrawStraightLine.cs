using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStraightLine : MonoBehaviour
{
    private LineRenderer linerender;
    private Vector2 mouseposition;
    private Vector2 startposition;
    
    // Start is called before the first frame update
    void Start()
    {
        linerender = GetComponent<LineRenderer>();
        linerender.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            startposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if(Input.GetMouseButton(0)){
            mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            linerender.SetPosition(0,new Vector3(startposition.x,startposition.y,0f));
            linerender.SetPosition(1,new Vector3(mouseposition.x,mouseposition.y,0f));
        }
    }
}
